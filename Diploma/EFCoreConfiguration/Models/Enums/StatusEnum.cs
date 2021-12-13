using System.Text.Json.Serialization;

namespace EFCoreConfiguration.Models.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Open,
        Closed
    }
}