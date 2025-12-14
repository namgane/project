namespace TravelWeb.Models
{
    public static class TripExtensions
    {
        public static string GenerateBookingLink(this Trip trip)
        {
            return $"https://www.google.com/search?q=đặt+vé+{trip.TransportType}+{trip.FromCity}+đi+{trip.ToProvince}";
        }
    }
}
