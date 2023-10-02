internal class TemperatureClient
{
    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri(GetBaseUrl()),
    };

    internal static async Task DoGetAndPrintAsync()
    {        
        while (true)
        {
            using HttpResponseMessage response = await sharedClient.GetAsync("currenttemperature");
        
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            
            Console.WriteLine($"{jsonResponse}\n");

            await Task.Delay(60_000);
        }


    }

    private static string GetBaseUrl()
    {
        // ToDo: if debug
        return "https://currenttemperatureapi.azurewebsites.net";
    }
}