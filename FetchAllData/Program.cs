using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string initialUrl = "https://prices.azure.com:443/api/retail/prices?$skip=0";
        List<JObject> allData = await FetchAllData(initialUrl);

        // Combine all JSON objects into a single JSON array
        JArray combinedData = new JArray(allData);

        // Write combined data to a file
        string filePath = @"C:\Users\lenovo\Desktop\Hack\allData.json";
        await File.WriteAllTextAsync(filePath, combinedData.ToString());

        Console.WriteLine($"Total pages fetched: {allData.Count}");
        Console.WriteLine($"Data saved to {filePath}");
        Console.ReadLine();
    }

    static async Task<List<JObject>> FetchAllData(string url)
    {
        List<JObject> allData = new List<JObject>();
        string nextPageUrl = url;

        while (!string.IsNullOrEmpty(nextPageUrl))
        {
            var response = await client.GetStringAsync(nextPageUrl);
            var jsonData = JObject.Parse(response);
            allData.Add(jsonData);
            Console.WriteLine(jsonData);
            nextPageUrl = jsonData["NextPageLink"]?.ToString();
        }

        return allData;
    }
}
