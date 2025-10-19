namespace TravelWeb.Models
{
    public class TravelQuiz
    {
        // --- 1. Địa lý - khí hậu ---
        public string? Region { get; set; }              // Miền Bắc / Trung / Nam
        public bool? IsMountainArea { get; set; }        // Có thích vùng núi không?
        public bool? IsCoastalArea { get; set; }         // Có thích vùng biển không?
        public bool? IsPlainArea { get; set; }           // Đồng bằng?
        public bool? IsHighland { get; set; }            // Cao nguyên?

        // --- 2. Văn hóa - phong tục ---
        public bool? LikeFestival { get; set; }          // Thích lễ hội, văn hóa dân gian?
        public bool? LikeEthnicCulture { get; set; }     // Thích trải nghiệm văn hóa dân tộc?
        public bool? LikeHistoricalSites { get; set; }   // Thích di tích, chùa chiền?

        // --- 3. Ẩm thực ---
        public bool? LikeSeafood { get; set; }           // Thích hải sản?
        public bool? LikeSpicyFood { get; set; }         // Thích món cay?
        public bool? LikeSweetFood { get; set; }         // Thích vị ngọt?
        public bool? LikeTraditionalFood { get; set; }   // Thích món truyền thống?

        // --- 4. Du lịch - địa danh ---
        public bool? LikeNatureTour { get; set; }        // Thích cảnh thiên nhiên, trekking?
        public bool? LikeCityTour { get; set; }          // Thích thành phố hiện đại?
        public bool? LikeUnescoSites { get; set; }       // Quan tâm di sản UNESCO?

        // --- 5. Con người & lối sống ---
        public bool? PreferFriendlyPeople { get; set; }  // Thích vùng có người thân thiện?
        public bool? PreferPeacefulLife { get; set; }    // Thích yên bình, mộc mạc?
        public bool? PreferDynamicLife { get; set; }     // Thích nhộn nhịp, sôi động?

        // --- 6. Kinh tế - đặc sản địa phương ---
        public bool? LikeHandicraft { get; set; }        // Quan tâm nghề thủ công, làng nghề?
        public bool? LikeAgriculture { get; set; }       // Thích đặc sản nông nghiệp (trà, gạo...)?
        public bool? LikeSeaProducts { get; set; }       // Thích đặc sản hải sản?

        // --- 7. Ngôn ngữ - giọng nói ---
        public string? AccentPreference { get; set; }    // "Bắc", "Trung", "Nam", "Tây Nguyên"...

        // --- 8. Kiến trúc - cảnh quan đô thị ---
        public bool? LikeAncientTown { get; set; }       // Thích phố cổ, nhà cổ?
        public bool? LikeModernResort { get; set; }      // Thích resort, khách sạn sang?
        public bool? LikeRuralVillage { get; set; }      // Thích vùng nông thôn, làng quê?

        // --- Tổng hợp người dùng ---
        public string? ExpectedMood { get; set; }        // “Thư giãn”, “Khám phá”, “Sống ảo”, “Ẩm thực”
        public string? SuggestedProvince { get; set; }   // Kết quả hệ thống dự đoán
    }
}
