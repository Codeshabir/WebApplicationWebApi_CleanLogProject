using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Client.Models
{
    public class CleaningLogsDTO
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string? contractorsName { get; set; }
        public string? employeeName { get; set; }
        public DateTime workDate { get; set; }
        public string? propertyAddress { get; set; }
        public string? locationCoordinates { get; set; }
        public TimeSpan workStartTime { get; set; }
        public string? weatherCondition { get; set; }
        public TimeSpan workCompletionTime { get; set; }
        public string? descriptionOfWorkPerformed { get; set; }
        public string? difficultiesOrObstaclesEncountered { get; set; }
        public string? generalCommentsOrObservations { get; set; }
        public string? photos { get; set; } // String to store comma-separated filenames
        public string? comments { get; set; }
        public List<IFormFile> photoFiles { get; set; } = new List<IFormFile>();
    }
}
