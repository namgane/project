using System;
using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Services
{
    /// <summary>
    /// Rule-based scoring engine + explainability for Travel Quiz.
    /// </summary>
    public static class QuizRuleEngine
    {
        public static readonly Dictionary<string, double> BaseWeights = new()
        {
            ["Region"] = 3.0,
            ["IsCoastalArea"] = 2.0,
            ["IsMountainArea"] = 2.0,
            ["LikeHistoricalSites"] = 2.5,
            ["LikeFestival"] = 1.5,
            ["LikeEthnicCulture"] = 2.0,
            ["LikeSeafood"] = 1.8,
            ["LikeSpicyFood"] = 1.2,
            ["PreferPeacefulLife"] = 1.0,
            ["PreferDynamicLife"] = 1.0,
            ["LikeNatureTour"] = 2.2,
            ["LikeCityTour"] = 1.8,
            ["ExpectedMood"] = 2.0,
            ["BudgetLevel"] = 2.0,
            ["TravelSeason"] = 2.2,
            ["TravelDurationDays"] = 1.0,
            ["TravelPace"] = 1.0
        };

        private static readonly Dictionary<string, double> AdaptiveWeights = BaseWeights.ToDictionary(k => k.Key, v => v.Value);

        public static List<Destination> Score(List<Destination> destinations, Dictionary<string, string> answers)
        {
            foreach (var dest in destinations)
            {
                ResetDestination(dest);
                ApplyRules(dest, answers);
            }

            var maxScore = destinations.Max(d => d.Score);
            if (maxScore < 1) maxScore = 1;
            foreach (var dest in destinations)
            {
                dest.NormalizedScore = Math.Round(dest.Score * 100.0 / maxScore, 1);
                // Tránh hiển thị toàn 0% trên UI
                if (dest.NormalizedScore <= 0 && dest.Score > 0) dest.NormalizedScore = 5;
            }

            return destinations;
        }

        public static void ReinforceTopRules(Destination top1)
        {
            if (top1 == null || top1.RuleMatches == null) return;

            foreach (var match in top1.RuleMatches)
            {
                if (!AdaptiveWeights.ContainsKey(match.RuleKey)) continue;
                AdaptiveWeights[match.RuleKey] = Math.Min(AdaptiveWeights[match.RuleKey] + 0.1, BaseWeights[match.RuleKey] * 1.6);
            }
        }

        public static Dictionary<string, double> GetAdaptiveWeights() => new(AdaptiveWeights);

        // ====================== INTERNALS ======================
        private static void ApplyRules(Destination dest, Dictionary<string, string> answers)
        {
            // 1. Region
            if (TryAnswer(answers, "Region", out var region) && region != "Không quan trọng" && dest.Region == region)
            {
                AddMatch(dest, "Region", Weight(dest, "Region"), "Cùng vùng bạn ưu tiên");
            }

            // 2. Beach / Mountain
            if (TryAnswer(answers, "IsCoastalArea", out var coast) && coast == "Có" && dest.HasBeach)
            {
                AddMatch(dest, "IsCoastalArea", Weight(dest, "IsCoastalArea"), "Có biển/đảo đúng sở thích");
            }
            if (TryAnswer(answers, "IsMountainArea", out var mountain) && mountain == "Có" && dest.HasMountain)
            {
                AddMatch(dest, "IsMountainArea", Weight(dest, "IsMountainArea"), "Có núi/trekking như bạn chọn");
            }

            // 3. Culture
            if (TryAnswer(answers, "LikeHistoricalSites", out var hist) && hist == "Có" && dest.HasCulture)
            {
                AddMatch(dest, "LikeHistoricalSites", Weight(dest, "LikeHistoricalSites"), "Nhiều di tích văn hóa/lịch sử");
            }
            if (TryAnswer(answers, "LikeFestival", out var fest) && fest == "Có" && dest.HasCulture)
            {
                AddMatch(dest, "LikeFestival", Weight(dest, "LikeFestival"), "Có lễ hội/hoạt động văn hóa địa phương");
            }
            if (TryAnswer(answers, "LikeEthnicCulture", out var ethnic) && ethnic == "Có" &&
                (dest.Region == "Tây Nguyên" || dest.Region == "Bắc"))
            {
                AddMatch(dest, "LikeEthnicCulture", Weight(dest, "LikeEthnicCulture"), "Trải nghiệm văn hóa dân tộc đặc sắc");
            }

            // 4. Food
            if (TryAnswer(answers, "LikeSeafood", out var seafood) && seafood == "Có" && dest.HasBeach)
            {
                AddMatch(dest, "LikeSeafood", Weight(dest, "LikeSeafood"), "Ẩm thực hải sản phong phú");
            }
            if (TryAnswer(answers, "LikeSpicyFood", out var spicy) && spicy == "Có" && dest.Region == "Trung")
            {
                AddMatch(dest, "LikeSpicyFood", Weight(dest, "LikeSpicyFood"), "Ẩm thực cay đậm vị miền Trung");
            }
            if (TryAnswer(answers, "ExpectedMood", out var mood) && mood == "Ẩm thực" && dest.HasFood)
            {
                AddMatch(dest, "ExpectedMood", Weight(dest, "ExpectedMood"), "Điểm mạnh về trải nghiệm ẩm thực");
            }

            // 5. Lifestyle
            if (TryAnswer(answers, "PreferPeacefulLife", out var peace) && peace == "Có" && dest.IsPeaceful)
            {
                AddMatch(dest, "PreferPeacefulLife", Weight(dest, "PreferPeacefulLife"), "Không khí yên bình, chậm rãi");
            }
            if (TryAnswer(answers, "PreferDynamicLife", out var dynamicLife) && dynamicLife == "Có" && dest.IsModernCity)
            {
                AddMatch(dest, "PreferDynamicLife", Weight(dest, "PreferDynamicLife"), "Sôi động/hiện đại đúng mong muốn");
            }

            // 6. Tour type
            if (TryAnswer(answers, "LikeNatureTour", out var nature) && nature == "Có" && dest.HasNature)
            {
                AddMatch(dest, "LikeNatureTour", Weight(dest, "LikeNatureTour"), "Thiên nhiên/trekking nổi bật");
            }
            if (TryAnswer(answers, "LikeCityTour", out var city) && city == "Có" && dest.IsModernCity)
            {
                AddMatch(dest, "LikeCityTour", Weight(dest, "LikeCityTour"), "City/resort đúng sở thích");
            }

            // 7. Budget
            if (TryAnswer(answers, "BudgetLevel", out var budget))
            {
                var cost = EstimateCostPerDay(dest);
                dest.BudgetNote = $"Ước tính {cost:0} USD/ngày";
                if (BudgetMatch(cost, budget))
                {
                    AddMatch(dest, "BudgetLevel", Weight(dest, "BudgetLevel"), $"Phù hợp mức chi '{budget}'");
                }
                else
                {
                    dest.Score = Math.Max(0, dest.Score - 1);
                }
            }

            // 8. Season
            if (TryAnswer(answers, "TravelSeason", out var season))
            {
                dest.SeasonRecommendation = SeasonNote(dest, season);
                if (SeasonMatch(dest, season))
                {
                    AddMatch(dest, "TravelSeason", Weight(dest, "TravelSeason"), $"Đẹp vào mùa {season}");
                }
            }

            // 9. Duration & pace
            if (TryAnswer(answers, "TravelDurationDays", out var daysStr))
            {
                var normalizedDays = daysStr.Replace("+", string.Empty);
                if (int.TryParse(normalizedDays, out var days))
                {
                    dest.DurationNote = DurationNote(dest, days);
                    if (days >= 5 && dest.HasNature)
                        AddMatch(dest, "TravelDurationDays", Weight(dest, "TravelDurationDays") * 0.6, "Nhiều ngày, hợp trekking/thiên nhiên");
                    if (days <= 3 && dest.IsModernCity)
                        AddMatch(dest, "TravelDurationDays", Weight(dest, "TravelDurationDays") * 0.6, "Trip ngắn, hợp city tour/ẩm thực");
                }
            }

            if (TryAnswer(answers, "TravelPace", out var pace))
            {
                if (pace == "Chậm rãi" && dest.IsPeaceful)
                    AddMatch(dest, "TravelPace", Weight(dest, "TravelPace") * 0.5, "Nhịp đi chậm, nơi yên bình");
                if (pace == "Dày đặc" && dest.IsModernCity)
                    AddMatch(dest, "TravelPace", Weight(dest, "TravelPace") * 0.5, "Lịch dày, thành phố nhiều hoạt động");
            }
        }

        private static void AddMatch(Destination dest, string ruleKey, double score, string explanation)
        {
            dest.Score += (int)Math.Round(score);
            dest.RuleMatches.Add(new RuleMatch
            {
                RuleKey = ruleKey,
                Score = Math.Round(score, 1),
                Explanation = explanation
            });
            if (!dest.MatchedRules.Contains(ruleKey))
                dest.MatchedRules.Add(ruleKey);
        }

        private static void ResetDestination(Destination dest)
        {
            dest.Score = 0;
            dest.NormalizedScore = 0;
            dest.MatchedRules.Clear();
            dest.RuleMatches.Clear();
            dest.SeasonRecommendation = string.Empty;
            dest.BudgetNote = string.Empty;
            dest.DurationNote = string.Empty;
        }

        private static bool TryAnswer(Dictionary<string, string> answers, string key, out string value)
        {
            return answers.TryGetValue(key, out value) && !string.IsNullOrWhiteSpace(value);
        }

        private static double Weight(Destination dest, string key)
        {
            var baseWeight = AdaptiveWeights.TryGetValue(key, out var w) ? w : 1;
            if (dest.HasBeach && dest.HasMountain)
            {
                baseWeight += 0.2;
            }
            return baseWeight * 10;
        }

        private static double EstimateCostPerDay(Destination dest)
        {
            double baseCost = 70;
            if (dest.HasBeach) baseCost += 10;
            if (dest.IsModernCity) baseCost += 15;
            if (dest.Region == "Nam") baseCost += 5;
            if (dest.Region == "Bắc") baseCost -= 2;
            if (dest.Region == "Tây Nguyên") baseCost -= 5;
            return baseCost;
        }

        private static bool BudgetMatch(double cost, string budgetLevel)
        {
            return budgetLevel switch
            {
                "Tiết kiệm" => cost <= 80,
                "Vừa phải" => cost > 60 && cost <= 120,
                "Cao cấp" => cost > 90,
                _ => true
            };
        }

        private static bool SeasonMatch(Destination dest, string season)
        {
            if (season == "Không quan trọng" || string.IsNullOrWhiteSpace(season)) return true;

            return dest.Region switch
            {
                "Bắc" => season is "Xuân" or "Thu",
                "Trung" => season is "Hè" or "Thu",
                "Nam" => season is "Đông" or "Xuân",
                "Tây Nguyên" => season is "Thu" or "Đông",
                _ => true
            };
        }

        private static string SeasonNote(Destination dest, string season)
        {
            if (string.IsNullOrWhiteSpace(season) || season == "Không quan trọng")
                return "Mùa nào cũng ổn";

            var best = dest.Region switch
            {
                "Bắc" => "Xuân/Thu là đẹp nhất, tránh nồm tháng 3",
                "Trung" => "Hè/Thu để tránh mưa bão cuối năm",
                "Nam" => "Đông/Xuân khô ráo, ít mưa",
                "Tây Nguyên" => "Thu/Đông săn mây đẹp",
                _ => "Linh hoạt theo lịch"
            };

            return $"Bạn chọn mùa {season}. Gợi ý: {best}";
        }

        private static string DurationNote(Destination dest, int days)
        {
            if (days >= 6 && dest.HasNature) return "Lịch trình dài, phù hợp trekking và khám phá thiên nhiên";
            if (days <= 3 && dest.IsModernCity) return "Trip ngắn, đi city tour và ẩm thực hợp lý";
            return "Có thể cân bằng giữa tham quan và nghỉ dưỡng";
        }
    }
}

