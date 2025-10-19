using System.Collections.Generic;

namespace TravelWeb.Models
{
    public class Destination
    {
        public string Name { get; set; }
        public bool HasBeach { get; set; }
        public bool HasMountain { get; set; }
        public bool HasCulture { get; set; }
        public bool HasFood { get; set; }
        public string Region { get; set; }   // Bắc / Trung / Nam / Tây Nguyên
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Score { get; set; } = 0;
    }

    public static class DestinationData
    {
        public static List<Destination> GetAll()
        {
            return new List<Destination>
            {
                // ====== MIỀN BẮC ======
                new Destination { Name = "Hà Nội", HasBeach = false, HasMountain = false, HasCulture = true, HasFood = true, Region = "Bắc", Description = "Thủ đô nghìn năm văn hiến với phố cổ, hồ Gươm và ẩm thực phong phú.", ImageUrl = "/images/hanoi.jpg" },
                new Destination { Name = "Hải Phòng - Hải Dương", HasBeach = true, HasMountain = false, HasCulture = true, HasFood = true, Region = "Bắc", Description = "Thành phố cảng năng động, nổi tiếng với đồ biển và bánh đa cua.", ImageUrl = "/images/haiphong.jpg" },
                new Destination { Name = "Quảng Ninh", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Bắc", Description = "Nổi tiếng với Vịnh Hạ Long – di sản thiên nhiên thế giới.", ImageUrl = "/images/quangninh.jpg" },
                new Destination { Name = "Lào Cai", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = true, Region = "Bắc", Description = "Vùng núi Tây Bắc với Sapa, Fansipan và chợ tình.", ImageUrl = "/images/laocai.jpg" },
                new Destination { Name = "Hà Giang", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = false, Region = "Bắc", Description = "Cao nguyên đá Đồng Văn hùng vĩ, vùng đất của người Mông.", ImageUrl = "/images/hagiang.jpg" },
                new Destination { Name = "Hưng Yên - Thái Bình", HasBeach = false, HasMountain = false, HasCulture = true, HasFood = true, Region = "Bắc", Description = "Vùng đồng bằng Bắc Bộ, nổi tiếng nhãn lồng và bánh cáy.", ImageUrl = "/images/hungyen.jpg" },
                new Destination { Name = "Ninh Bình", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = true, Region = "Bắc", Description = "Tràng An, Tam Cốc – di sản kép UNESCO, cảnh non nước hữu tình.", ImageUrl = "/images/ninhbinh.jpg" },
                new Destination { Name = "Thanh Hóa", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Bắc", Description = "Biển Sầm Sơn và đặc sản nem chua nổi tiếng.", ImageUrl = "/images/thanhhoa.jpg" },

                // ====== MIỀN TRUNG ======
                new Destination { Name = "Nghệ An", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Trung", Description = "Quê hương Bác Hồ, có biển Cửa Lò và núi rừng Pù Mát.", ImageUrl = "/images/nghean.jpg" },
                new Destination { Name = "Hà Tĩnh", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = false, Region = "Trung", Description = "Miền đất kiên trung, có chùa Hương Tích và biển Thiên Cầm.", ImageUrl = "/images/hatinh.jpg" },
                new Destination { Name = "Quảng Bình", HasBeach = true, HasMountain = true, HasCulture = false, HasFood = true, Region = "Trung", Description = "Vườn quốc gia Phong Nha – Kẻ Bàng, kỳ quan hang động.", ImageUrl = "/images/quangbinh.jpg" },
                new Destination { Name = "Huế", HasBeach = false, HasMountain = false, HasCulture = true, HasFood = true, Region = "Trung", Description = "Cố đô Huế – di sản văn hóa thế giới, ẩm thực cung đình.", ImageUrl = "/images/hue.jpg" },
                new Destination { Name = "Đà Nẵng", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Trung", Description = "Thành phố đáng sống, có biển Mỹ Khê và Bà Nà Hills.", ImageUrl = "/images/danang.jpg" },
                new Destination { Name = "Quảng Nam", HasBeach = true, HasMountain = false, HasCulture = true, HasFood = true, Region = "Trung", Description = "Phố cổ Hội An, Mỹ Sơn – di sản văn hóa thế giới.", ImageUrl = "/images/quangnam.jpg" },
                new Destination { Name = "Quảng Ngãi", HasBeach = true, HasMountain = true, HasCulture = false, HasFood = true, Region = "Trung", Description = "Đảo Lý Sơn – thiên đường biển đảo miền Trung.", ImageUrl = "/images/quangngai.jpg" },
                new Destination { Name = "Bình Định", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Trung", Description = "Xứ Nẫu hiền hòa, quê hương Tây Sơn, nổi tiếng Eo Gió.", ImageUrl = "/images/binhdinh.jpg" },
                new Destination { Name = "Phú Yên", HasBeach = true, HasMountain = true, HasCulture = false, HasFood = true, Region = "Trung", Description = "Xứ hoa vàng cỏ xanh, Ghềnh Đá Đĩa kỳ thú.", ImageUrl = "/images/phuyen.jpg" },
                new Destination { Name = "Khánh Hòa", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Trung", Description = "Nha Trang – trung tâm du lịch biển lớn nhất Việt Nam.", ImageUrl = "/images/nhatrang.jpg" },
                new Destination { Name = "Ninh Thuận", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Trung", Description = "Nắng gió, tháp Chàm và vườn nho Ba Mọi.", ImageUrl = "/images/ninhthuan.jpg" },
                new Destination { Name = "Bình Thuận", HasBeach = true, HasMountain = false, HasCulture = true, HasFood = true, Region = "Trung", Description = "Phan Thiết, Mũi Né – thiên đường nghỉ dưỡng biển.", ImageUrl = "/images/binhthuan.jpg" },

                // ====== TÂY NGUYÊN ======
                new Destination { Name = "Kon Tum", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = false, Region = "Tây Nguyên", Description = "Thủ phủ cà phê, nhà rông, văn hóa Tây Nguyên đặc sắc.", ImageUrl = "/images/kontum.jpg" },
                new Destination { Name = "Gia Lai", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = true, Region = "Tây Nguyên", Description = "Biển Hồ Pleiku, cảnh đẹp hùng vĩ của cao nguyên bazan.", ImageUrl = "/images/gialai.jpg" },
                new Destination { Name = "Đắk Lắk", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = true, Region = "Tây Nguyên", Description = "Buôn Ma Thuột, quê hương cà phê Việt Nam.", ImageUrl = "/images/daklak.jpg" },
                new Destination { Name = "Đắk Nông", HasBeach = false, HasMountain = true, HasCulture = false, HasFood = true, Region = "Tây Nguyên", Description = "Thác nước Dray Nur hùng vĩ và công viên địa chất UNESCO.", ImageUrl = "/images/daknong.jpg" },
                new Destination { Name = "Lâm Đồng", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = true, Region = "Tây Nguyên", Description = "Đà Lạt – thành phố sương mù, hoa và cà phê.", ImageUrl = "/images/dalat.jpg" },

                // ====== MIỀN NAM ======
                new Destination { Name = "TP.HCM - Bình Dương - Bà Rịa", HasBeach = true, HasMountain = false, HasCulture = true, HasFood = true, Region = "Nam", Description = "Vùng đô thị lớn nhất, có Vũng Tàu, ẩm thực đa dạng.", ImageUrl = "/images/hcm.jpg" },
                new Destination { Name = "Đồng Nai - Bình Phước", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = true, Region = "Nam", Description = "Vùng công nghiệp và du lịch sinh thái núi Chứa Chan.", ImageUrl = "/images/dongnai.jpg" },
                new Destination { Name = "Tây Ninh", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = false, Region = "Nam", Description = "Núi Bà Đen, trung tâm đạo Cao Đài đặc sắc.", ImageUrl = "/images/tayninh.jpg" },
                new Destination { Name = "Long An", HasBeach = false, HasMountain = false, HasCulture = false, HasFood = true, Region = "Nam", Description = "Cửa ngõ miền Tây, sông nước yên bình và đặc sản mắm.", ImageUrl = "/images/longan.jpg" },
                new Destination { Name = "Tiền Giang", HasBeach = false, HasMountain = false, HasCulture = true, HasFood = true, Region = "Nam", Description = "Du lịch miệt vườn Cái Bè, chợ nổi Cái Bè, trái cây tươi ngon.", ImageUrl = "/images/tiengiang.jpg" },
                new Destination { Name = "Vĩnh Long", HasBeach = false, HasMountain = false, HasCulture = true, HasFood = true, Region = "Nam", Description = "Miệt vườn sông nước, quê hương nghệ sĩ Nam Bộ.", ImageUrl = "/images/vinhlong.jpg" },
                new Destination { Name = "Cần Thơ", HasBeach = false, HasMountain = false, HasCulture = true, HasFood = true, Region = "Nam", Description = "Tây Đô, có chợ nổi Cái Răng và bến Ninh Kiều.", ImageUrl = "/images/cantho.jpg" },
                new Destination { Name = "An Giang", HasBeach = false, HasMountain = true, HasCulture = true, HasFood = true, Region = "Nam", Description = "Núi Sam, Bà Chúa Xứ, vùng đất tâm linh miền Tây.", ImageUrl = "/images/angiang.jpg" },
                new Destination { Name = "Kiên Giang", HasBeach = true, HasMountain = true, HasCulture = true, HasFood = true, Region = "Nam", Description = "Phú Quốc – đảo ngọc và Rạch Giá biển trời rộng lớn.", ImageUrl = "/images/kiengiang.jpg" },
                new Destination { Name = "Cà Mau", HasBeach = true, HasMountain = false, HasCulture = true, HasFood = true, Region = "Nam", Description = "Đất mũi cực Nam Tổ quốc, rừng ngập mặn và hải sản tươi.", ImageUrl = "/images/camau.jpg" }
            };
        }
    }
}
