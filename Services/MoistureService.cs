using SmartAgricultureApp_MAUI.Models;

namespace SmartAgricultureApp_MAUI.Services;

public class MoistureService
{
    private readonly SupabaseServiceClass _moistureService;

    public MoistureService()
    {
        _moistureService = new SupabaseServiceClass();
    }

    public async Task<double?> GetRecentMoisture()
    {
        var client = _moistureService?._supabaseClient;

        var result = await client
            .From<MoistureLog>()
            .Order("timestamp", Supabase.Postgrest.Constants.Ordering.Descending)
            .Limit(1)
            .Get();

        var recentRecord = result.Models.FirstOrDefault();
        return recentRecord?.Moisture;
    }
}