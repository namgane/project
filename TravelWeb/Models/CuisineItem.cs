namespace TravelWeb.Models
{
    public class CuisineItem
    {
        public int Id { get; set; }
        public string Province { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal AveragePrice { get; set; }
        public int Popularity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public double RatingAvg { get; set; }
        public int RatingCount { get; set; }

        // Thu?c tính tính toán Region (dùng cho View)
        public string Region => GetRegionByProvince(Province);

        // Logic xác ğ?nh vùng mi?n (DUY NH?T 1 HÀM)
        private static string GetRegionByProvince(string province)
        {
            return province switch
            {
                // Mi?n B?c
                "Hà N?i" or "H?i Ph?ng" or "Nam Ğ?nh" or "Phú Th?"
                    => "Mi?n B?c",

                // Mi?n Trung
                "Hu?" or "Ğà N?ng" or "Ninh Thu?n" or "Th?a Thiên Hu?"
                    => "Mi?n Trung",

                // Tây Nguyên
                "Ğ?k L?k" or "Lâm Ğ?ng"
                    => "Tây Nguyên",

                // Mi?n Nam
                "TP.HCM" or "C?n Gi?"
                    => "Mi?n Nam",

                // Mi?n Tây Nam B?
                "Sóc Trãng" or "Trà Vinh"
                    => "Mi?n Tây Nam B?",

                _ => "Khác"
            };
        }
    }
}
