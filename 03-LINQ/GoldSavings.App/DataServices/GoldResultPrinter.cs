using System;
using System.Collections.Generic;
using System.Xml.Linq;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public static class GoldResultPrinter
    {
        public static void PrintPrices(List<GoldPrice> prices, string title)
        {
            Console.WriteLine($"\n--- {title} ---");
            foreach (var price in prices)
            {
                Console.WriteLine($"{price.Date:yyyy-MM-dd} - {price.Price} PLN");
            }
        }

        public static void PrintSingleValue<T>(T value, string title)
        {
            Console.WriteLine($"\n{title}: {value}");
        }

        public static void PrintGoldPrices(List<Double> prices, string title)
        {

            Console.WriteLine($"\n--- {title} ---");
          foreach (var price in prices)
            {
                Console.WriteLine($" {price} PLN ");
            }
        }

        //Write a method that saves the list of prices to a file in XML format.

        public static void SaveToXml(List<GoldPrice> prices, string fileName)
        {
            Console.WriteLine($"\n--- Saving to XML file {fileName} ---");
            XDocument doc = new XDocument();
            XElement root = new XElement("GoldPrices");
            foreach (var price in prices)
            {
                XElement priceElement = new XElement("Price",
                    new XElement("Date", price.Date),
                    new XElement("Price", price.Price));
                root.Add(priceElement);
            }
            doc.Add(root);
            doc.Save(fileName);
            Console.WriteLine("Saved successfully.");

        }

        //Write a method that reads the contents of the XML file from the previous set using one
        //instruction (you cannot use more than one semicolon).

       public static List<GoldPrice> ReadFromXml(string fileName) =>
         XDocument.Load(fileName).Root.Elements("Price")
        .Select(priceElement => new GoldPrice
        {
            Date = DateTime.Parse(priceElement.Element("Date").Value),
            Price = double.Parse(priceElement.Element("Price").Value)
        }).ToList();
    }
}