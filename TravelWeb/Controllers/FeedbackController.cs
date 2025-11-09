using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;
using TravelWeb.Models;
using TravelWeb.Services;

namespace TravelWeb.Controllers
{
    public class FeedbackController : Controller
    {
        private const string SessionKey = "Feedback";

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Feedback());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Feedback model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var json = HttpContext.Session.GetString(SessionKey);
            var list = string.IsNullOrEmpty(json) ? new List<Feedback>() : (JsonSerializer.Deserialize<List<Feedback>>(json) ?? new List<Feedback>());
            list.Add(model);
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(list));
            // Also push to global store for admin view
            FeedbackStore.Add(model);
            TempData["Success"] = "Cảm ơn bạn đã phản hồi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult List()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            var list = string.IsNullOrEmpty(json) ? new List<Feedback>() : (JsonSerializer.Deserialize<List<Feedback>>(json) ?? new List<Feedback>());
            return View(list);
        }

        [HttpGet]
        public IActionResult Admin()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != "admin")
            {
                TempData["Error"] = "Bạn không có quyền truy cập.";
                return RedirectToAction("Index");
            }
            var all = FeedbackStore.GetAll();
            return View(all);
        }
    }
}


