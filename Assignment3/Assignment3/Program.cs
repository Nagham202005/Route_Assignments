using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace Assignment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question 01: String Efficiency
            Console.WriteLine("=== Question 01: String Performance ===");
            // (a) Explanation: Strings are immutable. Every += creates a new string object,
            // copying old data, which is slow and memory-intensive in loops.

            int iterations = 5000;
            Stopwatch sw = new Stopwatch();

            // (c) Timing the old version
            Console.WriteLine("Running String Concatenation...");
            string productList = "";
            sw.Start();
            for (int i = 1; i <= iterations; i++)
            {
                productList += "PROD-" + i + ",";
            }
            sw.Stop();
            long time1 = sw.ElapsedMilliseconds;
            Console.WriteLine($"String Concatenation took: {time1} ms");

            // (b) Rewriting using StringBuilder
            sw.Restart();
            Console.WriteLine("\nRunning StringBuilder...");
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= iterations; i++)
            {
                sb.Append("PROD-").Append(i).Append(",");
            }
            sw.Stop();
            long time2 = sw.ElapsedMilliseconds;
            Console.WriteLine($"StringBuilder took: {time2} ms");

            Console.WriteLine($"\nTime Difference: {time1 - time2} ms");
            Console.WriteLine("\nPress any key to move to Question 02...");
            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Question 02: Ticket Pricing System
            Console.WriteLine("=== Question 02: Cinema Ticket System ===");

            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter day of week (1-7, where 6=Fri, 7=Sat): ");
            int day = int.Parse(Console.ReadLine());

            Console.Write("Do you have a student ID? (yes/no): ");
            string studentInput = Console.ReadLine().ToLower();
            bool isStudent = (studentInput == "yes");

            double price = 0;
            string breakdown = "";

            if (age < 5) { price = 0; breakdown = "Base Price: Free"; }
            else if (age <= 12) { price = 30; breakdown = "Base Price (5-12): 30 LE"; }
            else if (age <= 59) { price = 50; breakdown = "Base Price (13-59): 50 LE"; }
            else { price = 25; breakdown = "Base Price (60+): 25 LE"; }

            if (price > 0 && (day == 6 || day == 7))
            {
                price += 10;
                breakdown += "\nWeekend Surcharge: +10 LE";
            }

            if (price > 0 && isStudent)
            {
                double discount = price * 0.20;
                price -= discount;
                breakdown += $"\nStudent Discount (20%): -{discount} LE";
            }

            Console.WriteLine("\n------- Breakdown -------");
            Console.WriteLine(breakdown);
            Console.WriteLine($"Final Price: {price} LE");

            Console.WriteLine("\nPress any key to move to Question 03...");
            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Question 03: Switch Statements
            Console.WriteLine("=== Question 03: File Extension Classifier ===");
            string fileExt = ".pdf";
            string fileType;

            // (a) Traditional Switch
            switch (fileExt.ToLower())
            {
                case ".pdf": fileType = "PDF Document"; break;
                case ".docx": case ".doc": fileType = "Word Document"; break;
                case ".xlsx": case ".xls": fileType = "Excel Spreadsheet"; break;
                case ".jpg": case ".png": case ".gif": fileType = "Image File"; break;
                default: fileType = "Unknown File Type"; break;
            }
            Console.WriteLine($"(Traditional) Type: {fileType}");

            // (b) Switch Expression
            fileType = fileExt.ToLower() switch
            {
                ".pdf" => "PDF Document",
                ".docx" or ".doc" => "Word Document",
                ".xlsx" or ".xls" => "Excel Spreadsheet",
                ".jpg" or ".png" or ".gif" => "Image File",
                _ => "Unknown File Type"
            };
            Console.WriteLine($"(Expression) Type: {fileType}");

            Console.WriteLine("\nPress any key to move to Question 04...");
            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Question 04: Ternary Operator
            Console.WriteLine("=== Question 04: Weather Advice (Ternary) ===");
            int temp = 35;

            // Rewriting using only ternary operators
            string advice = temp < 0 ? "Freezing! Stay indoors."
                          : temp < 15 ? "Cold. Wear a jacket."
                          : temp < 25 ? "Pleasant weather."
                          : temp < 35 ? "Warm. Stay hydrated."
                          : "Hot! Avoid sun exposure.";

            Console.WriteLine($"Temperature: {temp}, Advice: {advice}");

            // Discussion:
            // "Is the ternary version more readable?" 
            // Answer: No. Nested ternaries are harder to read and debug. 
            // Choose ternary for simple true/false assignments, but use if-else for complex logic like this.

            Console.WriteLine("\nPress any key to move to Question 05...");
            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Question 05: Password Validation
            Console.WriteLine("=== Question 05: Password Validation ===");
            int attempts = 0;
            bool isValid = false;

            do
            {
                Console.Write("\nEnter a password: ");
                string password = Console.ReadLine();
                attempts++;

                bool hasUpper = false, hasDigit = false, noSpace = true;
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpper = true;
                    if (char.IsDigit(c)) hasDigit = true;
                    if (char.IsWhiteSpace(c)) noSpace = false;
                }

                List<string> violations = new List<string>();
                if (password.Length < 8) violations.Add("- Minimum 8 characters");
                if (!hasUpper) violations.Add("- At least one uppercase letter");
                if (!hasDigit) violations.Add("- At least one digit");
                if (!noSpace) violations.Add("- No spaces allowed");

                if (violations.Count == 0)
                {
                    Console.WriteLine("Password accepted!");
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid password. Violations:");
                    foreach (var v in violations) Console.WriteLine(v);

                    if (attempts >= 5)
                    {
                        Console.WriteLine("\nAccount locked! Too many attempts.");
                        break;
                    }
                    Console.WriteLine($"Attempts remaining: {5 - attempts}");
                }

            } while (!isValid && attempts < 5);

            Console.WriteLine("\nPress any key to move to Question 06...");
            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Question 06: Array Processing
            Console.WriteLine("=== Question 06: Array Processing ===");
            int[] scores = { 85, 42, 91, 67, 55, 78, 39, 88, 72, 95, 60, 48 };

            // (a) Failing scores
            Console.Write("Failing scores (<50): ");
            foreach (int s in scores) if (s < 50) Console.Write(s + " ");

            // (b) First score above 90
            Console.Write("\nFirst score > 90: ");
            foreach (int s in scores)
            {
                if (s > 90) { Console.Write(s); break; }
            }

            // (c) Average excluding absent (<40)
            double sum = 0; int count = 0;
            foreach (int s in scores)
            {
                if (s < 40) continue; // Absent
                sum += s; count++;
            }
            Console.WriteLine($"\nClass Average (excluding <40): {sum / count:F2}");

            // (d) Grade ranges count
            int A = 0, B = 0, C = 0, D = 0, F = 0;
            foreach (int s in scores)
            {
                if (s >= 90) A++;
                else if (s >= 80) B++;
                else if (s >= 70) C++;
                else if (s >= 60) D++;
                else F++;
            }
            Console.WriteLine($"Grade Ranges: A:{A}, B:{B}, C:{C}, D:{D}, F:{F}");

            Console.WriteLine("\nAssignment Completed! Press any key to exit.");
            Console.ReadKey();
            #endregion
        }
    }
}