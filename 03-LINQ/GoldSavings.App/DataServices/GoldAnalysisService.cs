using System;
using System.Collections.Generic;
using System.Linq;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldAnalysisService
    {
        private readonly List<GoldPrice> _goldPrices;

        public GoldAnalysisService(List<GoldPrice> goldPrices)
        {
            _goldPrices = goldPrices;
        }
        public double GetAveragePrice()
        {
            return _goldPrices.Average(p => p.Price);
        }

        public List<GoldPrice> Get3HighestPrices()
        {
            return _goldPrices
            .Where(p => p.Date > DateTime.Today.AddYears(-1))
            .OrderByDescending(p => p.Price)
            .Take(3)
            .ToList();
        }

        public List<GoldPrice> Get3LowestPrices()
        {
            // query syntax
            var query = (from p in _goldPrices
                            where p.Date>=DateTime.Today.AddYears(-1)
                         orderby p.Price
                         select p).Take(3);
                         
                         return query.ToList();
            
                   }

                   //If one had bought gold in January 2020, is it possible that they would have earned
                    //more than 5%? On which days?

public List<GoldPrice> GetPrices2020andMoreThan5(){
    
        var prices2020 = _goldPrices.Where(p => p.Date.Year == 2020);
        var firstPrice = prices2020.First().Price;
        var prices2020andMoreThan5 = prices2020.Where(p => p.Price > firstPrice * 1.05).Take(1);
        
        return prices2020andMoreThan5.ToList();
}

        //Which 3 dates of 2022-2019 opens the second ten of the prices ranking? (note that
        //the app allows only to get data about the last … days)

        public List<GoldPrice> GetOpensSecondTen()
        {
            var prices2022_2019 = _goldPrices.Where(p => p.Date.Year >= 2019 && p.Date.Year <= 2022);
            var orderedPrices = prices2022_2019.OrderByDescending(p => p.Price);
            var twenty = orderedPrices.Take(20);
            var secondTen = orderedPrices.Skip(10).Take(3);
            return secondTen.ToList();
        }

        //(query syntax) What are the averages of gold prices in 2020, 2023, 2024?

        public List<double> GetAvg2020_2024()
        {
            var prices2020_2024 = _goldPrices.Where(p => p.Date.Year >= 2020 && p.Date.Year <= 2024);
            var groupedPrices = from p in prices2020_2024
                                where p.Date.Year==2020 || p.Date.Year==2023 || p.Date.Year==2024
                                group p by p.Date.Year;

            var avgPrices = groupedPrices.Select(g => g.Average(p => p.Price)).ToList();
            avgPrices = avgPrices.Select(p => Math.Round(p, 2)).ToList();
            return avgPrices;
        }

        //When it would be best to buy gold and sell it between 2020 and 2024? What would
        //be the return on investment?

        public List<GoldPrice> GetWhenToBuy()
        {
            var prices2020_2024 = _goldPrices.Where(p => p.Date.Year >= 2020 && p.Date.Year <= 2024);
            var minPrice = prices2020_2024.OrderBy(p => p.Price).First();
            var maxPrice = prices2020_2024.OrderByDescending(p => p.Price).First();
            var roi = maxPrice.Price - minPrice.Price;
            var result = new List<GoldPrice> { minPrice, maxPrice, new GoldPrice { Date = DateTime.Today, Price = roi } };
            return result;
        }
    }
}
