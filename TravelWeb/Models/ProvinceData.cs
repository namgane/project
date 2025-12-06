namespace TravelWeb.Models
{
    public class ProvinceInfo
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public (double lat, double lng) Coord { get; set; }
    }

    public static class ProvinceData
    {
        public static readonly List<ProvinceInfo> List = new List<ProvinceInfo>
        {
            new ProvinceInfo { Name = "Hà Nội", Region = "Miền Bắc", Coord = (21.0278, 105.8342) },
            new ProvinceInfo { Name = "Hải Phòng", Region = "Miền Bắc", Coord = (20.8449, 106.6881) },
            new ProvinceInfo { Name = "Quảng Ninh", Region = "Miền Bắc", Coord = (21.0064, 107.2925) },

            new ProvinceInfo { Name = "Đà Nẵng", Region = "Miền Trung", Coord = (16.0544, 108.2022) },
            new ProvinceInfo { Name = "Huế", Region = "Miền Trung", Coord = (16.4637, 107.5909) },
            new ProvinceInfo { Name = "Quảng Nam", Region = "Miền Trung", Coord = (15.5394, 108.0191) },

            new ProvinceInfo { Name = "Hồ Chí Minh", Region = "Miền Nam", Coord = (10.8231, 106.6297) },
            new ProvinceInfo { Name = "Cần Thơ", Region = "Miền Nam", Coord = (10.0452, 105.7469) },
            new ProvinceInfo { Name = "Vũng Tàu", Region = "Miền Nam", Coord = (10.4114, 107.1362) }
        };
    }
}
