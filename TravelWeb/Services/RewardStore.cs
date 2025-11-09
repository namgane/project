using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Services
{
    public class RewardRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = "guest";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
    }

    public class Discount
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMonths(1);
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class RewardStore
    {
        private static readonly ConcurrentBag<RewardRequest> Requests = new ConcurrentBag<RewardRequest>();
        private static readonly ConcurrentBag<Discount> Discounts = new ConcurrentBag<Discount>();

        public static RewardRequest AddRequest(string username)
        {
            var r = new RewardRequest { Username = username };
            Requests.Add(r);
            return r;
        }

        public static List<RewardRequest> GetAllRequests() => Requests.OrderByDescending(r => r.CreatedAt).ToList();

        public static void Approve(Guid requestId, string code, string description, DateTime expiresAt)
        {
            var req = Requests.FirstOrDefault(r => r.Id == requestId);
            if (req != null)
            {
                req.Status = "Approved";
                Discounts.Add(new Discount
                {
                    Username = req.Username,
                    Code = code,
                    Description = description,
                    ExpiresAt = expiresAt
                });
            }
        }

        public static void Reject(Guid requestId)
        {
            var req = Requests.FirstOrDefault(r => r.Id == requestId);
            if (req != null) req.Status = "Rejected";
        }

        public static List<Discount> GetByUser(string username)
        {
            return Discounts.Where(d => d.Username == username).OrderByDescending(d => d.CreatedAt).ToList();
        }
    }
}


