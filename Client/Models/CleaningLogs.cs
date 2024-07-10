using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Model
{
    public class CleaningLog
    {
        public int? Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Contractor's Name")]
        public string? ContractorsName { get; set; }

        [Display(Name = "Employee Name")]
        public string? EmployeeName { get; set; }

        [Display(Name = "Work Date")]
        [DataType(DataType.Date)]
        public DateTime WorkDate { get; set; } = DateTime.Now;

        [Display(Name = "Property Address")]
        public string? PropertyAddress { get; set; }

        [Display(Name = "Location Coordinates")]
        public string? LocationCoordinates { get; set; }

        [Display(Name = "Work Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan WorkStartTime { get; set; }

        [Display(Name = "Weather Condition")]
        public string? WeatherCondition { get; set; }

        [Display(Name = "Work Completion Time")]
        [DataType(DataType.Time)]
        public TimeSpan WorkCompletionTime { get; set; }

        [Display(Name = "Description of work performed")]
        public string? DescriptionOfWorkPerformed { get; set; }

        [Display(Name = "Difficulties or obstacles encountered")]
        public string? DifficultiesOrObstaclesEncountered { get; set; }

        [Display(Name = "General comments or observations")]
        public string? GeneralCommentsOrObservations { get; set; }

        // Change the type to List<string> to store file names
        public string? Photos { get; set; } 

        public string? Comments { get; set; }

        // NotMapped attribute prevents mapping to database
        [NotMapped]
        public List<IFormFile>? PhotoFiles { get; set; } = new List<IFormFile>();

        public CleaningLog()
        {
            WorkStartTime = DateTime.Now.TimeOfDay;
            WorkCompletionTime = DateTime.Now.TimeOfDay;
        }
    }
}
