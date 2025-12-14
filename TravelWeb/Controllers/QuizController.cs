using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;
using System;
using System.Text.Json;
using TravelWeb.Services;

namespace TravelWeb.Controllers
{
    public class QuizController : Controller
    {
        private static readonly List<(string Question, string Key, List<string> Options, string Hint)> Questions = new()
        {
            ("Bạn muốn đi du lịch ở miền nào?", "Region", new() { "Bắc", "Trung", "Nam", "Tây Nguyên", "Không quan trọng" }, "Ưu tiên khu vực để tối ưu thời tiết & di chuyển"),
            ("Bạn có thích đi biển không?", "IsCoastalArea", new() { "Có", "Không", "Bình thường" }, "Ảnh hưởng mạnh tới gợi ý các tỉnh ven biển"),
            ("Bạn có thích leo núi, đi trekking không?", "IsMountainArea", new() { "Có", "Không", "Thỉnh thoảng" }, "Ưu tiên địa hình núi và cao nguyên"),
            ("Ngân sách mỗi ngày của bạn?", "BudgetLevel", new() { "Tiết kiệm", "Vừa phải", "Cao cấp" }, "Dùng để lọc & chuẩn hóa điểm theo cost"),
            ("Bạn dự kiến đi trong mùa nào?", "TravelSeason", new() { "Xuân", "Hè", "Thu", "Đông", "Không quan trọng" }, "Gợi ý theo mùa đẹp nhất của điểm đến"),
            ("Bạn có quan tâm đến văn hóa, di tích lịch sử?", "LikeHistoricalSites", new() { "Có", "Không" }, "Match với điểm có văn hóa/di tích"),
            ("Bạn có thích trải nghiệm lễ hội, phong tục địa phương?", "LikeFestival", new() { "Có", "Không" }, "Ưu tiên nơi giàu lễ hội"),
            ("Bạn có thích khám phá văn hóa dân tộc thiểu số?", "LikeEthnicCulture", new() { "Có", "Không" }, "Ưu tiên Tây Bắc / Tây Nguyên"),
            ("Bạn có thích hải sản không?", "LikeSeafood", new() { "Có", "Không" }, "Ưu tiên biển đảo"),
            ("Bạn có thích món ăn cay?", "LikeSpicyFood", new() { "Có", "Không" }, "Ưu tiên miền Trung"),
            ("Bạn có thích không khí yên bình, mộc mạc?", "PreferPeacefulLife", new() { "Có", "Không" }, "Ưu tiên vùng sông nước / cao nguyên yên bình"),
            ("Bạn muốn nơi nhộn nhịp, hiện đại?", "PreferDynamicLife", new() { "Có", "Không" }, "Ưu tiên thành phố lớn"),
            ("Bạn có thích du lịch thiên nhiên, rừng núi, sông nước?", "LikeNatureTour", new() { "Có", "Không" }, "Ưu tiên điểm thiên nhiên"),
            ("Bạn thích du lịch thành phố, khu nghỉ dưỡng sang trọng?", "LikeCityTour", new() { "Có", "Không" }, "Ưu tiên city/resort"),
            ("Bạn thích nhịp đi chơi như thế nào?", "TravelPace", new() { "Chậm rãi", "Cân bằng", "Dày đặc" }, "Dùng để gợi ý số ngày & lịch trình"),
            ("Bạn muốn tìm nơi thư giãn, khám phá hay ẩm thực?", "ExpectedMood", new() { "Thư giãn", "Khám phá", "Sống ảo", "Ẩm thực" }, "Ưu tiên mood/experience"),
            ("Bạn dự kiến đi bao nhiêu ngày?", "TravelDurationDays", new() { "2", "3", "4", "5", "6", "7+" }, "Dùng để khớp lịch trình mẫu")
        };
        private static readonly List<QuizHistoryItem> History = new();

        // --------------------------------------------------------
        // 1. START QUIZ
        // --------------------------------------------------------
        [HttpGet]
        public IActionResult Start()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("qIndex", 0);
            HttpContext.Session.Set("answers", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(new Dictionary<string, string>()));
            return RedirectToAction("Question");
        }

        // --------------------------------------------------------
        // 2. SHOW QUESTION
        // --------------------------------------------------------
        [HttpGet]
        public IActionResult Question()
        {
            int qIndex = HttpContext.Session.GetInt32("qIndex") ?? 0;
            if (qIndex >= Questions.Count)
                return RedirectToAction("Result");

            var (question, key, options, hint) = Questions[qIndex];

            ViewBag.Question = question;
            ViewBag.Key = key;
            ViewBag.Options = options;
            ViewBag.Hint = hint;
            ViewBag.QuestionIndex = qIndex + 1;
            ViewBag.TotalQuestions = Questions.Count;

            return View();
        }

        // --------------------------------------------------------
        // 3. SAVE ANSWER
        // --------------------------------------------------------
        [HttpPost]
        public IActionResult Question(string key, string answer)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var stored = HttpContext.Session.Get("answers");
                var answers = stored != null
                    ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(stored)
                    : new Dictionary<string, string>();

