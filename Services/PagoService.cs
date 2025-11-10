using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StreamHub.Models;
using StreamHub.Dtos;

namespace StreamHub.Services
{
    public class PagoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://api.paylink.com"; // Base URL de la pasarela

        public PagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Registro del negocio
        public async Task<Negocio> RegistrarNegocioAsync(NegocioDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/business", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al registrar el negocio en PayLink");

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Negocio>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        // Crear un pago (POST /api/payments)
        public async Task<Pago> CrearPagoAsync(PagoDto dto, string apiKey)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/pagos", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al crear el pago en la pasarela");

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Pago>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        // Consultar pago por ID
        public async Task<Pago> ObtenerPagoPorIdAsync(string pagoId, string apiKey)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            var response = await _httpClient.GetAsync($"{_baseUrl}/api/pagos/{pagoId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo obtener el pago");

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Pago>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }
    }
}