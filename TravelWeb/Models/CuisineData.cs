using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;

namespace TravelWeb.Models
{
    public static class CuisineData
    {
        private static readonly List<CuisineItem> Items = new List<CuisineItem>
            {
            // ======= MIỀN BẮC =======
            // Hà Nội (Tổng 20 món ăn thường + 10 món chay)
            new CuisineItem { Province = "Hà Nội", Name = "Phở bò", Description = "Biểu tượng ẩm thực Việt, nước dùng thanh ngọt.", AveragePrice = 40000, Popularity = 100 },
            new CuisineItem { Province = "Hà Nội", Name = "Bún chả", Description = "Thịt nướng ăn cùng bún và nước chấm chua ngọt.", AveragePrice = 45000, Popularity = 98 },
            new CuisineItem { Province = "Hà Nội", Name = "Chả cá Lã Vọng", Description = "Cá chiên nghệ ăn kèm thì là, hành lá.", AveragePrice = 150000, Popularity = 96 },
            new CuisineItem { Province = "Hà Nội", Name = "Bún thang", Description = "Món bún cầu kỳ từ trứng, gà, giò.", AveragePrice = 50000, Popularity = 92 },
            new CuisineItem { Province = "Hà Nội", Name = "Xôi xéo", Description = "Xôi nếp vàng ăn với hành phi.", AveragePrice = 20000, Popularity = 90 },
            new CuisineItem { Province = "Hà Nội", Name = "Bánh cuốn Thanh Trì", Description = "Bánh mỏng dai, ăn kèm nước mắm.", AveragePrice = 30000, Popularity = 88 },
            new CuisineItem { Province = "Hà Nội", Name = "Trà chanh Nhà Thờ", Description = "Đồ uống đường phố nổi tiếng giới trẻ.", AveragePrice = 15000, Popularity = 85 },
            new CuisineItem { Province = "Hà Nội", Name = "Bánh tôm Hồ Tây", Description = "Tôm chiên giòn ăn cùng rau sống.", AveragePrice = 45000, Popularity = 91 },
            new CuisineItem { Province = "Hà Nội", Name = "Nem rán", Description = "Nem cuốn rán giòn, nhân thịt mộc nhĩ.", AveragePrice = 40000, Popularity = 93 },
            new CuisineItem { Province = "Hà Nội", Name = "Cà phê trứng", Description = "Cà phê béo ngậy với lòng đỏ trứng.", AveragePrice = 40000, Popularity = 94 },
            // THÊM 10 MÓN THƯỜNG HÀ NỘI
            new CuisineItem { Province = "Hà Nội", Name = "Bún riêu cua", Description = "Bún với riêu cua đồng, cà chua và đậu phụ.", AveragePrice = 35000, Popularity = 87 },
            new CuisineItem { Province = "Hà Nội", Name = "Bánh mì Dân Sinh", Description = "Bánh mì kẹp đầy đủ thịt nguội, pate.", AveragePrice = 25000, Popularity = 89 },
            new CuisineItem { Province = "Hà Nội", Name = "Nộm bò khô", Description = "Gỏi đu đủ trộn thịt bò khô, chua ngọt cay.", AveragePrice = 30000, Popularity = 86 },
            new CuisineItem { Province = "Hà Nội", Name = "Phở cuốn", Description = "Bánh phở cuộn thịt bò, rau sống, chấm nước mắm chua ngọt.", AveragePrice = 40000, Popularity = 84 },
            new CuisineItem { Province = "Hà Nội", Name = "Kem Tràng Tiền", Description = "Kem que truyền thống với nhiều vị.", AveragePrice = 15000, Popularity = 81 },
            new CuisineItem { Province = "Hà Nội", Name = "Bún đậu mắm tôm", Description = "Đậu phụ chiên, bún, chả cốm ăn kèm mắm tôm.", AveragePrice = 45000, Popularity = 97 },
            new CuisineItem { Province = "Hà Nội", Name = "Cháo sườn", Description = "Cháo sườn nóng, quẩy giòn.", AveragePrice = 20000, Popularity = 83 },
            new CuisineItem { Province = "Hà Nội", Name = "Ốc luộc", Description = "Ốc nóng chấm mắm gừng.", AveragePrice = 50000, Popularity = 82 },
            new CuisineItem { Province = "Hà Nội", Name = "Tào phớ", Description = "Tào phớ thạch, nước cốt dừa, trân châu.", AveragePrice = 15000, Popularity = 80 },
            new CuisineItem { Province = "Hà Nội", Name = "Cà phê cốt dừa", Description = "Cà phê đen đá xay với cốt dừa béo ngậy.", AveragePrice = 45000, Popularity = 95 },
            
            // Món Chay Hà Nội
            new CuisineItem { Province = "Hà Nội", Name = "Phở cuốn Chay", Description = "Phở cuốn rau củ thanh mát, chấm xì dầu.", AveragePrice = 30000, Popularity = 80 },
            new CuisineItem { Province = "Hà Nội", Name = "Bún ốc chuối đậu Chay", Description = "Bún riêu chay nấu chuối xanh, đậu phụ.", AveragePrice = 45000, Popularity = 78 },
            new CuisineItem { Province = "Hà Nội", Name = "Bánh mì Pate Chay", Description = "Bánh mì chay với pate đậu xanh nấm.", AveragePrice = 25000, Popularity = 75 },
            new CuisineItem { Province = "Hà Nội", Name = "Nem chua Chay", Description = "Nem làm từ bì chay và thính gạo.", AveragePrice = 35000, Popularity = 70 },
            new CuisineItem { Province = "Hà Nội", Name = "Lẩu nấm Chay", Description = "Lẩu thập cẩm các loại nấm tươi.", AveragePrice = 180000, Popularity = 82 },
            new CuisineItem { Province = "Hà Nội", Name = "Bún riêu cua Chay", Description = "Bún riêu chay nấu riêu đậu phụ.", AveragePrice = 40000, Popularity = 79 },
            new CuisineItem { Province = "Hà Nội", Name = "Chả giò Chay", Description = "Chả giò cuốn rau củ, miến, nấm.", AveragePrice = 35000, Popularity = 77 },
            new CuisineItem { Province = "Hà Nội", Name = "Nộm rau muống Chay", Description = "Nộm rau muống trộn lạc, chanh ớt.", AveragePrice = 25000, Popularity = 73 },
            new CuisineItem { Province = "Hà Nội", Name = "Miến xào nấm Chay", Description = "Miến dong xào với nhiều loại nấm.", AveragePrice = 35000, Popularity = 72 },
            new CuisineItem { Province = "Hà Nội", Name = "Bánh đúc nóng Chay", Description = "Bánh đúc nóng ăn kèm đậu phụ chiên.", AveragePrice = 20000, Popularity = 70 },

            // Hải Phòng (Tổng 20 món ăn thường + 10 món chay)
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh đa cua", Description = "Sợi bánh đa đỏ, nước cua đậm đà.", AveragePrice = 35000, Popularity = 95 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh mì cay", Description = "Ổ bánh nhỏ với pate cay đặc trưng.", AveragePrice = 10000, Popularity = 92 },
            new CuisineItem { Province = "Hải Phòng", Name = "Nem cua bể", Description = "Nem cuốn to nhân cua biển.", AveragePrice = 60000, Popularity = 90 },
            new CuisineItem { Province = "Hải Phòng", Name = "Ốc các loại", Description = "Hải sản tươi hấp dẫn giới trẻ.", AveragePrice = 70000, Popularity = 87 },
            new CuisineItem { Province = "Hải Phòng", Name = "Chè giun", Description = "Chè màu xanh mát mắt, vị ngọt thanh.", AveragePrice = 20000, Popularity = 82 },
            new CuisineItem { Province = "Hải Phòng", Name = "Lẩu cua đồng", Description = "Lẩu đặc sản đất Cảng.", AveragePrice = 150000, Popularity = 88 },
            new CuisineItem { Province = "Hải Phòng", Name = "Cơm rang cua bể", Description = "Cơm rang vàng với thịt cua.", AveragePrice = 80000, Popularity = 85 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh bèo Hải Phòng", Description = "Bánh dẻo nhân thịt, ăn với mắm.", AveragePrice = 25000, Popularity = 80 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bún cá cay", Description = "Bún cá đậm vị, cay nồng.", AveragePrice = 40000, Popularity = 89 },
            new CuisineItem { Province = "Hải Phòng", Name = "Sứa đỏ", Description = "Đặc sản mùa hè, chấm mắm tôm.", AveragePrice = 35000, Popularity = 83 },
            // THÊM 10 MÓN THƯỜNG HẢI PHÒNG
            new CuisineItem { Province = "Hải Phòng", Name = "Bún tôm", Description = "Bún nấu nước dùng tôm, đậm đà vị biển.", AveragePrice = 45000, Popularity = 86 },
            new CuisineItem { Province = "Hải Phòng", Name = "Gỏi tôm cá", Description = "Gỏi hải sản tươi sống đặc trưng.", AveragePrice = 75000, Popularity = 81 },
            new CuisineItem { Province = "Hải Phòng", Name = "Cháo trai", Description = "Cháo nấu với trai, quẩy giòn.", AveragePrice = 25000, Popularity = 79 },
            new CuisineItem { Province = "Hải Phòng", Name = "Lẩu thái hải sản", Description = "Lẩu thái chua cay với tôm, mực.", AveragePrice = 180000, Popularity = 84 },
            new CuisineItem { Province = "Hải Phòng", Name = "Ốc xào me", Description = "Ốc hương xào sốt me chua ngọt.", AveragePrice = 90000, Popularity = 91 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh cuốn nhân tôm", Description = "Bánh cuốn nóng nhân tôm tươi.", AveragePrice = 30000, Popularity = 78 },
            new CuisineItem { Province = "Hải Phòng", Name = "Kem dừa", Description = "Kem dừa Thái Lan, mát lạnh.", AveragePrice = 35000, Popularity = 77 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh gật gù", Description = "Bánh gạo mềm, ăn với nước chấm đặc biệt.", AveragePrice = 20000, Popularity = 75 },
            new CuisineItem { Province = "Hải Phòng", Name = "Chè dừa dầm", Description = "Chè dừa dầm béo ngậy, topping đa dạng.", AveragePrice = 25000, Popularity = 88 },
            new CuisineItem { Province = "Hải Phòng", Name = "Phở gà", Description = "Phở gà ta, nước dùng ngọt thanh.", AveragePrice = 40000, Popularity = 83 },
            
            // Món Chay Hải Phòng
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh đa Chay", Description = "Bánh đa chay nấu nước dùng rau củ, đậu phụ.", AveragePrice = 30000, Popularity = 75 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh mì que Chay", Description = "Bánh mì que giòn với pate chay.", AveragePrice = 8000, Popularity = 77 },
            new CuisineItem { Province = "Hải Phòng", Name = "Nem Vuông Chay", Description = "Nem vuông (nem cua bể) nhân nấm và đậu.", AveragePrice = 50000, Popularity = 70 },
            new CuisineItem { Province = "Hải Phòng", Name = "Súp Nấm Chay", Description = "Súp thập cẩm các loại nấm.", AveragePrice = 40000, Popularity = 68 },
            new CuisineItem { Province = "Hải Phòng", Name = "Chè kho Chay", Description = "Chè kho từ đậu xanh, ngọt nhẹ.", AveragePrice = 15000, Popularity = 65 },
            new CuisineItem { Province = "Hải Phòng", Name = "Canh Chua Chay", Description = "Canh chua chay với dọc mùng và đậu phụ.", AveragePrice = 45000, Popularity = 72 },
            new CuisineItem { Province = "Hải Phòng", Name = "Đậu phụ Tứ Xuyên Chay", Description = "Đậu phụ sốt cay Tứ Xuyên phiên bản chay.", AveragePrice = 55000, Popularity = 71 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bánh bèo Chay", Description = "Bánh bèo dẻo nhân đậu xanh.", AveragePrice = 20000, Popularity = 67 },
            new CuisineItem { Province = "Hải Phòng", Name = "Bún Chả Lá Lốt Chay", Description = "Thịt chay gói lá lốt nướng.", AveragePrice = 35000, Popularity = 73 },
            new CuisineItem { Province = "Hải Phòng", Name = "Gỏi Cuốn Chay", Description = "Gỏi cuốn rau củ, bún, chấm xì dầu.", AveragePrice = 25000, Popularity = 69 },
            
            // ======= MIỀN TRUNG =======
            // Đà Nẵng (Tổng 20 món ăn thường + 10 món chay)
            new CuisineItem { Province = "Đà Nẵng", Name = "Mì Quảng", Description = "Món đặc sản biểu tượng của Đà Nẵng.", AveragePrice = 35000, Popularity = 99 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh tráng cuốn thịt heo", Description = "Món ăn dân dã, chấm mắm nêm.", AveragePrice = 60000, Popularity = 95 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bún mắm nêm", Description = "Hương vị mạnh mẽ, đậm đà miền Trung.", AveragePrice = 30000, Popularity = 90 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Hải sản nướng", Description = "Đa dạng hải sản tươi ngon.", AveragePrice = 150000, Popularity = 94 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh xèo", Description = "Bánh giòn, nhân tôm thịt giá đỗ.", AveragePrice = 40000, Popularity = 88 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Ram cuốn cải", Description = "Ram rán giòn ăn cùng rau sống.", AveragePrice = 30000, Popularity = 85 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh bèo nậm lọc", Description = "Ba loại bánh Huế du nhập được yêu thích.", AveragePrice = 35000, Popularity = 83 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Gỏi cá Nam Ô", Description = "Món ăn độc đáo từ cá sống.", AveragePrice = 60000, Popularity = 91 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Cao lầu", Description = "Món gốc Hội An, được ưa chuộng.", AveragePrice = 40000, Popularity = 87 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bún chả cá", Description = "Nước dùng thanh, cá tươi.", AveragePrice = 35000, Popularity = 93 },
            // THÊM 10 MÓN THƯỜNG ĐÀ NẴNG
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh canh ghẹ", Description = "Bánh canh với thịt ghẹ tươi, nước dùng ngọt.", AveragePrice = 55000, Popularity = 92 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bún thịt nướng", Description = "Bún với thịt heo nướng thơm lừng.", AveragePrice = 35000, Popularity = 89 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Chè chuối", Description = "Chè chuối, nước cốt dừa béo ngậy.", AveragePrice = 15000, Popularity = 81 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Ốc hút", Description = "Ốc xào sả ớt, cay nồng.", AveragePrice = 40000, Popularity = 84 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Cơm gà Hội An", Description = "Cơm gà xé phay, ăn kèm rau thơm.", AveragePrice = 45000, Popularity = 96 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Trứng vịt lộn", Description = "Trứng vịt lộn luộc, ăn với rau răm, gừng.", AveragePrice = 10000, Popularity = 77 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Kem bơ", Description = "Kem bơ Đà Nẵng, béo và mát.", AveragePrice = 30000, Popularity = 86 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Chả bò", Description = "Chả làm từ thịt bò tươi, ngon dai.", AveragePrice = 80000, Popularity = 78 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Thịt xiên nướng", Description = "Thịt heo tẩm ướp nướng than hoa.", AveragePrice = 10000, Popularity = 82 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh mì Phượng", Description = "Bánh mì đặc biệt từ Hội An.", AveragePrice = 25000, Popularity = 98 },

            // Món Chay Đà Nẵng
            new CuisineItem { Province = "Đà Nẵng", Name = "Mì Quảng Chay", Description = "Mì Quảng chay với nấm, đậu phụ.", AveragePrice = 30000, Popularity = 80 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh tráng cuốn Chay", Description = "Bánh tráng cuốn rau củ, đậu hũ, chấm tương bần.", AveragePrice = 50000, Popularity = 78 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Cao Lầu Chay", Description = "Cao lầu chay với nước sốt xì dầu, rau sống.", AveragePrice = 35000, Popularity = 75 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh Xèo Chay", Description = "Bánh xèo giòn tan nhân củ sắn, nấm.", AveragePrice = 35000, Popularity = 77 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Chè Chuối Nướng Chay", Description = "Chè chuối nước cốt dừa nướng.", AveragePrice = 20000, Popularity = 72 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bánh Bột Lọc Chay", Description = "Bánh lọc nhân đậu xanh.", AveragePrice = 25000, Popularity = 70 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Cơm Sen Chay", Description = "Cơm rang hạt sen, nấm, rau củ.", AveragePrice = 45000, Popularity = 74 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Lẩu Rau Tập Tàng Chay", Description = "Lẩu rau thập cẩm thanh đạm.", AveragePrice = 150000, Popularity = 79 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Ram Bắp Chay", Description = "Ram chiên giòn nhân bắp (ngô).", AveragePrice = 30000, Popularity = 73 },
            new CuisineItem { Province = "Đà Nẵng", Name = "Bún Chả Giò Chay", Description = "Bún trộn chả giò chay, rau sống.", AveragePrice = 35000, Popularity = 71 },

            // Huế (Tổng 20 món ăn thường + 10 món chay)
            new CuisineItem { Province = "Huế", Name = "Bún bò Huế", Description = "Món đặc trưng cố đô, cay nồng đậm đà.", AveragePrice = 40000, Popularity = 100 },
            new CuisineItem { Province = "Huế", Name = "Cơm hến", Description = "Dân dã, rẻ mà ngon, đặc sản nổi tiếng.", AveragePrice = 20000, Popularity = 90 },
            new CuisineItem { Province = "Huế", Name = "Bánh bèo", Description = "Bánh mỏng, nhân tôm, nước mắm tỏi ớt.", AveragePrice = 25000, Popularity = 88 },
            new CuisineItem { Province = "Huế", Name = "Bánh nậm", Description = "Bánh gói lá chuối, mềm thơm.", AveragePrice = 25000, Popularity = 87 },
            new CuisineItem { Province = "Huế", Name = "Bánh lọc", Description = "Trong suốt, nhân tôm thịt, dai dẻo.", AveragePrice = 30000, Popularity = 89 },
            new CuisineItem { Province = "Huế", Name = "Chè Huế", Description = "Chè thập cẩm, bắp, sen, đậu xanh…", AveragePrice = 20000, Popularity = 91 },
            new CuisineItem { Province = "Huế", Name = "Cơm cung đình", Description = "Được phục vụ cầu kỳ, tinh tế.", AveragePrice = 200000, Popularity = 85 },
            new CuisineItem { Province = "Huế", Name = "Tré Huế", Description = "Món lên men chua nhẹ, thơm thịt.", AveragePrice = 50000, Popularity = 80 },
            new CuisineItem { Province = "Huế", Name = "Mè xửng", Description = "Đặc sản làm quà, dẻo ngọt.", AveragePrice = 30000, Popularity = 82 },
            new CuisineItem { Province = "Huế", Name = "Bún nghệ", Description = "Bún xào nghệ dân dã, thơm lừng.", AveragePrice = 30000, Popularity = 78 },
            // THÊM 10 MÓN THƯỜNG HUẾ
            new CuisineItem { Province = "Huế", Name = "Bánh khoái", Description = "Bánh xèo nhỏ, chiên giòn, nhân tôm thịt.", AveragePrice = 35000, Popularity = 95 },
            new CuisineItem { Province = "Huế", Name = "Nem lụi", Description = "Thịt nướng xiên tre ăn kèm rau sống, chấm nước lèo.", AveragePrice = 40000, Popularity = 97 },
            new CuisineItem { Province = "Huế", Name = "Tôm chua", Description = "Đặc sản lên men từ tôm, dùng làm gỏi.", AveragePrice = 70000, Popularity = 75 },
            new CuisineItem { Province = "Huế", Name = "Chè đậu ngự", Description = "Chè nấu từ đậu ngự, món chè quý tộc.", AveragePrice = 25000, Popularity = 81 },
            new CuisineItem { Province = "Huế", Name = "Bánh ướt", Description = "Bánh tráng ướt cuốn nhân tôm, thịt.", AveragePrice = 20000, Popularity = 77 },
            new CuisineItem { Province = "Huế", Name = "Bánh canh Nam Phổ", Description = "Bánh canh sền sệt, đậm đà vị tôm cua.", AveragePrice = 30000, Popularity = 86 },
            new CuisineItem { Province = "Huế", Name = "Thịt luộc chấm mắm ruốc", Description = "Thịt luộc đơn giản, điểm nhấn là mắm ruốc đặc trưng.", AveragePrice = 50000, Popularity = 79 },
            new CuisineItem { Province = "Huế", Name = "Bánh ít trần", Description = "Bánh nếp dẻo, nhân đậu xanh, tôm thịt.", AveragePrice = 25000, Popularity = 76 },
            new CuisineItem { Province = "Huế", Name = "Trứng vịt lộn xào me", Description = "Trứng vịt lộn xào me chua ngọt.", AveragePrice = 15000, Popularity = 73 },
            new CuisineItem { Province = "Huế", Name = "Bún giấm nuốc", Description = "Món bún dân dã với con nuốc (sứa) đặc trưng.", AveragePrice = 35000, Popularity = 74 },

            // Món Chay Huế
            new CuisineItem { Province = "Huế", Name = "Bún Bò Huế Chay", Description = "Bún chay đặc trưng cố đô, nước dùng nấm, rau củ.", AveragePrice = 35000, Popularity = 85 },
            new CuisineItem { Province = "Huế", Name = "Cơm Hến Chay", Description = "Cơm trộn hến chay (nấm xào), đậu phụ.", AveragePrice = 25000, Popularity = 75 },
            new CuisineItem { Province = "Huế", Name = "Bánh Bèo Chay", Description = "Bánh bèo nhân đậu xanh, nấm.", AveragePrice = 20000, Popularity = 72 },
            new CuisineItem { Province = "Huế", Name = "Bánh Nậm Chay", Description = "Bánh nậm chay nhân nấm, củ sắn.", AveragePrice = 20000, Popularity = 71 },
            new CuisineItem { Province = "Huế", Name = "Bánh Lọc Chay", Description = "Bánh lọc nhân đậu xanh dẻo thơm.", AveragePrice = 25000, Popularity = 73 },
            new CuisineItem { Province = "Huế", Name = "Nem Chả Chay (Lá Lốt)", Description = "Chả cuốn lá lốt làm từ nấm và đậu phụ.", AveragePrice = 40000, Popularity = 70 },
            new CuisineItem { Province = "Huế", Name = "Vả Trộn Chay", Description = "Món gỏi làm từ quả vả non, đậu phụ.", AveragePrice = 35000, Popularity = 68 },
            new CuisineItem { Province = "Huế", Name = "Chè Khoai Môn Chay", Description = "Chè khoai môn nấu nước cốt dừa.", AveragePrice = 15000, Popularity = 76 },
            new CuisineItem { Province = "Huế", Name = "Bánh Khoái Chay", Description = "Bánh khoái chiên giòn nhân nấm, giá đỗ.", AveragePrice = 35000, Popularity = 69 },
            new CuisineItem { Province = "Huế", Name = "Nấm Hấp Chay", Description = "Nấm tươi hấp xì dầu gừng.", AveragePrice = 50000, Popularity = 67 },

            // ======= MIỀN NAM =======
            // TP.HCM (Tổng 20 món ăn thường + 10 món chay)
            new CuisineItem { Province = "TP.HCM", Name = "Cơm tấm", Description = "Món ăn quốc dân của Sài Gòn.", AveragePrice = 40000, Popularity = 100 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh mì", Description = "Đa dạng nhân, ngon - nhanh - tiện.", AveragePrice = 25000, Popularity = 97 },
            new CuisineItem { Province = "TP.HCM", Name = "Hủ tiếu", Description = "Nước dùng ngọt thanh từ xương heo.", AveragePrice = 40000, Popularity = 95 },
            new CuisineItem { Province = "TP.HCM", Name = "Phá lấu", Description = "Lòng bò hầm nước dừa đậm đà.", AveragePrice = 30000, Popularity = 90 },
            new CuisineItem { Province = "TP.HCM", Name = "Bột chiên", Description = "Món ăn vặt nổi tiếng Sài Thành.", AveragePrice = 25000, Popularity = 88 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh tráng trộn", Description = "Ăn vặt thần thánh của giới trẻ.", AveragePrice = 20000, Popularity = 96 },
            new CuisineItem { Province = "TP.HCM", Name = "Gỏi cuốn", Description = "Cuốn tôm thịt chấm tương đặc trưng.", AveragePrice = 25000, Popularity = 91 },
            new CuisineItem { Province = "TP.HCM", Name = "Chè Sài Gòn", Description = "Ngọt mát, phong phú nguyên liệu.", AveragePrice = 20000, Popularity = 85 },
            new CuisineItem { Province = "TP.HCM", Name = "Lẩu mắm", Description = "Đậm vị miền Tây trong lòng thành phố.", AveragePrice = 200000, Popularity = 89 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh xèo", Description = "Bánh vàng giòn nhân tôm thịt giá đỗ.", AveragePrice = 45000, Popularity = 87 },
            // THÊM 10 MÓN THƯỜNG TP.HCM
            new CuisineItem { Province = "TP.HCM", Name = "Bún mắm", Description = "Bún với nước lèo mắm, hải sản, thịt heo quay.", AveragePrice = 50000, Popularity = 94 },
            new CuisineItem { Province = "TP.HCM", Name = "Bò né", Description = "Thịt bò né trên chảo gang, ăn kèm trứng, pate.", AveragePrice = 60000, Popularity = 93 },
            new CuisineItem { Province = "TP.HCM", Name = "Ốc Sài Gòn", Description = "Đa dạng các loại ốc, sò nướng, xào me.", AveragePrice = 80000, Popularity = 92 },
            new CuisineItem { Province = "TP.HCM", Name = "Miến gà", Description = "Miến nước dùng gà, thịt gà xé.", AveragePrice = 40000, Popularity = 86 },
            new CuisineItem { Province = "TP.HCM", Name = "Súp cua", Description = "Súp cua sền sệt, trứng cút, óc heo.", AveragePrice = 30000, Popularity = 84 },
            new CuisineItem { Province = "TP.HCM", Name = "Trà sữa", Description = "Đồ uống phổ biến với topping đa dạng.", AveragePrice = 35000, Popularity = 98 },
            new CuisineItem { Province = "TP.HCM", Name = "Khô bò Sài Gòn", Description = "Thịt bò khô tẩm gia vị, cay ngọt.", AveragePrice = 150000, Popularity = 79 },
            new CuisineItem { Province = "TP.HCM", Name = "Bún riêu gánh", Description = "Bún riêu cua, chả, huyết, mọc.", AveragePrice = 45000, Popularity = 83 },
            new CuisineItem { Province = "TP.HCM", Name = "Cơm cháy kho quẹt", Description = "Cơm cháy giòn rụm chấm kho quẹt đậm đà.", AveragePrice = 55000, Popularity = 80 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh canh cua", Description = "Bánh canh bột lọc/bột gạo với thịt cua.", AveragePrice = 60000, Popularity = 91 },

            // Món Chay TP.HCM
            new CuisineItem { Province = "TP.HCM", Name = "Cơm Tấm Chay", Description = "Cơm tấm bì chả sườn chay, nước mắm chay.", AveragePrice = 35000, Popularity = 90 },
            new CuisineItem { Province = "TP.HCM", Name = "Hủ Tiếu Nam Vang Chay", Description = "Hủ tiếu chay nước dùng nấm, rau củ.", AveragePrice = 40000, Popularity = 85 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh Mì Chay Đặc Biệt", Description = "Bánh mì chay với xíu mại, chả lụa chay.", AveragePrice = 25000, Popularity = 88 },
            new CuisineItem { Province = "TP.HCM", Name = "Bún Chả Giò Chay", Description = "Bún trộn chả giò chay, rau sống.", AveragePrice = 30000, Popularity = 83 },
            new CuisineItem { Province = "TP.HCM", Name = "Phá Lấu Chay", Description = "Lòng chay từ nấm, đậu phụ hầm nước dừa.", AveragePrice = 25000, Popularity = 78 },
            new CuisineItem { Province = "TP.HCM", Name = "Lẩu Nấm Chay", Description = "Lẩu nấm tươi, thanh ngọt, ăn kèm mì, bún.", AveragePrice = 180000, Popularity = 86 },
            new CuisineItem { Province = "TP.HCM", Name = "Bánh Xèo Chay", Description = "Bánh xèo miền Nam nhân nấm, củ sắn.", AveragePrice = 40000, Popularity = 80 },
            new CuisineItem { Province = "TP.HCM", Name = "Gỏi Cuốn Chay", Description = "Gỏi cuốn rau củ, đậu hũ, chấm tương đen.", AveragePrice = 20000, Popularity = 82 },
            new CuisineItem { Province = "TP.HCM", Name = "Chè Thập Cẩm Chay", Description = "Chè với nhiều loại đậu, thạch, nước cốt dừa.", AveragePrice = 15000, Popularity = 77 },
            new CuisineItem { Province = "TP.HCM", Name = "Miến Gà Chay", Description = "Miến nước dùng nấm, 'thịt gà' chay.", AveragePrice = 35000, Popularity = 75 },
            };

        // Alias/canonical province names for robust search
        private static readonly Dictionary<string, string> ProvinceAliases = new Dictionary<string, string>
        {
            { "ho chi minh", "TP.HCM" },
            { "tphcm", "TP.HCM" },
            { "tp hcm", "TP.HCM" },
            { "sai gon", "TP.HCM" },
            { "sài gòn", "TP.HCM" },
            { "ha noi", "Hà Nội" },
            { "hanoi", "Hà Nội" },
            { "da nang", "Đà Nẵng" },
            { "danang", "Đà Nẵng" },
            { "hue", "Huế" },
            { "hai phong", "Hải Phòng" },
            { "haiphong", "Hải Phòng" },
            { "khanh hoa", "Khánh Hòa" },
            { "nha trang", "Khánh Hòa" },
            { "can tho", "Cần Thơ" },
            { "cantho", "Cần Thơ" },
            // Món chay không cần alias vì đã gán thẳng vào tỉnh
        };

        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var normalized = text.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();
            foreach (var ch in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string CanonicalizeProvince(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var fold = RemoveDiacritics(input).ToLower().Trim();
            if (ProvinceAliases.TryGetValue(fold, out var canonical))
            {
                return canonical;
            }
            // Try direct match to any known province after folding
            var known = GetAllProvinces();
            foreach (var p in known)
            {
                if (RemoveDiacritics(p).ToLower() == fold) return p;
            }
            return input.Trim();
        }

        public static IEnumerable<string> GetAllProvinces()
        {
            return Items.Select(i => i.Province).Distinct().OrderBy(n => n);
        }

        public static List<CuisineItem> GetTopByProvince(string province, int top = 10, bool isVegetarian = false)
        {
            var canonical = CanonicalizeProvince(province);
            IEnumerable<CuisineItem> items;

            if (isVegetarian)
            {
                // Chỉ lấy các món có chữ "Chay" trong tên tại tỉnh đích
                items = Items.Where(i => i.Province.Equals(canonical, System.StringComparison.OrdinalIgnoreCase)
                                        && i.Name.IndexOf("Chay", System.StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else
            {
                // Lấy TẤT CẢ các món (bao gồm cả món chay, nếu có)
                items = Items.Where(i => i.Province.Equals(canonical, System.StringComparison.OrdinalIgnoreCase));
            }

            return items
                .OrderByDescending(i => i.Popularity)
                .Take(top)
                .ToList();
        }

        public static string CanonicalProvinceName(string input) => CanonicalizeProvince(input);

        public static List<string> FindSimilarProvinces(string query, int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<string>();
            var fold = RemoveDiacritics(query).ToLower().Trim();
            var all = GetAllProvinces().ToList();
            // 1) contains match on folded string
            var contains = all.Where(p => RemoveDiacritics(p).ToLower().Contains(fold)).ToList();
            if (contains.Count >= limit) return contains.Take(limit).ToList();
            // 2) startswith match as fallback
            var starts = all.Where(p => RemoveDiacritics(p).ToLower().StartsWith(fold)).ToList();
            foreach (var s in starts)
            {
                if (!contains.Contains(s)) contains.Add(s);
            }
            return contains.Take(limit).ToList();
        }

        public static int GetCountByProvince(string province)
        {
            if (string.IsNullOrWhiteSpace(province)) return 0;
            var canonical = CanonicalizeProvince(province);
            return Items.Count(i => i.Province.Equals(canonical, System.StringComparison.OrdinalIgnoreCase));
        }

        public static decimal GetAveragePriceForProvince(string province)
        {
            if (string.IsNullOrWhiteSpace(province)) return 0;
            var canonical = CanonicalizeProvince(province);
            var list = Items.Where(i => i.Province.Equals(canonical, System.StringComparison.OrdinalIgnoreCase)).ToList();
            if (list.Count == 0) return 0;
            return list.Average(i => i.AveragePrice);
        }
    }
}