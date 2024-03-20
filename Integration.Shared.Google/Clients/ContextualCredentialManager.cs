namespace Integration.Shared.Google.Clients;

public class ContextualCredentialManager : IContextualCredentialManager
{
    private string _loginCustomerId;
    private string _orgRefreshToken;

    public (string LoginCustomerId, string OrgRefreshToken) GetCredentials()
    {
        if (string.IsNullOrEmpty(_loginCustomerId) || string.IsNullOrEmpty(_orgRefreshToken))
            throw new Exception("Customer contextual credentials are not properly set.");

        return (_loginCustomerId, _orgRefreshToken);
    }

    public void SetCredentials(string loginCustomerId, string orgRefreshToken)
    {
        _loginCustomerId = loginCustomerId;
        _orgRefreshToken = orgRefreshToken;
    }
}
