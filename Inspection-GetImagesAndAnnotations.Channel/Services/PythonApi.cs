using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using InspectionGetImagesAndAnnotations.Messages.Dtos;

namespace InspectionGetImagesAndAnnotations.Channel
{
    public class PythonAPI
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string Url;
        private readonly string _username;
        private readonly string _password;

        public PythonAPI(string url, string username, string password)
        {
            Url = url;
            _username = username;
            _password = password;
        }

        public async Task<string?> SendToImageTrainingAPI(string url, InspectionRequest request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Add Basic Authentication header
                var byteArray = Encoding.ASCII.GetBytes($"{_username}:{_password}");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                HttpResponseMessage response = await _client.PostAsync(Url + url, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
