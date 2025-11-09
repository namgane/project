using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TravelWeb.Models;

namespace TravelWeb.Services
{
    public static class FeedbackStore
    {
        private static readonly ConcurrentBag<Feedback> AllFeedback = new ConcurrentBag<Feedback>();

        public static void Add(Feedback fb)
        {
            AllFeedback.Add(fb);
        }

        public static List<Feedback> GetAll()
        {
            return AllFeedback.OrderByDescending(f => f.CreatedAt).ToList();
        }
    }
}


