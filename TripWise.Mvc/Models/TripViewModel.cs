using System.ComponentModel.DataAnnotations;

namespace TripWise.Mvc.Models
{
    public class TripViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ActivityViewModel> Activities { get; set; } = new();
    }

    public class CreateTripViewModel
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Destination { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class EditTripViewModel
    {
        public string Id { get; set; } = null!;
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Destination { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class ActivityViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = null!;
    }
}
