using System;
using System.Collections.Generic;
using System.Linq;
using LINQ_Day01_G01.Models;
using static LINQ_Day01_G01.DataSource.Source;

namespace LINQAssignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Rest of Assignment 1

            Console.WriteLine("Rest of Assignment 1");

            // 2. Get a list of only the product names from ProductList.
            var res1_02 = ProductList.Select(p => p.ProductName);

            // 3. Sort all products by UnitPrice (ascending).
            var res1_03 = ProductList.OrderBy(p => p.UnitPrice);

            // 6. Create a new anonymous type (Name, Price, StockStatus)
            var res1_06 = ProductList.Select(p => new
            {
                Name = p.ProductName,
                Price = p.UnitPrice,
                StockStatus = p.UnitsInStock > 0 ? "Available" : "Out of Stock"
            });

            // 7. Print product's name along with its position (1-based).
            var res1_07 = ProductList.Select((p, index) => $"{index + 1}. {p.ProductName}");

            // 8. Sort ProductList by Category ascending, then UnitPrice descending.
            var res1_08 = ProductList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);

            // 11. Show position number alongside ProductName (Similar to 7)
            var res1_11 = ProductList.Select((p, index) => new { Index = index, p.ProductName });

            // 12. Sort words in array by length then case-insensitive.
            string[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var res1_12 = Arr.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);

            #endregion

            #region Assignment 2

            Console.WriteLine("Assignment 2");

            // 1. Get top 3 most expensive products
            var res2_01 = ProductList.OrderByDescending(p => p.UnitPrice).Take(3);

            // 2. show page 2 of products, with page size = 5
            var res2_02 = ProductList.Skip(5).Take(5);

            // 3. Take products as long as Their UnitPrice is less than $25
            var res2_03 = ProductList.TakeWhile(p => p.UnitPrice < 25);

            // 4. Check if ALL products in the "Seafood" category are in stock
            bool res2_04 = ProductList.Where(p => p.Category == "Seafood").All(p => p.UnitsInStock > 0);

            // 5. Check if the ID list contains 9
            int[] ids = { 3, 9, 13, 18 };
            bool res2_05 = ids.Contains(9);

            // 6. Group all products by Category and print each group with its product count.
            var res2_06 = ProductList.GroupBy(p => p.Category)
                                     .Select(g => new { Category = g.Key, Count = g.Count() });

            // 7. Group products by Category and project only product names per group
            var res2_07 = ProductList.GroupBy(p => p.Category)
                                     .Select(g => new { Category = g.Key, Names = g.Select(p => p.ProductName) });

            // 8. Find all categories that have MORE THAN 3 products
            var res2_08 = ProductList.GroupBy(p => p.Category)
                                     .Where(g => g.Count() > 3)
                                     .Select(g => g.Key);

            // 9. Using QUERY SYNTAX, group customers by Country { Country, Count, TotalOrderValue }
            var res2_09 = from c in CustomerList
                          group c by c.Country into g
                          select new
                          {
                              Country = g.Key,
                              Count = g.Count(),
                              TotalOrderValue = g.SelectMany(c => c.Orders).Sum(o => o.Total)
                          };

            // 10. Calculate the total number of units in stock across all products
            var res2_10 = ProductList.Sum(p => p.UnitsInStock);

            // 11. Find the CHEAPEST and MOST EXPENSIVE product prices
            var res2_11_Min = ProductList.Min(p => p.UnitPrice);
            var res2_11_Max = ProductList.Max(p => p.UnitPrice);

            // 12. Get a distinct list of all product categories
            var res2_12 = ProductList.Select(p => p.Category).Distinct();

            // 16. Get the first product whose price is greater than $50.
            var res2_16 = ProductList.FirstOrDefault(p => p.UnitPrice > 50);

            // 17. Try to get the first product with a price > $500 (returns null instead of throwing)
            var res2_17 = ProductList.FirstOrDefault(p => p.UnitPrice > 500);

            #endregion
            Console.WriteLine($"Total Stock: {res2_10}");

            Console.ReadLine();
        }
    }
}