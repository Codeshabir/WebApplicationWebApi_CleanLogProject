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
using Client.Models;
namespace Client.Services
{
    public class LandscapingLogsService
    {
        private readonly HttpClient _httpClient;

        public LandscapingLogsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:7258/"); // Replace with your Web API URL
        }

        //public async Task<IEnumerable<LandscapingLogDTO>> GetCleaningLogssAsync()
        //{
        //    var response = await _httpClient.GetAsync("/api/CleaningLogss");

        //    response.EnsureSuccessStatusCode();

        //    var content = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<IEnumerable<LandscapingLogDTO>>(content);
        //}

        public async Task<LandscapingLogsDTO> GetLandscapingLogAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/LandscapingLogsAPI/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LandscapingLogsDTO>(content);
        }

        public async Task<IEnumerable<LandscapingLogsDTO>> GetLandscapingLogsAsync()
        {
            var response = await _httpClient.GetAsync("/api/LandscapingLogsAPI");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var landscapingLogs = JsonSerializer.Deserialize<IEnumerable<LandscapingLogsDTO>>(content);
            return landscapingLogs ?? Enumerable.Empty<LandscapingLogsDTO>();
        }


        public async Task<LandscapingLogsDTO> CreateLandscapingLogAsync(LandscapingLogsDTO landscapingLog)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(landscapingLog.date.ToString("o")), "date");
                content.Add(new StringContent(landscapingLog.contractorsName), "contractorsName");
                content.Add(new StringContent(landscapingLog.subContractorsName), "subContractorsName");
                content.Add(new StringContent(landscapingLog.workDate.ToString("o")), "workDate");
                content.Add(new StringContent(landscapingLog.propertyAddress), "propertyAddress");
                content.Add(new StringContent(landscapingLog.locationCoordinates), "locationCoordinates");
                content.Add(new StringContent(landscapingLog.workStartTime.ToString()), "workStartTime");
                content.Add(new StringContent(landscapingLog.weatherCondition), "weatherCondition");
                content.Add(new StringContent(landscapingLog.workCompletionTime.ToString()), "workCompletionTime");
                content.Add(new StringContent(landscapingLog.descriptionOfWorkPerformed), "descriptionOfWorkPerformed");
                content.Add(new StringContent(landscapingLog.difficultiesOrObstaclesEncountered), "difficultiesOrObstaclesEncountered");
                content.Add(new StringContent(landscapingLog.suppliesUsed), "suppliesUsed");
                content.Add(new StringContent(landscapingLog.generalCommentsOrObservations), "generalCommentsOrObservations");
                content.Add(new StringContent(landscapingLog.area1), "area1");
                content.Add(new StringContent(landscapingLog.area2), "area2");
                content.Add(new StringContent(landscapingLog.area3), "area3");
                content.Add(new StringContent(landscapingLog.area4), "area4");
                content.Add(new StringContent(landscapingLog.area5), "area5");
                content.Add(new StringContent(landscapingLog.comments), "comments");

                // Handle pre-work photos
                if (landscapingLog.preWorkPhotos != null && landscapingLog.preWorkPhotos.Any())
                {
                    foreach (var file in landscapingLog.preWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "preWorkPhotos", file.FileName);
                    }
                }

                // Handle post-work photos
                if (landscapingLog.postWorkPhotos != null && landscapingLog.postWorkPhotos.Any())
                {
                    foreach (var file in landscapingLog.postWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "postWorkPhotos", file.FileName);
                    }
                }

                var response = await _httpClient.PostAsync("/api/LandscapingLogsAPI", content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LandscapingLogsDTO>(responseContent);
            }
        }

        public async Task UpdateLandscapingLogAsync(LandscapingLogsDTO landscapingLog)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(landscapingLog.date.ToString("o")), "date");
                content.Add(new StringContent(landscapingLog.contractorsName), "contractorsName");
                content.Add(new StringContent(landscapingLog.subContractorsName), "subContractorsName");
                content.Add(new StringContent(landscapingLog.workDate.ToString("o")), "workDate");
                content.Add(new StringContent(landscapingLog.propertyAddress), "propertyAddress");
                content.Add(new StringContent(landscapingLog.locationCoordinates), "locationCoordinates");
                content.Add(new StringContent(landscapingLog.workStartTime.ToString()), "workStartTime");
                content.Add(new StringContent(landscapingLog.weatherCondition), "weatherCondition");
                content.Add(new StringContent(landscapingLog.workCompletionTime.ToString()), "workCompletionTime");
                content.Add(new StringContent(landscapingLog.descriptionOfWorkPerformed), "descriptionOfWorkPerformed");
                content.Add(new StringContent(landscapingLog.difficultiesOrObstaclesEncountered), "difficultiesOrObstaclesEncountered");
                content.Add(new StringContent(landscapingLog.suppliesUsed), "suppliesUsed");
                content.Add(new StringContent(landscapingLog.generalCommentsOrObservations), "generalCommentsOrObservations");
                content.Add(new StringContent(landscapingLog.area1), "area1");
                content.Add(new StringContent(landscapingLog.area2), "area2");
                content.Add(new StringContent(landscapingLog.area3), "area3");
                content.Add(new StringContent(landscapingLog.area4), "area4");
                content.Add(new StringContent(landscapingLog.area5), "area5");
                content.Add(new StringContent(landscapingLog.comments), "comments");

                if (landscapingLog.preWorkPhotos != null && landscapingLog.preWorkPhotos.Any())
                {
                    foreach (var file in landscapingLog.preWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "preWorkPhotos", file.FileName);
                    }
                }

                if (landscapingLog.postWorkPhotos != null && landscapingLog.postWorkPhotos.Any())
                {
                    foreach (var file in landscapingLog.postWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "postWorkPhotos", file.FileName);
                    }
                }

                var apiUrl = $"/api/LandscapingLogsAPI/{landscapingLog.id}";
                var response = await _httpClient.PutAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/LandscapingLogsAPI/{id}");
            response.EnsureSuccessStatusCode();
        }

    }



}
