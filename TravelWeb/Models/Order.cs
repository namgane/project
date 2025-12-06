namespace TravelWeb.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public String Location { get; set; }
        public DateTime Time { get; set; } //giờ khách muốn đặt địa điểm 
    }
}
