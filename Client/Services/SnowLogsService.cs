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
    public class SnowLogsService
    {
        private readonly HttpClient _httpClient;

        public SnowLogsService(HttpClient httpClient)
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

        public async Task<SnowLogsDTO> GetLandscapingLogAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/SnowLogsAPI/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SnowLogsDTO>(content);
        }

        public async Task<IEnumerable<SnowLogsDTO>> GetLandscapingLogsAsync()
        {
            var response = await _httpClient.GetAsync("/api/SnowLogsAPI");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var landscapingLogs = JsonSerializer.Deserialize<IEnumerable<SnowLogsDTO>>(content);
            return landscapingLogs ?? Enumerable.Empty<SnowLogsDTO>();
        }

        public async Task UpdateLandscapingLogAsync(SnowLogsDTO snowLog)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(snowLog.date.ToString("o")), "date");
                content.Add(new StringContent(snowLog.contractorsName), "contractorsName");
                content.Add(new StringContent(snowLog.subContractorsName), "subContractorsName");
                content.Add(new StringContent(snowLog.workDate.ToString("o")), "workDate");
                content.Add(new StringContent(snowLog.propertyAddress), "propertyAddress");
                content.Add(new StringContent(snowLog.locationCoordinates), "locationCoordinates");
                content.Add(new StringContent(snowLog.workStartTime.ToString()), "workStartTime");
                content.Add(new StringContent(snowLog.weatherCondition), "weatherCondition");
                content.Add(new StringContent(snowLog.workCompletionTime.ToString()), "workCompletionTime");
                content.Add(new StringContent(snowLog.descriptionOfWorkPerformed), "descriptionOfWorkPerformed");
                content.Add(new StringContent(snowLog.difficultiesOrObstaclesEncountered), "difficultiesOrObstaclesEncountered");
                content.Add(new StringContent(snowLog.suppliesUsed), "suppliesUsed");
                content.Add(new StringContent(snowLog.generalCommentsOrObservations), "generalCommentsOrObservations");
                content.Add(new StringContent(snowLog.parkingArea), "parkingArea");
                content.Add(new StringContent(snowLog.parkingRamp), "parkingRamp");
                content.Add(new StringContent(snowLog.sidewalk), "sidewalk");
                content.Add(new StringContent(snowLog.stairwaysSteps), "stairwaysSteps");
                content.Add(new StringContent(snowLog.driveways), "driveways");
                content.Add(new StringContent(snowLog.comments), "comments");

                if (snowLog.preWorkPhotos != null && snowLog.preWorkPhotos.Any())
                {
                    foreach (var file in snowLog.preWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "preWorkPhotos", file.FileName);
                    }
                }

                if (snowLog.postWorkPhotos != null && snowLog.postWorkPhotos.Any())
                {
                    foreach (var file in snowLog.postWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "postWorkPhotos", file.FileName);
                    }
                }

                var apiUrl = $"/api/SnowLogsAPI/{snowLog.id}";
                var response = await _httpClient.PutAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<SnowLogsDTO> CreateLandscapingLogAsync(SnowLogsDTO snowLog)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(snowLog.date.ToString("o")), "date");
                content.Add(new StringContent(snowLog.contractorsName), "contractorsName");
                content.Add(new StringContent(snowLog.subContractorsName), "subContractorsName");
                content.Add(new StringContent(snowLog.workDate.ToString("o")), "workDate");
                content.Add(new StringContent(snowLog.propertyAddress), "propertyAddress");
                content.Add(new StringContent(snowLog.locationCoordinates), "locationCoordinates");
                content.Add(new StringContent(snowLog.workStartTime.ToString()), "workStartTime");
                content.Add(new StringContent(snowLog.weatherCondition), "weatherCondition");
                content.Add(new StringContent(snowLog.workCompletionTime.ToString()), "workCompletionTime");
                content.Add(new StringContent(snowLog.descriptionOfWorkPerformed), "descriptionOfWorkPerformed");
                content.Add(new StringContent(snowLog.difficultiesOrObstaclesEncountered), "difficultiesOrObstaclesEncountered");
                content.Add(new StringContent(snowLog.suppliesUsed), "suppliesUsed");
                content.Add(new StringContent(snowLog.generalCommentsOrObservations), "generalCommentsOrObservations");
                content.Add(new StringContent(snowLog.parkingArea), "parkingArea");
                content.Add(new StringContent(snowLog.parkingRamp), "parkingRamp");
                content.Add(new StringContent(snowLog.sidewalk), "sidewalk");
                content.Add(new StringContent(snowLog.stairwaysSteps), "stairwaysSteps");
                content.Add(new StringContent(snowLog.driveways), "driveways");
                content.Add(new StringContent(snowLog.comments), "comments");


                if (snowLog.preWorkPhotos != null && snowLog.preWorkPhotos.Any())
                {
                    foreach (var file in snowLog.preWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "preWorkPhotos", file.FileName);
                    }
                }

                if (snowLog.postWorkPhotos != null && snowLog.postWorkPhotos.Any())
                {
                    foreach (var file in snowLog.postWorkPhotos)
                    {
                        var fileStreamContent = new StreamContent(file.OpenReadStream());
                        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(fileStreamContent, "postWorkPhotos", file.FileName);
                    }
                }

                var response = await _httpClient.PostAsync("/api/SnowLogsAPI", content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SnowLogsDTO>(responseContent);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/SnowLogsAPI/{id}");
            response.EnsureSuccessStatusCode();
        }

    }



}
