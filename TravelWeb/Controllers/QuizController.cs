/*using Microsoft.AspNetCore.Mvc;
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
            var stored = HttpContext.Session.Get("answers");
            var answers = stored != null
                ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(stored)
                : Questions.ToDictionary(q => q.Key, q => HttpContext.Session.GetString(q.Key) ?? string.Empty);

            var destinations = DestinationData.GetAllNormalized();

            destinations = QuizRuleEngine.Score(destinations, answers);

            var top1 = destinations.OrderByDescending(d => d.Score).FirstOrDefault();
            QuizRuleEngine.ReinforceTopRules(top1);

            History.Add(new QuizHistoryItem
            {
                Answers = answers,
                TopResults = destinations.OrderByDescending(d => d.Score).Take(3).Select(d => d.Name).ToList(),
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
    }
}
*/