                answers[key] = answer;
                HttpContext.Session.Set("answers", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(answers));
            }

            int qIndex = (HttpContext.Session.GetInt32("qIndex") ?? 0) + 1;
            HttpContext.Session.SetInt32("qIndex", qIndex);

            if (qIndex >= Questions.Count)
                return RedirectToAction("Result");

            return RedirectToAction("Question");
        }

        // --------------------------------------------------------
        // 4. CALCULATE RESULT
        // --------------------------------------------------------
        [HttpGet]
        public IActionResult Result()
        {
            Dictionary<string, string> answers;
            try
            {
                var stored = HttpContext.Session.Get("answers");
                answers = stored != null
                    ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(stored) ?? new Dictionary<string, string>()
                    : Questions.ToDictionary(q => q.Key, q => HttpContext.Session.GetString(q.Key) ?? string.Empty);
            }
            catch
            {
                answers = Questions.ToDictionary(q => q.Key, q => HttpContext.Session.GetString(q.Key) ?? string.Empty);
            }

            var destinations = DestinationData.GetAllNormalized();
            if (destinations == null || !destinations.Any())
            {
                TempData["ErrorMessage"] = "Không tìm thấy dữ liệu điểm đến. Vui lòng thử lại.";
                return RedirectToAction("Start");
            }

            destinations = QuizRuleEngine.Score(destinations, answers);

            var top1 = destinations.OrderByDescending(d => d.Score).FirstOrDefault();
            if (top1 != null)
            {
                QuizRuleEngine.ReinforceTopRules(top1);
            }

            History.Add(new QuizHistoryItem
            {
                Answers = answers,
                TopResults = destinations.Where(d => d != null && !string.IsNullOrEmpty(d.Name))
                    .OrderByDescending(d => d.Score)
                    .Take(3)
                    .Select(d => d.Name)
                    .ToList(),
                TakenAt = DateTime.UtcNow
            });

            var top3 = destinations
                .OrderByDescending(d => d.Score)
                .ThenByDescending(d => d.NormalizedScore)
                .Take(3)
                .ToList();

            ViewBag.History = History.TakeLast(5).ToList();
            ViewBag.Answers = answers;
            ViewBag.Weights = QuizRuleEngine.GetAdaptiveWeights();
            return View(top3);
        }

        public List<string> GenerateComparisonReasons(Destination top1, Destination top2, Dictionary<string, string> userAnswers)
        {
            var reasons = new List<string>();

            // 1. So sánh Tiêu chí Trọng yếu: BUDGET (Ngân sách Cao cấp)
            if (userAnswers.GetValueOrDefault("BudgetLevel") == "Cao cấp" && top1.IsModernCity)
            {
                if (!top2.IsModernCity || top1.Score > top2.Score + 10) // Giả định thành phố lớn có dịch vụ cao cấp hơn
                {
                    reasons.Add($"**BudgetLevel: Cao cấp (High weight):** {top1.Name} là đô thị lớn/trung tâm du lịch (như {top1.Province}) có hệ thống dịch vụ 5 sao, resort và khu giải trí quy mô, phù hợp hơn với mức chi tiêu **Cao cấp** của bạn so với {top2.Name}.");
                }
            }

            // 2. So sánh Tiêu chí Trọng yếu: DIVERSITY (Đa dạng Thiên nhiên/Tốc độ Dày đặc)
            bool top1HasDiverseNature = top1.HasMountain && top1.HasBeach;
            bool top2HasDiverseNature = top2.HasMountain && top2.HasBeach;

            if (userAnswers.GetValueOrDefault("TravelPace") == "Dày đặc" && top1HasDiverseNature && !top2HasDiverseNature)
            {
                reasons.Add($"**TravelPace: Dày đặc & Nature:** {top1.Name} có lợi thế vượt trội khi kết hợp cả **Núi và Biển ({top1.Province})** trong một khu vực. Điều này cho phép bạn thực hiện lịch trình **Dày đặc** và đa dạng trong chuyến đi **7+ ngày** mà {top2.Name} không thể sánh bằng.");
            }
            else if (userAnswers.GetValueOrDefault("TravelPace") == "Dày đặc" && top1.AttractionCount > top2.AttractionCount + 2)
            {
                reasons.Add($"**TravelPace: Dày đặc (Tốc độ):** {top1.Name} có số lượng điểm tham quan chính (Attractions) dày đặc hơn (tổng {top1.AttractionCount} điểm), tạo điều kiện cho lịch trình khám phá **Dày đặc** mà bạn yêu cầu.");
            }

            // 3. So sánh Tiêu chí Phụ trợ: FOOD (Tâm trạng Ẩm thực)
            if (userAnswers.GetValueOrDefault("ExpectedMood") == "Ẩm thực" && top1.HasFood)
            {
                if (top1.Region == "Trung" && top2.Region == "Bắc")
                {
                    reasons.Add($"**ExpectedMood: Ẩm thực (Miền):** {top1.Name} (Miền Trung) có sự đa dạng ẩm thực độc đáo (mì Quảng, bún bò, cao lầu) phù hợp với tâm trạng **Ẩm thực** bạn chọn, bổ sung hương vị khác biệt so với {top2.Name} (Miền Bắc/Nam).");
                }
            }

            // Thêm câu kết luận chung nếu lý do quá ít
            if (reasons.Count == 0)
            {
                reasons.Add($"Sự khác biệt điểm số chủ yếu đến từ sự phù hợp tuyệt đối của {top1.Name} đối với sự kết hợp các yếu tố phụ như **{userAnswers.GetValueOrDefault("TravelSeason", "Mùa")}** và **{userAnswers.GetValueOrDefault("Region", "Khu vực")}** mà bạn đã chọn.");
            }

            return reasons;
        }
    }
}