using System;
using System.Collections.Generic;

namespace ShopMasterApp
{
    #region 1. Product Model
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // "Electronics", "Clothing", "Food", "Books"
        public double Price { get; set; }
        public int Stock { get; set; }
    }
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            #region 2. Product Catalog (Starter Code)
            List<Product> catalog = new List<Product>
            {
                new Product { Id=1, Name="Laptop", Category="Electronics", Price=1200, Stock=10 },
                new Product { Id=2, Name="Phone", Category="Electronics", Price=800, Stock=25 },
                new Product { Id=3, Name="T-Shirt", Category="Clothing", Price=30, Stock=100 },
                new Product { Id=4, Name="Jeans", Category="Clothing", Price=60, Stock=50 },
                new Product { Id=5, Name="Chocolate", Category="Food", Price=5, Stock=200 },
                new Product { Id=6, Name="Coffee Beans", Category="Food", Price=15, Stock=80 },
                new Product { Id=7, Name="C# Book", Category="Books", Price=45, Stock=30 },
                new Product { Id=8, Name="Novel", Category="Books", Price=20, Stock=60 },
                new Product { Id=9, Name="Headphones", Category="Electronics", Price=150, Stock=40 },
                new Product { Id=10, Name="Jacket", Category="Clothing", Price=120, Stock=15 }
            };
            #endregion

            #region Task 01: Smart Product Search Execution
            Console.WriteLine("--- Electronics ---");
            var electronics = SearchProducts(catalog, p => p.Category == "Electronics");
            electronics.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));

            Console.WriteLine("\n--- Under $50 ---");
            var cheapProducts = SearchProducts(catalog, p => p.Price < 50);
            cheapProducts.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));

            Console.WriteLine("\n--- In Stock ---");
            var inStock = SearchProducts(catalog, p => p.Stock > 0);
            inStock.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));

            Console.WriteLine("\n--- Clothing Under $100 ---");
            var cheapClothing = SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100);
            cheapClothing.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));
            #endregion

            #region Task 03.1: Print Reports Execution
            Console.WriteLine("\n--- Short Report ---");
            PrintReport(catalog, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

            Console.WriteLine("\n--- Detailed Report ---");
            PrintReport(catalog, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));
            #endregion

            #region Task 03.2: Transform Products Execution
            Console.WriteLine("\n--- Summary List ---");
            var summary = TransformProducts(catalog, p => $"{p.Name} (${p.Price})");
            summary.ForEach(s => Console.WriteLine(s));

            Console.WriteLine("\n--- Price Labels ---");
            var labels = TransformProducts(catalog, p => $"{p.Name}: {(p.Price > 100 ? "Expensive!" : "Affordable")}");
            labels.ForEach(l => Console.WriteLine(l));
            #endregion

            #region Task 03.3: Filter Products Execution
            Console.WriteLine("\n--- Low-Stock Alert ---");
            var lowStock = FilterProducts(catalog, p => p.Stock < 20);
            lowStock.ForEach(p => Console.WriteLine($"[LOW STOCK] {p.Name}: only {p.Stock} left!"));
            #endregion

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        #region Logic Methods (The actual implementation)

        public static List<Product> SearchProducts(List<Product> products, Func<Product, bool> filter)
        {
            List<Product> result = new List<Product>();
            foreach (var prod in products)
            {
                if (filter(prod)) result.Add(prod);
            }
            return result;
        }

        public static void PrintReport(List<Product> products, Action<Product> reportAction)
        {
            foreach (var prod in products)
            {
                reportAction(prod);
            }
        }

        public static List<TResult> TransformProducts<TResult>(List<Product> products, Func<Product, TResult> transformer)
        {
            List<TResult> results = new List<TResult>();
            foreach (var prod in products)
            {
                results.Add(transformer(prod));
            }
            return results;
        }

        public static List<Product> FilterProducts(List<Product> products, Predicate<Product> condition)
        {
            List<Product> result = new List<Product>();
            foreach (var prod in products)
            {
                if (condition(prod)) result.Add(prod);
            }
            return result;
        }

        #endregion
    }
}