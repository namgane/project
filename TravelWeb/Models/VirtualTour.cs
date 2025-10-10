using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWeb.Models
{
    public class VirtualTour
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên địa điểm")]
        public string LocationName { get; set; }

        [Required]
        [Display(Name = "Link Google Street View")]
        public string StreetViewLink { get; set; }

        // Khóa ngoại đến Tour
        [ForeignKey("Tour")]
        public int TourId { get; set; }

        public Tour? Tour { get; set; }
    }
}
