using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class QuizController : Controller
    {
        private static readonly List<(string Question, string Key, List<string> Options)> Questions = new()
        {
            ("Bạn muốn đi du lịch ở miền nào?", "Region", new() { "Bắc", "Trung", "Nam", "Tây Nguyên", "Không quan trọng" }),
            ("Bạn có thích đi biển không?", "IsCoastalArea", new() { "Có", "Không", "Bình thường" }),
            ("Bạn có thích leo núi, đi trekking không?", "IsMountainArea", new() { "Có", "Không", "Thỉnh thoảng" }),
            ("Bạn có quan tâm đến văn hóa, di tích lịch sử?", "LikeHistoricalSites", new() { "Có", "Không" }),
            ("Bạn có thích trải nghiệm lễ hội, phong tục địa phương?", "LikeFestival", new() { "Có", "Không" }),
            ("Bạn có thích khám phá văn hóa dân tộc thiểu số?", "LikeEthnicCulture", new() { "Có", "Không" }),
            ("Bạn có thích hải sản không?", "LikeSeafood", new() { "Có", "Không" }),
            ("Bạn có thích món ăn cay?", "LikeSpicyFood", new() { "Có", "Không" }),
            ("Bạn có thích không khí yên bình, mộc mạc?", "PreferPeacefulLife", new() { "Có", "Không" }),
            ("Bạn muốn nơi nhộn nhịp, hiện đại?", "PreferDynamicLife", new() { "Có", "Không" }),
            ("Bạn có thích du lịch thiên nhiên, rừng núi, sông nước?", "LikeNatureTour", new() { "Có", "Không" }),
            ("Bạn thích du lịch thành phố, khu nghỉ dưỡng sang trọng?", "LikeCityTour", new() { "Có", "Không" }),
            ("Bạn muốn tìm nơi thư giãn, nghỉ dưỡng hay khám phá mạo hiểm?", "ExpectedMood", new() { "Thư giãn", "Khám phá", "Sống ảo", "Ẩm thực" })
        };

        [HttpGet]
        public IActionResult Start()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("qIndex", 0);
            return RedirectToAction("Question");
        }

        [HttpGet]
        public IActionResult Question()
        {
            int qIndex = HttpContext.Session.GetInt32("qIndex") ?? 0;
            if (qIndex >= Questions.Count)
                return RedirectToAction("Result");

            var (question, key, options) = Questions[qIndex];
            ViewBag.Question = question;
            ViewBag.Key = key;
            ViewBag.Options = options;
            ViewBag.QuestionIndex = qIndex + 1;
            ViewBag.TotalQuestions = Questions.Count;
            return View();
        }

        [HttpPost]
        public IActionResult Question(string key, string answer)
        {
            if (!string.IsNullOrEmpty(key))
                HttpContext.Session.SetString(key, answer);

            int qIndex = (HttpContext.Session.GetInt32("qIndex") ?? 0) + 1;
            HttpContext.Session.SetInt32("qIndex", qIndex);

            if (qIndex >= Questions.Count)
                return RedirectToAction("Result");

            return RedirectToAction("Question");
        }

        [HttpGet]
        public IActionResult Result()
        {
            var answers = Questions.ToDictionary(q => q.Key, q => HttpContext.Session.GetString(q.Key));
            var destinations = DestinationData.GetAll();

            foreach (var dest in destinations)
            {
                // --- 1. Theo vùng ---
                if (answers["Region"] != null && dest.Region == answers["Region"])
                    dest.Score += 3;

                // --- 2. Thích biển ---
                if (answers["IsCoastalArea"] == "Có" && dest.HasBeach) dest.Score += 2;
                if (answers["IsMountainArea"] == "Có" && dest.HasMountain) dest.Score += 2;

                // --- 3. Văn hóa ---
                if (answers["LikeHistoricalSites"] == "Có" && dest.HasCulture) dest.Score += 2;
                if (answers["LikeFestival"] == "Có" && dest.HasCulture) dest.Score += 1;
                if (answers["LikeEthnicCulture"] == "Có" && dest.Region == "Tây Nguyên" || dest.Region == "Bắc") dest.Score += 2;

                // --- 4. Ẩm thực ---
                if (answers["LikeSeafood"] == "Có" && dest.HasBeach) dest.Score += 2;
                if (answers["LikeSpicyFood"] == "Có" && dest.Region == "Trung") dest.Score += 1;
                if (answers["ExpectedMood"] == "Ẩm thực" && dest.HasFood) dest.Score += 2;

                // --- 5. Phong cách sống ---
                if (answers["PreferPeacefulLife"] == "Có" && dest.Region != "Nam") dest.Score += 1;
                if (answers["PreferDynamicLife"] == "Có" && dest.Region == "Nam") dest.Score += 1;

                // --- 6. Loại hình du lịch ---
                if (answers["LikeNatureTour"] == "Có" && dest.HasMountain) dest.Score += 2;
                if (answers["LikeCityTour"] == "Có" && dest.Region == "Nam") dest.Score += 2;
            }

            // --- Chọn top 3 điểm cao nhất ---
            var top3 = destinations.OrderByDescending(d => d.Score).Take(3).ToList();
            return View(top3);
        }
    }
}
