﻿using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Security.Cryptography.X509Certificates;

namespace DemoConsoleApp.Utilities
{
    public class KeyVaultManager
    {
        public string GetConnStringFromAKV()
        {
            var tenantId = "123";
            var clientId = "456";
            var subjectName = "";
            var KeyVaultName = "";
            var KeyVaultSecretName = "";

            // Get the certifcate to use to encrypt the key.
            X509Certificate2 cert = GetCertificateFromStore(subjectName);
            if (cert == null)
            {
                Console.WriteLine("Certificate '{0}' not found.", subjectName);
            }

            var credential = new ClientCertificateCredential(
                tenantId,
                clientId,
                cert,
                new ClientCertificateCredentialOptions
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                    SendCertificateChain = true
                });

            var secretClient = new SecretClient(new Uri($"https://{KeyVaultName}.vault.azure.net"), credential);
            var _secret = secretClient.GetSecret(KeyVaultSecretName);
            return _secret.Value.Value;
        }

        private static X509Certificate2 GetCertificateFromStore(string certName)
        {
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }
        }
    }
}
