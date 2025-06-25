using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace Smart_Warehouse.Services.PLCServices
{
    public class PLCAPIService
    {
        private readonly HttpClient _httpClient;
        public PLCAPIService()
        {
            _httpClient = new()
            {
                BaseAddress = new Uri("http://192.168.1.13:7119/") // Replace with your PLC API base URL
            };
        }

        public async Task<string> ReadPLCDataAsync(string address)
        {
            try
            {
                //return await _httpClient.GetStringAsync($"api/MXPLC/read/{address}");
                return await _httpClient.GetStringAsync($"api/MXPLC/");
            }
            catch (HttpRequestException e)
            {
                // Handle error
                throw new Exception("Error fetching data from PLC API", e);
            }
        }

        public async Task WritePLCDataAsync(string address, string value)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/MXPLC/write/{address}", value);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                // Handle error
                throw new Exception("Error sending data to PLC API", e);
            }
        }
    }
}
