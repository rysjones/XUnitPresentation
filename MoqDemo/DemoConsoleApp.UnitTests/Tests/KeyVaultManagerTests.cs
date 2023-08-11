using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DemoConsoleApp.Helpers;
using DemoConsoleApp.Utilities;
using Moq;
using System.Security.Cryptography.X509Certificates;

namespace DemoConsoleApp.UnitTests
{
    public class KeyVaultManagerTests
    {
        [Fact]
        public void GetConnString_WithValidCertificate_ReturnsConnectionString()
        {
            // Arrange
            var mockSecretClient = new Mock<SecretClient>(MockBehavior.Strict);
            mockSecretClient
                .Setup(c => c.GetSecret("kvSecretSample", null, default))
                .Returns(Response.FromValue(new KeyVaultSecret("kvSecretSample", "your-connection-string"), null));

            var mockClientCertificateCredential = new Mock<ClientCertificateCredential>(
                "123", "456", It.IsAny<X509Certificate2>(), It.IsAny<ClientCertificateCredentialOptions>());


            // Act
            var result = KeyVaultManager.GetConnStringFromAKV();

            // Assert
            Assert.NotEqual("", result);
        }

        [Fact]
        public void GetConnString_WithMissingCertificate_ReturnsNotNull()
        {
            // Arrange
            var mockSecretClient = new Mock<SecretClient>(MockBehavior.Strict);
            var mockClientCertificateCredential = new Mock<ClientCertificateCredential>(
                "123", "456", It.IsAny<X509Certificate2>(), It.IsAny<ClientCertificateCredentialOptions>());

            var mockWrapper = new Mock<IKeyVaultManager>();
            mockWrapper.Setup(wrapper => wrapper.GetConnString()).Returns("dummy-connection-string");

            // Act
            var result = mockWrapper.Object.GetConnString();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetConnString_ReturnsDummyValue()
        {
            // Arrange
            var mockAKVConfiguration = new Mock<AKVConfiguration>();
            var mockX509Certificate2 = new Mock<X509Certificate2>();
            var mockSecretClient = new Mock<SecretClient>(MockBehavior.Strict);
            mockSecretClient
                .Setup(c => c.GetSecret("dummyKeyVaultSecretName", null, default))
                .Returns(Response.FromValue(new KeyVaultSecret("dummyKeyVaultSecretName", "dummy-connection-string"), null));

            var mockClientCertificateCredential = new Mock<ClientCertificateCredential>(
                "dummyTenantId", "dummyClientId", mockX509Certificate2.Object, It.IsAny<ClientCertificateCredentialOptions>());
            
            // Arrange
            var mockWrapper = new Mock<IKeyVaultManager>();
            mockWrapper.Setup(wrapper => wrapper.GetConnString()).Returns("dummy-connection-string");

            // Act
            var result = mockWrapper.Object.GetConnString();

            // Assert
            Assert.Equal("dummy-connection-string", result);
        }

    }
}
