using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
        public class LandscapingLogs
        {
            public int Id { get; set; }

            [Display(Name = "Date")]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Now;

            [Display(Name = "Contractor's Name")]
            public string ContractorsName { get; set; }

            [Display(Name = "Sub-Contractor's Name")]
            public string SubContractorsName { get; set; }

            [Display(Name = "Work Date")]
            [DataType(DataType.Date)]
            public DateTime WorkDate { get; set; } = DateTime.Now;

            [Display(Name = "Property Address")]
            public string PropertyAddress { get; set; }

            [Display(Name = "Location Coordinates")]
            public string LocationCoordinates { get; set; }

            [Display(Name = "Work Start Time")]
            [DataType(DataType.Time)]
            public TimeSpan WorkStartTime { get; set; }

            [Display(Name = "Weather Condition")]
            public string WeatherCondition { get; set; }

            [Display(Name = "Work Completion Time")]
            [DataType(DataType.Time)]
            public TimeSpan WorkCompletionTime { get; set; }

            [Display(Name = "Description of work performed")]
            public string DescriptionOfWorkPerformed { get; set; }

            [Display(Name = "Difficulties or obstacles encountered")]
            public string DifficultiesOrObstaclesEncountered { get; set; }

            [Display(Name = "Supplies Used")]
            public string SuppliesUsed { get; set; }

            [Display(Name = "General comments or observations (if any)")]
            public string GeneralCommentsOrObservations { get; set; }

            [Display(Name = "Area1")]
            public string Area1 { get; set; }

            [Display(Name = "Area2")]
            public string Area2 { get; set; }

            [Display(Name = "Area3")]
            public string Area3 { get; set; }

            [Display(Name = "Area4")]
            public string Area4 { get; set; }

            [Display(Name = "Area5")]
            public string Area5 { get; set; }

            [NotMapped]    
            [Display(Name = "Pre-Work Photos")]
            public IFormFile PreWorkPhotos { get; set; }
        
            [NotMapped]
            [Display(Name = "Post-Work Photos")]
            public IFormFile PostWorkPhotos { get; set; }

            [Display(Name = "Comments")]
            public string Comments { get; set; }

            public string PreWorkPhotosPath { get; set; }
            public string PostWorkPhotosPath { get; set; }

            // Constructor to set default values
            public LandscapingLogs()
            {
                WorkStartTime = DateTime.Now.TimeOfDay;
                WorkCompletionTime = DateTime.Now.TimeOfDay;
            }
        }

    public class SnowLogs
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Contractor's Name")]
        public string ContractorsName { get; set; }

        [Display(Name = "Sub-Contractor's Name")]
        public string SubContractorsName { get; set; }

        [Display(Name = "Work Date")]
        [DataType(DataType.Date)]
        public DateTime WorkDate { get; set; } = DateTime.Now;

        [Display(Name = "Property Address")]
        public string PropertyAddress { get; set; }

        [Display(Name = "Location Coordinates")]
        public string LocationCoordinates { get; set; }

        [Display(Name = "Work Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan WorkStartTime { get; set; }

        [Display(Name = "Weather Condition")]
        public string WeatherCondition { get; set; }

        [Display(Name = "Work Completion Time")]
        [DataType(DataType.Time)]
        public TimeSpan WorkCompletionTime { get; set; }

        [Display(Name = "Description of work performed")]
        public string DescriptionOfWorkPerformed { get; set; }

        [Display(Name = "Difficulties or obstacles encountered")]
        public string DifficultiesOrObstaclesEncountered { get; set; }

        [Display(Name = "Supplies Used")]
        public string SuppliesUsed { get; set; }

        [Display(Name = "General comments or observations (if any)")]
        public string GeneralCommentsOrObservations { get; set; }

        [Display(Name = "Parking Areas")]
        public string ParkingArea { get; set; }

        [Display(Name = "Parking Ramp")]
        public string ParkingRamp { get; set; }

        [Display(Name = "Sidewalk")]
        public string Sidewalk { get; set; }

        [Display(Name = "Stairways Steps")]
        public string StairwaysSteps { get; set; }

        [Display(Name = "Driveways")]
        public string Driveways { get; set; }

        [NotMapped]
        [Display(Name = "Pre-Work Photos")]
        public IFormFile PreWorkPhotos { get; set; }

        [NotMapped]
        [Display(Name = "Post-Work Photos")]
        public IFormFile PostWorkPhotos { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        public string PreWorkPhotosPath { get; set; }
        public string PostWorkPhotosPath { get; set; }

        // Constructor to set default values
        public SnowLogs()
        {
            WorkStartTime = DateTime.Now.TimeOfDay;
            WorkCompletionTime = DateTime.Now.TimeOfDay;
        }
    }
}

