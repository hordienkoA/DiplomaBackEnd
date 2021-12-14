using System.Text.Json.Serialization;

namespace EFCoreConfiguration.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Open,
        Closed
    }
}