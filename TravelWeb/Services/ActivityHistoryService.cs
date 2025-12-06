/*using Microsoft.EntityFrameworkCore;
using TravelWeb.Data;
using TravelWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWeb.Services
{
    public static class ActivityHistoryService
    {
        /// <summary>
        /// Log một hoạt động của user vào lịch sử
        /// </summary>
        public static async Task LogActivityAsync(
            TravelContext context,
            int userId,
            string activityType,
            string? itemId = null,
            string? itemTitle = null,
            string? description = null,
            string? location = null,
            string? metadata = null)
        {
            var activity = new ActivityHistory
            {
                UserId = userId,
                ActivityType = activityType,
                ItemId = itemId,
                ItemTitle = itemTitle,
                Description = description,
                Location = location,
                Metadata = metadata,
                CreatedAt = DateTime.UtcNow
            };

            context.ActivityHistories.Add(activity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Lấy lịch sử hoạt động của user
        /// </summary>
        public static async Task<List<ActivityHistory>> GetUserActivitiesAsync(
            TravelContext context,
            int userId,
            int? limit = null)
        {
            var query = context.ActivityHistories
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt);

            if (limit.HasValue)
            {
                query = (IOrderedQueryable<ActivityHistory>)query.Take(limit.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Lấy lịch sử hoạt động theo loại
        /// </summary>
        public static async Task<List<ActivityHistory>> GetUserActivitiesByTypeAsync(
            TravelContext context,
            int userId,
            string activityType,
            int? limit = null)
        {
            var query = context.ActivityHistories
                .Where(a => a.UserId == userId && a.ActivityType == activityType)
                .OrderByDescending(a => a.CreatedAt);

            if (limit.HasValue)
            {
                query = (IOrderedQueryable<ActivityHistory>)query.Take(limit.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Đếm số lượng hoạt động của user
        /// </summary>
        public static async Task<int> GetUserActivityCountAsync(
            TravelContext context,
            int userId,
            string? activityType = null)
        {
            var query = context.ActivityHistories.Where(a => a.UserId == userId);

            if (!string.IsNullOrEmpty(activityType))
            {
                query = query.Where(a => a.ActivityType == activityType);
            }

            return await query.CountAsync();
        }
    }
}


*/