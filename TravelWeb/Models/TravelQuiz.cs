using System;
using System.Collections.Generic;

namespace TravelWeb.Models
{
    public class TravelQuiz
    {
        // ===================== 1. Địa lý - khí hậu =====================
        public string Region { get; set; }                   // Bắc / Trung / Nam / Tây Nguyên / Không quan trọng
        public bool IsMountainArea { get; set; }             // Thích leo núi, trekking?
        public bool IsCoastalArea { get; set; }              // Thích biển?
        public bool IsPlainArea { get; set; }                // Thích đồng bằng?
        public bool IsHighland { get; set; }                 // Cao nguyên?

        // ===================== 2. Văn hóa - phong tục =====================
        public bool LikeFestival { get; set; }               // Lễ hội, phong tục
        public bool LikeEthnicCulture { get; set; }          // Văn hóa dân tộc thiểu số
        public bool LikeHistoricalSites { get; set; }        // Di tích văn hóa

        // ===================== 3. Ẩm thực =====================
        public bool LikeSeafood { get; set; }                // Hải sản
        public bool LikeSpicyFood { get; set; }              // Món cay
        public bool LikeSweetFood { get; set; }              // Món ngọt
        public bool LikeTraditionalFood { get; set; }        // Món truyền thống

        // ===================== 4. Loại hình du lịch =====================
        public bool LikeNatureTour { get; set; }             // Thiên nhiên, trekking
        public bool LikeCityTour { get; set; }               // Thành phố hiện đại
        public bool LikeUnescoSites { get; set; }            // Di sản UNESCO

        // ===================== 5. Con người & lối sống =====================
        public bool PreferFriendlyPeople { get; set; }       // Người thân thiện
        public bool PreferPeacefulLife { get; set; }         // Yên bình, mộc mạc
        public bool PreferDynamicLife { get; set; }          // Nhộn nhịp

        // ===================== 6. Kinh tế - đặc sản =====================
        public bool LikeHandicraft { get; set; }             // Làng nghề thủ công
        public bool LikeAgriculture { get; set; }            // Nông sản
        public bool LikeSeaProducts { get; set; }            // Đặc sản hải sản

        // ===================== 7. Ngôn ngữ - giọng nói =====================
        public string AccentPreference { get; set; }         // Bắc / Trung / Nam / Tây Nguyên

        // ===================== 8. Kiến trúc =====================
        public bool LikeAncientTown { get; set; }            // Phố cổ
        public bool LikeModernResort { get; set; }           // Resort sang trọng
        public bool LikeRuralVillage { get; set; }           // Vùng nông thôn

        // ===================== Tổng hợp =====================
        public string ExpectedMood { get; set; }             // Thư giãn / Khám phá / Sống ảo / Ẩm thực
        public string SuggestedProvince { get; set; }        // Kết quả dự đoán

        // ===================== Ngân sách - thời gian =====================
        public string BudgetLevel { get; set; }              // Tiết kiệm / Vừa phải / Cao cấp
        public string TravelSeason { get; set; }             // Xuân / Hè / Thu / Đông / Không quan trọng
        public int TravelDurationDays { get; set; }          // Số ngày dự kiến
        public string TravelPace { get; set; }               // Chậm rãi / Cân bằng / Dày đặc
    }

    public class QuizHistoryItem
    {
        public Dictionary<string, string> Answers { get; set; } = new();
        public List<string> TopResults { get; set; } = new();
        public DateTime TakenAt { get; set; } = DateTime.UtcNow;
    }
}
