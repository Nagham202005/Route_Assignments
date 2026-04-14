using System;
using System.Collections.Generic;
using System.Linq;
using LINQ_Day01_G01.Models;
using static LINQ_Day01_G01.DataSource.Source;

namespace LINQAssignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Get all products from the "Seafood" category. Print each product's name and price.
            Console.WriteLine("--- Question 01 ---");
            var result01 = ProductList.Where(P => P.Category == "Seafood")
                                      .Select(P => new { P.ProductName, P.UnitPrice });

            foreach (var item in result01)
                Console.WriteLine(item);


            // 4. Get all products where UnitPrice is between 10 and 30
            Console.WriteLine("\n--- Question 04 ---");
            var result04 = ProductList.Where(P => P.UnitPrice >= 10 && P.UnitPrice <= 30);

            foreach (var item in result04)
                Console.WriteLine(item);


            // 5. Get all products that are in stock (UnitsInStock > 0) and belong to the "Condiments" category.
            Console.WriteLine("\n--- Question 05 ---");
            var result05 = ProductList.Where(P => P.UnitsInStock > 0 && P.Category == "Condiments");

            foreach (var item in result05)
                Console.WriteLine(item);


            // 9. Get all products from the "Beverages" category, sorted by UnitsInStock descending. Print name and stock.
            Console.WriteLine("\n--- Question 09 ---");
            var result09 = ProductList.Where(P => P.Category == "Beverages")
                                      .OrderByDescending(P => P.UnitsInStock)
                                      .Select(P => new { P.ProductName, P.UnitsInStock });

            foreach (var item in result09)
                Console.WriteLine(item);


            // 10. Using QUERY SYNTAX with a compound from clause, list all orders placed in 1997 or later 
            // showing CustomerID and OrderDate.
            Console.WriteLine("\n--- Question 10 (Query Syntax) ---");
            var result10 = from C in CustomerList
                           from O in C.Orders
                           where O.OrderDate.Year >= 1997
                           select new { C.CustomerID, O.OrderDate };

            foreach (var item in result10)
                Console.WriteLine(item);


            // 13. Create a list of all digits in the array whose second letter is 'i' 
            // that is reversed from the order in the original array.
            Console.WriteLine("\n--- Question 13 ---");
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var result13 = digits.Where(d => d.Length > 1 && d[1] == 'i')
                                 .Reverse();

            foreach (var item in result13)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}