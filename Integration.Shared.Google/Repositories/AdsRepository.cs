using Google.Ads.GoogleAds.V14.Services;
using Integration.Shared.Domain.Models;

namespace Integration.Shared.Google.Repositories;

public class AdsRepository : IAdsRepository
{
    private readonly ClientManager _clientManager;

    public AdsRepository(ClientManager clientManager)
    {
        _clientManager = clientManager;
    }

    public async Task<string> GetCustomerManagerIdAsync(string sessionCustomerId, string searchCustomerId, string orgRefreshToken)
    {
        var resource = string.Empty;
        var googleClient = _clientManager.Create(orgRefreshToken);

        string query = $@"SELECT
                                    customer_client.client_customer,
                                    customer_client.level,
                                    customer_client.manager,
                                    customer_client.descriptive_name,
                                    customer_client.currency_code,
                                    customer_client.time_zone,
                                    customer_client.id,
                                    customer_client.resource_name
                                    FROM customer_client
                                    WHERE customer_client.resource_name = 'customers/{searchCustomerId}/customerClients/{sessionCustomerId}'";

        using (var adsService = googleClient.GetService(GoogleServices.GoogleAdsService))
        {
            var response = adsService.SearchAsync(searchCustomerId, query, pageSize: 1);
            await foreach (var row in response)
                return row.CustomerClient.ResourceName.Split('/')[1];
        }
        return string.Empty;
    }

    public async Task<long> GetConversionActionIdAsync(string customerId, string customerManagerId, string orgRefreshToken, string conversionActionName)
    {
        var googleClient = _clientManager.Create(customerManagerId, orgRefreshToken);

        string query = $"SELECT conversion_action.id FROM conversion_action WHERE conversion_action.name = '{conversionActionName}'";
        using (var adsService = googleClient.GetService(GoogleServices.GoogleAdsService))
        {
            var response = adsService.SearchAsync(customerId, query, pageSize: 1);
            await foreach (var row in response)
                return row.ConversionAction.Id;
        }
        return 0;
    }

