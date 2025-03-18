using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");

        // Step 1: Get gold prices
       GoldDataService dataService = new GoldDataService();


int[] years = { 2019, 2020, 2021,2022,2023 ,2024};

//List<GoldPrice> goldPrices = new List<GoldPrice>();

        DateTime startDate1 = new DateTime(2025,01,01);
        DateTime endDate1 = DateTime.Now;
        List<GoldPrice> goldPrices = dataService.GetGoldPrices(startDate1, endDate1).GetAwaiter().GetResult();

for (int i = 0; i < years.Length; i++)
{
    DateTime startDate = new DateTime(years[i], 01, 01);
    DateTime endDate = new DateTime(years[i], 12, 31);
    
    List<GoldPrice> prices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();
    goldPrices.AddRange(prices);
}



        if (goldPrices.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {goldPrices.Count} records. Ready for analysis.");

        // Step 2: Perform analysis
        GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        var avgPrice = analysisService.GetAveragePrice();
        //a. (method and query syntax) What are the TOP 3 highest and TOP 3 lowest prices of
        //gold within the last year?
        var top3HP= analysisService.Get3HighestPrices();
        var top3LP = analysisService.Get3LowestPrices();

        //b.
        var prices2020andMoreThan5 = analysisService.GetPrices2020andMoreThan5();
        //c.
    var opensSecondTen = analysisService.GetOpensSecondTen();
    //d.
    var prices2020_2024 = analysisService.GetAvg2020_2024();

//e. 
var whenToBuy = analysisService.GetWhenToBuy();

        // Step 3: Print results
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");
        GoldResultPrinter.PrintPrices(top3HP, "Top 3 Highest Gold Prices");
        GoldResultPrinter.PrintPrices(top3LP, "Top 3 Lowest Gold Prices");
        GoldResultPrinter.PrintPrices(prices2020andMoreThan5, "Proce in 2020 more than 5%");

        GoldResultPrinter.PrintPrices(opensSecondTen, "Second Ten of the Prices Ranking");

        GoldResultPrinter.PrintGoldPrices(prices2020_2024, "Averages of Gold Prices in 2020, 2023, 2024");
        GoldResultPrinter.PrintPrices(whenToBuy, "When to Buy Gold");

       
        GoldResultPrinter.SaveToXml(goldPrices, "goldPrices.xml");

        List<GoldPrice> readPrices = GoldResultPrinter.ReadFromXml("goldPrices.xml");
        Console.WriteLine("Read from XML file:");
        GoldResultPrinter.PrintPrices(readPrices, "Gold Prices");

        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

    }
}
