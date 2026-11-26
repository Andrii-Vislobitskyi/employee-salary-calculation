using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using EMP.Common.Configuration;
using EMP.Common.Dto.APP_Dto;
using EMP.DataAccess.Models;
using EMP.Services.Services.Interfaces;
using EMP.Services.Services.Statistic_Report.Models;


namespace EMP.Services.Services
{
    public class MathService : IMathStatistic
    {   
        private readonly EmployeeAPIConfig _config;
        public MathService(EmployeeAPIConfig config)
        {
            _config = config;
        }
        public async Task<StatisticByDayDto> GetStatisticDayDtoAsync(JObject? obj, string fullName)
        {
            return await Task.Run(() =>
            {
                JToken[]? bills = obj?["Bills"]?.ToArray();

                List<StatisticItem> billItems = new List<StatisticItem>();

                StatisticByDayDto statisticDto = new StatisticByDayDto();


                if (bills != null && bills?.Count() != 0)
                {
                    foreach (var item in bills)
                    {
                        billItems.Add(
                                    new StatisticItem
                                    {
                                        BillId = item.Value<int>("ID"),
                                        BillNumber = item.Value<int>("BillNumber"),
                                        Total = item.Value<decimal>("Total"),
                                        Date = item.Value<DateTime>("Closed"),
                                        CloseUserName = item.Value<string>("CloseUserName"),
                                    }
                            );
                    }

                    billItems = billItems.Where(b => b.CloseUserName == fullName).ToList();

                    decimal total = billItems.Sum(b => b.Total);

                    if (billItems.Count() != 0)
                    {
                        statisticDto.Date = billItems.First().Date.Date;
                    }
                   
                    statisticDto.Total = total;
                    statisticDto.Bonus_Salary = Math.Round((total * (decimal)_config.BonusRate) / 100, 2, MidpointRounding.AwayFromZero);
                    statisticDto.StatisticItems = billItems;

                }
                else
                {
                    statisticDto.Date = DateTime.Now.Date;
                }

                return statisticDto;
            });
        }


        public async Task<StatisticByRangeDto> GetStatisticRangeDtoAsync(JObject? obj, int externalId)
        {
            return await Task.Run(() =>
            {
                JToken[]? bills = obj?["Bills"]?.ToArray();

                StatisticByRangeDto rangeDto = new StatisticByRangeDto();

                List<object> billItems = new List<object>();

                List<int> staffDiscountID = GetStaffDiscount_BillId_ByRangeDate_Async(obj).GetAwaiter().GetResult();

                if (bills !=null && bills?.Count() != 0)
                {
                    foreach (var item in bills)
                    {
                        if (!staffDiscountID.Contains(Convert.ToInt32(item?.Value<int>("ID"))))
                        {
                            billItems.Add(new
                            {
                                CloseUser = item.Value<string>("CloseUser"),
                                CloseUserID = item.Value<int>("CloseUserID"),
                                Closed = item.Value<DateTime>("Closed"),
                                Payments = item.Value<object>("Payments")
                            });
                        }      
                    }


                    List<object> selectedItems = GenerateFilteredByUserList(billItems, externalId);             

                    object firstBill = selectedItems.First();

                    string jsonString = JsonConvert.SerializeObject(firstBill);

                    var jsonDocument = System.Text.Json.JsonDocument.Parse(jsonString);


                    rangeDto.CloseUser = jsonDocument.RootElement.EnumerateObject().ToList()
                                             .Where(p => p.Name == "CloseUser").Select(p => p.Value.ToString()).First();

                    rangeDto.ExternalId = Convert.ToInt32(jsonDocument.RootElement.EnumerateObject().ToList()
                                             .Where(p => p.Name == "CloseUserID").Select(p => p.Value.ToString()).First());

                    rangeDto.Total = CalculateTotal(selectedItems);

                    rangeDto.Bonus_Salary = Math.Round((rangeDto.Total * (decimal)_config.BonusRate) / 100, 2, MidpointRounding.AwayFromZero);

                    var closedDates = (List<DateTime>)selectedItems.Select(b => Convert.ToDateTime(b.GetType()?.GetProperty("Closed")?.GetValue(b))).ToList();

                    var dates = closedDates.DistinctBy(d => d.Date);

                    rangeDto.Count_WorkShift = dates.Count();
                }

                return rangeDto;
            });            
        }

        public async Task<int?> GetUserExternalID_Async(User? user, JObject? obj)
        {
            return await Task.Run(() =>
            {
                JToken[]? users = obj?["Items"]?.ToArray();

                int ID = users.Where(u => u.Value<string>("FirstName") == user?.FirstName 
                                       && u.Value<string>("LastName") == user?.LastName 
                                       && u.Value<string>("Patronymic") == user?.Patronymic)
                                       .Select(u => u.Value<int>("ID")).First();

                return ID;
            });
           
        }