    public async Task<List<AdWordsClickPerformanceReportModel>> GetClickPerformanceReportAsync(string customerId, string customerManagerId,
                                                                                               string refreshToken, DateTime reportDate)
    {
        var googleClient = _clientManager.Create(customerManagerId, refreshToken);

        var result = new List<AdWordsClickPerformanceReportModel>();
        var adWordStartDateRange = reportDate.ToString("yyyy-MM-dd");
        var adWordEndDateRange = reportDate.ToString("yyyy-MM-dd");

        var query = string.Format(@"
                            SELECT ad_group.id,
                            ad_group.name,
                            customer.id,
                            campaign.id,
                            campaign.name,
                            click_view.gclid,
                            click_view.keyword,
                            click_view.keyword_info.text,
                            click_view.keyword_info.match_type,
                            metrics.clicks,
                            segments.date,
                            segments.device,
                            segments.click_type
                            FROM click_view
                            WHERE segments.date BETWEEN '{0}' AND '{1}' AND click_view.keyword_info.text IS NOT NULL", adWordStartDateRange, adWordEndDateRange);

        using (var adsService = googleClient.GetService(GoogleServices.GoogleAdsService))
        {
            var response = adsService.SearchAsync(customerId, query);
            await foreach (GoogleAdsRow row in response)
            {
                var item = new AdWordsClickPerformanceReportModel();
                item.AdGroupId = row?.AdGroup?.Id.ToString();
                item.AdGroupName = row?.AdGroup?.Name;
                item.ExternalCustomerId = row?.Customer?.Id.ToString();
                item.CreativeId = null; //deprecated
                item.CriteriaId = null; //deprecated
                item.CriteriaParameters = row?.ClickView?.KeywordInfo?.Text;
                item.GclId = row?.ClickView?.Gclid;
                item.Date = row?.Segments?.Date;
                item.Device = row?.Segments?.Device.ToString();
                item.CampaignId = row?.Campaign?.Id.ToString();
                item.CampaignName = row?.Campaign?.Name;
                item.Clicks = row?.Metrics?.Clicks.ToString();
                item.ClickType = row?.Segments?.ClickType.ToString();
                item.KeywordMatchType = row?.ClickView?.KeywordInfo?.MatchType.ToString();
                result.Add(item);
            }
        }

        return result;
    }

    public async Task<List<AdWordsCallMetricsReportModel>> GetCallDetailReportAsync(string customerId, string customerManagerId,
                                                                                    string refreshToken, DateTime reportDate)
    {
        var googleClient = _clientManager.Create(customerManagerId, refreshToken);

        var result = new List<AdWordsCallMetricsReportModel>();
        var adWordStartDateRange = reportDate.ToString("yyyy-MM-dd 00:00:00");
        var adWordEndDateRange = reportDate.ToString("yyyy-MM-dd 23:59:59");

        var query = String.Format(@"
                            SELECT call_view.call_duration_seconds,
                            call_view.call_status,
                            call_view.call_tracking_display_location,
                            call_view.caller_area_code,
                            call_view.caller_country_code,
                            call_view.end_call_date_time,
                            call_view.resource_name,
                            call_view.start_call_date_time,
                            call_view.caller_area_code,
                            call_view.call_duration_seconds,
                            call_view.type,
                            ad_group.id,
                            ad_group.name,
                            ad_group.campaign,
                            ad_group.status,
                            campaign.id,
                            campaign.name,
                            campaign.status,
                            customer.id,
                            customer.descriptive_name,
                            customer.time_zone
                            FROM call_view
                            WHERE call_view.end_call_date_time >= '{0}' AND call_view.end_call_date_time <= '{1}'", adWordStartDateRange, adWordEndDateRange);

        using (var adsService = googleClient.GetService(GoogleServices.GoogleAdsService))
        {
            var response = adsService.SearchAsync(customerId, query);
            
                await foreach (GoogleAdsRow row in response)
                {
                    var item = new AdWordsCallMetricsReportModel();
                    item.AccountTimeZone = row?.Customer?.TimeZone;
                    item.AdGroupId = row?.AdGroup?.Id.ToString();
                    item.AdGroupName = row?.AdGroup?.Name;
                    item.AdGroupStatus = row?.AdGroup?.Status.ToString(); ;
                    item.CallDuration = row?.CallView?.CallDurationSeconds.ToString();
                    item.CallEndTime = row?.CallView?.EndCallDateTime;
                    item.CallerCountryCallingCode = row?.CallView?.CallerCountryCode;
                    item.CallerNationalDesignatedCode = row?.CallView?.CallerAreaCode;
                    item.CallStartTime = row?.CallView?.StartCallDateTime;
                    item.CallStatus = row?.CallView?.CallStatus.ToString();
                    item.CallType = row?.CallView?.Type.ToString();
                    item.CampaignId = row?.Campaign?.Id.ToString();
                    item.CampaignName = row?.Campaign?.Name;
                    item.CampaignStatus = row?.Campaign?.Status.ToString();
                    item.CustomerDescriptiveName = row?.Customer?.DescriptiveName;

                    item.StartTimeUtc = ConvertAdWordsDateToUTC(item.CallStartTime, item.AccountTimeZone);
                    item.EndTimeUtc = ConvertAdWordsDateToUTC(item.CallEndTime, item.AccountTimeZone);

                    result.Add(item);
                }
        };
        return result;
    }

    private System.DateTime ConvertAdWordsDateToUTC(string dateString, string gmt)
    {
        var code = "";
        var index = gmt.IndexOf("GMT");
        if (index != -1)
        {
            code = gmt.Substring(index + 3, 6);
        }
        var date = System.DateTime.SpecifyKind(System.DateTime.Parse(dateString + $" {code}"), DateTimeKind.Unspecified);
        var utcTime = date.ToUniversalTime();

        return utcTime;
    }
}
