using Microsoft.AspNetCore.Mvc;
using TravelWeb.Models;
using System;
using System.Linq;

namespace TravelWeb.Controllers
{
    public class PassportController : Controller
    {
        // Giả lập lưu session người dùng
        private static TravelPassport _userPassport = new()
        {
            UserId = "U001",
            UserName = "Khách du lịch",
            TotalPoints = 0,
            TravelLevel = 1
        };

        public IActionResult Index()
        {
            return View(_userPassport);
        }

        [HttpPost]
        public IActionResult AddStamp(string province, string eventName)
        {
            var stamp = new PassportStamp
            {
                Province = province,
                EventName = eventName,
                DateEarned = DateTime.Now,
                Points = new Random().Next(10, 25),
                BadgeIcon = "/images/icons/stamp.png"
            };

            _userPassport.AddStamp(stamp);
            TempData["Message"] = $"Bạn vừa nhận dấu mộc tại {province} 🎉";

            return RedirectToAction("Index");
        }
    }
}
