using Newtonsoft.Json;
using Supabase.Postgrest.Models;

namespace SmartAgricultureApp_MAUI.Models
{
    public class HumidityLog : BaseModel
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        
        [JsonProperty("humidity")]
        public double Humidity { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
