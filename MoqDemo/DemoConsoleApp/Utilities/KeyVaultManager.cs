using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DemoConsoleApp.Data;
using System.Security.Cryptography.X509Certificates;

namespace DemoConsoleApp.Utilities
{
    public class KeyVaultManager: IKeyVaultManager
    {
        private readonly AKVConfig _config;

        public KeyVaultManager(IAKVConfiguration config)
        {
            _config =  config.GetConfig();
        }       
        
        public string GetConnStringFromAKV()
        {
            string errorMsg;
            if (!ValidateConfigValues(out errorMsg)) return "";

            try
            {
                // Get the certifcate to use to encrypt the key.
                X509Certificate2 cert = GetCertificateFromStore(_config.SubjectName);
                if (cert == null)
                {
                    Console.WriteLine("Certificate '{0}' not found.", _config.SubjectName);
                }

                var credential = new ClientCertificateCredential(
                    _config.TenantId,
                    _config.ClientId,
                    cert,
                    new ClientCertificateCredentialOptions
                    {
                        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                        SendCertificateChain = true
                    });

                var secretClient = new SecretClient(new Uri($"https://{_config.KeyVaultName}.vault.azure.net"), credential);
                var _secret = secretClient.GetSecret(_config.KeyVaultSecretName);
                return _secret.Value.Value;
            }
            catch
            {
                return "";
            }
        }

        private X509Certificate2 GetCertificateFromStore(string certName)
        {
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectName, certName, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
            }
            catch {
                throw;
            }
            finally
            {
                store.Close();
            }
        }

        public bool ValidateConfigValues(out string errorMessage)
        {
            errorMessage = "";

            // Validate TenantId, ClientId, and SubjectName
            if (string.IsNullOrEmpty(_config.TenantId))
            {
                errorMessage = "TenantId is missing.";
                return false;
            }

            if (string.IsNullOrEmpty(_config.ClientId))
            {
                errorMessage = "ClientId is missing.";
                return false;
            }

            if (string.IsNullOrEmpty(_config.SubjectName))
            {
                errorMessage = "SubjectName is missing.";
                return false;
            }

            try
            {
                // Get the certificate
                X509Certificate2 cert = GetCertificateFromStore(_config.SubjectName);
                if (cert == null)
                {
                    errorMessage = $"Certificate '{_config.SubjectName}' not found.";
                    return false;
                }

                // Validate KeyVaultName and KeyVaultSecretName
                if (string.IsNullOrEmpty(_config.KeyVaultName))
                {
                    errorMessage = "KeyVaultName is missing.";
                    return false;
                }

                if (string.IsNullOrEmpty(_config.KeyVaultSecretName))
                {
                    errorMessage = "KeyVaultSecretName is missing.";
                    return false;
                }

                // All components are valid
                return true;
            }
            catch (Exception)
            {
                errorMessage = "An error occurred while validating components.";
                return false;
            }
        }

        public bool SomeLogic(string connString)
        {
            if (string.IsNullOrEmpty(connString)) return false;
            return true;
        }
    }

    public interface IKeyVaultManager {
        string GetConnStringFromAKV();
        bool ValidateConfigValues(out string errorMessage);
        bool SomeLogic(string connString);
    }
}
