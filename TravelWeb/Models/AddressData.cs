using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class AddressData
    {
        private static Random _random = new Random();

        // Địa chỉ nhà hàng/quán ăn cụ thể (gấp 3-4 lần)
        private static readonly Dictionary<string, List<string>> RestaurantAddresses = new Dictionary<string, List<string>>
        {
            ["Hà Nội"] = new List<string>
            {
                "📍 Phở Gia Truyền Bát Đàn, 49 Bát Đàn, Hoàn Kiếm",
                "📍 Phở Thìn, 13 Lò Đúc, Hai Bà Trưng",
                "📍 Bún Chả Hàng Mành, 1 Hàng Mành, Hoàn Kiếm",
                "📍 Bún Chả Đắc Kim, 1 Hàng Mành, Hoàn Kiếm",
                "📍 Chả Cá Lã Vọng, 14 Chả Cá, Hoàn Kiếm",
                "📍 Quán Bún Thang Bà Đức, 48 Cầu Gỗ, Hoàn Kiếm",
                "📍 Xôi Yến, 35B Nguyễn Hữu Huân, Hoàn Kiếm",
                "📍 Bánh Cuốn Thanh Vân, 14 Hàng Gà, Hoàn Kiếm",
                "📍 Bánh Tôm Hồ Tây, 1 Thanh Niên, Ba Đình",
                "📍 Cafe Giảng, 39 Nguyễn Hữu Huân, Hoàn Kiếm",
                "📍 Nem Phùng, 3 Hàng Than, Ba Đình",
                "📍 Phở Suông, 24B Trung Yên, Cầu Giấy",
                "📍 Bún Riêu Cua Bà Hoà, 120 Trần Quốc Toản, Hoàn Kiếm",
                "📍 Miến Lươn Ngon, 87 Hàng Điếu, Hoàn Kiếm",
                "📍 Cơm Gà Bà Buội, 22 Phan Bội Châu, Hoàn Kiếm",
                "📍 Quán Ăn Ngon, 18 Phan Bội Châu, Hoàn Kiếm",
                "📍 Bánh Đúc Bà Châu, ngõ Hàng Khoai, Đống Đa",
                "📍 Chè Lam, 108 Nguyễn Đình Chiểu, Hai Bà Trưng"
            },
            ["Đà Lạt"] = new List<string>
            {
                "📍 Mì Quảng Bà Mua, 119 Phan Đình Phùng, Phường 2",
                "📍 Lẩu Dê Hà Giang, 108 Nguyễn Văn Trỗi, Phường 4",
                "📍 Nem Nướng Ninh Hòa, 39 Bùi Thị Xuân, Phường 2",
                "📍 Phở Hòa, 114 Nguyễn Văn Trỗi, Phường 4",
                "📍 Bánh Căn Đà Lạt, Chợ Đà Lạt, Nguyễn Thị Minh Khai",
                "📍 Lẩu Gà Lá É Út Dzẩm, 21 Khe Sanh, Phường 10",
                "📍 Quán Hồng Hạnh, 65 Trần Phú, Phường 4",
                "📍 Bánh Mì Phượng, 10 Khu Hòa Bình, Phường 1",
                "📍 Bánh Tráng Nướng 59, 59 Hai Bà Trưng, Phường 6",
                "📍 Cơm Niêu Sapa, 28 Hai Bà Trưng, Phường 6",
                "📍 Bánh Ướt Lòng Gà Hoàng Diệu, 41 Hoàng Diệu, Phường 6",
                "📍 Ốc Oanh, 43 Nguyễn Chí Thanh, Phường 1",
                "📍 Chả Cá Đà Lạt, 6 Phan Như Thạch, Phường 1",
                "📍 Gà Nướng Ụ Gà, 27 Hồ Tùng Mậu, Phường 3",
                "📍 Đậu Hủ Khói Phương Mai, 204 Phan Đình Phùng, Phường 2"
            },
            ["Đà Nẵng"] = new List<string>
            {
                "📍 Mì Quảng 1A, 1A Hải Phòng, Thanh Khê",
                "📍 Bánh Tráng Cuốn Thịt Heo Hoàng Béo, 109 Nguyễn Chí Thanh, Hải Châu",
                "📍 Bún Mắm Nêm Số 11, 11 Núi Thành, Hải Châu",
                "📍 Hải Sản Bé Mặn, 205 Nguyễn Tri Phương, Hải Châu",
                "📍 Bánh Xèo Bà Dưỡng, 280 Hoàng Diệu, Hải Châu",
                "📍 Bún Chả Cá 75, 75 Trần Quốc Toản, Hải Châu",
                "📍 Cơm Gà Bà Nga, 85 Phan Châu Trinh, Hải Châu",
                "📍 Bánh Canh Ghẹ Nga, 188 Lê Duẩn, Hải Châu",
                "📍 Bánh Bèo Bà Cụ, 98 Hải Phòng, Thanh Khê",
                "📍 Nem Lụi Mỹ Khê, 80 Võ Nguyên Giáp, Sơn Trà",
                "📍 Bánh Tráng Thịt Heo Phạm Văn Đồng, 290 Phạm Văn Đồng, Sơn Trà",
                "📍 Lẩu Thái Sơn Trà, 200 Võ Nguyên Giáp, Sơn Trà",
                "📍 Gỏi Cá Nam Ô, làng chài Nam Ô, Hòa Hiệp Nam",
                "📍 Bánh Bột Lọc Huế, 88 Phan Chu Trinh, Hải Châu",
                "📍 Chè Hẻm 49, 49 Trần Quốc Toản, Hải Châu"
            },
            ["Nha Trang"] = new List<string>
            {
                "📍 Lẩu Ếch Hồng, 25 Hoàng Hoa Thám, Lộc Thọ",
                "📍 Nem Nướng Ninh Hòa Chợ Đầm, Chợ Đầm, Vĩnh Hải",
                "📍 Bún Cá Dọc Lết, Đường 2/4, Vĩnh Nguyên",
                "📍 Bánh Căn Cô Ba, 16 Nguyễn Thiện Thuật, Vĩnh Hải",
                "📍 Quán Ốc 105, 105 Nguyễn Thiện Thuật, Vĩnh Hải",
                "📍 Nem Nướng Vườn Cam, 100 Thích Quảng Đức, Phương Sơn",
                "📍 Bánh Xèo Cô Hai, 110 Hùng Vương, Tân Lập",
                "📍 Lẩu Cá Quán 78, 78 Trần Phú, Lộc Thọ",
                "📍 Bún Sứa Út Dung, 6 Pasteur, Vĩnh Hải",
                "📍 Bánh Canh Chả Cá 69, 69 Nguyễn Đức Thuận, Vĩnh Hòa",
                "📍 Hải Sản Đại Dương, 112 Trần Phú, Lộc Thọ",
                "📍 Bún Chả Cá Ngon, 95 Hoàng Hoa Thám, Lộc Thọ",
                "📍 Chè Bưởi Nha Trang, 20 Thống Nhất, Vĩnh Hải"
            },
            ["TP.HCM"] = new List<string>
            {
                "📍 Cơm Tấm Mộc, 221 Trần Hưng Đạo, Q1",
                "📍 Bánh Mì Huỳnh Hoa, 26 Lê Thị Riêng, Q1",
                "📍 Hủ Tiếu Nam Vang Thành Đạt, 468 Trần Hưng Đạo, Q1",
                "📍 Phá Lấu Cô Út, 35 Tân Sơn Nhì, Tân Phú",
                "📍 Bột Chiên Bến Thành, Chợ Bến Thành, Q1",
                "📍 Bánh Tráng Trộn Tây Đô, 262 Phạm Ngũ Lão, Q1",
                "📍 Gỏi Cuốn Bà Sơn, 102 Nguyễn Trãi, Q1",
                "📍 Lẩu Mắm U Minh, 35 Nguyễn Bỉnh Khiêm, Q1",
                "📍 Bánh Xèo 46A, 46A Đinh Công Trang, Q1",
                "📍 Bún Riêu 21, 21 Nguyễn Thiện Thuật, Q3",
                "📍 Bánh Canh Cua 87, 87 Trần Khắc Chân, Q1",
                "📍 Hủ Tiếu Mỹ Tho Thanh Xuân, 217 Hai Bà Trưng, Q3",
                "📍 Cơm Tấm Sườn Bì, 138 Nguyễn Văn Cừ, Q1",
                "📍 Bánh Mì Ông Màu, 43 Nguyễn Trãi, Q1",
                "📍 Chè Thạch Sài Gòn, 89 Cách Mạng Tháng 8, Q3",
                "📍 Bánh Khọt Vũng Tàu 81, 81 Đề Thám, Q1"
            },
            ["Huế"] = new List<string>
            {
                "📍 Bún Bò Huế Bà Tường, 17 Lý Thường Kiệt, Phú Hội",
                "📍 Cơm Hến Ngọc, 74 Trần Cao Vân, Phú Hòa",
                "📍 Bánh Bèo Bà Cu, 5 Đinh Tiên Hoàng, Vĩnh Ninh",
                "📍 Bánh Nậm Bà Tuyết, 10 Nguyễn Công Trứ, Phú Hội",
                "📍 Bánh Lọc Chợ Đông Ba, Chợ Đông Ba, Phú Hội",
                "📍 Chè Hến Ngon, 36 Nguyễn Công Trứ, Phú Hội",
                "📍 Cơm Cung Đình Hương Sen, 6 Lê Lợi, Vĩnh Ninh",
                "📍 Tré Huế Chị Tý, 40 Phan Bội Châu, Vĩnh Ninh",
                "📍 Mè Xửng Thanh Trì, 8 Hùng Vương, Phú Hội",
                "📍 Bún Nghệ Cô Hương, 15 Chí Lăng, Phú Hội",
                "📍 Bánh Khoái Cô Đỡ, 11 Nguyễn Bỉnh Khiêm, Vĩnh Ninh",
                "📍 Nem Lụi Lệ, 8 Phạm Hồng Thái, Vĩnh Ninh",
                "📍 Bún Thịt Nướng Cô Mai, 17 Chí Lăng, Phú Hội"
            },
            ["Phú Quốc"] = new List<string>
            {
                "📍 Hải Sản Phú Quốc, Chợ Đêm Phú Quốc, Dương Đông",
                "📍 Bánh Canh Ghẹ, 99 Trần Hưng Đạo, Dương Đông",
                "📍 Bún Quậy Phú Quốc, 45 Nguyễn Trung Trực, Dương Đông",
                "📍 Nhum Nướng Mai Ong, Chợ Đêm, Dương Đông",
                "📍 Gỏi Cá Trích, 88 Trần Hưng Đạo, Dương Đông",
                "📍 Ốc Nướng Tiêu Xanh, Chợ Đêm, Dương Đông",
                "📍 Bún Kèn Phú Quốc, 65 Nguyễn Trung Trực, Dương Đông",
                "📍 Hàu Nướng Mỡ Hành, 120 Trần Hưng Đạo, Dương Đông",
                "📍 Bánh Tráng Nướng, Chợ Đêm, Dương Đông",
                "📍 Lẩu Hải Sản, 78 Bạch Đằng, Dương Đông"
            },
            ["Hải Phòng"] = new List<string>
            {
                "📍 Bánh Đa Cua Bà Tân, 20 Hoàng Văn Thụ, Hồng Bàng",
                "📍 Bánh Mì Cay Tú Linh, 45 Lê Duẩn, Ngô Quyền",
                "📍 Nem Cua Bể Bà Bảy, 78 Điện Biên Phủ, Lê Chân",
                "📍 Ốc Hải Phòng 88, 88 Lạch Tray, Ngô Quyền",
                "📍 Chè Giun Cô Ba, 32 Minh Khai, Hồng Bàng",
                "📍 Lẩu Cua Đồng, 156 Tô Hiệu, Lê Chân",
                "📍 Cơm Rang Cua Bể, 67 Trần Nguyên Hãn, Lê Chân",
                "📍 Bánh Bèo Hải Phòng, 25 Trần Phú, Ngô Quyền"
            },
            ["Quảng Ninh"] = new List<string>
            {
                "📍 Hải Sản Sống Hạ Long, Chợ Hạ Long, Bãi Cháy",
                "📍 Chả Mực Hạ Long, 98 Hạ Long, Bãi Cháy",
                "📍 Sứa Đỏ Quảng Ninh, 156 Vũ Văn Hiếu, Hạ Long",
                "📍 Nem Cua Bể, 45 Bạch Đằng, Hạ Long",
                "📍 Ốc Nướng Chợ Đêm, Chợ Đêm Hạ Long, Bãi Cháy"
            },
            ["Lào Cai"] = new List<string>
            {
                "📍 Thắng Cố Sapa, 15 Cầu Mây, Sa Pa",
                "📍 Cá Tầm Sapa, 89 Fansipan, Sa Pa",
                "📍 Lẩu Dê Đen, 45 Hoàng Liên, Sa Pa",
                "📍 Bánh Mì Chảo Sapa, 67 Xuân Viên, Sa Pa"
            },
            ["Ninh Bình"] = new List<string>
            {
                "📍 Cơm Cháy Chả Cá, 12 Đường Hồng, Ninh Bình",
                "📍 Dê Núi Ninh Bình, 56 Lê Hồng Phong, Ninh Bình",
                "📍 Măng Tre Non, 23 Trần Hưng Đạo, Ninh Bình",
                "📍 Miến Lươn Tam Cốc, 89 Tam Cốc, Hoa Lư"
            },
            ["Thanh Hóa"] = new List<string>
            {
                "📍 Nem Chua Thanh Hóa, 45 Phan Chu Trinh, TP Thanh Hóa",
                "📍 Chả Rươi, 78 Trần Phú, TP Thanh Hóa",
                "📍 Hải Sản Sầm Sơn, 123 Hồ Xuân Hương, Sầm Sơn",
                "📍 Cơm Lam Thanh Hóa, 56 Quang Trung, TP Thanh Hóa"
            },
            ["Nghệ An"] = new List<string>
            {
                "📍 Bánh Mướt Nghệ An, 34 Quang Trung, TP Vinh",
                "📍 Chả Cốm Cửa Lò, 89 Bình Minh, Cửa Lò",
                "📍 Hải Sản Cửa Lò, 123 Bãi Biển, Cửa Lò",
                "📍 Nem Chua Yên Thành, 67 Hà Huy Tập, TP Vinh"
            },
            ["Quảng Bình"] = new List<string>
            {
                "📍 Ram Tép Quảng Bình, 45 Phạm Văn Đồng, Đồng Hới",
                "📍 Bánh Xèo Tôm Nhảy, 78 Quách Xuân Kỳ, Đồng Hới",
                "📍 Cháo Canh Quảng Bình, 123 Trương Phước Phan, Đồng Hới",
                "📍 Bún Măng Vịt, 56 Lý Thường Kiệt, Đồng Hới"
            },
            ["Quảng Nam"] = new List<string>
            {
                "📍 Cao Lầu Hội An, 26 Thái Phiên, Hội An",
                "📍 Bánh Mì Phượng, 2B Phan Châu Trinh, Hội An",
                "📍 Mỳ Quảng Bà Mua, 6B Trưng Nữ Vương, Hội An",
                "📍 Cơm Gà Hội An, 12 Bà Triệu, Hội An",
                "📍 Bánh Bao Bánh Vạc, 45 Nguyễn Thị Minh Khai, Hội An"
            },
            ["Bình Định"] = new List<string>
            {
                "📍 Bánh Hỏi Lòng Heo, 78 Nguyễn Huệ, Quy Nhơn",
                "📍 Nhum Biển Quy Nhơn, 123 Xuân Diệu, Quy Nhơn",
                "📍 Bánh Ít Lá Gai, 45 Trần Hưng Đạo, Quy Nhơn",
                "📍 Chả Ram Tôm Đất, 56 Lê Hồng Phong, Quy Nhơn"
            },
            ["Phú Yên"] = new List<string>
            {
                "📍 Bánh Hỏi Cháo Lòng, 34 Ngô Gia Tự, Tuy Hòa",
                "📍 Bánh Căn Mini, 67 Trần Hưng Đạo, Tuy Hòa",
                "📍 Cá Ngừ Đại Dương, 89 Lê Duẩn, Tuy Hòa",
                "📍 Ốc Phú Yên, 123 Hùng Vương, Tuy Hòa"
            },
            ["Khánh Hòa"] = new List<string>
            {
                "📍 Bún Cá Nha Trang, 32 Nguyễn Thị Minh Khai, Nha Trang",
                "📍 Nem Nướng Ninh Hòa, 78 Yersin, Nha Trang",
                "📍 Bánh Căn Cô Ba, 45 Hoàng Hoa Thám, Nha Trang",
                "📍 Bún Sứa Nha Trang, 67 Pasteur, Nha Trang",
                "📍 Bánh Canh Chả Cá, 123 Nguyễn Thiện Thuật, Nha Trang"
            },
            ["Bình Thuận"] = new List<string>
            {
                "📍 Bánh Căn Phan Thiết, 45 Nguyễn Tất Thành, Phan Thiết",
                "📍 Bánh Xèo Tôm Nhảy, 78 Trần Hưng Đạo, Phan Thiết",
                "📍 Lẩu Thả Phan Thiết, 123 Nguyễn Huệ, Phan Thiết",
                "📍 Hải Sản Mũi Né, 234 Nguyễn Đình Chiểu, Mũi Né"
            },
            ["Lâm Đồng"] = new List<string>
            {
                "📍 Lẩu Dê Đà Lạt, 56 Phan Đình Phùng, Đà Lạt",
                "📍 Nem Nướng Đà Lạt, 89 Bùi Thị Xuân, Đà Lạt",
                "📍 Bánh Căn Mini, 123 Nguyễn Thị Minh Khai, Đà Lạt",
                "📍 Bánh Ướt Lòng Gà, 45 Hoàng Diệu, Đà Lạt"
            },
            ["Đắk Lắk"] = new List<string>
            {
                "📍 Lẩu Cá Linh Bông Điên Điển, 45 Lê Duẩn, BMT",
                "📍 Nem Nướng Gia Lai, 78 Phan Chu Trinh, BMT",
                "📍 Cơm Lam Tây Nguyên, 123 Lý Thường Kiệt, BMT",
                "📍 Gà Nướng Tây Nguyên, 56 Trần Nhật Duật, BMT"
            },
            ["Cần Thơ"] = new List<string>
            {
                "📍 Bánh Xèo Cần Thơ, 34 Hai Bà Trưng, Ninh Kiều",
                "📍 Lẩu Mắm Cần Thơ, 67 Nguyễn An Ninh, Ninh Kiều",
                "📍 Hủ Tiếu Cần Thơ, 89 Mậu Thân, Ninh Kiều",
                "📍 Bún Riêu Cua Đồng, 123 Trần Phú, Ninh Kiều"
            },
            ["An Giang"] = new List<string>
            {
                "📍 Bún Nước Lèo, 45 Tôn Đức Thắng, Châu Đốc",
                "📍 Lẩu Mắm U Minh, 78 Lê Lợi, Châu Đốc",
                "📍 Cá Lóc Nướng Trui, 123 Núi Sam, Châu Đốc",
                "📍 Bánh Xèo Miền Tây, 56 Nguyễn Văn Thoại, Châu Đốc"
            },
            ["Kiên Giang"] = new List<string>
            {
                "📍 Hải Sản Phú Quốc, 234 Trần Hưng Đạo, Dương Đông",
                "📍 Bún Quậy Phú Quốc, 123 30/4, Dương Đông",
                "📍 Ghẹ Hấp Phú Quốc, 89 Nguyễn Trung Trực, Dương Đông",
                "📍 Nhum Nướng Phú Quốc, 67 Bạch Đằng, Dương Đông"
            }
        };

        // Địa chỉ quán cafe cụ thể
        private static readonly Dictionary<string, List<string>> CafeAddresses = new Dictionary<string, List<string>>
        {
            ["Hà Nội"] = new List<string>
            {
                "📍 Cafe Giảng, 39 Nguyễn Hữu Huân, Hoàn Kiếm",
                "📍 Highlands Coffee, 1 Lê Thái Tổ, Hoàn Kiếm",
                "📍 The Coffee House, 18 Tràng Tiền, Hoàn Kiếm",
                "📍 Cộng Cà Phê, 152 Trần Quang Khải, Hoàn Kiếm",
                "📍 Cafe Phố Cổ, 11 Hàng Gai, Hoàn Kiếm",
                "📍 Cafe Lâm, 60 Nguyễn Hữu Huân, Hoàn Kiếm",
                "📍 Tranquil Books & Coffee, 5 Nguyễn Quang Bích, Hoàn Kiếm",
                "📍 Joma Bakery Cafe, 20 Lý Quốc Sư, Hoàn Kiếm"
            },
            ["Đà Lạt"] = new List<string>
            {
                "📍 Mê Linh Coffee Garden, 1 Đường Yersin, Phường 10",
                "📍 Cafe Tùng, 200 Khe Sanh, Phường 10",
                "📍 Đà Lạt Memory Coffee, 2 Hòa Bình, Phường 1",
                "📍 Cafe Trên Đồi, 88 Trần Quốc Toản, Phường 1",
                "📍 Windmills Cafe, 48 Hai Bà Trưng, Phường 6",
                "📍 An Cafe, 72 Bùi Thị Xuân, Phường 2",
                "📍 Maze Bar, 65 Truong Cong Dinh, Phường 3",
                "📍 V Cafe, 1/1 Bùi Thị Xuân, Phường 2"
            },
            ["Đà Nẵng"] = new List<string>
            {
                "📍 43 Factory Coffee, 150 Bạch Đằng, Hải Châu",
                "📍 The Coffee House, 216 Trần Phú, Hải Châu",
                "📍 Cộng Cà Phê, 234 Trần Phú, Hải Châu",
                "📍 Altitude Roastery, 16 Núi Thành, Hải Châu",
                "📍 Starbucks Đà Nẵng, 50 Bạch Đằng, Hải Châu",
                "📍 Milano Coffee, 19 Lê Duẩn, Hải Châu",
                "📍 Tranquil Cafe, 93 Phan Chu Trinh, Hải Châu"
            },
            ["Nha Trang"] = new List<string>
            {
                "📍 Sailing Club, 72-74 Trần Phú, Lộc Thọ",
                "📍 Highlands Coffee Trần Phú, 86 Trần Phú, Lộc Thọ",
                "📍 The Coffee House, 2D Trần Quang Khải, Lộc Thọ",
                "📍 La Louisianne, 98 Nguyễn Thiện Thuật, Vĩnh Hải",
                "📍 Cộng Cà Phê, 77 Trần Phú, Lộc Thọ"
            },
            ["TP.HCM"] = new List<string>
            {
                "📍 The Coffee House Nguyễn Huệ, 62 Nguyễn Huệ, Q1",
                "📍 Highlands Coffee Bitexco, 2 Hải Triều, Q1",
                "📍 Starbucks Đồng Khởi, 72 Đồng Khởi, Q1",
                "📍 Cộng Cà Phê Bùi Viện, 26 Bùi Viện, Q1",
                "📍 The Workshop Coffee, 27 Ngô Đức Kế, Q1",
                "📍 L'Usine, 151/3 Đồng Khởi, Q1",
                "📍 Saigon Ơi, 54 Hồ Xuân Hương, Q3"
            },
            ["Huế"] = new List<string>
            {
                "📍 Cafe on Thu Wheels, 1/2 Nguyễn Tri Phương, Vĩnh Ninh",
                "📍 DMZ Bar, 60 Lê Lợi, Vĩnh Ninh",
                "📍 Cafe Ý Tưởng, 5 Nguyễn Tri Phương, Vĩnh Ninh",
                "📍 Joly Coffee, 88 Hùng Vương, Phú Hội"
            },
            ["Phú Quốc"] = new List<string>
            {
                "📍 Buddy Ice Cream & Cafe, 116 Trần Hưng Đạo, Dương Đông",
                "📍 Coco Bar, 126 Trần Hưng Đạo, Dương Đông",
                "📍 The Secret Garden, 136 Trần Hưng Đạo, Dương Đông"
            },
            ["Hải Phòng"] = new List<string>
            {
                "📍 Highlands Coffee Chợ Sắt, 45 Điện Biên Phủ, Lê Chân",
                "📍 The Coffee House Minh Khai, 78 Minh Khai, Hồng Bàng",
                "📍 Cộng Cà Phê, 123 Lạch Tray, Ngô Quyền"
            },
            ["Quảng Ninh"] = new List<string>
            {
                "📍 The Coffee House Hạ Long, 56 Hạ Long, Bãi Cháy",
                "📍 Highlands Coffee Vũ Văn Hiếu, 89 Vũ Văn Hiếu, Hạ Long",
                "📍 Cộng Cà Phê Hạ Long, 123 Bạch Đằng, Hạ Long"
            },
            ["Lào Cai"] = new List<string>
            {
                "📍 Sapa Moment Cafe, 23 Xuân Viên, Sa Pa",
                "📍 Nature View Cafe, 45 Fansipan, Sa Pa",
                "📍 Cafe In The Clouds, 67 Cầu Mây, Sa Pa"
            },
            ["Ninh Bình"] = new List<string>
            {
                "📍 Trang An Cafe, 34 Đường Hồng, Ninh Bình",
                "📍 The Coffee House Ninh Bình, 67 Lê Hồng Phong, Ninh Bình"
            },
            ["Thanh Hóa"] = new List<string>
            {
                "📍 Highlands Coffee Thanh Hóa, 78 Phan Chu Trinh, TP Thanh Hóa",
                "📍 The Coffee House Sầm Sơn, 123 Hồ Xuân Hương, Sầm Sơn"
            },
            ["Nghệ An"] = new List<string>
            {
                "📍 Highlands Coffee Vinh, 56 Quang Trung, TP Vinh",
                "📍 The Coffee House Cửa Lò, 89 Bình Minh, Cửa Lò"
            },
            ["Quảng Bình"] = new List<string>
            {
                "📍 Phong Nha Cafe, 78 Phạm Văn Đồng, Đồng Hới",
                "📍 The Coffee House Đồng Hới, 123 Trương Phước Phan, Đồng Hới"
            },
            ["Quảng Nam"] = new List<string>
            {
                "📍 Reaching Out Tea House, 131 Trần Phú, Hội An",
                "📍 Faifo Coffee, 132 Trần Phú, Hội An",
                "📍 Mia Coffee, 10 Bà Triệu, Hội An"
            },
            ["Bình Định"] = new List<string>
            {
                "📍 The Coffee House Quy Nhơn, 123 Xuân Diệu, Quy Nhơn",
                "📍 Highlands Coffee Quy Nhơn, 78 Nguyễn Huệ, Quy Nhơn"
            },
            ["Phú Yên"] = new List<string>
            {
                "📍 Đà Rằng Coffee, 67 Trần Hưng Đạo, Tuy Hòa",
                "📍 The Coffee House Tuy Hòa, 89 Lê Duẩn, Tuy Hòa"
            },
            ["Khánh Hòa"] = new List<string>
            {
                "📍 Sailing Club Beach Bar, 72 Trần Phú, Nha Trang",
                "📍 Cộng Cà Phê Nha Trang, 77 Trần Phú, Nha Trang",
                "📍 Highlands Coffee Yersin, 123 Yersin, Nha Trang"
            },
            ["Bình Thuận"] = new List<string>
            {
                "📍 Mũi Né Coffee, 234 Nguyễn Đình Chiểu, Mũi Né",
                "📍 The Coffee House Phan Thiết, 123 Nguyễn Huệ, Phan Thiết"
            },
            ["Lâm Đồng"] = new List<string>
            {
                "📍 Mê Linh Coffee Garden, 1 Yersin, Đà Lạt",
                "📍 Cafe Tùng, 200 Khe Sanh, Đà Lạt",
                "📍 An Cafe, 72 Bùi Thị Xuân, Đà Lạt"
            },
            ["Đắk Lắk"] = new List<string>
            {
                "📍 Trung Nguyên Legend Cafe, 56 Lê Duẩn, BMT",
                "📍 Highlands Coffee BMT, 123 Lý Thường Kiệt, BMT"
            },
            ["Cần Thơ"] = new List<string>
            {
                "📍 The Coffee House Ninh Kiều, 67 Mậu Thân, Ninh Kiều",
                "📍 Highlands Coffee Cần Thơ, 89 Nguyễn An Ninh, Ninh Kiều"
            },
            ["An Giang"] = new List<string>
            {
                "📍 Café Núi Sam View, 78 Núi Sam, Châu Đốc",
                "📍 The Coffee House Châu Đốc, 123 Lê Lợi, Châu Đốc"
            },
            ["Kiên Giang"] = new List<string>
            {
                "📍 Buddy Ice Cream & Cafe, 116 Trần Hưng Đạo, Dương Đông",
                "📍 Coco Bar Phú Quốc, 126 Trần Hưng Đạo, Dương Đông",
                "📍 The Secret Garden, 136 Trần Hưng Đạo, Dương Đông"
            }
        };

        // Địa chỉ chợ đêm/khu ẩm thực
        private static readonly Dictionary<string, List<string>> NightMarketAddresses = new Dictionary<string, List<string>>
        {
            ["Hà Nội"] = new List<string>
            {
                "📍 Phố đi bộ Hồ Gươm, khu vực Hàng Đào - Hàng Ngang, Hoàn Kiếm",
                "📍 Khu ẩm thực phố cổ, Hàng Buồm - Hàng Giấy, Hoàn Kiếm",
                "📍 Chợ đêm Đồng Xuân, ngõ Đồng Xuân, Hoàn Kiếm",
                "📍 Khu ẩm thực Tây Sơn, Đống Đa"
            },
            ["Đà Lạt"] = new List<string>
            {
                "📍 Chợ đêm Đà Lạt, Nguyễn Thị Minh Khai, Phường 1",
                "📍 Khu ẩm thực Hòa Bình, Đường Hòa Bình, Phường 1"
            },
            ["Đà Nẵng"] = new List<string>
            {
                "📍 Chợ đêm Sơn Trà, Võ Nguyên Giáp, Sơn Trà",
                "📍 Khu ẩm thực Bạch Đằng, Đường Bạch Đằng, Hải Châu",
                "📍 Chợ Hàn, Chợ Hàn, Hải Châu"
            },
            ["Nha Trang"] = new List<string>
            {
                "📍 Chợ đêm Nha Trang, Khu vực Chợ Đầm, Vĩnh Hải",
                "📍 Khu ẩm thực đêm Trần Phú, Đường Trần Phú, Lộc Thọ"
            },
            ["TP.HCM"] = new List<string>
            {
                "📍 Chợ đêm Bến Thành, Chợ Bến Thành, Q1",
                "📍 Phố đi bộ Nguyễn Huệ, Nguyễn Huệ, Q1",
                "📍 Khu phố Tây Bùi Viện, Bùi Viện - Đề Thám, Q1",
                "📍 Chợ Lớn, Khu phố Hoa, Q5"
            },
            ["Huế"] = new List<string>
            {
                "📍 Chợ Đông Ba, Chợ Đông Ba, Phú Hội",
                "📍 Bờ Nam sông Hương, Đường Lê Lợi, Vĩnh Ninh",
                "📍 Khu vực Bến Ngự, Đường Lê Lợi, Vĩnh Ninh"
            },
            ["Phú Quốc"] = new List<string>
            {
                "📍 Chợ đêm Phú Quốc, Bạch Đằng, Dương Đông",
                "📍 Khu ẩm thực Trần Hưng Đạo, Trần Hưng Đạo, Dương Đông"
            },
            ["Hải Phòng"] = new List<string>
            {
                "📍 Chợ đêm Đồ Sơn, Khu vực bãi biển Đồ Sơn",
                "📍 Khu ẩm thực Lạch Tray, Đường Lạch Tray, Ngô Quyền"
            },
            ["Quảng Ninh"] = new List<string>
            {
                "📍 Chợ đêm Hạ Long, Bãi Cháy, Hạ Long",
                "📍 Khu ẩm thực Vũ Văn Hiếu, Đường Vũ Văn Hiếu, Hạ Long"
            },
            ["Lào Cai"] = new List<string>
            {
                "📍 Chợ đêm Sapa, Đường Cầu Mây, Sa Pa",
                "📍 Khu ẩm thực trung tâm Sapa, Sa Pa"
            },
            ["Ninh Bình"] = new List<string>
            {
                "📍 Chợ đêm Ninh Bình, Trung tâm TP Ninh Bình",
                "📍 Khu ẩm thực Đường Hồng, Ninh Bình"
            },
            ["Thanh Hóa"] = new List<string>
            {
                "📍 Chợ đêm Sầm Sơn, Bãi biển Sầm Sơn",
                "📍 Khu ẩm thực Phan Chu Trinh, TP Thanh Hóa"
            },
            ["Nghệ An"] = new List<string>
            {
                "📍 Chợ đêm Cửa Lò, Bãi biển Cửa Lò",
                "📍 Khu ẩm thực Quang Trung, TP Vinh"
            },
            ["Quảng Bình"] = new List<string>
            {
                "📍 Chợ đêm Đồng Hới, Trung tâm Đồng Hới",
                "📍 Khu ẩm thực Phạm Văn Đồng, Đồng Hới"
            },
            ["Quảng Nam"] = new List<string>
            {
                "📍 Chợ đêm Hội An, Đường Nguyễn Hoàng, Hội An",
                "📍 Phố cổ Hội An về đêm, Khu phố cổ, Hội An"
            },
            ["Bình Định"] = new List<string>
            {
                "📍 Chợ đêm Quy Nhơn, Khu vực Chợ Quy Nhơn",
                "📍 Khu ẩm thực Xuân Diệu, Quy Nhơn"
            },
            ["Phú Yên"] = new List<string>
            {
                "📍 Chợ đêm Tuy Hòa, Trung tâm Tuy Hòa",
                "📍 Khu ẩm thực Trần Hưng Đạo, Tuy Hòa"
            },
            ["Khánh Hòa"] = new List<string>
            {
                "📍 Chợ đêm Nha Trang, Khu vực Chợ Đầm, Vĩnh Hải",
                "📍 Khu ẩm thực Trần Phú, Đường Trần Phú, Lộc Thọ"
            },
            ["Bình Thuận"] = new List<string>
            {
                "📍 Chợ đêm Phan Thiết, Trung tâm Phan Thiết",
                "📍 Khu ẩm thực Mũi Né, Đường Nguyễn Đình Chiểu, Mũi Né"
            },
            ["Lâm Đồng"] = new List<string>
            {
                "📍 Chợ đêm Đà Lạt, Nguyễn Thị Minh Khai, Đà Lạt",
                "📍 Khu ẩm thực Hòa Bình, Đà Lạt"
            },
            ["Đắk Lắk"] = new List<string>
            {
                "📍 Chợ đêm Buôn Ma Thuột, Trung tâm BMT",
                "📍 Khu ẩm thực Lê Duẩn, BMT"
            },
            ["Cần Thơ"] = new List<string>
            {
                "📍 Bến Ninh Kiều về đêm, Đường Hai Bà Trưng, Ninh Kiều",
                "📍 Chợ đêm Cần Thơ, Khu vực chợ Cần Thơ"
            },
            ["An Giang"] = new List<string>
            {
                "📍 Chợ đêm Châu Đốc, Trung tâm Châu Đốc",
                "📍 Khu ẩm thực Lê Lợi, Châu Đốc"
            },
            ["Kiên Giang"] = new List<string>
            {
                "📍 Chợ đêm Phú Quốc, Bạch Đằng, Dương Đông",
                "📍 Khu ẩm thực Trần Hưng Đạo, Dương Đông"
            }
        };

        public static string GetRestaurantAddress(string destination, string dishName)
        {
            if (RestaurantAddresses.TryGetValue(destination, out var addresses))
            {
                return addresses[_random.Next(addresses.Count)];
            }

            // Fallback với địa chỉ mặc định cũng cụ thể hơn
            return $"📍 Khu phố ẩm thực trung tâm {destination}";
        }

        public static string GetCafeAddress(string destination)
        {
            if (CafeAddresses.TryGetValue(destination, out var addresses))
            {
                return addresses[_random.Next(addresses.Count)];
            }

            return $"📍 Quán cafe view đẹp trung tâm {destination}";
        }

        public static string GetNightMarketAddress(string destination)
        {
            if (NightMarketAddresses.TryGetValue(destination, out var addresses))
            {
                return addresses[_random.Next(addresses.Count)];
            }

            return $"📍 Khu ẩm thực đêm {destination}";
        }

        public static string GetBreakfastAddress(string destination)
        {
            // Địa chỉ quán ăn sáng cụ thể
            if (RestaurantAddresses.TryGetValue(destination, out var addresses) && addresses.Count > 0)
            {
                return addresses[_random.Next(addresses.Count)];
            }

            return $"📍 Quán ăn sáng địa phương trung tâm {destination}";
        }
    }
}