        public async Task<List<int>> GetStaffDiscount_BillId_ByCurrentDay_Async(JObject obj)
        {
            //TODO => find id to current day !!!
            return new List<int>();
        }

        public async Task<List<int>> GetStaffDiscount_BillId_ByRangeDate_Async(JObject obj)
        {
            return await Task.Run(() =>
            {
                List<int> billsID = new List<int>();

                JToken[]? bills = obj?["Bills"]?.ToArray();

                if (bills!=null && bills?.Length != 0)
                {
                    foreach (var bill in bills)
                    {
                        var array = bill?["Discounts"];

                        if (array.Any())
                        {
                            foreach (var item in array)
                            {
                                int cardNumber = Convert.ToInt32(item?.Value<int>("CardID"));
                                if (cardNumber == 4474)
                                {
                                    billsID.Add(Convert.ToInt32(bill?.Value<int>("ID")));
                                }
                            }
                        }
                    }
                }

                return billsID;
            });         
        }


        public Task<DiscountReportDto> GetStaffDiscountReport_ByDay_Async(JObject obj)
        {
            throw new NotImplementedException();
        }

        public Task<DiscountReportDto> GetStaffDiscountReport_ByRangeDAte_Async(JObject obj)
        {
            throw new NotImplementedException();
        }


        #region Generate List objects filtered by current user

        private List<object> GenerateFilteredByUserList(List<object> billItems, int externalId)
       {
            List<object> selectedItems = new List<object>();

            billItems.ForEach(bill =>
            {
                string jsonBill = JsonConvert.SerializeObject(bill);
                var jsonBillDocument = System.Text.Json.JsonDocument.Parse(jsonBill);

                var bill_externalId = Convert.ToInt32(jsonBillDocument.RootElement.EnumerateObject().ToList()
                                            .Where(p => p.Name == "CloseUserID").Select(p => p.Value.ToString()).First());


                if (bill_externalId == externalId)
                {
                    JsonElement root = jsonBillDocument.RootElement;

                    if (root.TryGetProperty("Payments", out JsonElement paymentsElement))
                    {
                        selectedItems.Add(new
                        {
                            CloseUser = jsonBillDocument.RootElement.EnumerateObject().ToList()
                                         .Where(p => p.Name == "CloseUser").Select(p => p.Value.ToString()).First(),
                            CloseUserID = bill_externalId,
                            Closed = jsonBillDocument.RootElement.EnumerateObject().ToList()
                                         .Where(p => p.Name == "Closed").Select(p => p.Value.ToString()).First(),
                            Payments = paymentsElement.ToString()
                        });
                    }
                }
            });
         
            return selectedItems;
        }

        #endregion


        #region Utils Calculate total sum
        private decimal CalculateTotal(List<object> billItems)
        {
            decimal total = 0;


            billItems.ForEach(bill =>
            {
                total += GetValueTotal(bill);
            });

            return total;
        }


        private decimal GetValueTotal(object bill)
        {
            decimal total = 0;
            string jsonString = JsonConvert.SerializeObject(bill);

            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                JsonElement root = doc.RootElement;

                foreach (JsonProperty property in root.EnumerateObject())
                {
                    if (property.Name == "Payments")
                    {
                        var prop = property.ToString();

                        int count = prop.Count(c => c == '{');

                        if (count == 1)
                        {
                            char charBegin = '{';
                            char charEnd = '}';

                            int indexBegin = prop.IndexOf(charBegin);
                            int indexEnd = prop.IndexOf(charEnd);

                            int length = indexEnd - indexBegin + 1;

                            string sub = prop.Substring(indexBegin, length);

                            sub = sub.Replace(@"\", string.Empty);

                            JObject o = JObject.Parse(sub);

                            total = Convert.ToDecimal(o.GetValue("Total"));
                        }
                        else
                        {
                            char charBegin = '{';
                            char charEnd = '}';


                            int indexBegin = prop.IndexOf(charBegin);
                            int indexEnd = prop.LastIndexOf(charEnd);


                            int length = indexEnd - indexBegin + 1;

                            string sub = prop.Substring(indexBegin, length);

                            sub = sub.Replace(@"\", string.Empty);


                            string[] strings = sub.Split("},", StringSplitOptions.RemoveEmptyEntries);


                            strings[0] = string.Concat(strings[0], charEnd);


                            foreach (string s in strings)
                            {
                                JObject obj = JObject.Parse(s);

                                total += Convert.ToDecimal(obj.GetValue("Total"));
                            }

                        }                                                       
                    }                                                 
                }
            }

            return total;
        }
        #endregion
    }
}
