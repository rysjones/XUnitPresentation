using DemoConsoleApp.Utilities;

namespace DemoConsoleApp.Helpers
{
    public interface IKeyVaultManager
    {
        string GetConnString();
    }

    public class KeyVaultManagerWrapper : IKeyVaultManager
    {
        private KeyVaultManager _keyVaultManager;

        public KeyVaultManagerWrapper(KeyVaultManager keyVaultManager)
        {
            _keyVaultManager = keyVaultManager;
        }

        public string GetConnString()
        {
            return _keyVaultManager.GetConnString();
        }
    }
}
