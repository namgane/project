using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public class Destination
    {
        // Thông tin cơ bản
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;   // Bắc / Trung / Nam / Tây Nguyên
        public string ImageUrl { get; set; } = string.Empty;

        // Tọa độ địa điểm chính
        public double Latitude { get; set; }  // Vĩ độ
        public double Longitude { get; set; } // Kinh độ

        // Đặc điểm địa điểm (cho Quiz)
        public bool HasBeach { get; set; } = false;
        public bool HasMountain { get; set; } = false;
        public bool HasCulture { get; set; } = false;
        public bool HasFood { get; set; } = false;

        // Điểm số (cho Quiz)
        public int Score { get; set; } = 0;

        // Danh sách các điểm tham quan
        public List<AttractionPoint> Attractions { get; set; } = new List<AttractionPoint>();
    }

    public class AttractionPoint
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Type { get; set; } = string.Empty; // "Cultural", "Nature", "Entertainment", "Shopping", "Food"
        public int VisitDuration { get; set; } // Thời gian tham quan (phút)
        public decimal EntranceFee { get; set; } // Phí vào cửa
    }

    public static class DestinationData
    {
        private static readonly List<Destination> Destinations = new List<Destination>
        {
            // ====== MIỀN BẮC ======
            new Destination
            {
                Name = "Hà Nội",
                Description = "Thủ đô nghìn năm văn hiến với phố cổ, hồ Gươm và ẩm thực phong phú.",
                Province = "Hà Nội",
                Region = "Bắc",
                ImageUrl = "/images/hanoi.jpg",
                Latitude = 21.0285,
                Longitude = 105.8542,
                HasBeach = false,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Hồ Gươm",
                        Description = "Biểu tượng của Hà Nội, trung tâm phố cổ",
                        Latitude = 21.0285,
                        Longitude = 105.8542,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Văn Miếu",
                        Description = "Trường đại học đầu tiên của Việt Nam",
                        Latitude = 21.0267,
                        Longitude = 105.8355,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 30000
                    },
                    new AttractionPoint
                    {
                        Name = "Lăng Bác",
                        Description = "Lăng Chủ tịch Hồ Chí Minh",
                        Latitude = 21.0374,
                        Longitude = 105.8345,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Chùa Một Cột",
                        Description = "Kiến trúc độc đáo, di tích lịch sử",
                        Latitude = 21.0350,
                        Longitude = 105.8340,
                        Type = "Cultural",
                        VisitDuration = 30,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Bảo tàng Lịch sử",
                        Description = "Tìm hiểu lịch sử Việt Nam",
                        Latitude = 21.0245,
                        Longitude = 105.8580,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 40000
                    }
                }
            },

            new Destination
            {
                Name = "Hải Phòng - Hải Dương",
                Description = "Thành phố cảng năng động, nổi tiếng với đồ biển và bánh đa cua.",
                Province = "Hải Phòng",
                Region = "Bắc",
                ImageUrl = "/images/haiphong.jpg",
                Latitude = 20.8449,
                Longitude = 106.6881,
                HasBeach = true,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Chợ Sắt",
                        Description = "Trung tâm mua sắm Hải Phòng",
                        Latitude = 20.8647,
                        Longitude = 106.6839,
                        Type = "Shopping",
                        VisitDuration = 90,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Nhà hát Lớn",
                        Description = "Kiến trúc Pháp đẹp",
                        Latitude = 20.8645,
                        Longitude = 106.6812,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Đồ Sơn",
                        Description = "Bãi biển nổi tiếng Hải Phòng",
                        Latitude = 20.7074,
                        Longitude = 106.7890,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Cát Bà",
                        Description = "Vịnh Lan Hạ, Vườn Quốc gia",
                        Latitude = 20.7273,
                        Longitude = 107.0486,
                        Type = "Nature",
                        VisitDuration = 360,
                        EntranceFee = 40000
                    }
                }
            },

            new Destination
            {
                Name = "Quảng Ninh",
                Description = "Nổi tiếng với Vịnh Hạ Long – di sản thiên nhiên thế giới.",
                Province = "Quảng Ninh",
                Region = "Bắc",
                ImageUrl = "/images/quangninh.jpg",
                Latitude = 20.9509,
                Longitude = 107.0763,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Vịnh Hạ Long",
                        Description = "Di sản thiên nhiên thế giới UNESCO",
                        Latitude = 20.9101,
                        Longitude = 107.1839,
                        Type = "Nature",
                        VisitDuration = 360,
                        EntranceFee = 250000
                    },
                    new AttractionPoint
                    {
                        Name = "Đảo Tuần Châu",
                        Description = "Khu du lịch giải trí lớn",
                        Latitude = 20.9095,
                        Longitude = 107.0505,
                        Type = "Entertainment",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Hang Sửng Sốt",
                        Description = "Hang động đẹp nhất Vịnh Hạ Long",
                        Latitude = 20.8056,
                        Longitude = 107.1222,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Chợ đêm Hạ Long",
                        Description = "Trung tâm mua sắm và ẩm thực",
                        Latitude = 20.9560,
                        Longitude = 107.0790,
                        Type = "Shopping",
                        VisitDuration = 120,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Lào Cai",
                Description = "Vùng núi Tây Bắc với Sapa, Fansipan và chợ tình.",
                Province = "Lào Cai",
                Region = "Bắc",
                ImageUrl = "/images/laocai.jpg",
                Latitude = 22.4809,
                Longitude = 103.9756,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Sapa",
                        Description = "Thị trấn du lịch núi cao nổi tiếng",
                        Latitude = 22.3364,
                        Longitude = 103.8438,
                        Type = "Nature",
                        VisitDuration = 300,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Fansipan",
                        Description = "Nóc nhà Đông Dương - 3143m",
                        Latitude = 22.3025,
                        Longitude = 103.7750,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 700000
                    },
                    new AttractionPoint
                    {
                        Name = "Thác Bạc",
                        Description = "Thác nước đẹp ở Sapa",
                        Latitude = 22.3100,
                        Longitude = 103.8200,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 70000
                    },
                    new AttractionPoint
                    {
                        Name = "Bản Cát Cát",
                        Description = "Làng văn hóa người Mông",
                        Latitude = 22.3500,
                        Longitude = 103.8300,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 70000
                    }
                }
            },

            new Destination
            {
                Name = "Hà Giang",
                Description = "Cao nguyên đá Đồng Văn hùng vĩ, vùng đất của người Mông.",
                Province = "Hà Giang",
                Region = "Bắc",
                ImageUrl = "/images/hagiang.jpg",
                Latitude = 22.8230,
                Longitude = 104.9784,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = false,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Đồng Văn",
                        Description = "Phố cổ cao nhất Việt Nam",
                        Latitude = 23.2767,
                        Longitude = 105.3628,
                        Type = "Cultural",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Cột cờ Lũng Cú",
                        Description = "Điểm cực Bắc Tổ quốc",
                        Latitude = 23.3595,
                        Longitude = 105.3193,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Đèo Mã Pì Lèng",
                        Description = "Một trong tứ đại đỉnh đèo",
                        Latitude = 23.1833,
                        Longitude = 105.3500,
                        Type = "Nature",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Núi Đôi Cô Tiên",
                        Description = "Biểu tượng của Quản Bạ",
                        Latitude = 23.0833,
                        Longitude = 105.0000,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Hưng Yên - Thái Bình",
                Description = "Vùng đồng bằng Bắc Bộ, nổi tiếng nhãn lồng và bánh cáy.",
                Province = "Hưng Yên",
                Region = "Bắc",
                ImageUrl = "/images/hungyen.jpg",
                Latitude = 20.6467,
                Longitude = 106.0514,
                HasBeach = false,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Chùa Hương",
                        Description = "Quần thể di tích nổi tiếng",
                        Latitude = 20.6500,
                        Longitude = 106.0500,
                        Type = "Cultural",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Làng nghề Lý Nhân",
                        Description = "Làng nghề truyền thống",
                        Latitude = 20.6200,
                        Longitude = 106.0800,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Ninh Bình",
                Description = "Tràng An, Tam Cốc – di sản kép UNESCO, cảnh non nước hữu tình.",
                Province = "Ninh Bình",
                Region = "Bắc",
                ImageUrl = "/images/ninhbinh.jpg",
                Latitude = 20.2506,
                Longitude = 105.9745,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Tràng An",
                        Description = "Di sản kép UNESCO",
                        Latitude = 20.2500,
                        Longitude = 105.9100,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 250000
                    },
                    new AttractionPoint
                    {
                        Name = "Tam Cốc",
                        Description = "Vịnh Hạ Long trên cạn",
                        Latitude = 20.2333,
                        Longitude = 105.9167,
                        Type = "Nature",
                        VisitDuration = 150,
                        EntranceFee = 150000
                    },
                    new AttractionPoint
                    {
                        Name = "Bái Đính",
                        Description = "Chùa lớn nhất Việt Nam",
                        Latitude = 20.2200,
                        Longitude = 105.8500,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Hang Múa",
                        Description = "View toàn cảnh Tam Cốc tuyệt đẹp",
                        Latitude = 20.2450,
                        Longitude = 105.9250,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 100000
                    }
                }
            },

            new Destination
            {
                Name = "Thanh Hóa",
                Description = "Biển Sầm Sơn và đặc sản nem chua nổi tiếng.",
                Province = "Thanh Hóa",
                Region = "Bắc",
                ImageUrl = "/images/thanhhoa.jpg",
                Latitude = 19.8067,
                Longitude = 105.7851,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Sầm Sơn",
                        Description = "Bãi biển nổi tiếng miền Bắc",
                        Latitude = 19.7450,
                        Longitude = 105.9050,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Khu di tích Lam Kinh",
                        Description = "Cố đô nhà Lê",
                        Latitude = 19.6333,
                        Longitude = 105.4667,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 50000
                    },
                    new AttractionPoint
                    {
                        Name = "Biển Hải Tiến",
                        Description = "Bãi biển đẹp và yên tĩnh",
                        Latitude = 19.7833,
                        Longitude = 105.9500,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    }
                }
            },

            // ====== MIỀN TRUNG ======
            new Destination
            {
                Name = "Nghệ An",
                Description = "Quê hương Bác Hồ, có biển Cửa Lò và núi rừng Pù Mát.",
                Province = "Nghệ An",
                Region = "Trung",
                ImageUrl = "/images/nghean.jpg",
                Latitude = 18.6737,
                Longitude = 105.6812,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Cửa Lò",
                        Description = "Bãi biển nổi tiếng Nghệ An",
                        Latitude = 18.7900,
                        Longitude = 105.7300,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Kim Liên",
                        Description = "Quê hương Bác Hồ",
                        Latitude = 19.3833,
                        Longitude = 105.5000,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Pù Mát",
                        Description = "Vườn quốc gia",
                        Latitude = 19.0500,
                        Longitude = 104.7500,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 60000
                    }
                }
            },

            new Destination
            {
                Name = "Hà Tĩnh",
                Description = "Miền đất kiên trung, có chùa Hương Tích và biển Thiên Cầm.",
                Province = "Hà Tĩnh",
                Region = "Trung",
                ImageUrl = "/images/hatinh.jpg",
                Latitude = 18.3430,
                Longitude = 105.9050,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = false,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Thiên Cầm",
                        Description = "Bãi biển đẹp và hoang sơ",
                        Latitude = 18.4333,
                        Longitude = 106.0500,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Chùa Hương Tích",
                        Description = "Ngôi chùa linh thiêng",
                        Latitude = 18.2667,
                        Longitude = 105.7667,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Quảng Bình",
                Description = "Vườn quốc gia Phong Nha – Kẻ Bàng, kỳ quan hang động.",
                Province = "Quảng Bình",
                Region = "Trung",
                ImageUrl = "/images/quangbinh.jpg",
                Latitude = 17.4677,
                Longitude = 106.5975,
                HasBeach = true,
                HasMountain = true,
                HasCulture = false,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Phong Nha",
                        Description = "Hang động đẹp nhất thế giới",
                        Latitude = 17.5980,
                        Longitude = 106.3000,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 150000
                    },
                    new AttractionPoint
                    {
                        Name = "Hang Sơn Đoòng",
                        Description = "Hang động lớn nhất thế giới",
                        Latitude = 17.4570,
                        Longitude = 106.2840,
                        Type = "Nature",
                        VisitDuration = 300,
                        EntranceFee = 70000000
                    },
                    new AttractionPoint
                    {
                        Name = "Suối Nước Moọc",
                        Description = "Suối nước trong xanh như ngọc bích",
                        Latitude = 17.5500,
                        Longitude = 106.2500,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 80000
                    }
                }
            },

            new Destination
            {
                Name = "Huế",
                Description = "Cố đô Huế – di sản văn hóa thế giới, ẩm thực cung đình.",
                Province = "Thừa Thiên Huế",
                Region = "Trung",
                ImageUrl = "/images/hue.jpg",
                Latitude = 16.4637,
                Longitude = 107.5909,
                HasBeach = false,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Trung tâm Huế",
                        Description = "Khu vực trung tâm thành phố Huế",
                        Latitude = 16.4637,
                        Longitude = 107.5909,
                        Type = "Shopping",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Đại Nội",
                        Description = "Hoàng thành triều Nguyễn",
                        Latitude = 16.4673,
                        Longitude = 107.5789,
                        Type = "Cultural",
                        VisitDuration = 180,
                        EntranceFee = 200000
                    },
                    new AttractionPoint
                    {
                        Name = "Chùa Thiên Mụ",
                        Description = "Ngôi chùa cổ nhất Huế",
                        Latitude = 16.4545,
                        Longitude = 107.5556,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Lăng Khải Định",
                        Description = "Lăng tẩm kiến trúc độc đáo",
                        Latitude = 16.4360,
                        Longitude = 107.6150,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 150000
                    },
                    new AttractionPoint
                    {
                        Name = "Lăng Tự Đức",
                        Description = "Lăng tẩm lớn và đẹp nhất",
                        Latitude = 16.4211,
                        Longitude = 107.6372,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 150000
                    }
                }
            },

            new Destination
            {
                Name = "Đà Nẵng",
                Description = "Thành phố đáng sống, có biển Mỹ Khê và Bà Nà Hills.",
                Province = "Đà Nẵng",
                Region = "Trung",
                ImageUrl = "/images/danang.jpg",
                Latitude = 16.0544,
                Longitude = 108.2022,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Trung tâm Đà Nẵng",
                        Description = "Khu vực trung tâm thành phố",
                        Latitude = 16.0544,
                        Longitude = 108.2022,
                        Type = "Shopping",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Cầu Rồng",
                        Description = "Cầu phun lửa nổi tiếng",
                        Latitude = 16.0608,
                        Longitude = 108.2271,
                        Type = "Entertainment",
                        VisitDuration = 45,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Bãi biển Mỹ Khê",
                        Description = "Một trong những bãi biển đẹp nhất thế giới",
                        Latitude = 16.0401,
                        Longitude = 108.2425,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Ngũ Hành Sơn",
                        Description = "Ngũ hành sơn với hang động và chùa",
                        Latitude = 16.0073,
                        Longitude = 108.2625,
                        Type = "Cultural",
                        VisitDuration = 150,
                        EntranceFee = 40000
                    },
                    new AttractionPoint
                    {
                        Name = "Bán đảo Sơn Trà",
                        Description = "Thiên nhiên hoang sơ, chùa Linh Ứng",
                        Latitude = 16.1076,
                        Longitude = 108.2704,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Quảng Nam",
                Description = "Phố cổ Hội An, Mỹ Sơn – di sản văn hóa thế giới.",
                Province = "Quảng Nam",
                Region = "Trung",
                ImageUrl = "/images/quangnam.jpg",
                Latitude = 15.5770,
                Longitude = 108.4800,
                HasBeach = true,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Hội An",
                        Description = "Phố cổ di sản UNESCO",
                        Latitude = 15.8801,
                        Longitude = 108.3380,
                        Type = "Cultural",
                        VisitDuration = 240,
                        EntranceFee = 120000
                    },
                    new AttractionPoint
                    {
                        Name = "Mỹ Sơn",
                        Description = "Thánh địa Chăm Pa",
                        Latitude = 15.7644,
                        Longitude = 108.1231,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 150000
                    },
                    new AttractionPoint
                    {
                        Name = "Cù Lao Chàm",
                        Description = "Đảo sinh thái tuyệt đẹp",
                        Latitude = 15.9500,
                        Longitude = 108.5167,
                        Type = "Nature",
                        VisitDuration = 300,
                        EntranceFee = 100000
                    },
                    new AttractionPoint
                    {
                        Name = "Biển An Bàng",
                        Description = "Bãi biển yên tĩnh gần Hội An",
                        Latitude = 15.9167,
                        Longitude = 108.3500,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Quảng Ngãi",
                Description = "Đảo Lý Sơn – thiên đường biển đảo miền Trung.",
                Province = "Quảng Ngãi",
                Region = "Trung",
                ImageUrl = "/images/quangngai.jpg",
                Latitude = 15.1214,
                Longitude = 108.8044,
                HasBeach = true,
                HasMountain = true,
                HasCulture = false,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Đảo Lý Sơn",
                        Description = "Đảo tỏi với cảnh đẹp kỳ vĩ",
                        Latitude = 15.3833,
                        Longitude = 109.1167,
                        Type = "Nature",
                        VisitDuration = 360,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Cổng Tò Vò",
                        Description = "Cổng đá tự nhiên trên biển",
                        Latitude = 15.3900,
                        Longitude = 109.1200,
                        Type = "Nature",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Hang Câu",
                        Description = "Hang động đẹp trên đảo",
                        Latitude = 15.3800,
                        Longitude = 109.1150,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 20000
                    }
                }
            },

            new Destination
            {
                Name = "Bình Định",
                Description = "Xứ Nẫu hiền hòa, quê hương Tây Sơn, nổi tiếng Eo Gió.",
                Province = "Bình Định",
                Region = "Trung",
                ImageUrl = "/images/binhdinh.jpg",
                Latitude = 13.7830,
                Longitude = 109.2192,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Eo Gió",
                        Description = "Bãi biển hoang sơ tuyệt đẹp",
                        Latitude = 13.9667,
                        Longitude = 109.2667,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Kỳ Co",
                        Description = "Maldives của Việt Nam",
                        Latitude = 13.9500,
                        Longitude = 109.2500,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 50000
                    },
                    new AttractionPoint
                    {
                        Name = "Tháp Đôi",
                        Description = "Di tích Chăm Pa",
                        Latitude = 13.7500,
                        Longitude = 109.2000,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 22000
                    }
                }
            },

            new Destination
            {
                Name = "Phú Yên",
                Description = "Xứ hoa vàng cỏ xanh, Ghềnh Đá Đĩa kỳ thú.",
                Province = "Phú Yên",
                Region = "Trung",
                ImageUrl = "/images/phuyen.jpg",
                Latitude = 13.0882,
                Longitude = 109.0929,
                HasBeach = true,
                HasMountain = true,
                HasCulture = false,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Ghềnh Đá Đĩa",
                        Description = "Kỳ quan đá núi lửa",
                        Latitude = 13.4667,
                        Longitude = 109.2667,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 22000
                    },
                    new AttractionPoint
                    {
                        Name = "Gành Đá Đĩa",
                        Description = "Bãi đá độc đáo",
                        Latitude = 13.4700,
                        Longitude = 109.2700,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Vịnh Vũng Rô",
                        Description = "Vịnh biển đẹp như tranh",
                        Latitude = 12.9500,
                        Longitude = 109.3000,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Khánh Hòa",
                Description = "Nha Trang – trung tâm du lịch biển lớn nhất Việt Nam.",
                Province = "Khánh Hòa",
                Region = "Trung",
                ImageUrl = "/images/nhatrang.jpg",
                Latitude = 12.2388,
                Longitude = 109.1967,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Bãi biển Nha Trang",
                        Description = "Bãi biển đẹp nhất Việt Nam",
                        Latitude = 12.2451,
                        Longitude = 109.1943,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Vinpearl Land",
                        Description = "Công viên giải trí lớn",
                        Latitude = 12.2191,
                        Longitude = 109.1900,
                        Type = "Entertainment",
                        VisitDuration = 360,
                        EntranceFee = 800000
                    },
                    new AttractionPoint
                    {
                        Name = "Tháp Bà Ponagar",
                        Description = "Di tích Chăm Pa cổ",
                        Latitude = 12.2650,
                        Longitude = 109.1953,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 22000
                    },
                    new AttractionPoint
                    {
                        Name = "Hòn Chồng",
                        Description = "Khu du lịch với đá độc đáo",
                        Latitude = 12.2729,
                        Longitude = 109.1837,
                        Type = "Nature",
                        VisitDuration = 60,
                        EntranceFee = 22000
                    }
                }
            },

            new Destination
            {
                Name = "Ninh Thuận",
                Description = "Nắng gió, tháp Chàm và vườn nho Ba Mọi.",
                Province = "Ninh Thuận",
                Region = "Trung",
                ImageUrl = "/images/ninhthuan.jpg",
                Latitude = 11.6739,
                Longitude = 108.8629,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Vĩnh Hy",
                        Description = "Vịnh biển đẹp nhất Ninh Thuận",
                        Latitude = 11.5833,
                        Longitude = 109.1667,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Tháp Chàm Po Klong Garai",
                        Description = "Tháp Chăm cổ",
                        Latitude = 11.6667,
                        Longitude = 108.9833,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 30000
                    },
                    new AttractionPoint
                    {
                        Name = "Vườn Nho Ba Mọi",
                        Description = "Vườn nho lớn nhất VN",
                        Latitude = 11.7000,
                        Longitude = 108.9500,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 50000
                    }
                }
            },

            new Destination
            {
                Name = "Bình Thuận",
                Description = "Phan Thiết, Mũi Né – thiên đường nghỉ dưỡng biển.",
                Province = "Bình Thuận",
                Region = "Trung",
                ImageUrl = "/images/binhthuan.jpg",
                Latitude = 10.9289,
                Longitude = 108.1008,
                HasBeach = true,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Mũi Né",
                        Description = "Bãi biển và đồi cát đẹp",
                        Latitude = 10.9333,
                        Longitude = 108.2833,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Đồi cát bay",
                        Description = "Sa mạc mini độc đáo",
                        Latitude = 10.9500,
                        Longitude = 108.2500,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 25000
                    },
                    new AttractionPoint
                    {
                        Name = "Suối Tiên",
                        Description = "Suối nước đỏ kỳ lạ",
                        Latitude = 10.9800,
                        Longitude = 108.2300,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 20000
                    },
                    new AttractionPoint
                    {
                        Name = "Làng chài Mũi Né",
                        Description = "Làng chài truyền thống",
                        Latitude = 10.9400,
                        Longitude = 108.2900,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    }
                }
            },

            // ====== TÂY NGUYÊN ======
            new Destination
            {
                Name = "Kon Tum",
                Description = "Thủ phủ cà phê, nhà rông, văn hóa Tây Nguyên đặc sắc.",
                Province = "Kon Tum",
                Region = "Tây Nguyên",
                ImageUrl = "/images/kontum.jpg",
                Latitude = 14.3497,
                Longitude = 108.0005,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = false,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Nhà thờ gỗ Kon Tum",
                        Description = "Nhà thờ kiến trúc độc đáo",
                        Latitude = 14.3500,
                        Longitude = 108.0000,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Làng Kon Kơtu",
                        Description = "Làng văn hóa dân tộc",
                        Latitude = 14.3700,
                        Longitude = 108.0200,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Cầu treo Kon Klor",
                        Description = "Cầu treo tre dài nhất Việt Nam",
                        Latitude = 14.3300,
                        Longitude = 107.9800,
                        Type = "Nature",
                        VisitDuration = 45,
                        EntranceFee = 10000
                    }
                }
            },

            new Destination
            {
                Name = "Gia Lai",
                Description = "Biển Hồ Pleiku, cảnh đẹp hùng vĩ của cao nguyên bazan.",
                Province = "Gia Lai",
                Region = "Tây Nguyên",
                ImageUrl = "/images/gialai.jpg",
                Latitude = 13.9833,
                Longitude = 108.0000,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Biển Hồ",
                        Description = "Hồ núi lửa tuyệt đẹp",
                        Latitude = 14.0000,
                        Longitude = 108.0500,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Chùa Minh Thành",
                        Description = "Chùa gỗ đẹp nhất Tây Nguyên",
                        Latitude = 13.9800,
                        Longitude = 108.0100,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Thác Pơ Mơ Long",
                        Description = "Thác nước hùng vĩ",
                        Latitude = 13.9500,
                        Longitude = 107.9500,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 20000
                    }
                }
            },

            new Destination
            {
                Name = "Đắk Lắk",
                Description = "Buôn Ma Thuột, quê hương cà phê Việt Nam.",
                Province = "Đắk Lắk",
                Region = "Tây Nguyên",
                ImageUrl = "/images/daklak.jpg",
                Latitude = 12.6667,
                Longitude = 108.0500,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Làng Cổ Êđê",
                        Description = "Làng văn hóa Êđê truyền thống",
                        Latitude = 12.7000,
                        Longitude = 108.0800,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 30000
                    },
                    new AttractionPoint
                    {
                        Name = "Hồ Lắk",
                        Description = "Hồ nước đẹp, cưỡi voi",
                        Latitude = 12.5167,
                        Longitude = 108.2167,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Thác Dray Nur",
                        Description = "Thác nước hùng vĩ nhất Tây Nguyên",
                        Latitude = 12.6000,
                        Longitude = 108.3000,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 50000
                    }
                }
            },

            new Destination
            {
                Name = "Đắk Nông",
                Description = "Thác nước Dray Nur hùng vĩ và công viên địa chất UNESCO.",
                Province = "Đắk Nông",
                Region = "Tây Nguyên",
                ImageUrl = "/images/daknong.jpg",
                Latitude = 12.2646,
                Longitude = 107.6098,
                HasBeach = false,
                HasMountain = true,
                HasCulture = false,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Công viên địa chất Đắk Nông",
                        Description = "Công viên địa chất UNESCO",
                        Latitude = 12.2500,
                        Longitude = 107.6000,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Thác Gia Long",
                        Description = "Thác nước 7 tầng đẹp",
                        Latitude = 12.2800,
                        Longitude = 107.6200,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 30000
                    }
                }
            },

            new Destination
            {
                Name = "Lâm Đồng",
                Description = "Đà Lạt – thành phố sương mù, hoa và cà phê.",
                Province = "Lâm Đồng",
                Region = "Tây Nguyên",
                ImageUrl = "/images/dalat.jpg",
                Latitude = 11.9404,
                Longitude = 108.4583,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Trung tâm Đà Lạt",
                        Description = "Khu vực trung tâm thành phố với chợ Đà Lạt",
                        Latitude = 11.9404,
                        Longitude = 108.4583,
                        Type = "Shopping",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Hồ Xuân Hương",
                        Description = "Hồ nước đẹp nhất Đà Lạt, phù hợp dạo bộ",
                        Latitude = 11.9383,
                        Longitude = 108.4420,
                        Type = "Nature",
                        VisitDuration = 90,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Dinh Bảo Đại",
                        Description = "Cung điện mùa hè của vua Bảo Đại",
                        Latitude = 11.9264,
                        Longitude = 108.4453,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 40000
                    },
                    new AttractionPoint
                    {
                        Name = "Thác Datanla",
                        Description = "Thác nước đẹp, có xe lăn ống trượt",
                        Latitude = 11.9053,
                        Longitude = 108.4386,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 50000
                    },
                    new AttractionPoint
                    {
                        Name = "Langbiang",
                        Description = "Núi cao nhất Đà Lạt, view toàn cảnh tuyệt đẹp",
                        Latitude = 12.0428,
                        Longitude = 108.4586,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 80000
                    }
                }
            },

            // ====== MIỀN NAM ======
            new Destination
            {
                Name = "TP.HCM - Bình Dương - Bà Rịa",
                Description = "Vùng đô thị lớn nhất, có Vũng Tàu, ẩm thực đa dạng.",
                Province = "TP.HCM",
                Region = "Nam",
                ImageUrl = "/images/hcm.jpg",
                Latitude = 10.8231,
                Longitude = 106.6297,
                HasBeach = true,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Bến Thành",
                        Description = "Chợ Bến Thành - biểu tượng Sài Gòn",
                        Latitude = 10.7720,
                        Longitude = 106.6981,
                        Type = "Shopping",
                        VisitDuration = 120,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Nhà thờ Đức Bà",
                        Description = "Nhà thờ kiến trúc Pháp nổi tiếng",
                        Latitude = 10.7797,
                        Longitude = 106.6990,
                        Type = "Cultural",
                        VisitDuration = 30,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Dinh Độc Lập",
                        Description = "Di tích lịch sử quan trọng",
                        Latitude = 10.7769,
                        Longitude = 106.6955,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 40000
                    },
                    new AttractionPoint
                    {
                        Name = "Bảo tàng Chứng tích chiến tranh",
                        Description = "Tìm hiểu lịch sử chiến tranh Việt Nam",
                        Latitude = 10.7793,
                        Longitude = 106.6918,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 40000
                    },
                    new AttractionPoint
                    {
                        Name = "Phố đi bộ Nguyễn Huệ",
                        Description = "Con đường đi bộ sầm uất nhất Sài Gòn",
                        Latitude = 10.7743,
                        Longitude = 106.7021,
                        Type = "Entertainment",
                        VisitDuration = 90,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Đồng Nai - Bình Phước",
                Description = "Vùng công nghiệp và du lịch sinh thái núi Chứa Chan.",
                Province = "Đồng Nai",
                Region = "Nam",
                ImageUrl = "/images/dongnai.jpg",
                Latitude = 10.9463,
                Longitude = 107.1519,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Núi Chứa Chan",
                        Description = "Núi cao với view đẹp",
                        Latitude = 11.2500,
                        Longitude = 107.3000,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Hồ Trị An",
                        Description = "Hồ nhân tạo lớn",
                        Latitude = 11.0500,
                        Longitude = 107.2000,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Tây Ninh",
                Description = "Núi Bà Đen, trung tâm đạo Cao Đài đặc sắc.",
                Province = "Tây Ninh",
                Region = "Nam",
                ImageUrl = "/images/tayninh.jpg",
                Latitude = 11.3103,
                Longitude = 106.0983,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = false,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Núi Bà Đen",
                        Description = "Núi thiêng miền Nam",
                        Latitude = 11.2333,
                        Longitude = 106.1667,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 100000
                    },
                    new AttractionPoint
                    {
                        Name = "Tòa thánh Cao Đài",
                        Description = "Trung tâm đạo Cao Đài",
                        Latitude = 11.3100,
                        Longitude = 106.1000,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Long An",
                Description = "Cửa ngõ miền Tây, sông nước yên bình và đặc sản mắm.",
                Province = "Long An",
                Region = "Nam",
                ImageUrl = "/images/longan.jpg",
                Latitude = 10.6957,
                Longitude = 106.2431,
                HasBeach = false,
                HasMountain = false,
                HasCulture = false,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Khu du lịch Đồng Tháp Mười",
                        Description = "Du lịch sinh thái đồng quê",
                        Latitude = 10.7000,
                        Longitude = 106.2500,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    }
                }
            },

            new Destination
            {
                Name = "Tiền Giang",
                Description = "Du lịch miệt vườn Cái Bè, chợ nổi Cái Bè, trái cây tươi ngon.",
                Province = "Tiền Giang",
                Region = "Nam",
                ImageUrl = "/images/tiengiang.jpg",
                Latitude = 10.3592,
                Longitude = 106.3619,
                HasBeach = false,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Chợ nổi Cái Bè",
                        Description = "Chợ nổi đặc trưng miền Tây",
                        Latitude = 10.3000,
                        Longitude = 105.9500,
                        Type = "Cultural",
                        VisitDuration = 120,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Miệt vườn Cái Bè",
                        Description = "Du lịch vườn trái cây",
                        Latitude = 10.3200,
                        Longitude = 105.9700,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 50000
                    }
                }
            },

            new Destination
            {
                Name = "Vĩnh Long",
                Description = "Miệt vườn sông nước, quê hương nghệ sĩ Nam Bộ.",
                Province = "Vĩnh Long",
                Region = "Nam",
                ImageUrl = "/images/vinhlong.jpg",
                Latitude = 10.2397,
                Longitude = 105.9572,
                HasBeach = false,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Cù lao An Bình",
                        Description = "Đảo sinh thái miệt vườn",
                        Latitude = 10.2500,
                        Longitude = 105.9500,
                        Type = "Nature",
                        VisitDuration = 240,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Vườn trái cây",
                        Description = "Thưởng thức trái cây miền Tây",
                        Latitude = 10.2400,
                        Longitude = 105.9600,
                        Type = "Food",
                        VisitDuration = 120,
                        EntranceFee = 30000
                    }
                }
            },

            new Destination
            {
                Name = "Cần Thơ",
                Description = "Tây Đô, có chợ nổi Cái Răng và bến Ninh Kiều.",
                Province = "Cần Thơ",
                Region = "Nam",
                ImageUrl = "/images/cantho.jpg",
                Latitude = 10.0452,
                Longitude = 105.7469,
                HasBeach = false,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Chợ nổi Cái Răng",
                        Description = "Chợ nổi lớn nhất miền Tây",
                        Latitude = 10.0300,
                        Longitude = 105.7800,
                        Type = "Cultural",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Bến Ninh Kiều",
                        Description = "Biểu tượng Cần Thơ",
                        Latitude = 10.0333,
                        Longitude = 105.7833,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Vườn cò Bằng Lăng",
                        Description = "Khu bảo tồn chim cò",
                        Latitude = 10.1000,
                        Longitude = 105.8000,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 30000
                    }
                }
            },

            new Destination
            {
                Name = "An Giang",
                Description = "Núi Sam, Bà Chúa Xứ, vùng đất tâm linh miền Tây.",
                Province = "An Giang",
                Region = "Nam",
                ImageUrl = "/images/angiang.jpg",
                Latitude = 10.5216,
                Longitude = 105.1258,
                HasBeach = false,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Núi Sam",
                        Description = "Núi thiêng miền Tây",
                        Latitude = 10.5167,
                        Longitude = 105.0833,
                        Type = "Cultural",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Miếu Bà Chúa Xứ",
                        Description = "Nơi thờ Bà Chúa Xứ",
                        Latitude = 10.5200,
                        Longitude = 105.0850,
                        Type = "Cultural",
                        VisitDuration = 60,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Rừng tràm Trà Sư",
                        Description = "Rừng tràm đẹp nhất miền Tây",
                        Latitude = 10.6000,
                        Longitude = 105.2000,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 20000
                    }
                }
            },

            new Destination
            {
                Name = "Kiên Giang",
                Description = "Phú Quốc – đảo ngọc và Rạch Giá biển trời rộng lớn.",
                Province = "Kiên Giang",
                Region = "Nam",
                ImageUrl = "/images/kiengiang.jpg",
                Latitude = 10.2899,
                Longitude = 103.9840,
                HasBeach = true,
                HasMountain = true,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Bãi Sao",
                        Description = "Bãi biển đẹp nhất Phú Quốc",
                        Latitude = 10.1669,
                        Longitude = 104.0347,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Vinpearl Safari",
                        Description = "Vườn thú hoang dã lớn nhất VN",
                        Latitude = 10.3794,
                        Longitude = 103.9648,
                        Type = "Entertainment",
                        VisitDuration = 240,
                        EntranceFee = 650000
                    },
                    new AttractionPoint
                    {
                        Name = "Chợ đêm Phú Quốc",
                        Description = "Khám phá ẩm thực đêm",
                        Latitude = 10.2267,
                        Longitude = 103.9670,
                        Type = "Food",
                        VisitDuration = 120,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Cáp treo Hòn Thơm",
                        Description = "Cáp treo dài nhất thế giới",
                        Latitude = 10.1265,
                        Longitude = 104.0013,
                        Type = "Entertainment",
                        VisitDuration = 180,
                        EntranceFee = 300000
                    }
                }
            },

            new Destination
            {
                Name = "Cà Mau",
                Description = "Đất mũi cực Nam Tổ quốc, rừng ngập mặn và hải sản tươi.",
                Province = "Cà Mau",
                Region = "Nam",
                ImageUrl = "/images/camau.jpg",
                Latitude = 9.1526,
                Longitude = 105.1960,
                HasBeach = true,
                HasMountain = false,
                HasCulture = true,
                HasFood = true,
                Attractions = new List<AttractionPoint>
                {
                    new AttractionPoint
                    {
                        Name = "Mũi Cà Mau",
                        Description = "Điểm cực Nam Tổ quốc",
                        Latitude = 8.6000,
                        Longitude = 104.7333,
                        Type = "Nature",
                        VisitDuration = 120,
                        EntranceFee = 0
                    },
                    new AttractionPoint
                    {
                        Name = "Rừng U Minh Hạ",
                        Description = "Rừng ngập mặn đặc trưng",
                        Latitude = 9.2000,
                        Longitude = 105.0500,
                        Type = "Nature",
                        VisitDuration = 180,
                        EntranceFee = 50000
                    },
                    new AttractionPoint
                    {
                        Name = "Chợ nổi Năm Căn",
                        Description = "Chợ nổi nổi tiếng",
                        Latitude = 8.7500,
                        Longitude = 104.9500,
                        Type = "Cultural",
                        VisitDuration = 90,
                        EntranceFee = 0
                    }
                }
            }
        };

        public static List<Destination> GetAll()
        {
            return Destinations;
        }

        public static Destination GetByName(string name)
        {
            return Destinations.FirstOrDefault(d =>
                d.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                d.Name.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                name.Contains(d.Name, StringComparison.OrdinalIgnoreCase));
        }

        // Tính khoảng cách giữa 2 điểm theo công thức Haversine (km)
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Bán kính Trái Đất (km)
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public static List<(string From, string To, double Distance)> GetRouteSegmentsWithCoordinates(string destinationName)
        {
            var destination = GetByName(destinationName);
            if (destination == null || destination.Attractions.Count == 0)
            {
                return new List<(string, string, double)>();
            }

            var segments = new List<(string From, string To, double Distance)>();
            var attractions = destination.Attractions;

            // Tạo route từ điểm này sang điểm khác
            for (int i = 0; i < attractions.Count - 1; i++)
            {
                var from = attractions[i];
                var to = attractions[i + 1];
                var distance = CalculateDistance(from.Latitude, from.Longitude, to.Latitude, to.Longitude);

                segments.Add((from.Name, to.Name, Math.Round(distance, 1)));
            }

            // Thêm chặng quay về điểm đầu
            if (attractions.Count > 1)
            {
                var last = attractions[attractions.Count - 1];
                var first = attractions[0];
                var distance = CalculateDistance(last.Latitude, last.Longitude, first.Latitude, first.Longitude);
                segments.Add((last.Name, first.Name, Math.Round(distance, 1)));
            }

            return segments;
        }
    }
}