using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;

namespace TravelWeb.Models
{
    public static class CuisineData
    {
        private static readonly List<CuisineItem> Items = new List<CuisineItem>
            {
            // ======= MIỀN BẮC =======
            // Hà Nội
            new CuisineItem { Province = "Hà Nội", Name = "Phở bò", Description = "Biểu tượng ẩm thực Việt, nước dùng thanh ngọt.", AveragePrice = 40000, ImageUrl = "/images/pho.jpg", Popularity = 100 },
            new CuisineItem { Province = "Hà Nội", Name = "Bún chả", Description = "Thịt nướng ăn cùng bún và nước chấm chua ngọt.", AveragePrice = 45000, ImageUrl = "/images/buncha.jpg", Popularity = 98 },
            new CuisineItem { Province = "Hà Nội", Name = "Chả cá Lã Vọng", Description = "Cá chiên nghệ ăn kèm thì là, hành lá.", AveragePrice = 150000, ImageUrl = "/images/chaca.jpg", Popularity = 96 },
            new CuisineItem { Province = "Hà Nội", Name = "Bún thang", Description = "Món bún cầu kỳ từ trứng, gà, giò.", AveragePrice = 50000, ImageUrl = "/images/bunthang.jpg", Popularity = 92 },
            new CuisineItem { Province = "Hà Nội", Name = "Xôi xéo", Description = "Xôi nếp vàng ăn với hành phi.", AveragePrice = 20000, ImageUrl = "/images/ xoixeo.jpg", Popularity = 90 },
            new CuisineItem { Province = "Hà Nội", Name = "Bánh cuốn Thanh Trì", Description = "Bánh mỏng dai, ăn kèm nước mắm.", AveragePrice = 30000, ImageUrl = "/images/banhcuon.jpg", Popularity = 88 },
            new CuisineItem { Province = "Hà Nội", Name = "Trà chanh Nhà Thờ", Description = "Đồ uống đường phố nổi tiếng giới trẻ.", AveragePrice = 15000, ImageUrl = "/images/trachanh.jpg", Popularity = 85 },
            new CuisineItem { Province = "Hà Nội", Name = "Bánh tôm Hồ Tây", Description = "Tôm chiên giòn ăn cùng rau sống.", AveragePrice = 45000, ImageUrl = "/images/banhtom.jpg", Popularity = 91 },
            new CuisineItem { Province = "Hà Nội", Name = "Nem rán", Description = "Nem cuốn rán giòn, nhân thịt mộc nhĩ.", AveragePrice = 40000, ImageUrl = "/images/nemran.jpg", Popularity = 93 },
            new CuisineItem { Province = "Hà Nội", Name = "Cà phê trứng", Description = "Cà phê béo ngậy với lòng đỏ trứng.", AveragePrice = 40000, ImageUrl = "/images/caphetrung.jpg", Popularity = 94 },

            // Hải Phòng
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh đa cua", Description = "Sợi bánh đa đỏ, nước cua đậm đà.", AveragePrice = 35000, ImageUrl = "/images/banhdacua.jpg", Popularity = 95 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh mì cay", Description = "Ổ bánh nhỏ với pate cay đặc trưng.", AveragePrice = 10000, ImageUrl = "/images/banhmicay.jpg", Popularity = 92 },
            new CuisineItem { Province = "Hải Phòng", Name = "Nem cua bể", Description = "Nem cuốn to nhân cua biển.", AveragePrice = 60000, ImageUrl = "/images/nemcuabe.jpg", Popularity = 90 },
            new CuisineItem { Province = "Hải Phòng", Name = "Ốc các loại", Description = "Hải sản tươi hấp dẫn giới trẻ.", AveragePrice = 70000, ImageUrl = "/images/oc.jpg", Popularity = 87 },
            new CuisineItem { Province = "Hải Phòng", Name = "Chè giun", Description = "Chè màu xanh mát mắt, vị ngọt thanh.", AveragePrice = 20000, ImageUrl = "/images/chegiun.jpg", Popularity = 82 },
            new CuisineItem { Province = "Hải Phòng", Name = "Lẩu cua đồng", Description = "Lẩu đặc sản đất Cảng.", AveragePrice = 150000, ImageUrl = "/images/laucua.jpg", Popularity = 88 },
            new CuisineItem { Province = "Hải Phòng", Name = "Cơm rang cua bể", Description = "Cơm rang vàng với thịt cua.", AveragePrice = 80000, ImageUrl = "/images/comrangcua.jpg", Popularity = 85 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh bèo Hải Phòng", Description = "Bánh dẻo nhân thịt, ăn với mắm.", AveragePrice = 25000, ImageUrl = "/images/banhbeo.jpg", Popularity = 80 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bún cá cay", Description = "Bún cá đậm vị, cay nồng.", AveragePrice = 40000, ImageUrl = "/images/buncacay.jpg", Popularity = 89 },
            new CuisineItem { Province = "Hải Phòng", Name = "Sứa đỏ", Description = "Đặc sản mùa hè, chấm mắm tôm.", AveragePrice = 35000, ImageUrl = "/images/suado.jpg", Popularity = 83 },

