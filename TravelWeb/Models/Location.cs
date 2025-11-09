namespace TravelWeb.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string MoTa { get; set; } = string.Empty;
        public int Type { get; set; }
    }
}

