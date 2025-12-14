using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class AddressData
    {
        private static Random _random = new Random();

        // Địa chỉ nhà hàng/quán ăn cụ thể (đã bổ sung quán chay cho mọi tỉnh)
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
                "📍 Chè Lam, 108 Nguyễn Đình Chiểu, Hai Bà Trưng",
                // >>> 10 QUÁN CHAY TẠI HÀ NỘI (Đã bổ sung) <<<
                "📍 Quán Chay Nàng Tấm, 79 Quán Sứ, Hoàn Kiếm",
                "📍 Ưu Đàm Chay, 34 Hàng Bài, Hoàn Kiếm",
                "📍 Veggie Castle (Buffet Chay), 7 Yên Ninh, Ba Đình",
                "📍 Cơm Chay Khai Tâm, 141 Triệu Việt Vương, Hai Bà Trưng",
                "📍 An Lạc Chay, 107 D11, Thái Thịnh, Đống Đa",
                "📍 V's Home Cooking, 5 Đặng Thai Mai, Tây Hồ",
                "📍 Buffet Chay Hương Thiền, 261 Xã Đàn, Đống Đa",
                "📍 Nhà hàng Chay Vô Ưu, 55 Nguyễn Trãi, Thanh Xuân",
                "📍 Quán Chay Bồ Đề Tâm, 88 Võ Thị Sáu, Hai Bà Trưng",
                "📍 Cơm Chay Tịnh Thực, 59 Nguyễn Chí Thanh, Ba Đình"
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
                "📍 Đậu Hủ Khói Phương Mai, 204 Phan Đình Phùng, Phường 2",
                // >>> 10 QUÁN CHAY TẠI ĐÀ LẠT (Đã bổ sung) <<<
                "📍 Quán chay Từ Hạnh, 14 Khe Sanh, Phường 10",
                "📍 Cơm Chay An Lạc, 24/2 Nhà Chung, Phường 3",
                "📍 Loving Hut (Ẩm thực quốc tế), 14 Khe Sanh, Phường 10",
                "📍 Cơm Chay Sen, 78 Phan Đình Phùng, Phường 2",
                "📍 Quán Chay Hoa Sen, 6 Thiện Ý, Phường 4",
                "📍 Vườn Hồng Chay, 37 Bùi Thị Xuân, Phường 2",
                "📍 Cơm Chay Giác Tâm, 57 Mai Hắc Đế, Phường 6",
                "📍 Lẩu Chay Chùa, 10 Trương Công Định, Phường 1",
                "📍 An Lạc Thiên Thuỷ, 185 Bùi Thị Xuân, Phường 2",
                "📍 Buffet Chay Đà Lạt, 12 Nguyễn Văn Cừ, Phường 1"
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
                "📍 Chè Hẻm 49, 49 Trần Quốc Toản, Hải Châu",
                // >>> 10 QUÁN CHAY TẠI ĐÀ NẴNG (Đã bổ sung) <<<
                "📍 Quán Chay Thiền Tâm, 90 Ngũ Hành Sơn, Ngũ Hành Sơn",
                "📍 Cơm Chay Diệu Hương, 182 Phan Châu Trinh, Hải Châu",
                "📍 Quán Chay An Lạc, 23/2A Lý Tự Trọng, Hải Châu",
                "📍 Quán Chay Từ Thiện, 34/4 Hoàng Diệu, Hải Châu",
                "📍 Cơm Chay Khai Tâm, 18 Lý Tự Trọng, Hải Châu",
                "📍 Quán Chay Rõ ràng, 85 Ngô Quyền, Sơn Trà",
                "📍 Chay Bát Nhã, 14/1 Lê Hồng Phong, Hải Châu",
                "📍 Buffet Chay Hạnh Phúc, 123 Nguyễn Văn Linh, Hải Châu",
                "📍 Bún Chay Cô Ba, 45 Lê Duẩn, Hải Châu",
                "📍 Mì Chay Tịnh Tâm, 68 Trần Quốc Toản, Hải Châu"
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
                "📍 Chè Bưởi Nha Trang, 20 Thống Nhất, Vĩnh Hải",
                // >>> 10 QUÁN CHAY TẠI NHA TRANG (Đã bổ sung) <<<
                "📍 Cơm Chay Bồ Đề, 89 Thống Nhất, Vạn Thắng",
                "📍 Quán Chay Sen, 12 Lê Thành Phương, Vạn Thắng",
                "📍 Cơm Chay Thiện Duyên, 100 Ngô Gia Tự, Phước Tiến",
                "📍 Quán Chay An Nhiên, 45 Nguyễn Thị Minh Khai, Lộc Thọ",
                "📍 Cơm Chay Tịnh Tâm, 32 Nguyễn Thiện Thuật, Lộc Thọ",
                "📍 Lẩu Nấm Chay, 60 Trần Phú, Lộc Thọ",
                "📍 Món Chay Âu Lạc, 12 Hùng Vương, Lộc Thọ",
                "📍 Quán Chay Phúc Lộc Thọ, 25 Pasteur, Xương Huân",
                "📍 Cơm Chay Hạnh Phúc, 77 Hoàng Văn Thụ, Phương Sài",
                "📍 Quán Chay Tịnh Độ, 15 Trần Quý Cáp, Vạn Thắng"
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
                "📍 Bánh Khọt Vũng Tàu 81, 81 Đề Thám, Q1",
                // >>> 10 QUÁN CHAY TẠI TP.HCM (Đã bổ sung) <<<
                "📍 Hum Vegetarian (Cao cấp), 32 Võ Văn Tần, Q3",
                "📍 Quán Chay Bùi Viện, 180 Nguyễn Công Trứ, Q1",
                "📍 Quán Chay Bông Súng, 86 Nguyễn Công Trứ, Q1",
                "📍 Chay Garden Restaurant, 52 Võ Văn Tần, Q3",
                "📍 Nhà hàng Chay Mandala, 11 Sương Nguyệt Ánh, Q1",
                "📍 Buffet Chay Chân Nguyên, 115 Nguyễn Thái Bình, Q1",
                "📍 Chay Âu Lạc, 13-15-17 Huỳnh Đình Hai, Bình Thạnh",
                "📍 Quán Chay Giác Ngộ, 39 Sư Vạn Hạnh, Q10",
                "📍 Cơm Chay Diệu Lý, 115 Trần Hưng Đạo, Q5",
                "📍 Lẩu Nấm Chay, 25 Nguyễn Bỉnh Khiêm, Q1"
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
                "📍 Bún Thịt Nướng Cô Mai, 17 Chí Lăng, Phú Hội",
                // >>> 10 QUÁN CHAY TẠI HUẾ (Đã bổ sung) <<<
                "📍 Quán Chay Bồ Đề, 11 Lê Lợi, Vĩnh Ninh",
                "📍 Cơm Chay Thanh Liễu, 50 Kim Long, Kim Long",
                "📍 Cơm Chay Tịnh Tâm, 41 Bến Nghé, Phú Hội",
                "📍 Quán Chay Liên Hoa, 3 Lê Quý Đôn, Phú Hội",
                "📍 Quán Chay Tường Vân, 143 Trương Công Định, Thuận Thành",
                "📍 Bánh Khoái Chay, 65 Nguyễn Công Trứ, Phú Hội",
                "📍 Phở Chay Huế, 32 Chi Lăng, Phú Hậu",
                "📍 Bún Chay Huế, 17 Nguyễn Khuyến, Phú Hội",
                "📍 Quán Chay Diệu Đế, 100 Lý Thái Tổ, Phú Hậu",
                "📍 Cơm Chay Hoa Đăng, 55 Lý Thường Kiệt, Phú Nhuận"
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
                "📍 Lẩu Hải Sản, 78 Bạch Đằng, Dương Đông",
                // >>> 10 QUÁN CHAY TẠI PHÚ QUỐC (Đã bổ sung) <<<
                "📍 Cơm Chay Bồ Đề, 100 Trần Hưng Đạo, Dương Đông",
                "📍 Quán Chay Hạnh Phúc, 24 Nguyễn Trung Trực, Dương Đông",
                "📍 Chay Thiên Lý, 123 30/4, Dương Đông",
                "📍 Quán Chay Phước Thiện, 67 Bạch Đằng, Dương Đông",
                "📍 Cơm Chay Phú Quốc, 40 Trần Hưng Đạo, Dương Đông",
                "📍 Quán Chay Sen, 78 Trần Hưng Đạo, Dương Đông",
                "📍 Cơm Chay An Nhiên, 11 Nguyễn Trãi, Dương Đông",
                "📍 Chay Vĩnh Nghiêm, 90 Nguyễn Trung Trực, Dương Đông",
                "📍 Quán Chay Từ Bi, 15 Dương Đông, Dương Đông",
                "📍 Lẩu Chay Phú Quốc, 55 30/4, Dương Đông"
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
                "📍 Bánh Bèo Hải Phòng, 25 Trần Phú, Ngô Quyền",
                // >>> 10 QUÁN CHAY TẠI HẢI PHÒNG (Đã bổ sung) <<<
                "📍 Quán Chay Tâm Liên, 22 Lương Khánh Thiện, Ngô Quyền",
                "📍 Cơm Chay An Lạc, 55 Cát Cụt, Lê Chân",
                "📍 Quán Chay Sen Vàng, 88 Hoàng Văn Thụ, Hồng Bàng",
                "📍 Buffet Chay Bồ Đề, 123 Đà Nẵng, Ngô Quyền",
                "📍 Quán Chay Vô Ưu, 45 Lê Lợi, Ngô Quyền",
                "📍 Cơm Chay Hạnh Phúc, 78 Tô Hiệu, Lê Chân",
                "📍 Quán Chay Thiện Tâm, 19 Điện Biên Phủ, Hồng Bàng",
                "📍 Lẩu Chay Hải Phòng, 67 Lạch Tray, Ngô Quyền",
                "📍 Món Chay Tịnh Độ, 11 Minh Khai, Hồng Bàng",
                "📍 Quán Chay Từ Bi, 33 Trần Phú, Ngô Quyền"
            },
            ["Quảng Ninh"] = new List<string>
            {
                "📍 Hải Sản Sống Hạ Long, Chợ Hạ Long, Bãi Cháy",
                "📍 Chả Mực Hạ Long, 98 Hạ Long, Bãi Cháy",
                "📍 Sứa Đỏ Quảng Ninh, 156 Vũ Văn Hiếu, Hạ Long",
                "📍 Nem Cua Bể, 45 Bạch Đằng, Hạ Long",
                "📍 Ốc Nướng Chợ Đêm, Chợ Đêm Hạ Long, Bãi Cháy",
                // >>> 10 QUÁN CHAY TẠI QUẢNG NINH (Đã bổ sung) <<<
                "📍 Cơm Chay A Di Đà, 325 Bãi Cháy, Hạ Long",
                "📍 Quán Chay Vạn Hạnh, 18 Lê Thánh Tông, Hạ Long",
                "📍 Quán Chay Tâm An, 68 Cái Dăm, Bãi Cháy",
                "📍 Quán Chay Phúc Lộc Thọ, 45 Trần Hưng Đạo, Hạ Long",
                "📍 Cơm Chay Thiện Tâm, 79 Nguyễn Văn Cừ, Hạ Long",
                "📍 Quán Chay Chùa Lôi Âm, 11 Hoàng Hoa Thám, Hạ Long",
                "📍 Buffet Chay Hạ Long, 123 Bãi Cháy, Hạ Long",
                "📍 Quán Chay Vô Lượng, 88 Vũ Văn Hiếu, Hạ Long",
                "📍 Cơm Chay Tịnh Độ, 55 Trần Phú, Cẩm Phả",
                "📍 Quán Chay Từ Hạnh, 22 Cao Xanh, Hạ Long"
            },
            ["Lào Cai"] = new List<string>
            {
                "📍 Thắng Cố Sapa, 15 Cầu Mây, Sa Pa",
                "📍 Cá Tầm Sapa, 89 Fansipan, Sa Pa",
                "📍 Lẩu Dê Đen, 45 Hoàng Liên, Sa Pa",
                "📍 Bánh Mì Chảo Sapa, 67 Xuân Viên, Sa Pa",
                // >>> 10 QUÁN CHAY TẠI LÀO CAI (Đã bổ sung) <<<
                "📍 Cơm Chay Sa Pa, 18 Thạch Sơn, Sa Pa",
                "📍 Quán Chay Tịnh Tâm, 56 Xuân Viên, Sa Pa",
                "📍 Chay Bồ Đề, 8 Cầu Mây, Sa Pa",
                "📍 Cơm Chay Lào Cai, 123 Nguyễn Huệ, TP Lào Cai",
                "📍 Quán Chay An Lạc, 45 Hàm Rồng, Sa Pa",
                "📍 Chay Tâm An, 78 Ngũ Chỉ Sơn, Sa Pa",
                "📍 Cơm Chay Thiện Duyên, 20 Fansipan, Sa Pa",
                "📍 Quán Chay Vô Ưu, 35 Cầu Mây, Sa Pa",
                "📍 Lẩu Chay Sapa, 99 Hoàng Liên, Sa Pa",
                "📍 Món Chay Tịnh Độ, 11 Thạch Sơn, Sa Pa"
            },
            ["Ninh Bình"] = new List<string>
            {
                "📍 Cơm Cháy Chả Cá, 12 Đường Hồng, Ninh Bình",
                "📍 Dê Núi Ninh Bình, 56 Lê Hồng Phong, Ninh Bình",
                "📍 Măng Tre Non, 23 Trần Hưng Đạo, Ninh Bình",
                "📍 Miến Lươn Tam Cốc, 89 Tam Cốc, Hoa Lư",
                // >>> 10 QUÁN CHAY TẠI NINH BÌNH (Đã bổ sung) <<<
                "📍 Cơm Chay Khánh Chi, 79 Tràng An, Hoa Lư",
                "📍 Quán Chay Phù Vân, 33 Nguyễn Công Trứ, TP Ninh Bình",
                "📍 Chay An Lạc, 123 Trần Hưng Đạo, TP Ninh Bình",
                "📍 Cơm Chay Vĩnh Nghiêm, 55 Dinh Tiên Hoàng, TP Ninh Bình",
                "📍 Quán Chay Hoa Lư, 18 Tam Cốc, Hoa Lư",
                "📍 Chay Tịnh Tâm, 67 Lê Hồng Phong, TP Ninh Bình",
                "📍 Cơm Chay Thiện Duyên, 20 Đường Hồng, TP Ninh Bình",
                "📍 Quán Chay Vô Ưu, 88 Xuân Thành, TP Ninh Bình",
                "📍 Lẩu Chay Ninh Bình, 99 Lê Thái Tổ, TP Ninh Bình",
                "📍 Món Chay Từ Bi, 11 Nguyễn Văn Trỗi, TP Ninh Bình"
            },
            ["Thanh Hóa"] = new List<string>
            {
                "📍 Nem Chua Thanh Hóa, 45 Phan Chu Trinh, TP Thanh Hóa",
                "📍 Chả Rươi, 78 Trần Phú, TP Thanh Hóa",
                "📍 Hải Sản Sầm Sơn, 123 Hồ Xuân Hương, Sầm Sơn",
                "📍 Cơm Lam Thanh Hóa, 56 Quang Trung, TP Thanh Hóa",
                // >>> 10 QUÁN CHAY TẠI THANH HÓA (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 77 Trường Thi, TP Thanh Hóa",
                "📍 Cơm Chay Tịnh Tâm, 123 Hàng Đồng, TP Thanh Hóa",
                "📍 Quán Chay Sen Vàng, 45 Phan Chu Trinh, TP Thanh Hóa",
                "📍 Buffet Chay Thanh Hóa, 88 Lê Hoàn, TP Thanh Hóa",
                "📍 Quán Chay Từ Bi, 22 Trần Phú, TP Thanh Hóa",
                "📍 Cơm Chay Thiện Duyên, 67 Bà Triệu, TP Thanh Hóa",
                "📍 Quán Chay Vô Ưu, 19 Hạc Thành, TP Thanh Hóa",
                "📍 Lẩu Chay Sầm Sơn, 55 Hồ Xuân Hương, Sầm Sơn",
                "📍 Món Chay Tịnh Độ, 99 Nguyễn Trãi, TP Thanh Hóa",
                "📍 Quán Chay Phước Thiện, 11 Lê Quý Đôn, TP Thanh Hóa"
            },
            ["Nghệ An"] = new List<string>
            {
                "📍 Bánh Mướt Nghệ An, 34 Quang Trung, TP Vinh",
                "📍 Chả Cốm Cửa Lò, 89 Bình Minh, Cửa Lò",
                "📍 Hải Sản Cửa Lò, 123 Bãi Biển, Cửa Lò",
                "📍 Nem Chua Yên Thành, 67 Hà Huy Tập, TP Vinh",
                // >>> 10 QUÁN CHAY TẠI NGHỆ AN (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Quang Trung, TP Vinh",
                "📍 Cơm Chay Tịnh Tâm, 45 Nguyễn Thị Minh Khai, TP Vinh",
                "📍 Quán Chay Sen, 78 Nguyễn Du, TP Vinh",
                "📍 Buffet Chay Nghệ An, 99 Lê Lợi, TP Vinh",
                "📍 Quán Chay Từ Bi, 22 Trần Phú, TP Vinh",
                "📍 Cơm Chay Thiện Duyên, 67 Nguyễn Văn Cừ, TP Vinh",
                "📍 Quán Chay Vô Ưu, 19 Lê Hồng Phong, TP Vinh",
                "📍 Lẩu Chay Cửa Lò, 55 Bình Minh, Cửa Lò",
                "📍 Món Chay Tịnh Độ, 99 Hà Huy Tập, TP Vinh",
                "📍 Quán Chay Phước Thiện, 11 Hồ Tùng Mậu, TP Vinh"
            },
            ["Quảng Bình"] = new List<string>
            {
                "📍 Ram Tép Quảng Bình, 45 Phạm Văn Đồng, Đồng Hới",
                "📍 Bánh Xèo Tôm Nhảy, 78 Quách Xuân Kỳ, Đồng Hới",
                "📍 Cháo Canh Quảng Bình, 123 Trương Phước Phan, Đồng Hới",
                "📍 Bún Măng Vịt, 56 Lý Thường Kiệt, Đồng Hới",
                // >>> 10 QUÁN CHAY TẠI QUẢNG BÌNH (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Trần Hưng Đạo, Đồng Hới",
                "📍 Cơm Chay Tịnh Tâm, 45 Phan Bội Châu, Đồng Hới",
                "📍 Quán Chay Sen Vàng, 78 Quách Xuân Kỳ, Đồng Hới",
                "📍 Buffet Chay Đồng Hới, 99 Lý Thường Kiệt, Đồng Hới",
                "📍 Quán Chay Từ Bi, 22 Quang Trung, Đồng Hới",
                "📍 Cơm Chay Thiện Duyên, 67 Phạm Văn Đồng, Đồng Hới",
                "📍 Quán Chay Vô Ưu, 19 Hùng Vương, Đồng Hới",
                "📍 Lẩu Chay Đồng Hới, 55 Nguyễn Hữu Cảnh, Đồng Hới",
                "📍 Món Chay Tịnh Độ, 99 Mẹ Suốt, Đồng Hới",
                "📍 Quán Chay Phước Thiện, 11 Lê Lợi, Đồng Hới"
            },
            ["Quảng Nam"] = new List<string>
            {
                "📍 Cao Lầu Hội An, 26 Thái Phiên, Hội An",
                "📍 Bánh Mì Phượng, 2B Phan Châu Trinh, Hội An",
                "📍 Mỳ Quảng Bà Mua, 6B Trưng Nữ Vương, Hội An",
                "📍 Cơm Gà Hội An, 12 Bà Triệu, Hội An",
                "📍 Bánh Bao Bánh Vạc, 45 Nguyễn Thị Minh Khai, Hội An",
                // >>> 10 QUÁN CHAY TẠI QUẢNG NAM (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Trần Hưng Đạo, Hội An",
                "📍 Cơm Chay Tịnh Tâm, 45 Phan Châu Trinh, Hội An",
                "📍 Quán Chay Sen Vàng, 78 Lê Lợi, Hội An",
                "📍 Buffet Chay Hội An, 99 Lý Thường Kiệt, Hội An",
                "📍 Quán Chay Từ Bi, 22 Trần Phú, Hội An",
                "📍 Cơm Chay Thiện Duyên, 67 Bà Triệu, Hội An",
                "📍 Quán Chay Vô Ưu, 19 Nguyễn Thị Minh Khai, Hội An",
                "📍 Lẩu Chay Hội An, 55 Cửa Đại, Hội An",
                "📍 Món Chay Tịnh Độ, 99 Hùng Vương, Hội An",
                "📍 Quán Chay Phước Thiện, 11 Thái Phiên, Hội An"
            },
            ["Bình Định"] = new List<string>
            {
                "📍 Bánh Hỏi Lòng Heo, 78 Nguyễn Huệ, Quy Nhơn",
                "📍 Nhum Biển Quy Nhơn, 123 Xuân Diệu, Quy Nhơn",
                "📍 Bánh Ít Lá Gai, 45 Trần Hưng Đạo, Quy Nhơn",
                "📍 Chả Ram Tôm Đất, 56 Lê Hồng Phong, Quy Nhơn",
                // >>> 10 QUÁN CHAY TẠI BÌNH ĐỊNH (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Nguyễn Huệ, Quy Nhơn",
                "📍 Cơm Chay Tịnh Tâm, 45 Lê Lợi, Quy Nhơn",
                "📍 Quán Chay Sen Vàng, 78 Xuân Diệu, Quy Nhơn",
                "📍 Buffet Chay Quy Nhơn, 99 Trần Hưng Đạo, Quy Nhơn",
                "📍 Quán Chay Từ Bi, 22 Bạch Đằng, Quy Nhơn",
                "📍 Cơm Chay Thiện Duyên, 67 Phan Chu Trinh, Quy Nhơn",
                "📍 Quán Chay Vô Ưu, 19 Ngô Mây, Quy Nhơn",
                "📍 Lẩu Chay Quy Nhơn, 55 Nguyễn Thái Học, Quy Nhơn",
                "📍 Món Chay Tịnh Độ, 99 Trần Cao Vân, Quy Nhơn",
                "📍 Quán Chay Phước Thiện, 11 Nguyễn Trãi, Quy Nhơn"
            },
            ["Phú Yên"] = new List<string>
            {
                "📍 Bánh Hỏi Cháo Lòng, 34 Ngô Gia Tự, Tuy Hòa",
                "📍 Bánh Căn Mini, 67 Trần Hưng Đạo, Tuy Hòa",
                "📍 Cá Ngừ Đại Dương, 89 Lê Duẩn, Tuy Hòa",
                "📍 Ốc Phú Yên, 123 Hùng Vương, Tuy Hòa",
                // >>> 10 QUÁN CHAY TẠI PHÚ YÊN (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Hùng Vương, Tuy Hòa",
                "📍 Cơm Chay Tịnh Tâm, 45 Lê Duẩn, Tuy Hòa",
                "📍 Quán Chay Sen Vàng, 78 Trần Hưng Đạo, Tuy Hòa",
                "📍 Buffet Chay Tuy Hòa, 99 Nguyễn Huệ, Tuy Hòa",
                "📍 Quán Chay Từ Bi, 22 Bạch Đằng, Tuy Hòa",
                "📍 Cơm Chay Thiện Duyên, 67 Ngô Gia Tự, Tuy Hòa",
                "📍 Quán Chay Vô Ưu, 19 Duy Tân, Tuy Hòa",
                "📍 Lẩu Chay Phú Yên, 55 Trần Phú, Tuy Hòa",
                "📍 Món Chay Tịnh Độ, 99 Nguyễn Công Trứ, Tuy Hòa",
                "📍 Quán Chay Phước Thiện, 11 Lê Lợi, Tuy Hòa"
            },
            ["Khánh Hòa"] = new List<string>
            {
                "📍 Bún Cá Nha Trang, 32 Nguyễn Thị Minh Khai, Nha Trang",
                "📍 Nem Nướng Ninh Hòa, 78 Yersin, Nha Trang",
                "📍 Bánh Căn Cô Ba, 45 Hoàng Hoa Thám, Nha Trang",
                "📍 Bún Sứa Nha Trang, 67 Pasteur, Nha Trang",
                "📍 Bánh Canh Chả Cá, 123 Nguyễn Thiện Thuật, Nha Trang",
                // >>> 10 QUÁN CHAY TẠI KHÁNH HÒA (Đã bổ sung) <<<
                "📍 Cơm Chay Bồ Đề, 89 Thống Nhất, Nha Trang",
                "📍 Quán Chay Sen, 12 Lê Thành Phương, Nha Trang",
                "📍 Cơm Chay Thiện Duyên, 100 Ngô Gia Tự, Nha Trang",
                "📍 Quán Chay An Nhiên, 45 Nguyễn Thị Minh Khai, Nha Trang",
                "📍 Cơm Chay Tịnh Tâm, 32 Nguyễn Thiện Thuật, Nha Trang",
                "📍 Lẩu Nấm Chay, 60 Trần Phú, Nha Trang",
                "📍 Món Chay Âu Lạc, 12 Hùng Vương, Nha Trang",
                "📍 Quán Chay Phúc Lộc Thọ, 25 Pasteur, Nha Trang",
                "📍 Cơm Chay Hạnh Phúc, 77 Hoàng Văn Thụ, Nha Trang",
                "📍 Quán Chay Tịnh Độ, 15 Trần Quý Cáp, Nha Trang"
            },
            ["Bình Thuận"] = new List<string>
            {
                "📍 Bánh Căn Phan Thiết, 45 Nguyễn Tất Thành, Phan Thiết",
                "📍 Bánh Xèo Tôm Nhảy, 78 Trần Hưng Đạo, Phan Thiết",
                "📍 Lẩu Thả Phan Thiết, 123 Nguyễn Huệ, Phan Thiết",
                "📍 Hải Sản Mũi Né, 234 Nguyễn Đình Chiểu, Mũi Né",
                // >>> 10 QUÁN CHAY TẠI BÌNH THUẬN (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Nguyễn Huệ, Phan Thiết",
                "📍 Cơm Chay Tịnh Tâm, 45 Trần Hưng Đạo, Phan Thiết",
                "📍 Quán Chay Sen Vàng, 78 Nguyễn Tất Thành, Phan Thiết",
                "📍 Buffet Chay Phan Thiết, 99 Thủ Khoa Huân, Phan Thiết",
                "📍 Quán Chay Từ Bi, 22 Võ Văn Kiệt, Phan Thiết",
                "📍 Cơm Chay Thiện Duyên, 67 Lê Hồng Phong, Phan Thiết",
                "📍 Quán Chay Vô Ưu, 19 Hải Thượng Lãn Ông, Phan Thiết",
                "📍 Lẩu Chay Mũi Né, 55 Nguyễn Đình Chiểu, Mũi Né",
                "📍 Món Chay Tịnh Độ, 99 Trần Quý Cáp, Phan Thiết",
                "📍 Quán Chay Phước Thiện, 11 Lý Thường Kiệt, Phan Thiết"
            },
            ["Lâm Đồng"] = new List<string>
            {
                "📍 Lẩu Dê Đà Lạt, 56 Phan Đình Phùng, Đà Lạt",
                "📍 Nem Nướng Đà Lạt, 89 Bùi Thị Xuân, Đà Lạt",
                "📍 Bánh Căn Mini, 123 Nguyễn Thị Minh Khai, Đà Lạt",
                "📍 Bánh Ướt Lòng Gà, 45 Hoàng Diệu, Đà Lạt"
                // Lưu ý: Đà Lạt đã được liệt kê ở trên
            },
            ["Đắk Lắk"] = new List<string>
            {
                "📍 Lẩu Cá Linh Bông Điên Điển, 45 Lê Duẩn, BMT",
                "📍 Nem Nướng Gia Lai, 78 Phan Chu Trinh, BMT",
                "📍 Cơm Lam Tây Nguyên, 123 Lý Thường Kiệt, BMT",
                "📍 Gà Nướng Tây Nguyên, 56 Trần Nhật Duật, BMT",
                // >>> 10 QUÁN CHAY TẠI ĐẮK LẮK (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Lê Duẩn, BMT",
                "📍 Cơm Chay Tịnh Tâm, 45 Phan Chu Trinh, BMT",
                "📍 Quán Chay Sen Vàng, 78 Lý Thường Kiệt, BMT",
                "📍 Buffet Chay BMT, 99 Nguyễn Công Trứ, BMT",
                "📍 Quán Chay Từ Bi, 22 Trần Hưng Đạo, BMT",
                "📍 Cơm Chay Thiện Duyên, 67 Phan Bội Châu, BMT",
                "📍 Quán Chay Vô Ưu, 19 Y Jút, BMT",
                "📍 Lẩu Chay Tây Nguyên, 55 Nguyễn Chí Thanh, BMT",
                "📍 Món Chay Tịnh Độ, 99 Ama Khê, BMT",
                "📍 Quán Chay Phước Thiện, 11 Ngô Quyền, BMT"
            },
            ["Cần Thơ"] = new List<string>
            {
                "📍 Bánh Xèo Cần Thơ, 34 Hai Bà Trưng, Ninh Kiều",
                "📍 Lẩu Mắm Cần Thơ, 67 Nguyễn An Ninh, Ninh Kiều",
                "📍 Hủ Tiếu Cần Thơ, 89 Mậu Thân, Ninh Kiều",
                "📍 Bún Riêu Cua Đồng, 123 Trần Phú, Ninh Kiều",
                // >>> 10 QUÁN CHAY TẠI CẦN THƠ (Đã bổ sung) <<<
                "📍 Quán Chay 173, 173 Trần Hưng Đạo, Ninh Kiều",
                "📍 Cơm Chay Bồ Đề, 89 Hùng Vương, Ninh Kiều",
                "📍 Quán Chay Thiện Duyên, 56 Xô Viết Nghệ Tĩnh, Ninh Kiều",
                "📍 Buffet Chay Cần Thơ, 99 Lý Tự Trọng, Ninh Kiều",
                "📍 Quán Chay Từ Bi, 22 Mậu Thân, Ninh Kiều",
                "📍 Cơm Chay An Lạc, 67 Nguyễn An Ninh, Ninh Kiều",
                "📍 Quán Chay Vô Ưu, 19 Trần Văn Khéo, Ninh Kiều",
                "📍 Lẩu Chay Miền Tây, 55 Hai Bà Trưng, Ninh Kiều",
                "📍 Món Chay Tịnh Độ, 99 Trần Phú, Ninh Kiều",
                "📍 Quán Chay Phước Thiện, 11 Ngô Quyền, Ninh Kiều"
            },
            ["An Giang"] = new List<string>
            {
                "📍 Bún Nước Lèo, 45 Tôn Đức Thắng, Châu Đốc",
                "📍 Lẩu Mắm U Minh, 78 Lê Lợi, Châu Đốc",
                "📍 Cá Lóc Nướng Trui, 123 Núi Sam, Châu Đốc",
                "📍 Bánh Xèo Miền Tây, 56 Nguyễn Văn Thoại, Châu Đốc",
                // >>> 10 QUÁN CHAY TẠI AN GIANG (Đã bổ sung) <<<
                "📍 Quán Chay An Lạc, 123 Tôn Đức Thắng, Long Xuyên",
                "📍 Cơm Chay Tịnh Tâm, 45 Lê Lợi, Châu Đốc",
                "📍 Quán Chay Sen Vàng, 78 Trần Hưng Đạo, Long Xuyên",
                "📍 Buffet Chay Châu Đốc, 99 Thủ Khoa Huân, Châu Đốc",
                "📍 Quán Chay Từ Bi, 22 Nguyễn Văn Thoại, Châu Đốc",
                "📍 Cơm Chay Thiện Duyên, 67 Nguyễn Trãi, Long Xuyên",
                "📍 Quán Chay Vô Ưu, 19 Thoại Ngọc Hầu, Châu Đốc",
                "📍 Lẩu Chay Núi Sam, 55 Núi Sam, Châu Đốc",
                "📍 Món Chay Tịnh Độ, 99 Phan Huy Chú, Long Xuyên",
                "📍 Quán Chay Phước Thiện, 11 Hai Bà Trưng, Châu Đốc"
            },
            ["Kiên Giang"] = new List<string>
            {
                "📍 Hải Sản Phú Quốc, 234 Trần Hưng Đạo, Dương Đông",
                "📍 Bún Quậy Phú Quốc, 123 30/4, Dương Đông",
                "📍 Ghẹ Hấp Phú Quốc, 89 Nguyễn Trung Trực, Dương Đông",
                "📍 Nhum Nướng Phú Quốc, 67 Bạch Đằng, Dương Đông",
                // >>> 10 QUÁN CHAY TẠI KIÊN GIANG (Đã bổ sung) <<<
                "📍 Cơm Chay Bồ Đề, 100 Trần Hưng Đạo, Rạch Giá",
                "📍 Quán Chay Hạnh Phúc, 24 Nguyễn Trung Trực, Phú Quốc",
                "📍 Chay Thiên Lý, 123 30/4, Rạch Giá",
                "📍 Quán Chay Phước Thiện, 67 Bạch Đằng, Phú Quốc",
                "📍 Cơm Chay Phú Quốc, 40 Trần Hưng Đạo, Phú Quốc",
                "📍 Quán Chay Sen, 78 Trần Hưng Đạo, Rạch Giá",
                "📍 Cơm Chay An Nhiên, 11 Nguyễn Trãi, Rạch Giá",
                "📍 Chay Vĩnh Nghiêm, 90 Nguyễn Trung Trực, Rạch Giá",
                "📍 Quán Chay Từ Bi, 15 Dương Đông, Phú Quốc",
                "📍 Lẩu Chay Rạch Giá, 55 Trần Phú, Rạch Giá"
            },
            // DANH SÁCH CHUNG CHO CÁC MÓN CHAY (Key: "Chay")
            ["Chay"] = new List<string>
            {
                "📍 Quán Chay Phổ Hiền, 123 Lương Định Của",
                "📍 Cơm Chay Tịnh Tâm, 45 Trần Quang Khải",
                "📍 Nhà hàng Chay Bồ Đề Tâm, 88 Võ Thị Sáu",
                "📍 Quán Chay Vô Ưu, 55 Nguyễn Trãi",
                "📍 Cơm Chay Thiện Duyên, 100 Ngô Gia Tự",
                "📍 Quán Chay An Lạc, 77 Trường Thi",
                "📍 Buffet Chay Sen Vàng, 99 Lý Thường Kiệt",
                "📍 Món Chay Từ Bi, 22 Trần Hưng Đạo",
                "📍 Quán Chay Phước Thiện, 11 Lê Lợi",
                "📍 Lẩu Chay Thanh Đạm, 55 Nguyễn Du"
            }
        };

        // Địa chỉ quán cafe cụ thể (Giữ nguyên)
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

        // Địa chỉ chợ đêm/khu ẩm thực (Giữ nguyên)
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

        // =======================================================
        // ✅ FIX LỖI OVERLOAD CHO GETBREAKFASTADDRESS
        // =======================================================

        // Phương thức gốc (chỉ nhận điểm đến)
        public static string GetBreakfastAddress(string destination)
        {
            // Logic tương tự GetRestaurantAddress cho món ăn sáng
            if (RestaurantAddresses.TryGetValue(destination, out var addresses) && addresses.Count > 0)
            {
                // Loại bỏ các quán chay trong fallback mặc định nếu không có tên món cụ thể
                var nonVegAddresses = addresses.Where(a => a != null && a.IndexOf("Chay", StringComparison.OrdinalIgnoreCase) < 0).ToList();
                if (nonVegAddresses != null && nonVegAddresses.Count > 0)
                {
                    return nonVegAddresses[_random.Next(nonVegAddresses.Count)];
                }
            }

            return $"📍 Quán ăn sáng địa phương trung tâm {destination}";
        }

        // ✅ PHƯƠNG THỨC OVERLOAD ĐÃ FIX (Nhận 2 đối số)
        public static string GetBreakfastAddress(string destination, string dishName)
        {
            // Nếu có tên món ăn cụ thể, ta thử tìm quán chay/mặn phù hợp
            if (!string.IsNullOrWhiteSpace(dishName))
            {
                bool isVegetarian = dishName.IndexOf("Chay", StringComparison.OrdinalIgnoreCase) >= 0;

                if (RestaurantAddresses.TryGetValue(destination, out var localAddresses))
                {
                    var filteredAddresses = localAddresses.Where(a => a.IndexOf(isVegetarian ? "Chay" : "📍 Quán", StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                    if (filteredAddresses != null && filteredAddresses.Count > 0)
                    {
                        // Chọn một quán ngẫu nhiên từ danh sách đã lọc
                        return filteredAddresses[_random.Next(filteredAddresses.Count)];
                    }
                }

                // Fallback nếu không tìm thấy địa chỉ cụ thể: Dùng tên món ăn
                return $"📍 Quán {dishName} tại khu vực {destination}";
            }

            // Fallback về phương thức gốc
            return GetBreakfastAddress(destination);
        }

        // =======================================================
        // ✅ FIX LOGIC VÀ CHUẨN HÓA LẠI GETRESTAURANTADDRESS (Đã có 2 tham số)
        // =======================================================
        public static string GetRestaurantAddress(string destination, string dishName)
        {
            // 1. Kiểm tra chế độ ăn
            bool isVegetarian = dishName.IndexOf("Chay", StringComparison.OrdinalIgnoreCase) >= 0;

            if (RestaurantAddresses.TryGetValue(destination, out var addresses))
            {
                if (isVegetarian)
                {
                    // Lọc lấy các quán có chữ "Chay"
                    var vegAddresses = addresses.Where(a => a != null && a.IndexOf("Chay", StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    if (vegAddresses != null && vegAddresses.Count > 0)
                    {
                        return vegAddresses[_random.Next(vegAddresses.Count)];
                    }
                }
                else
                {
                    // Lọc lấy các quán KHÔNG có chữ "Chay" (Ăn Mặn)
                    var nonVegAddresses = addresses.Where(a => a != null && a.IndexOf("Chay", StringComparison.OrdinalIgnoreCase) < 0).ToList();
                    if (nonVegAddresses != null && nonVegAddresses.Count > 0)
                    {
                        return nonVegAddresses[_random.Next(nonVegAddresses.Count)];
                    }
                }
            }

            // Fallback nếu không có địa chỉ cụ thể cho chế độ ăn đó
            return $"📍 Khu phố ẩm thực {(isVegetarian ? "Chay " : "")} trung tâm {destination}";
        }


        public static string GetCafeAddress(string destination)
        {
            if (CafeAddresses.TryGetValue(destination, out var addresses) && addresses != null && addresses.Count > 0)
            {
                return addresses[_random.Next(addresses.Count)];
            }

            return $"📍 Quán cafe view đẹp trung tâm {destination}";
        }

        public static string GetNightMarketAddress(string destination)
        {
            if (NightMarketAddresses.TryGetValue(destination, out var addresses) && addresses != null && addresses.Count > 0)
            {
                return addresses[_random.Next(addresses.Count)];
            }

            return $"📍 Khu ẩm thực đêm {destination}";
        }
    }
}