using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DemoConsoleApp.Utilities;
using Moq;
using System.Security.Cryptography.X509Certificates;

namespace DemoConsoleApp.UnitTests
{
    public class KeyVaultManagerTests
    {
        [Fact]
        public void GetConnStringFromAKV_WithValidCertificate_ReturnsConnectionString()
        {
            // Arrange
            var mockSecretClient = new Mock<SecretClient>(MockBehavior.Strict);
            mockSecretClient
                .Setup(c => c.GetSecret("kvSecretSample","", default))
                .Returns(Response.FromValue(new KeyVaultSecret("kvSecretSample", "your-connection-string"), null)); 

            var mockClientCertificateCredential = new Mock<ClientCertificateCredential>(
                "123", "456", It.IsAny<X509Certificate2>(), It.IsAny<ClientCertificateCredentialOptions>());

            var keyVaultManager = new KeyVaultManager();

            // Act
            var result = keyVaultManager.GetConnStringFromAKV();

            // Assert
            Assert.Equal("your-connection-string", result);
        }

        [Fact]
        public void GetConnStringFromAKV2_WithMissingCertificate_ReturnsNull()
        {
            // Arrange
            var mockSecretClient = new Mock<SecretClient>(MockBehavior.Strict);
            var mockClientCertificateCredential = new Mock<ClientCertificateCredential>(
                "123", "456", It.IsAny<X509Certificate2>(), It.IsAny<ClientCertificateCredentialOptions>());

            var keyVaultManager = new KeyVaultManager();

            // Act
            var result = keyVaultManager.GetConnStringFromAKV();

            // Assert
            Assert.Null(result);
        }
    }
}