            // ======= MIỀN TRUNG =======
            // Đà Nẵng
            new CuisineItem { Province = "Đà Nẵng", Name = "Mì Quảng", Description = "Món đặc sản biểu tượng của Đà Nẵng.", AveragePrice = 35000, ImageUrl = "/images/miquang.jpg", Popularity = 99 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh tráng cuốn thịt heo", Description = "Món ăn dân dã, chấm mắm nêm.", AveragePrice = 60000, ImageUrl = "/images/banhtrang.jpg", Popularity = 95 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bún mắm nêm", Description = "Hương vị mạnh mẽ, đậm đà miền Trung.", AveragePrice = 30000, ImageUrl = "/images/bunmam.jpg", Popularity = 90 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Hải sản nướng", Description = "Đa dạng hải sản tươi ngon.", AveragePrice = 150000, ImageUrl = "/images/haisan.jpg", Popularity = 94 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh xèo", Description = "Bánh giòn, nhân tôm thịt giá đỗ.", AveragePrice = 40000, ImageUrl = "/images/banhxeo.jpg", Popularity = 88 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Ram cuốn cải", Description = "Ram rán giòn ăn cùng rau sống.", AveragePrice = 30000, ImageUrl = "/images/ramcuoncai.jpg", Popularity = 85 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh bèo nậm lọc", Description = "Ba loại bánh Huế du nhập được yêu thích.", AveragePrice = 35000, ImageUrl = "/images/banhbeo.jpg", Popularity = 83 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Gỏi cá Nam Ô", Description = "Món ăn độc đáo từ cá sống.", AveragePrice = 60000, ImageUrl = "/images/goica.jpg", Popularity = 91 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Cao lầu", Description = "Món gốc Hội An, được ưa chuộng.", AveragePrice = 40000, ImageUrl = "/images/caolau.jpg", Popularity = 87 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bún chả cá", Description = "Nước dùng thanh, cá tươi.", AveragePrice = 35000, ImageUrl = "/images/bunchaca.jpg", Popularity = 93 },

            // Huế
            new CuisineItem { Province = "Huế", Name = "Bún bò Huế", Description = "Món đặc trưng cố đô, cay nồng đậm đà.", AveragePrice = 40000, ImageUrl = "/images/bunbohue.jpg", Popularity = 100 },
            new CuisineItem { Province = "Huế", Name = "Cơm hến", Description = "Dân dã, rẻ mà ngon, đặc sản nổi tiếng.", AveragePrice = 20000, ImageUrl = "/images/comhen.jpg", Popularity = 90 },
            new CuisineItem { Province = "Huế", Name = "Bánh bèo", Description = "Bánh mỏng, nhân tôm, nước mắm tỏi ớt.", AveragePrice = 25000, ImageUrl = "/images/banhbeohue.jpg", Popularity = 88 },
            new CuisineItem { Province = "Huế", Name = "Bánh nậm", Description = "Bánh gói lá chuối, mềm thơm.", AveragePrice = 25000, ImageUrl = "/images/banhnam.jpg", Popularity = 87 },
            new CuisineItem { Province = "Huế", Name = "Bánh lọc", Description = "Trong suốt, nhân tôm thịt, dai dẻo.", AveragePrice = 30000, ImageUrl = "/images/banhloc.jpg", Popularity = 89 },
            new CuisineItem { Province = "Huế", Name = "Chè Huế", Description = "Chè thập cẩm, bắp, sen, đậu xanh…", AveragePrice = 20000, ImageUrl = "/images/chehue.jpg", Popularity = 91 },
            new CuisineItem { Province = "Huế", Name = "Cơm cung đình", Description = "Được phục vụ cầu kỳ, tinh tế.", AveragePrice = 200000, ImageUrl = "/images/comcungdinh.jpg", Popularity = 85 },
            new CuisineItem { Province = "Huế", Name = "Tré Huế", Description = "Món lên men chua nhẹ, thơm thịt.", AveragePrice = 50000, ImageUrl = "/images/trehue.jpg", Popularity = 80 },
            new CuisineItem { Province = "Huế", Name = "Mè xửng", Description = "Đặc sản làm quà, dẻo ngọt.", AveragePrice = 30000, ImageUrl = "/images/mexung.jpg", Popularity = 82 },
            new CuisineItem { Province = "Huế", Name = "Bún nghệ", Description = "Bún xào nghệ dân dã, thơm lừng.", AveragePrice = 30000, ImageUrl = "/images/bunnghe.jpg", Popularity = 78 },

            // ======= MIỀN NAM =======
            // TP.HCM
            new CuisineItem { Province = "TP.HCM", Name = "Cơm tấm", Description = "Món ăn quốc dân của Sài Gòn.", AveragePrice = 40000, ImageUrl = "/images/comtam.jpg", Popularity = 100 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh mì", Description = "Đa dạng nhân, ngon - nhanh - tiện.", AveragePrice = 25000, ImageUrl = "/images/banhmi.jpg", Popularity = 97 },
            new CuisineItem { Province = "TP.HCM", Name = "Hủ tiếu", Description = "Nước dùng ngọt thanh từ xương heo.", AveragePrice = 40000, ImageUrl = "/images/hutieu.jpg", Popularity = 95 },
            new CuisineItem { Province = "TP.HCM", Name = "Phá lấu", Description = "Lòng bò hầm nước dừa đậm đà.", AveragePrice = 30000, ImageUrl = "/images/phalau.jpg", Popularity = 90 },
            new CuisineItem { Province = "TP.HCM", Name = "Bột chiên", Description = "Món ăn vặt nổi tiếng Sài Thành.", AveragePrice = 25000, ImageUrl = "/images/botchien.jpg", Popularity = 88 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh tráng trộn", Description = "Ăn vặt thần thánh của giới trẻ.", AveragePrice = 20000, ImageUrl = "/images/banhtrangtron.jpg", Popularity = 96 },
            new CuisineItem { Province = "TP.HCM", Name = "Gỏi cuốn", Description = "Cuốn tôm thịt chấm tương đặc trưng.", AveragePrice = 25000, ImageUrl = "/images/goicuon.jpg", Popularity = 91 },
            new CuisineItem { Province = "TP.HCM", Name = "Chè Sài Gòn", Description = "Ngọt mát, phong phú nguyên liệu.", AveragePrice = 20000, ImageUrl = "/images/chesaigon.jpg", Popularity = 85 },
            new CuisineItem { Province = "TP.HCM", Name = "Lẩu mắm", Description = "Đậm vị miền Tây trong lòng thành phố.", AveragePrice = 200000, ImageUrl = "/images/laumam.jpg", Popularity = 89 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh xèo", Description = "Bánh vàng giòn nhân tôm thịt giá đỗ.", AveragePrice = 45000, ImageUrl = "/images/banhxeo.jpg", Popularity = 87 },
        };

        // Alias/canonical province names for robust search
        private static readonly Dictionary<string, string> ProvinceAliases = new Dictionary<string, string>
        {
            { "ho chi minh", "TP.HCM" },
            { "tphcm", "TP.HCM" },
            { "tp hcm", "TP.HCM" },
            { "sai gon", "TP.HCM" },
            { "sài gòn", "TP.HCM" },
            { "ha noi", "Hà Nội" },
            { "hanoi", "Hà Nội" },
            { "da nang", "Đà Nẵng" },
            { "danang", "Đà Nẵng" },
            { "hue", "Huế" },
            { "hai phong", "Hải Phòng" },
            { "haiphong", "Hải Phòng" },
            { "khanh hoa", "Khánh Hòa" },
            { "nha trang", "Khánh Hòa" },
            { "can tho", "Cần Thơ" },
            { "cantho", "Cần Thơ" }
        };

        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var normalized = text.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();
            foreach (var ch in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string CanonicalizeProvince(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var fold = RemoveDiacritics(input).ToLower().Trim();
            if (ProvinceAliases.TryGetValue(fold, out var canonical))
            {
                return canonical;
            }
            // Try direct match to any known province after folding
            var known = GetAllProvinces();
            foreach (var p in known)
            {
                if (RemoveDiacritics(p).ToLower() == fold) return p;
            }
            return input.Trim();
        }

        public static IEnumerable<string> GetAllProvinces()
        {
            return Items.Select(i => i.Province).Distinct().OrderBy(n => n);
        }

        public static List<CuisineItem> GetTopByProvince(string province, int top = 10)
        {
            var canonical = CanonicalizeProvince(province);
            return Items
                .Where(i => i.Province.Equals(canonical, System.StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(i => i.Popularity)
                .Take(top)
                .ToList();
        }

        public static string CanonicalProvinceName(string input) => CanonicalizeProvince(input);

        public static List<string> FindSimilarProvinces(string query, int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<string>();
            var fold = RemoveDiacritics(query).ToLower().Trim();
            var all = GetAllProvinces().ToList();
            // 1) contains match on folded string
            var contains = all.Where(p => RemoveDiacritics(p).ToLower().Contains(fold)).ToList();
            if (contains.Count >= limit) return contains.Take(limit).ToList();
            // 2) startswith match as fallback
            var starts = all.Where(p => RemoveDiacritics(p).ToLower().StartsWith(fold)).ToList();
            foreach (var s in starts)
            {
                if (!contains.Contains(s)) contains.Add(s);
            }
            return contains.Take(limit).ToList();
        }

        public static int GetCountByProvince(string province)
        {
            if (string.IsNullOrWhiteSpace(province)) return 0;
            var canonical = CanonicalizeProvince(province);
            return Items.Count(i => i.Province.Equals(canonical, System.StringComparison.OrdinalIgnoreCase));
        }

        public static decimal GetAveragePriceForProvince(string province)
        {
            if (string.IsNullOrWhiteSpace(province)) return 0;
            var canonical = CanonicalizeProvince(province);
            var list = Items.Where(i => i.Province.Equals(canonical, System.StringComparison.OrdinalIgnoreCase)).ToList();
            if (list.Count == 0) return 0;
            return list.Average(i => i.AveragePrice);
        }
    }
}


