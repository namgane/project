using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class FavoritesController : Controller
    {
        private const string SessionKey = "Favorites";

        private List<FavoriteItem> GetFavorites()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json)) return new List<FavoriteItem>();
            return JsonSerializer.Deserialize<List<FavoriteItem>>(json) ?? new List<FavoriteItem>();
        }

        private void SaveFavorites(List<FavoriteItem> items)
        {
            var json = JsonSerializer.Serialize(items);
            HttpContext.Session.SetString(SessionKey, json);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = GetFavorites();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(FavoriteItem item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Id))
            {
                TempData["Error"] = "Mục không hợp lệ.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var list = GetFavorites();
            if (!list.Any(x => x.Id == item.Id))
            {
                list.Add(item);
                SaveFavorites(list);
                TempData["Success"] = "Đã lưu vào Bộ sưu tập.";
            }
            else
            {
                TempData["Info"] = "Mục này đã có trong Bộ sưu tập.";
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string id)
        {
            var list = GetFavorites();
            list = list.Where(x => x.Id != id).ToList();
            SaveFavorites(list);
            TempData["Success"] = "Đã xóa khỏi Bộ sưu tập.";
            return RedirectToAction("Index");
        }
    }
}


