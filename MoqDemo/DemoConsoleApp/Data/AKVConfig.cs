using System.Runtime.Serialization;

namespace DemoConsoleApp.Data
{
    [Serializable]
    [DataContract]
    public class AKVConfig
    {
        [DataMember]
        public string? TenantId { get; set; }
        [DataMember]
        public string? ClientId { get; set; }
        [DataMember]
        public string? SubjectName { get; set; }
        [DataMember]
        public string? KeyVaultName { get; set; }
        [DataMember]
        public string? KeyVaultSecretName { get; set; }
    }
}
