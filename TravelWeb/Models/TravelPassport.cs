using System;
using System.Collections.Generic;

namespace TravelWeb.Models
{
    public class TravelPassport
    {
        public string UserId { get; set; }           // ID người dùng (hoặc session)
        public string UserName { get; set; }         // Tên hiển thị
        public int TotalPoints { get; set; }         // Tổng điểm hiện tại
        public int TravelLevel { get; set; }         // Cấp độ du lịch
        public List<PassportStamp> Stamps { get; set; } = new(); // Dấu mộc du lịch

        public void AddStamp(PassportStamp stamp)
        {
            Stamps.Add(stamp);
            TotalPoints += stamp.Points;
            UpdateLevel();
        }

        private void UpdateLevel()
        {
            if (TotalPoints < 50) TravelLevel = 1;
            else if (TotalPoints < 100) TravelLevel = 2;
            else if (TotalPoints < 200) TravelLevel = 3;
            else TravelLevel = 4;
        }
    }

    public class PassportStamp
    {
        public string Province { get; set; }        // Địa phương
        public string EventName { get; set; }       // Lễ hội / chuyến đi
        public DateTime DateEarned { get; set; }    // Ngày nhận
        public int Points { get; set; }             // Điểm thưởng
        public string BadgeIcon { get; set; }       // Hình huy hiệu
    }
}
