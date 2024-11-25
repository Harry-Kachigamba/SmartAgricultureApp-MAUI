using Newtonsoft.Json;
using Supabase.Postgrest.Models;

namespace SmartAgricultureApp_MAUI.Models
{
    public class MoistureLog : BaseModel
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        
        [JsonProperty("moisture")]
        public double Moisture { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}

