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

            // Tính toán chi phí phương tiện theo số người
            double transportBudget = CalculateTransportCost(transportType, days, budget, people);

            // Phân chia chi phí - Ăn uống & giải trí tính theo đầu người
            double foodBudgetPerPerson = (budget * 0.24) / people;
            double hotelBudget = budget * 0.28;
            double funBudgetPerPerson = (budget * 0.15) / people;
            double otherBudget = budget * 0.10;

            var plan = new TripPlan
            {
                Destination = request.Destination,
                SuggestedDays = days,
                TotalBudget = request.Budget,
                TransportOptions = SuggestTransport(request.Destination, transportBudget),
                HotelSuggestions = SuggestHotels(request.Destination, hotelBudget, people),
                DailyPlans = GenerateDailyExpenses(request.Destination, days, foodBudgetPerPerson, funBudgetPerPerson, transportBudget, transportType, people)
            };

            // ✅ Tính toán chi tiết phương tiện
            plan.TransportCalculation = TransportCalculatorService.CalculateTransport(
                request.Destination,
                transportType,
                people,
                "08:45"
            );

            // Tính tổng chi phí thực tế
            double totalDailyCost = 0;
            foreach (var d in plan.DailyPlans)
                totalDailyCost += d.TotalCost;

            double estimatedTotal = totalDailyCost + hotelBudget + otherBudget;

            // Đảm bảo chi phí không vượt ngân sách
            if (estimatedTotal > budget)
            {
                double scaleFactor = (budget * 0.95) / estimatedTotal;

                foreach (var dailyPlan in plan.DailyPlans)
                {
                    foreach (var activity in dailyPlan.Activities)
                    {
                        if (activity.Type == "Ăn uống" || activity.Type == "Giải trí" || activity.Type == "Di chuyển")
                        {
                            activity.Cost *= scaleFactor;
                        }
                    }
                }

                estimatedTotal = 0;
                foreach (var d in plan.DailyPlans)
                {
                    double dayTotal = 0;
                    foreach (var a in d.Activities)
                        dayTotal += a.Cost;
                    estimatedTotal += dayTotal;
                }
                estimatedTotal += hotelBudget + otherBudget;
            }

            plan.EstimatedTotalCost = Math.Min(estimatedTotal, budget * 0.98);

            return plan;
        }

        // Tính chi phí phương tiện theo loại xe VÀ SỐ NGƯỜI
        private static double CalculateTransportCost(string transportType, int days, double budget, int people)
        {
            int dailyTrips = 5; // Trung bình 5 chuyến/ngày
            double avgDistancePerTrip = 8.0; // 8km/chuyến
            double totalDistance = avgDistancePerTrip * dailyTrips * days;
            double totalCost = 0;

            switch (transportType)
            {
                case "Xe riêng":
                    // ✅ XE RIÊNG = Xe máy của mình - CHỈ TỐN XĂNG (KHÔNG THUÊ)
                    int numOwnMotorbikes = (int)Math.Ceiling(people / 2.0); // 2 người/xe
                    double ownMotorbikeFuelConsumption = 2.5; // Lít/100km
                    double fuelPrice = 23000; // VNĐ/lít
                    double ownMotorbikeFuelCost = (totalDistance / 100) * ownMotorbikeFuelConsumption * fuelPrice;
                    totalCost = ownMotorbikeFuelCost * numOwnMotorbikes; // CHỈ TÍNH XĂNG
                    break;

                case "Xe bus":
                    // Vé bus cố định - NHÂN SỐ NGƯỜI
                    double busFare = 7000; // ~7k/lượt/người
                    totalCost = busFare * dailyTrips * days * people;
                    break;

                case "Taxi":
                    // Giá theo km - CHIA CHO NHÓM (tối đa 4 người/xe)
                    double taxiFarePerKm = 11000; // ~11k/km
                    int numTaxis = (int)Math.Ceiling(people / 4.0); // 4 người/taxi
                    totalCost = totalDistance * taxiFarePerKm * numTaxis;
                    break;

                case "Xe máy":
                    // ✅ XE MÁY THUÊ = 100k/ngày/xe + xăng (2 người/xe)
                    int numMotorbikes = (int)Math.Ceiling(people / 2.0); // 2 người/xe
                    double rentalCostPerBike = 100000 * days; // 100k/ngày/xe
                    double motorbikeFuelConsumption = 2.5; // Lít/100km
                    double motorbikeFuelCost = (totalDistance / 100) * motorbikeFuelConsumption * 23000;
                    totalCost = (rentalCostPerBike + motorbikeFuelCost) * numMotorbikes;
                    break;

                case "Kết hợp tự động":
                default:
                    // Taxi/Grab trung bình - CHIA CHO NHÓM
                    int numCars = (int)Math.Ceiling(people / 4.0);
                    totalCost = totalDistance * 11000 * numCars;
                    break;
            }

            // Giới hạn không quá 20% ngân sách
            double maxTransportBudget = budget * 0.20;
            if (totalCost > maxTransportBudget)
            {
                totalCost = maxTransportBudget;
            }

            return totalCost;
        }

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
            // Tính số phòng cần (2-3 người/phòng)
            int numRooms = (int)Math.Ceiling(people / 2.5); // Trung bình 2.5 người/phòng

            return new List<string>
            {
                $"Khách sạn 3* tại trung tâm {dest} (~{(budget / numRooms / 3):N0} VNĐ/phòng/đêm) × {numRooms} phòng",
                $"Homestay/Airbnb giá rẻ (~{(budget / numRooms / 5):N0} VNĐ/phòng/đêm) × {numRooms} phòng"
            };
        }

        private static List<DailyExpense> GenerateDailyExpenses(string dest, int days, double foodBudgetPerPerson, double funBudgetPerPerson, double transportBudget, string transportType, int people)
        {
            var dailyPlans = new List<DailyExpense>();
            var canonicalProvince = CuisineData.CanonicalProvinceName(dest);
            var cuisineTop = CuisineData.GetTopByProvince(canonicalProvince, 20);

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
            };

            var usedCuisineIndices = new HashSet<int>();
            var usedAttractionIndices = new HashSet<int>();

            double dailyTransportCost = transportBudget / days;
            var (transportIcon, transportName) = GetTransportInfo(transportType);

            // ✅ KHOẢNG CÁCH CAO HƠN VÀ RANDOM MỖI LẦN (4-8 km mỗi chặng)
            double[] baseDistances = { 6.2, 7.0, 7.5, 7.8, 7.6 }; // 5 chặng/ngày, trung bình 5-7km
            double[] segmentDistances = baseDistances.Select(d => d * (0.8 + _random.NextDouble() * 0.4)).ToArray(); // Random ±20%
            double totalDailyDistance = segmentDistances.Sum();

            // Tính chi phí theo km (VNĐ/km) dựa trên loại phương tiện
            double costPerKm = 0;
            switch (transportType)
            {
                case "Xe riêng":
                case "Xe máy":
                    // Xe máy: xăng 2.5 lít/100km × 23,000 đ/lít = 575 đ/km
                    costPerKm = (2.5 / 100) * 23000;
                    break;
                case "Taxi":
                case "Kết hợp tự động":
                    // Taxi/Grab: ~11,000 đ/km
                    costPerKm = 11000;
                    break;
                case "Xe bus":
                    // Bus: giá cố định ~7,000 đ/lượt (không phụ thuộc km)
                    costPerKm = dailyTransportCost / totalDailyDistance;
                    break;
                default:
                    costPerKm = dailyTransportCost / totalDailyDistance;
                    break;
            }

            // ✅ FIX: Số xe cần thiết (Taxi 4 người/xe, còn lại 2 người/xe)
            int numVehicles;
            if (transportType == "Xe bus")
            {
                numVehicles = 1; // Bus chở cả nhóm
            }
            else if (transportType == "Taxi" || transportType == "Kết hợp tự động")
            {
                numVehicles = (int)Math.Ceiling(people / 4.0); // Taxi: 4 người/xe
            }
            else // Xe riêng, Xe máy
            {
                numVehicles = (int)Math.Ceiling(people / 2.0); // 2 người/xe máy
            }

            for (int day = 1; day <= days; day++)
            {
                var activities = new List<Activity>();

                // SÁNG: Ăn sáng
                var breakfast = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                if (breakfast != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "07:00 - 08:30",
                        Type = "Ăn uống",
                        Name = $"Ăn sáng: {breakfast.Name}",
                        Description = $"{breakfast.Description} — Món đặc trưng {canonicalProvince}. ({people} người)",
                        Address = AddressData.GetBreakfastAddress(dest),
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

                // DI CHUYỂN đến điểm tham quan
                double distance1 = segmentDistances[0];
                double cost1 = distance1 * costPerKm * numVehicles;
                activities.Add(new Activity
                {
                    Time = "08:45 - 09:15",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} đến điểm tham quan",
                    Description = $"Di chuyển từ nơi ăn sáng đến điểm du lịch. ({distance1:F1} km)",
                    Address = $"📍 Từ khu trung tâm đến điểm tham quan - {distance1:F1} km",
                    Cost = cost1
                });

                // THAM QUAN
                var attraction = GetRandomUnusedAttraction(attractions, usedAttractionIndices);
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

                // DI CHUYỂN đến nhà hàng
                double distance2 = segmentDistances[1];
                double cost2 = distance2 * costPerKm * numVehicles;
                activities.Add(new Activity
                {
                    Time = "12:45 - 13:00",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} đến nhà hàng",
                    Description = $"Di chuyển đến nhà hàng ăn trưa. ({distance2:F1} km)",
                    Address = $"📍 Từ điểm tham quan đến khu ẩm thực - {distance2:F1} km",
                    Cost = cost2
                });

                // ĂN TRƯA
                var lunch = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                if (lunch != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "13:00 - 14:30",
                        Type = "Ăn uống",
                        Name = $"Ăn trưa: {lunch.Name}",
                        Description = $"{lunch.Description} — Giá trung bình khoảng {lunch.AveragePrice:N0} đ/người. ({people} người)",
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

                // DI CHUYỂN đến cafe
                double distance3 = segmentDistances[2];
                double cost3 = distance3 * costPerKm * numVehicles;
                activities.Add(new Activity
                {
                    Time = "14:45 - 15:00",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} đến quán cafe",
                    Description = $"Di chuyển đến quán cafe nghỉ ngơi. ({distance3:F1} km)",
                    Address = $"📍 Từ nhà hàng đến khu cafe - {distance3:F1} km",
                    Cost = cost3
                });

                // GIẢI TRÍ CHIỀU
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

                // DI CHUYỂN về khách sạn
                double distance4 = segmentDistances[3];
                double cost4 = distance4 * costPerKm * numVehicles;
                activities.Add(new Activity
                {
                    Time = "17:45 - 18:15",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} về khách sạn/nhà hàng",
                    Description = $"Di chuyển về khách sạn nghỉ ngơi hoặc đến nhà hàng ăn tối. ({distance4:F1} km)",
                    Address = $"📍 Từ quán cafe về khu trung tâm - {distance4:F1} km",
                    Cost = cost4
                });

                // ĂN TỐI
                var dinner = GetRandomUnusedCuisineItem(cuisineTop, usedCuisineIndices);
                if (dinner != null)
                {
                    activities.Add(new Activity
                    {
                        Time = "18:30 - 20:00",
                        Type = "Ăn uống",
                        Name = $"Ăn tối: {dinner.Name}",
                        Description = $"{dinner.Description} — Giá trung bình khoảng {dinner.AveragePrice:N0} đ/người. ({people} người)",
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

                // DI CHUYỂN về khách sạn cuối ngày
                double distance5 = segmentDistances[4] * (0.8 + _random.NextDouble() * 0.4);
                double cost5 = distance5 * costPerKm * numVehicles;
                activities.Add(new Activity
                {
                    Time = "20:15 - 20:30",
                    Type = "Di chuyển",
                    Name = $"{transportIcon} {transportName} về khách sạn",
                    Description = $"Kết thúc ngày, về khách sạn nghỉ ngơi. ({distance5:F1} km)",
                    Address = $"📍 Từ nhà hàng về khách sạn - {distance5:F1} km",
                    Cost = cost5
                });

                dailyPlans.Add(new DailyExpense { DayNumber = day, Activities = activities });
            }

            return dailyPlans;
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

        private static CuisineItem GetRandomUnusedCuisineItem(List<CuisineItem> cuisines, HashSet<int> usedIndices)
        {
            if (cuisines == null || cuisines.Count == 0) return null;

            var availableIndices = Enumerable.Range(0, cuisines.Count)
                .Where(i => !usedIndices.Contains(i))
                .ToList();

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

            if (availableIndices.Count == 0) return null;

            var selectedIndex = availableIndices[_random.Next(availableIndices.Count)];
            usedIndices.Add(selectedIndex);
            return attractions[selectedIndex];
        }

        private static double RandomVariation()
        {
            return 0.85 + (_random.NextDouble() * 0.30);
        }
    }
}