using SmartAgricultureApp_MAUI.Models;
using Supabase.Postgrest.Models;

namespace SmartAgricultureApp_MAUI.Services;

public class HistoryPageService : BaseModel
{
    private readonly SupabaseServiceClass _historyTemperatureService, _historyHumidityService, _historyMoistureService;

    public HistoryPageService()
    {
        _historyTemperatureService = new SupabaseServiceClass();
        _historyHumidityService = new SupabaseServiceClass();
        _historyMoistureService = new SupabaseServiceClass();
    }
    
    public async Task<List<double?>> GetRecentTemperature(int limit = 5)
    {
        var client = _historyTemperatureService?._supabaseClient;

        var result = await client
            .From<TemperatureLog>()
            .Order("timestamp", Supabase.Postgrest.Constants.Ordering.Descending)
            .Limit(limit)
            .Get();

        return result.Models.Select(r => (double?)r.Temperature).ToList();
    }

    public async Task<List<double?>> GetRecentHumidity(int limit = 5)
    {
        var client = _historyHumidityService?._supabaseClient;

        var result = await client
            .From<HumidityLog>()
            .Order("timestamp", Supabase.Postgrest.Constants.Ordering.Descending)
            .Limit(limit)
            .Get();

        return result.Models.Select(r => (double?)r.Humidity).ToList();
    }

    public async Task<List<double?>> GetRcentMoisture(int limit = 5)
    {
        var client = _historyMoistureService?._supabaseClient;

        var result = await client
            .From<MoistureLog>()
            .Order("timestamp", Supabase.Postgrest.Constants.Ordering.Descending)
            .Limit(limit)
            .Get();

        return result.Models.Select(r => (double?)r.Moisture).ToList();
    }

}