using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using EMP.Common.Dto.Auth_Dto;
using EMP.Common.Dto.APP_Dto;
using EMP.Http.Interfaces;


namespace EMP.Http.Services
{
    public class ManagerAPIService : IManagerAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl = string.Empty;
     
        public ManagerAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ExternalAPIClient");
            _remoteServiceBaseUrl = $"{_httpClient.BaseAddress}";
        }
       
        public async Task<(JObject?, JObject?)> GetMultipleData(StatisticRequestFilter filter, RangeDateReq range)
        {        
            _httpClient.DefaultRequestHeaders.Add("AccessToken", filter.Token);

            string today = JsonConvert.SerializeObject(filter.CurrentDayFilter);
            var today_content = new StringContent(today, Encoding.UTF8, "application/json");

            string range_date = JsonConvert.SerializeObject(range);
            var range_content = new StringContent(range_date, Encoding.UTF8, "application/json");

            string todayClosedUrl = ExternalAPI.BillAPI.GetClosedBills(_remoteServiceBaseUrl);
            string rangeClosedUrl = ExternalAPI.BillAPI.UploadsCloseBills(_remoteServiceBaseUrl);

            // Створюємо задачі (Tasks) для кожного запиту
            Task<HttpResponseMessage> taskToday = _httpClient.PostAsync(todayClosedUrl, today_content);
            Task<HttpResponseMessage> taskRange = _httpClient.PostAsync(rangeClosedUrl, range_content);

            // Очікуємо завершення обох задач одночасної=
            HttpResponseMessage responseToday = await taskToday;
            HttpResponseMessage responseRange = await taskRange;

            // Перевіряемо успішність відповідей
            if (responseToday.IsSuccessStatusCode && responseRange.IsSuccessStatusCode)
            {
                string contentToday = await responseToday.Content.ReadAsStringAsync();
                string contentRange = await responseRange.Content.ReadAsStringAsync();

                JObject? todayObj = JsonConvert.DeserializeObject<JObject>(contentToday);
                JObject? rangeObj = JsonConvert.DeserializeObject<JObject>(contentRange);

                // Повертаємо дані з обох запитів
                return new(todayObj, rangeObj);
            }
      
            return new();
        }

        public async Task<JObject> GetCurrentDayClosedBills_Async(CurrentDayClosedBillsDto dto, string token)
        {         
            _httpClient.DefaultRequestHeaders.Add("AccessToken", token);
            string uri = ExternalAPI.BillAPI.GetClosedBills(_remoteServiceBaseUrl);

            string json = JsonConvert.SerializeObject(dto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            JObject? obj = JsonConvert.DeserializeObject<JObject>(responseBody);

            return obj;
        
        }

        public async Task<JObject> GetRangeDayClosedBills_Async(RangeDateReq range, string token)
        {        
            _httpClient.DefaultRequestHeaders.Add("AccessToken", token);

            string uri = ExternalAPI.BillAPI.UploadsCloseBills(_remoteServiceBaseUrl);

            string json = JsonConvert.SerializeObject(range);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            JObject? obj = JsonConvert.DeserializeObject<JObject>(responseBody);

            return obj;        
        }

        public async Task<string?> AuthenticateAsync(UserCardCodeDTO cardCodeDTO)
        {
            string uri = ExternalAPI.Authenticate.GetAuthenticate(_remoteServiceBaseUrl);
            string json = JsonConvert.SerializeObject(cardCodeDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            JObject? obj = JsonConvert.DeserializeObject<JObject>(responseBody);

            string? externalToken = obj?["Token"]?.ToString();

            return externalToken;
        }

        public async Task<JObject?> GetExternalUsers_Async(string? token)
        {
            _httpClient.DefaultRequestHeaders.Add("AccessToken", token);
            string uri = ExternalAPI.UserAPI.GetAllWaiters(_remoteServiceBaseUrl);
            var content = new StringContent(new JObject().ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            JObject? obj = JsonConvert.DeserializeObject<JObject>(responseBody);

           return obj;
        }
    }
}

