namespace Integration.Shared.Google.Clients;

public interface IContextualCredentialManager
{
    (string LoginCustomerId, string OrgRefreshToken) GetCredentials();
    void SetCredentials(string loginCustomerId, string refreshToken);
}