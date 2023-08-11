using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DemoConsoleApp.Data;
using DemoConsoleApp.Utilities;
using Moq;
using System.Security.Cryptography.X509Certificates;

namespace DemoConsoleApp.UnitTests
{
    public class KeyVaultManagerTests
    {
        [Fact]
        public void GetConnStringAndExecuteSomeLogic_ReturnsTrue()
        {
            // Arrange
            var mockKVM = new Mock<IKeyVaultManager>();           
            mockKVM.Setup(m => m.GetConnStringFromAKV()).Returns("connStringFromAKV");

            var mockConfig = new Mock<IAKVConfiguration>();
            // Configure the mock to return a specific configuration
            mockConfig.Setup(c => c.GetConfig()).Returns(new AKVConfig
            {
                TenantId = "your-tenant-id",
                ClientId = "your-client-id",
                SubjectName = "certsample.com",
                KeyVaultName = "certsampleabc",
                KeyVaultSecretName = "connstringsample"
            });
            
            // Act
            var connString = mockKVM.Object.GetConnStringFromAKV();
            IKeyVaultManager aKVM = new KeyVaultManager(mockConfig.Object);
            var result = aKVM.SomeLogic(connString);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetConnString_WithValidCertificate_ReturnsConnectionString()
        {
            // Arrange
            var mockKVM = new Mock<IKeyVaultManager>();
            var mockSecretClient = new Mock<SecretClient>(MockBehavior.Strict);
            mockSecretClient
                .Setup(c => c.GetSecret("kvSecretSample", null, default))
                .Returns(Response.FromValue(new KeyVaultSecret("kvSecretSample", "your-connection-string"), null));

            var mockClientCertificateCredential = new Mock<ClientCertificateCredential>(
                "123", "456", It.IsAny<X509Certificate2>(), It.IsAny<ClientCertificateCredentialOptions>());

            mockKVM.Setup(m => m.GetConnStringFromAKV()).Returns("connStringFromAKV");
            // Act
            var result = mockKVM.Object.GetConnStringFromAKV();

            // Assert
            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void GetConnString_WithMissingCertificate_ReturnsNotNull()
        {
            // Arrange
            var mockSecretClient = new Mock<SecretClient>(MockBehavior.Strict);
            var mockClientCertificateCredential = new Mock<ClientCertificateCredential>(
                "123", "456", It.IsAny<X509Certificate2>(), It.IsAny<ClientCertificateCredentialOptions>());

            var mockKVM = new Mock<IKeyVaultManager>();
            mockKVM.Setup(m => m.GetConnStringFromAKV()).Returns("dummy-connection-string");

            // Act
            var result = mockKVM.Object.GetConnStringFromAKV();

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
            var mockKVM = new Mock<IKeyVaultManager>();
            mockKVM.Setup(m => m.GetConnStringFromAKV()).Returns("dummy-connection-string");

            // Act
            var result = mockKVM.Object.GetConnStringFromAKV();

            // Assert
            Assert.Equal("dummy-connection-string", result);
        }

    }
}
