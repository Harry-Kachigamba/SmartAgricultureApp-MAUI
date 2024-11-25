using SmartAgricultureApp_MAUI.Models;
using Supabase.Postgrest.Models;

namespace SmartAgricultureApp_MAUI.Services;

public class HumidityService : BaseModel
{
    private readonly SupabaseServiceClass _humidityService;

    public HumidityService()
    {
        _humidityService = new SupabaseServiceClass();
    }

    public async Task<double?> GetRecentHumidity()
    {
        var client = _humidityService?._supabaseClient;

        var result = await client
            .From<HumidityLog>()
            .Order("timestamp", Supabase.Postgrest.Constants.Ordering.Descending)
            .Limit(1)
            .Get();
        
        var recentRecord = result.Models.FirstOrDefault();
        return recentRecord?.Humidity;
    }
}