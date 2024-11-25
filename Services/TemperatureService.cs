using SmartAgricultureApp_MAUI.Models;

namespace SmartAgricultureApp_MAUI.Services;

public class TemperatureService
{
    private readonly SupabaseServiceClass _temperatureService;

    public TemperatureService()
    {
        _temperatureService = new SupabaseServiceClass();
    }

    public async Task<double?> GetRecentTemperature()
    {
        var client = _temperatureService?._supabaseClient;

        var result = await client
            .From<TemperatureLog>()
            .Order("timestamp", Supabase.Postgrest.Constants.Ordering.Descending)
            .Limit(1)
            .Get();

        var recentRecord = result.Models.FirstOrDefault();
        return recentRecord?.Temperature;
    }
}