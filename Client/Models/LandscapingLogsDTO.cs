using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class LandscapingLogsDTO
    {
        public int id { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string? contractorsName { get; set; }
        public string? subContractorsName { get; set; }
        public DateTime workDate { get; set; } = DateTime.Now;
        public string? propertyAddress { get; set; }
        public string? locationCoordinates { get; set; }
        public TimeSpan workStartTime { get; set; }
        public string? weatherCondition { get; set; }
        public TimeSpan workCompletionTime { get; set; }
        public string? descriptionOfWorkPerformed { get; set; }
        public string? difficultiesOrObstaclesEncountered { get; set; }
        public string? suppliesUsed { get; set; }
        public string? generalCommentsOrObservations { get; set; }
        public string? area1 { get; set; }
        public string? area2 { get; set; }
        public string? area3 { get; set; }
        public string? area4 { get; set; }
        public string? area5 { get; set; }
        public List<IFormFile>? preWorkPhotos { get; set; } = new List<IFormFile>();
        public List<IFormFile>? postWorkPhotos { get; set; } = new List<IFormFile>();
        public string? comments { get; set; }
        public string? preWorkPhotosPath { get; set; }
        public string? postWorkPhotosPath { get; set; }

        // Constructor to set default values
        public LandscapingLogsDTO()
        {
            workStartTime = DateTime.Now.TimeOfDay;
            workCompletionTime = DateTime.Now.TimeOfDay;
        }
    }


    public class SnowLogsDTO
    {
        public int id { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string? contractorsName { get; set; }
        public string? subContractorsName { get; set; }
        public DateTime workDate { get; set; } = DateTime.Now;
        public string? propertyAddress { get; set; }
        public string? locationCoordinates { get; set; }
        public TimeSpan workStartTime { get; set; }
        public string? weatherCondition { get; set; }
        public TimeSpan workCompletionTime { get; set; }
        public string? descriptionOfWorkPerformed { get; set; }
        public string? difficultiesOrObstaclesEncountered { get; set; }
        public string? suppliesUsed { get; set; }
        public string? generalCommentsOrObservations { get; set; }
        public string? parkingArea { get; set; }
        public string? parkingRamp { get; set; }
        public string? sidewalk { get; set; }
        public string? stairwaysSteps { get; set; }
        public string? driveways { get; set; }
        public List<IFormFile>? preWorkPhotos { get; set; } = new List<IFormFile>();
        public List<IFormFile>? postWorkPhotos { get; set; } = new List<IFormFile>();
        public string? comments { get; set; }
        public string? preWorkPhotosPath { get; set; }
        public string? postWorkPhotosPath { get; set; }

        // Constructor to set default values
        public SnowLogsDTO()
        {
            workStartTime = DateTime.Now.TimeOfDay;
            workCompletionTime = DateTime.Now.TimeOfDay;
        }
    }
}
