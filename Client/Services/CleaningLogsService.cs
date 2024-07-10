using Client.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
namespace Client.Services
{
    public class CleaningLogsService
    {
        private readonly HttpClient _httpClient;

        public CleaningLogsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:7258/"); // Replace with your Web API URL
        }

        public async Task<CleaningLogsDTO> GetCleaningLogsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/CleaningLogsAPI/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CleaningLogsDTO>(content);
        }
        public async Task<IEnumerable<CleaningLogsDTO>> GetCleaningLogsAsync()
        {
            var response = await _httpClient.GetAsync("/api/CleaningLogsAPI");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var CleaningLogss = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CleaningLogsDTO>>(content);
            return CleaningLogss ?? Enumerable.Empty<CleaningLogsDTO>();
        }
        public async Task<CleaningLogsDTO> CreateCleaningLogsAsync(CleaningLogsDTO cleaningLogs)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(cleaningLogs.date.ToString("o")), "date");
                content.Add(new StringContent(cleaningLogs.contractorsName), "contractorsName");
                content.Add(new StringContent(cleaningLogs.employeeName), "employeeName");
                content.Add(new StringContent(cleaningLogs.workDate.ToString("o")), "workDate");
                content.Add(new StringContent(cleaningLogs.propertyAddress), "propertyAddress");
                content.Add(new StringContent(cleaningLogs.locationCoordinates), "locationCoordinates");
                content.Add(new StringContent(cleaningLogs.workStartTime.ToString()), "workStartTime");
                content.Add(new StringContent(cleaningLogs.weatherCondition), "weatherCondition");
                content.Add(new StringContent(cleaningLogs.workCompletionTime.ToString()), "workCompletionTime");
                content.Add(new StringContent(cleaningLogs.descriptionOfWorkPerformed), "descriptionOfWorkPerformed");
                content.Add(new StringContent(cleaningLogs.difficultiesOrObstaclesEncountered), "difficultiesOrObstaclesEncountered");
                content.Add(new StringContent(cleaningLogs.generalCommentsOrObservations), "generalCommentsOrObservations");
                content.Add(new StringContent(cleaningLogs.comments), "comments");

                foreach (var file in cleaningLogs.photoFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "photoFiles", file.FileName);
                    }
                }

                var response = await _httpClient.PostAsync("/api/CleaningLogsAPI", content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CleaningLogsDTO>(responseContent);
            }
        }




        // UpdateCleaningLogsAsync method in CleaningLogsService.cs
        public async Task UpdateCleaningLogsAsync(CleaningLogsDTO cleaningLogs)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(cleaningLogs.date.ToString("o")), "date");
                content.Add(new StringContent(cleaningLogs.contractorsName), "contractorsName");
                content.Add(new StringContent(cleaningLogs.employeeName), "employeeName");
                content.Add(new StringContent(cleaningLogs.workDate.ToString("o")), "workDate");
                content.Add(new StringContent(cleaningLogs.propertyAddress), "propertyAddress");
                content.Add(new StringContent(cleaningLogs.locationCoordinates), "locationCoordinates");
                content.Add(new StringContent(cleaningLogs.workStartTime.ToString()), "workStartTime");
                content.Add(new StringContent(cleaningLogs.weatherCondition), "weatherCondition");
                content.Add(new StringContent(cleaningLogs.workCompletionTime.ToString()), "workCompletionTime");
                content.Add(new StringContent(cleaningLogs.descriptionOfWorkPerformed), "descriptionOfWorkPerformed");
                content.Add(new StringContent(cleaningLogs.difficultiesOrObstaclesEncountered), "difficultiesOrObstaclesEncountered");
                content.Add(new StringContent(cleaningLogs.generalCommentsOrObservations), "generalCommentsOrObservations");
                content.Add(new StringContent(cleaningLogs.comments), "comments");
                foreach (var file in cleaningLogs.photoFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "photoFiles", file.FileName);
                    }
                }

                var apiUrl = $"/api/CleaningLogsAPI/{cleaningLogs.id}"; // Adjust endpoint if necessary
                var response = await _httpClient.PutAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/CleaningLogsAPI/{id}");
            response.EnsureSuccessStatusCode();
        }

    }



}
