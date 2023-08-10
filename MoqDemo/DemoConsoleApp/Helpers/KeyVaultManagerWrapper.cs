using DemoConsoleApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Helpers
{
    public interface IKeyVaultManagerWrapper
    {
        string GetConnString();
    }

    public class KeyVaultManagerWrapper : IKeyVaultManagerWrapper
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
