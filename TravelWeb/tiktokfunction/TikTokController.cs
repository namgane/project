using Microsoft.AspNetCore.Mvc;
using System.Web;
using TravelWeb.Models;

namespace TravelWeb.tiktokfunction
{
    public class TikTokController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var topSearches = TikTokSearchService.GetTopSearchesThisMonth();
            ViewBag.TopSearches = topSearches;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập từ khóa tìm kiếm!";
                return RedirectToAction("Index");
            }

            try
            {
                TikTokSearchService.AddSearch(keyword);

                string encoded = System.Net.WebUtility.UrlEncode(keyword);
                string tiktokSearchUrl = $"https://www.tiktok.com/search?q={encoded}";

                // Nếu người dùng dán link video TikTok cụ thể
                string embedHtml = "";
                if (keyword.Contains("tiktok.com/"))
                {
                    try
                    {
                        using (var http = new HttpClient())
                        {
                            http.Timeout = TimeSpan.FromSeconds(10);
                            var apiUrl = $"https://www.tiktok.com/oembed?url={keyword}";
                            var response = await http.GetStringAsync(apiUrl);
                            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                            embedHtml = json.html;
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["WarningMessage"] = "Không thể tải video nhúng. Bạn có thể xem trực tiếp trên TikTok.";
                    }
                }

                ViewBag.Keyword = keyword;
                ViewBag.EmbedHtml = embedHtml;
                ViewBag.TikTokUrl = tiktokSearchUrl;
                ViewBag.TopSearches = TikTokSearchService.GetTopSearchesThisMonth();

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tìm kiếm. Vui lòng thử lại!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Result(string keyword, string searchUrl)
        {
            if (string.IsNullOrWhiteSpace(keyword) || string.IsNullOrWhiteSpace(searchUrl))
            {
                TempData["ErrorMessage"] = "Thông tin tìm kiếm không hợp lệ!";
                return RedirectToAction("Index");
            }

            ViewBag.Keyword = keyword;
            ViewBag.SearchUrl = searchUrl;
            
            return View();
        }
    }
}
