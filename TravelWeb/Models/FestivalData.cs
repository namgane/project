    using System;
using System.Collections.Generic;

namespace TravelWeb.Models
{
    public static class FestivalData
    {
        public static List<Festival> GetAll()
        {
            return new List<Festival>
            {
                // === Tháng 1 - 2 (Đầu năm) ===
                new Festival {
                    Name = "Lễ hội Chùa Hương",
                    Province = "Hà Nội",
                    Region = "Miền Bắc",
                    StartDate = new DateTime(DateTime.Now.Year, 1, 27),
                    EndDate = new DateTime(DateTime.Now.Year, 3, 15),
                    Description = "Lễ hội tâm linh lớn nhất miền Bắc, hành hương về đất Phật.",
                    Highlight = "Tín ngưỡng, du lịch tâm linh, thắng cảnh Hương Sơn.",
                    ImageUrl = "/images/festivals/chua-huong.jpg",
                    ImageUrls = new List<string> {
                        "/images/festivals/chuahuongp2.webp", 
                        "/images/festivals/chuahuongp1.webp",
                        "/images/festivals/chuahuongp3.webp"
                    }
                },
                new Festival {
                    Name = "Lễ hội Lim",
                    Province = "Bắc Ninh",
                    Region = "Miền Bắc",
                    StartDate = new DateTime(DateTime.Now.Year, 2, 12),
                    EndDate = new DateTime(DateTime.Now.Year, 2, 14),
                    Description = "Lễ hội quan họ đặc sắc của vùng Kinh Bắc.",
                    Highlight = "Hát đối quan họ, trò chơi dân gian, mặc áo tứ thân.",
                    ImageUrl = "/images/festivals/hoilimp1.webp",
                    ImageUrls = new List<string> { 
                        "/images/festivals/hoilimp1.webp",
                        "/images/festivals/hoilimp2.webp",
                        "/images/festivals/hoilimp3.webp"
                    }
                },
                new Festival {
                    Name = "Lễ hội Đền Trần",
                    Province = "Nam Định",
                    Region = "Miền Bắc",
                    StartDate = new DateTime(DateTime.Now.Year, 2, 15),
                    EndDate = new DateTime(DateTime.Now.Year, 2, 17),
                    Description = "Tưởng nhớ công đức các vua Trần, cầu may mắn đầu năm.",
                    Highlight = "Lễ khai ấn, nghi thức rước nước, tế cá.",
                    ImageUrl = "/images/festivals/hoidentranp1.webp",
                    ImageUrls = new List<string> {
                        "/images/festivals/hoidentranp1.webp",
                        "/images/festivals/hoidentranp2.webp",
                        "/images/festivals/hoidentranp3.webp"
                    }
                },

                // === Tháng 3 - 4 ===
                new Festival {
                    Name = "Lễ hội Cà phê Buôn Ma Thuột",
                    Province = "Đắk Lắk",
                    Region = "Tây Nguyên",
                    StartDate = new DateTime(DateTime.Now.Year, 3, 10),
                    EndDate = new DateTime(DateTime.Now.Year, 3, 15),
                    Description = "Tôn vinh cây cà phê Việt Nam và văn hóa Tây Nguyên.",
                    Highlight = "Lễ diễu hành, hội chợ cà phê, biểu diễn cồng chiêng.",
                    ImageUrl = "/images/festivals/caphep1.webp",
                    ImageUrls = new List<string> {
                        "/images/festivals/caphep1.webp",
                        "/images/festivals/caphep2.webp",
                        "/images/festivals/caphep3.webp"
                    }
                },
                new Festival {
                    Name = "Lễ hội Đền Hùng (Giỗ Tổ Hùng Vương)",
                    Province = "Phú Thọ",
                    Region = "Miền Bắc",
                    StartDate = new DateTime(DateTime.Now.Year, 4, 18),
                    EndDate = new DateTime(DateTime.Now.Year, 4, 18),
                    Description = "Tưởng nhớ công ơn các vua Hùng dựng nước.",
                    Highlight = "Tín ngưỡng dân gian, nghi lễ quốc gia, rước kiệu vua Hùng.",
                    ImageUrl = "/images/festivals/denhungp1.webp",
                    ImageUrls = new List<string> {
                        "/images/festivals/denhungp1.webp",
                        "/images/festivals/denhungp1.webp",
                        "/images/festivals/denhungp1.webp"
                    }
                },
                new Festival {
                    Name = "Lễ hội Đua voi Tây Nguyên",
                    Province = "Đắk Lắk",
                    Region = "Tây Nguyên",
                    StartDate = new DateTime(DateTime.Now.Year, 3, 12),
                    EndDate = new DateTime(DateTime.Now.Year, 3, 13),
                    Description = "Lễ hội đua voi độc đáo tại Buôn Đôn.",
                    Highlight = "Đua voi, cưỡi voi, văn hóa Tây Nguyên.",
                    ImageUrl = "/images/festivals/voip1.webp",
                    ImageUrls = new List<string> {
                        "/images/festivals/voip1.webp",
                        "/images/festivals/voip2.webp",
                        "/images/festivals/voip3.webp"
                    }
                },

                // === Tháng 5 - 6 ===
                new Festival {
                    Name = "Festival Huế",
                    Province = "Thừa Thiên Huế",
                    Region = "Miền Trung",
                    StartDate = new DateTime(DateTime.Now.Year, 6, 10),
                    EndDate = new DateTime(DateTime.Now.Year, 6, 17),
                    Description = "Lễ hội văn hóa - nghệ thuật quốc tế tôn vinh di sản Cố đô.",
                    Highlight = "Trình diễn áo dài, ẩm thực, nghệ thuật truyền thống.",
                    ImageUrl = "/images/festivals/festival-hue.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/festival-hue.jpg", 
                        "/images/festivals/festival-hue-2.jpg", 
                        "/images/festivals/festival-hue-3.jpg" 
                    }
                },
                new Festival {
                    Name = "Lễ hội Pháo hoa Quốc tế Đà Nẵng",
                    Province = "Đà Nẵng",
                    Region = "Miền Trung",
                    StartDate = new DateTime(DateTime.Now.Year, 6, 1),
                    EndDate = new DateTime(DateTime.Now.Year, 7, 1),
                    Description = "Sự kiện nghệ thuật quốc tế quy mô lớn bên sông Hàn.",
                    Highlight = "Trình diễn pháo hoa, âm nhạc, du lịch biển.",
                    ImageUrl = "/images/festivals/phao-hoa-danang.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/phao-hoa-danang.jpg", 
                        "/images/festivals/phao-hoa-danang-2.jpg", 
                        "/images/festivals/phao-hoa-danang-3.jpg" 
                    }
                },

