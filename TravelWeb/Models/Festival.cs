using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TravelWeb.Models
{
    public class Festival
    {
        // =========================
        // 1. THUỘC TÍNH CƠ SỞ
        // =========================

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new();

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Highlight { get; set; } = string.Empty;

        // =========================
        // 2. THUỘC TÍNH TÍNH TOÁN
        // =========================

        public string Season => StartDate.Month switch
        {
            1 or 2 or 3 => "Xuân",
            4 or 5 or 6 => "Hè",
            7 or 8 or 9 => "Thu",
            _ => "Đông"
        };

        public string DateRange =>
            StartDate.Month == EndDate.Month
                ? $"{StartDate:dd} - {EndDate:dd/MM/yyyy}"
                : $"{StartDate:dd/MM} - {EndDate:dd/MM/yyyy}";

        public string Type => Name switch
        {
            var n when n.Contains("Chùa Hương", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Đền Trần", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Vu Lan", StringComparison.OrdinalIgnoreCase)
                => "Tâm linh",

            var n when n.Contains("Lim", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Đền Hùng", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Festival Huế", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Katê", StringComparison.OrdinalIgnoreCase)
                => "Văn hóa",

            var n when n.Contains("Đua voi", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Oóc Om Bóc", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Nghinh Ông", StringComparison.OrdinalIgnoreCase)
                => "Dân gian",

            _ => "Đặc sắc"
        };

        public string PopularityLevel => Name switch
        {
            var n when n.Contains("Chùa Hương", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Đền Hùng", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Pháo hoa", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Festival Huế", StringComparison.OrdinalIgnoreCase)
                => "Nổi bật",

            var n when n.Contains("Lim", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Cà phê", StringComparison.OrdinalIgnoreCase)
                    || n.Contains("Hoa Đà Lạt", StringComparison.OrdinalIgnoreCase)
                => "Phổ biến",

            _ => "Bình thường"
        };

        // =========================
        // 3. TIỆN ÍCH – CHUYÊN NGHIỆP
        // =========================

        public string MainImage =>
            ImageUrls?.FirstOrDefault() ?? ImageUrl;

        public string Slug =>
            Regex.Replace(
                RemoveDiacritics(Name).ToLower(),
                @"[^a-z0-9]+",
                "-"
            ).Trim('-');

        private static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
