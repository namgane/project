using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class ReviewsController : Controller
    {
        [HttpPost]
        public IActionResult Add(string cuisineId, string returnProvince, string displayName, int rating, string comment)
        {
            var json = HttpContext.Session.GetString("Reviews");
            var store = string.IsNullOrEmpty(json)
                ? new Dictionary<string, List<ReviewItem>>()
                : JsonSerializer.Deserialize<Dictionary<string, List<ReviewItem>>>(json)
                    ?? new Dictionary<string, List<ReviewItem>>();

            if (!store.ContainsKey(cuisineId))
                store[cuisineId] = new List<ReviewItem>();

            store[cuisineId].Add(new ReviewItem
            {
                UserName = displayName,
                Rating = rating,
                Comment = comment,
                
            });

            HttpContext.Session.SetString("Reviews", JsonSerializer.Serialize(store));

            return RedirectToAction("Province", "Cuisine", new { name = returnProvince });
        }
    }
}