                // === Tháng 7 - 8 ===
                new Festival {
                    Name = "Lễ hội Katê",
                    Province = "Ninh Thuận",
                    Region = "Miền Trung",
                    StartDate = new DateTime(DateTime.Now.Year, 7, 28),
                    EndDate = new DateTime(DateTime.Now.Year, 7, 29),
                    Description = "Lễ hội truyền thống của người Chăm Bà-la-môn.",
                    Highlight = "Múa hát dân tộc Chăm, rước thần Po Nagar.",
                    ImageUrl = "/images/festivals/kate.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/kate.jpg", 
                        "/images/festivals/kate-2.jpg", 
                        "/images/festivals/kate-3.jpg" 
                    }
                },
                new Festival {
                    Name = "Lễ hội Vu Lan Báo Hiếu",
                    Province = "TP.HCM",
                    Region = "Miền Nam",
                    StartDate = new DateTime(DateTime.Now.Year, 8, 15),
                    EndDate = new DateTime(DateTime.Now.Year, 8, 15),
                    Description = "Lễ hội tri ân cha mẹ, cầu an, phóng sinh, thả đèn hoa đăng.",
                    Highlight = "Nghi thức Phật giáo, tặng hoa hồng cho cha mẹ.",
                    ImageUrl = "/images/festivals/vu-lan.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/vu-lan.jpg", 
                        "/images/festivals/vu-lan-2.jpg", 
                        "/images/festivals/vu-lan-3.jpg" 
                    }
                },

                // === Tháng 9 - 10 ===
                new Festival {
                    Name = "Lễ hội Nghinh Ông Cần Giờ",
                    Province = "TP.HCM",
                    Region = "Miền Nam",
                    StartDate = new DateTime(DateTime.Now.Year, 9, 16),
                    EndDate = new DateTime(DateTime.Now.Year, 9, 18),
                    Description = "Lễ hội cầu ngư, tưởng nhớ cá Ông của ngư dân Nam Bộ.",
                    Highlight = "Rước Ông, đua ghe, ẩm thực hải sản.",
                    ImageUrl = "/images/festivals/nghinh-ong.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/nghinh-ong.jpg", 
                        "/images/festivals/nghinh-ong-2.jpg", 
                        "/images/festivals/nghinh-ong-3.jpg" 
                    }
                },
                new Festival {
                    Name = "Lễ hội Oóc Om Bóc - Đua ghe Ngo",
                    Province = "Sóc Trăng",
                    Region = "Miền Tây Nam Bộ",
                    StartDate = new DateTime(DateTime.Now.Year, 11, 15),
                    EndDate = new DateTime(DateTime.Now.Year, 11, 17),
                    Description = "Lễ hội lớn của người Khmer Nam Bộ.",
                    Highlight = "Đua ghe ngo, múa dân tộc, ẩm thực Khmer.",
                    ImageUrl = "/images/festivals/dua-ghe-ngo.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/dua-ghe-ngo.jpg", 
                        "/images/festivals/dua-ghe-ngo-2.jpg", 
                        "/images/festivals/dua-ghe-ngo-3.jpg" 
                    }
                },
                new Festival {
                    Name = "Lễ hội Ok Om Bok Trà Vinh",
                    Province = "Trà Vinh",
                    Region = "Miền Tây Nam Bộ",
                    StartDate = new DateTime(DateTime.Now.Year, 11, 14),
                    EndDate = new DateTime(DateTime.Now.Year, 11, 16),
                    Description = "Tạ ơn thần Mặt Trăng phù hộ mùa màng bội thu.",
                    Highlight = "Đua ghe ngo, đèn nước, âm nhạc Khmer.",
                    ImageUrl = "/images/festivals/ok-om-bok.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/ok-om-bok.jpg", 
                        "/images/festivals/ok-om-bok-2.jpg", 
                        "/images/festivals/ok-om-bok-3.jpg" 
                    }
                },

                // === Cuối năm ===
                new Festival {
                    Name = "Lễ hội Hoa Đà Lạt",
                    Province = "Lâm Đồng",
                    Region = "Tây Nguyên",
                    StartDate = new DateTime(DateTime.Now.Year, 12, 20),
                    EndDate = new DateTime(DateTime.Now.Year, 12, 30),
                    Description = "Lễ hội tôn vinh vẻ đẹp hoa Đà Lạt và du lịch xanh.",
                    Highlight = "Triển lãm hoa, diễu hành xe hoa, check-in Hồ Xuân Hương.",
                    ImageUrl = "/images/festivals/hoa-da-lat.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/hoa-da-lat.jpg", 
                        "/images/festivals/hoa-da-lat-2.jpg", 
                        "/images/festivals/hoa-da-lat-3.jpg" 
                    }
                },
                new Festival {
                    Name = "Countdown Sài Gòn",
                    Province = "TP.HCM",
                    Region = "Miền Nam",
                    StartDate = new DateTime(DateTime.Now.Year, 12, 31),
                    EndDate = new DateTime(DateTime.Now.Year + 1, 1, 1),
                    Description = "Đại tiệc âm nhạc đón năm mới tại phố đi bộ Nguyễn Huệ.",
                    Highlight = "Pháo hoa, âm nhạc, ẩm thực đường phố.",
                    ImageUrl = "/images/festivals/countdown-sg.jpg",
                    ImageUrls = new List<string> { 
                        "/images/festivals/countdown-sg.jpg", 
                        "/images/festivals/countdown-sg-2.jpg", 
                        "/images/festivals/countdown-sg-3.jpg" 
                    }
                }
            };
        }
    }
}
