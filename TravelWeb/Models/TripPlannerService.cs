using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class TripPlannerService
    {
        private static Random _random = new Random();

        public static TripPlan GenerateDetailedPlan(TripRequest request)
        {
            _random = new Random(Guid.NewGuid().GetHashCode());

            int days = request.Days ?? SuggestDaysByBudget(request.Budget);
            var budget = request.Budget;
            int people = Math.Max(1, request.NumberOfPeople);
            string transportType = request.TransportType ?? "Kết hợp tự động";

            // Phân chia chi phí - Ăn uống & giải trí tính theo đầu người
            double foodBudgetPerPerson = (budget * 0.24) / people;
            double hotelBudget = budget * 0.28;
            double funBudgetPerPerson = (budget * 0.15) / people;
            double otherBudget = budget * 0.10;

            // ✅ BƯỚC 1: Tạo lịch trình chi tiết VÀ ÁP DỤNG LỌC CHAY/MẶN NGAY TỪ ĐẦU
            var dailyPlansResult = GenerateDailyExpenses(
                request.Destination,
                days,
                foodBudgetPerPerson,
                funBudgetPerPerson,
                0,
                transportType,
                people,
                request.IsVegetarian // ✅ TRUYỀN THAM SỐ LỌC MỚI
            );

            // ✅ BƯỚC 2 & 3: Tính toán khoảng cách và chi phí vận chuyển
            double totalActualDistance = 0;
            foreach (var day in dailyPlansResult)
            {
                foreach (var activity in day.Activities)
                {
                    if (activity.Type == "Di chuyển" && activity.Address != null)
                    {
                        var parts = activity.Address.Split('-');
                        if (parts.Length >= 2)
                        {
                            var kmPart = parts[parts.Length - 1].Trim().Replace("km", "").Trim();
                            if (double.TryParse(kmPart, out double km))
                            {
                                totalActualDistance += km;
                            }
                        }
                    }
                }
            }

            var transportCalc = CalculateDetailedTransportCost(
                transportType,
                days,
                people,
                totalActualDistance
            );

            // ✅ BƯỚC 4: Điều chỉnh chi phí di chuyển trong lịch trình
            double totalTransportCostInItinerary = 0;
            foreach (var day in dailyPlansResult)
            {
                foreach (var activity in day.Activities)
                {
                    if (activity.Type == "Di chuyển")
                    {
                        totalTransportCostInItinerary += activity.Cost;
                    }
                }
            }

            if (totalTransportCostInItinerary > 0 &&
                transportType != "Xe riêng" &&
                transportType != "Xe máy")
            {
                double adjustmentFactor = transportCalc.TotalTransportCost / totalTransportCostInItinerary;
                foreach (var day in dailyPlansResult)
                {
                    foreach (var activity in day.Activities)
                    {
                        if (activity.Type == "Di chuyển")
                        {
                            activity.Cost *= adjustmentFactor;
                        }
                    }
                }
            }

            var plan = new TripPlan
            {
                Destination = request.Destination,
                SuggestedDays = days,
                TotalBudget = request.Budget,
                TransportOptions = SuggestTransport(request.Destination, transportCalc.TotalTransportCost),
                HotelSuggestions = SuggestHotels(request.Destination, hotelBudget, people),
                DailyPlans = dailyPlansResult,
                TransportCalculation = transportCalc
            };

            // Tính lại EstimatedTotalCost
            double totalDailyCost = plan.DailyPlans.Sum(d => d.TotalCost);
            double estimatedTotal = totalDailyCost + hotelBudget + otherBudget + transportCalc.TotalTransportCost;

            if (estimatedTotal > budget)
            {
                double scaleFactor = (budget * 0.95) / estimatedTotal;

                foreach (var dailyPlan in plan.DailyPlans)
                {
                    foreach (var activity in dailyPlan.Activities)
                    {
                        if (activity.Type == "Ăn uống" || activity.Type == "Giải trí")
                        {
                            activity.Cost *= scaleFactor;
                        }
                    }
                }
                estimatedTotal = plan.DailyPlans.Sum(d => d.TotalCost) + plan.TransportCalculation.TotalTransportCost + hotelBudget + otherBudget;
            }

            plan.EstimatedTotalCost = Math.Min(estimatedTotal, budget * 0.98);

            return plan;
        }

        // --- HÀM CalculateDetailedTransportCost (Giữ nguyên) ---
        private static TransportCalculation CalculateDetailedTransportCost(
            string transportType,
            int days,
            int people,
            double totalDistance)
        {
            int numVehicles = 0;
            double totalCost = 0;
            double fuelCost = 0;
            double rentalCost = 0;

            switch (transportType)
            {
                case "Xe riêng":
                    numVehicles = (int)Math.Ceiling(people / 2.0);
                    double ownFuelConsumption = 2.5;
                    double fuelPrice = 23000;
                    fuelCost = (totalDistance / 100) * ownFuelConsumption * fuelPrice * numVehicles;
                    totalCost = fuelCost;
                    break;

                case "Xe máy":
                    numVehicles = (int)Math.Ceiling(people / 2.0);
                    rentalCost = 100000 * days * numVehicles;
                    double rentFuelConsumption = 2.5;
                    fuelCost = (totalDistance / 100) * rentFuelConsumption * 23000 * numVehicles;
                    totalCost = rentalCost + fuelCost;
                    break;

                case "Xe bus":
                    numVehicles = 1;
                    double busFarePerTrip = 7000;
                    int tripsPerDay = 5;
                    totalCost = busFarePerTrip * tripsPerDay * days * people;
                    break;

                case "Taxi":
                case "Kết hợp tự động":
                    numVehicles = (int)Math.Ceiling(people / 4.0);
                    double taxiRatePerKm = 11000;
                    totalCost = totalDistance * taxiRatePerKm * numVehicles;
                    break;
            }

            double averageSpeed = 40;
            double totalDuration = (totalDistance / averageSpeed) * 60;

            return new TransportCalculation
            {
                TransportType = transportType,
                NumberOfPeople = people,
                VehiclesNeeded = numVehicles,
                TotalDistance = totalDistance,
                TotalTransportCost = totalCost,
                FuelCost = fuelCost,
                TotalDuration = totalDuration,
                Segments = new List<RouteSegment>()
            };
        }

        // --- HÀM GenerateDailyExpenses ĐÃ SỬA ---
        private static List<DailyExpense> GenerateDailyExpenses(
            string dest,
            int days,
            double foodBudgetPerPerson,
            double funBudgetPerPerson,
            double transportBudget,
            string transportType,
            int people,
            bool isVegetarian) // ✅ THAM SỐ MỚI ĐỂ LỌC
        {
            var dailyPlans = new List<DailyExpense>();
            var canonicalProvince = CuisineData.CanonicalProvinceName(dest);

            // ✅ LỌC MÓN ĂN NGAY TỪ ĐẦU DỰA TRÊN isVegetarian
            var cuisineTop = CuisineData.GetTopByProvince(canonicalProvince, 20, isVegetarian);

            cuisineTop = cuisineTop.OrderBy(x => _random.Next()).ToList();

            var destinationInfo = DestinationData.GetAll()
                .FirstOrDefault(d => string.Equals(d.Name, dest, StringComparison.OrdinalIgnoreCase)
                    || (!string.IsNullOrWhiteSpace(d.Name) && d.Name.IndexOf(dest, StringComparison.OrdinalIgnoreCase) >= 0)
                    || (!string.IsNullOrWhiteSpace(dest) && dest.IndexOf(d.Name ?? string.Empty, StringComparison.OrdinalIgnoreCase) >= 0));

            var attractions = destinationInfo?.Attractions?.OrderBy(x => _random.Next()).ToList()
                ?? new List<AttractionPoint>();

            var afternoonActivities = new List<string>
            {
                "Cà phê/Ngắm hoàng hôn",
                "Cafe view đẹp/Chụp ảnh",
                "Mua sắm/Thư giãn tại trung tâm"
            };

            var usedCuisineIndices = new HashSet<int>();
            var usedAttractionIndices = new HashSet<int>();

            var (transportIcon, transportName) = GetTransportInfo(transportType);

            double[] baseDistances = { 6.5, 7.2, 7.8, 7.3, 6.8 };

            int numVehicles;
            if (transportType == "Xe bus")
            {
                numVehicles = 1;
            }
            else if (transportType == "Taxi" || transportType == "Kết hợp tự động")
            {
                numVehicles = (int)Math.Ceiling(people / 4.0);
            }
            else
            {
                numVehicles = (int)Math.Ceiling(people / 2.0);
            }

            double costPerKm = 0;
            switch (transportType)
            {
                case "Xe riêng":
                case "Xe máy":
                    costPerKm = (2.5 / 100) * 23000;
                    break;
                case "Taxi":
                case "Kết hợp tự động":
                    costPerKm = 11000;
                    break;
                case "Xe bus":
                    costPerKm = 7000 / 8.0;
                    break;
            }

            for (int day = 1; day <= days; day++)
            {
                var activities = new List<Activity>();
                double[] segmentDistances = baseDistances
                    .Select(d => d * (0.85 + _random.NextDouble() * 0.3))
                    .ToArray();

                var breakfast = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                var lunch = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                var dinner = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                var attraction = GetRandomUnusedAttraction(attractions, usedAttractionIndices);

                // --- SÁNG: Ăn sáng ---
                if (breakfast != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "07:00 - 08:30",
                        Type = "Ăn uống",
                        Name = $"Ăn sáng: {breakfast.Name}",
                        // ✅ CẬP NHẬT MÔ TẢ ĐỂ PHẢN ÁNH CHẾ ĐỘ ĂN
                        Description = $"{breakfast.Description} — Món {(isVegetarian ? "chay " : "")}đặc trưng {canonicalProvince}. ({people} người)",
                        Address = AddressData.GetBreakfastAddress(dest, breakfast.Name),
                        Cost = (double)breakfast.AveragePrice * people * RandomVariation()
                    });
                }
                else
                {
                    activities.Add(new Activity
                    {
                        Time = "07:00 - 08:30",
                        Type = "Ăn uống",
                        Name = $"Ăn sáng tại quán địa phương ở {dest}",
                        Description = $"Bữa sáng nhẹ nhàng với món địa phương. ({people} người)",
                        Address = AddressData.GetBreakfastAddress(dest),
                        Cost = (foodBudgetPerPerson / 3) * people * RandomVariation()
                    });
                }

                // --- DI CHUYỂN 1: Đến điểm tham quan ---
                double distance1 = segmentDistances[0];
                activities.Add(new Activity
                {
                    Time = "08:45 - 09:15",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} đến điểm tham quan",
                    Description = $"Di chuyển từ nơi ăn sáng đến điểm du lịch. ({numVehicles} xe)",
                    Address = $"📍 Từ khu trung tâm đến điểm tham quan - {distance1:F1} km",
                    Cost = distance1 * costPerKm * numVehicles
                });

                // --- THAM QUAN ---
                if (attraction != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "09:30 - 12:30",
                        Type = "Tham quan",
                        Name = $"Tham quan: {attraction.Name}",
                        Description = $"{attraction.Description} ({people} vé)",
                        Address = $"📍 {attraction.Name}, {dest}",
                        Cost = ((double)attraction.EntranceFee * people + (funBudgetPerPerson * people / days * 0.6)) * RandomVariation()
                    });
                }
                else
                {
                    var mainPlaceName = destinationInfo?.Name ?? dest;
                    var mainPlaceDesc = destinationInfo?.Description ?? $"Khám phá các điểm đến nổi bật tại {dest}.";
                    activities.Add(new Activity
                    {
                        Time = "09:30 - 12:30",
                        Type = "Tham quan",
                        Name = $"Tham quan: {mainPlaceName}",
                        Description = $"{mainPlaceDesc} ({people} người)",
                        Address = $"📍 Trung tâm {dest}",
                        Cost = (funBudgetPerPerson * people / days * 0.6) * RandomVariation()
                    });
                }

                // --- DI CHUYỂN 2: Đến nhà hàng ---
                double distance2 = segmentDistances[1];
                activities.Add(new Activity
                {
                    Time = "12:45 - 13:00",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} đến nhà hàng",
                    Description = $"Di chuyển đến nhà hàng ăn trưa. ({numVehicles} xe)",
                    Address = $"📍 Từ điểm tham quan đến khu ẩm thực - {distance2:F1} km",
                    Cost = distance2 * costPerKm * numVehicles
                });

                // --- ĂN TRƯA ---
                if (lunch != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "13:00 - 14:30",
                        Type = "Ăn uống",
                        Name = $"Ăn trưa: {lunch.Name}",
                        // ✅ CẬP NHẬT MÔ TẢ ĐỂ PHẢN ÁNH CHẾ ĐỘ ĂN
                        Description = $"{lunch.Description} — Giá trung bình khoảng {lunch.AveragePrice:N0} đ/người. Món {(isVegetarian ? "chay " : "")}đặc trưng. ({people} người)",
                        Address = AddressData.GetRestaurantAddress(dest, lunch.Name),
                        Cost = (double)lunch.AveragePrice * people * RandomVariation()
                    });
                }
                else
                {
                    activities.Add(new Activity
                    {
                        Time = "13:00 - 14:30",
                        Type = "Ăn uống",
                        Name = "Ăn trưa đặc sản địa phương",
                        Description = $"Thưởng thức món ăn đặc trưng {dest}. ({people} người)",
                        Address = AddressData.GetRestaurantAddress(dest, "Món địa phương"),
                        Cost = (foodBudgetPerPerson / 3) * people * RandomVariation()
                    });
                }

                // --- DI CHUYỂN 3: Đến cafe ---
                double distance3 = segmentDistances[2];
                activities.Add(new Activity
                {
                    Time = "14:45 - 15:00",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} đến quán cafe",
                    Description = $"Di chuyển đến quán cafe nghỉ ngơi. ({numVehicles} xe)",
                    Address = $"📍 Từ nhà hàng đến khu cafe - {distance3:F1} km",
                    Cost = distance3 * costPerKm * numVehicles
                });

                // --- GIẢI TRÍ CHIỀU ---
                var afternoonActivity = afternoonActivities[_random.Next(afternoonActivities.Count)];
                activities.Add(new Activity
                {
                    Time = "15:00 - 17:30",
                    Type = "Giải trí",
                    Name = afternoonActivity,
                    Description = $"Chọn địa điểm có view đẹp tại {dest} để nghỉ ngơi và chụp ảnh. ({people} người)",
                    Address = AddressData.GetCafeAddress(dest),
                    Cost = (funBudgetPerPerson * people / days * 0.4) * RandomVariation()
                });

                // --- DI CHUYỂN 4: Về khách sạn/nhà hàng ---
                double distance4 = segmentDistances[3];
                activities.Add(new Activity
                {
                    Time = "17:45 - 18:15",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} về khách sạn/nhà hàng",
                    Description = $"Di chuyển về khách sạn nghỉ ngơi hoặc đến nhà hàng ăn tối. ({numVehicles} xe)",
                    Address = $"📍 Từ quán cafe về khu trung tâm - {distance4:F1} km",
                    Cost = distance4 * costPerKm * numVehicles
                });

                // --- ĂN TỐI ---
                if (dinner != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "18:30 - 20:00",
                        Type = "Ăn uống",
                        Name = $"Ăn tối: {dinner.Name}",
                        // ✅ CẬP NHẬT MÔ TẢ ĐỂ PHẢN ÁNH CHẾ ĐỘ ĂN
                        Description = $"{dinner.Description} — Giá trung bình khoảng {dinner.AveragePrice:N0} đ/người. Món {(isVegetarian ? "chay " : "")}đặc trưng. ({people} người)",
                        Address = AddressData.GetRestaurantAddress(dest, dinner.Name),
                        Cost = (double)dinner.AveragePrice * people * RandomVariation()
                    });
                }
                else
                {
                    activities.Add(new Activity
                    {
                        Time = "18:30 - 20:00",
                        Type = "Ăn uống",
                        Name = "Ăn tối và dạo phố đêm",
                        Description = $"Thưởng thức ẩm thực đêm tại {dest}. ({people} người)",
                        Address = AddressData.GetNightMarketAddress(dest),
                        Cost = (foodBudgetPerPerson / 3) * people * RandomVariation()
                    });
                }

                // --- DI CHUYỂN 5: Về khách sạn cuối ngày ---
                double distance5 = segmentDistances[4];
                activities.Add(new Activity
                {
                    Time = "20:15 - 20:30",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} về khách sạn",
                    Description = $"Kết thúc ngày, về khách sạn nghỉ ngơi. ({numVehicles} xe)",
                    Address = $"📍 Từ nhà hàng về khách sạn - {distance5:F1} km",
                    Cost = distance5 * costPerKm * numVehicles
                });

                dailyPlans.Add(new DailyExpense { DayNumber = day, Activities = activities });
            }

            return dailyPlans;
        }

        // --- Các hàm hỗ trợ GetRandomUnused... (Giữ nguyên) ---
        private static CuisineItem GetRandomUnusedCuisineItem(List<CuisineItem> cuisines, HashSet<int> usedIndices)
        {
            if (cuisines == null || cuisines.Count == 0) return null;

            var availableIndices = Enumerable.Range(0, cuisines.Count)
                .Where(i => !usedIndices.Contains(i))
                .ToList();

            if (availableIndices.Count == 0)
            {
                usedIndices.Clear();
                availableIndices = Enumerable.Range(0, cuisines.Count).ToList();
            }

            if (availableIndices.Count == 0) return null;

            var selectedIndex = availableIndices[_random.Next(availableIndices.Count)];
            usedIndices.Add(selectedIndex);
            return cuisines[selectedIndex];
        }

        private static AttractionPoint GetRandomUnusedAttraction(List<AttractionPoint> attractions, HashSet<int> usedIndices)
        {
            if (attractions == null || attractions.Count == 0) return null;

            var availableIndices = Enumerable.Range(0, attractions.Count)
                .Where(i => !usedIndices.Contains(i))
                .ToList();

            if (availableIndices.Count == 0)
            {
                usedIndices.Clear();
                availableIndices = Enumerable.Range(0, attractions.Count).ToList();
            }

            if (availableIndices.Count == 0) return null;

            var selectedIndex = availableIndices[_random.Next(availableIndices.Count)];
            usedIndices.Add(selectedIndex);
            return attractions[selectedIndex];
        }

        // ... (Các hàm hỗ trợ GetTransportInfo, SuggestDaysByBudget, SuggestTransport, SuggestHotels, RandomVariation giữ nguyên)
        private static int SuggestDaysByBudget(double budget)
        {
            if (budget < 3000000) return 2;
            if (budget < 6000000) return 3;
            if (budget < 10000000) return 4;
            return 5;
        }

        private static List<string> SuggestTransport(string dest, double budget)
        {
            return new List<string>
            {
                $"Xe giường nằm tới {dest} (khoảng {budget * 0.3:N0} VNĐ)",
                $"Máy bay (nếu xa, tầm {budget * 0.7:N0} VNĐ)"
            };
        }

        private static List<string> SuggestHotels(string dest, double budget, int people)
        {
            int numRooms = (int)Math.Ceiling(people / 2.5);

            return new List<string>
            {
                $"Khách sạn 3* tại trung tâm {dest} (~{(budget / numRooms / 3):N0} VNĐ/phòng/đêm) × {numRooms} phòng",
                $"Homestay/Airbnb giá rẻ (~{(budget / numRooms / 5):N0} VNĐ/phòng/đêm) × {numRooms} phòng"
            };
        }

        private static (string Icon, string Name) GetTransportInfo(string transportType)
        {
            switch (transportType)
            {
                case "Xe riêng":
                    return ("🏍️", "Xe máy riêng");
                case "Xe bus":
                    return ("🚌", "Xe bus");
                case "Taxi":
                    return ("🚕", "Taxi");
                case "Xe máy":
                    return ("🏍️", "Xe máy thuê");
                case "Kết hợp tự động":
                default:
                    return ("🚕", "Grab/Taxi");
            }
        }

        private static double RandomVariation()
        {
            return 0.85 + (_random.NextDouble() * 0.30);
        }
    }
}