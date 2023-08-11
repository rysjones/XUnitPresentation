using DemoConsoleApp.Data;
using DemoConsoleApp.Utilities;
using Moq;

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
            var connString = mockKVM.Object.GetConnStringFromAKV();
            IKeyVaultManager aKVM = new KeyVaultManager(mockConfig.Object);

            // Act
            var result = aKVM.SomeLogic(connString);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyMethodCallOnGetConnStringFromAKV()
        {
            // Arrange
            var mockConfig = new Mock<IAKVConfiguration>();
            mockConfig.Setup(config => config.GetConfig())
                      .Returns(new AKVConfig
                      {
                          TenantId = "123",
                          ClientId = "456",
                          SubjectName = "certsample.com",
                          KeyVaultName = "certsampleabc",
                          KeyVaultSecretName = "connstringsample"
                      }); 

            var keyVaultManager = new KeyVaultManager(mockConfig.Object);

            // Act
            var result = keyVaultManager.GetConnStringFromAKV();

            // Assert
            Assert.NotNull(result);

            // Verify that the GetConfig method was called on the mockConfig
            mockConfig.Verify(config => config.GetConfig(), Times.Once);
        }

        [Fact]
        public void GetConnString_ReturnsDummyValue()
        {            
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
