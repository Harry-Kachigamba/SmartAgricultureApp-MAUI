using Newtonsoft.Json;
using Supabase.Postgrest.Models;

namespace SmartAgricultureApp_MAUI.Models
{
    public class TemperatureLog : BaseModel
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
            
    }
}
