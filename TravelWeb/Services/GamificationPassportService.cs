using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Services
{
    public enum PassportStampType
    {
        Festival,
        CuisineReview,
        TripPlan
    }

    public class PassportStamp
    {
        public PassportStampType Type { get; set; }
        public string? Meta { get; set; } // province or title
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class PassportState
    {
        public string Username { get; set; } = "guest";
        public List<PassportStamp> Stamps { get; set; } = new List<PassportStamp>();
        public HashSet<string> Badges { get; set; } = new HashSet<string>();

        public int TotalStamps => Stamps.Count;
        public int FestivalCount => Stamps.Count(s => s.Type == PassportStampType.Festival);
        public int CuisineReviewCount => Stamps.Count(s => s.Type == PassportStampType.CuisineReview);
        public int TripPlanCount => Stamps.Count(s => s.Type == PassportStampType.TripPlan);
        public int ProvincesExplored => Stamps.Where(s => !string.IsNullOrWhiteSpace(s.Meta)).Select(s => s.Meta).Distinct().Count();
    }

    public static class GamificationPassportService
    {
        private static readonly ConcurrentDictionary<string, PassportState> UserToPassport = new ConcurrentDictionary<string, PassportState>();

        public static PassportState GetOrCreate(string username)
        {
            username = string.IsNullOrWhiteSpace(username) ? "guest" : username;
            return UserToPassport.GetOrAdd(username, u => new PassportState { Username = u });
        }

        public static PassportState AddStamp(string username, PassportStampType type, string? meta = null)
        {
            var p = GetOrCreate(username);
            p.Stamps.Add(new PassportStamp { Type = type, Meta = meta });
            EvaluateBadges(p);
            return p;
        }

        private static void EvaluateBadges(PassportState p)
        {
            if (p.CuisineReviewCount >= 3) p.Badges.Add("Food Explorer");
            if (p.TripPlanCount >= 3) p.Badges.Add("Trip Architect");
            if (p.FestivalCount >= 2) p.Badges.Add("Festival Hopper");
            if (p.TotalStamps >= 5) p.Badges.Add("Travel Master");
        }

        public static List<PassportState> GetLeaderboard(int top = 10)
        {
            return UserToPassport.Values
                .OrderByDescending(p => p.TotalStamps)
                .ThenByDescending(p => p.ProvincesExplored)
                .ThenByDescending(p => p.CuisineReviewCount)
                .Take(top)
                .ToList();
        }
    }
}


