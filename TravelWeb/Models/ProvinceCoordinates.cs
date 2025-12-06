namespace TravelWeb.Models
{
    public static class ProvinceCoordinates
    {
        public static Dictionary<string, (double lat, double lng)> Data =
            new Dictionary<string, (double lat, double lng)>
            {
                ["Hà Nội"] = (21.0285, 105.8542),
                ["Hồ Chí Minh"] = (10.8231, 106.6297),
                ["Đà Nẵng"] = (16.0471, 108.2068),
                // add your others...
            };
    }
}
