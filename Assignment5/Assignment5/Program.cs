using System;

namespace Assignment5
{
    #region Enums and Helper Methods
    // Part 1 Enum
    enum DayOfWeek
    {
        Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday
    }

    // Final Project Enum
    enum Grade { A, B, C, D, F }

    internal class Program
    {
        #region Part 3 & Final Project Static Methods
        // Part 3: Calculator Methods
        static double Add(double a, double b) => a + b;
        static double Subtract(double a, double b) => a - b;
        static double Multiply(double a, double b) => a * b;
        static double Divide(double a, double b)
        {
            if (b == 0) return 0; // Handle division by zero
            return a / b;
        }

        // Part 3: Circle Calculator with 'out'
        static void CalculateCircle(double radius, out double area, out double circumference)
        {
            area = Math.PI * Math.Pow(radius, 2);
            circumference = 2 * Math.PI * radius;
        }

        // Final Project Methods
        static Grade GetGrade(int score)
        {
            if (score >= 90) return Grade.A;
            if (score >= 80) return Grade.B;
            if (score >= 70) return Grade.C;
            if (score >= 60) return Grade.D;
            return Grade.F;
        }

        static double CalculateAverage(int[] scores)
        {
            double sum = 0;
            foreach (var s in scores) sum += s;
            return sum / scores.Length;
        }

        static void GetMinMax(int[] scores, out int min, out int max)
        {
            min = scores[0];
            max = scores[0];
            foreach (var s in scores)
            {
                if (s < min) min = s;
                if (s > max) max = s;
            }
        }
        #endregion

        static void Main(string[] args)
        {
            #region Part 1: Enums (Day of the Week)
            Console.WriteLine("--- Part 1: Enums ---");
            Console.Write("Enter a day number (1-7): ");
            int dayNum = int.Parse(Console.ReadLine()) - 1;

            DayOfWeek selectedDay = (DayOfWeek)dayNum;
            Console.WriteLine($"Day: {selectedDay}");

            switch (selectedDay)
            {
                case DayOfWeek.Friday:
                case DayOfWeek.Saturday:
                    Console.WriteLine("It's the Weekend");
                    break;
                default:
                    Console.WriteLine("It's a Workday");
                    break;
            }
            Console.WriteLine("\n----------------------\n");
            #endregion

            #region Part 2: Arrays Q1 (Statistics)
            Console.WriteLine("--- Part 2: Array Statistics ---");
            Console.Write("Enter array size: ");
            int size = int.Parse(Console.ReadLine());
            int[] numbers = new int[size];

            for (int i = 0; i < size; i++)
            {
                Console.Write($"Enter element [{i}]: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }

            int sum = 0;
            int maxArr = numbers[0];
            int minArr = numbers[0];

            for (int i = 0; i < size; i++)
            {
                sum += numbers[i];
                if (numbers[i] > maxArr) maxArr = numbers[i];
                if (numbers[i] < minArr) minArr = numbers[i];
            }

            Console.WriteLine($"Sum = {sum}");
            Console.WriteLine($"Average = {(double)sum / size}");
            Console.WriteLine($"Max = {maxArr}");
            Console.WriteLine($"Min = {minArr}");
            Console.Write("Reverse = ");
            for (int i = size - 1; i >= 0; i--)
            {
                Console.Write(numbers[i] + (i == 0 ? "" : ", "));
            }
            Console.WriteLine("\n\n----------------------\n");
            #endregion

            #region Part 2: Arrays Q2 (Student Matrix)
            Console.WriteLine("--- Part 2: Student Grades Matrix ---");
            int[,] matrix = new int[3, 4];
            double totalClassSum = 0;

            for (int i = 0; i < 3; i++)
            {
                double studentSum = 0;
                Console.WriteLine($"Enter grades for Student {i + 1}:");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write($" Subject {j + 1}: ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                    studentSum += matrix[i, j];
                }
                totalClassSum += studentSum;
                Console.WriteLine($"Average for Student {i + 1}: {studentSum / 4}");
            }
            Console.WriteLine($"Overall Class Average: {totalClassSum / (3 * 4)}");
            Console.WriteLine("\n----------------------\n");
            #endregion

            #region Part 3: Functions (Calculator & Circle)
            Console.WriteLine("--- Part 3: Functions ---");
            
            Console.Write("Enter first number: ");
            double n1 = double.Parse(Console.ReadLine());
            Console.Write("Enter second number: ");
            double n2 = double.Parse(Console.ReadLine());
            Console.Write("Enter operation (+, -, *, /): ");
            char op = char.Parse(Console.ReadLine());

            double result = op switch
            {
                '+' => Add(n1, n2),
                '-' => Subtract(n1, n2),
                '*' => Multiply(n1, n2),
                '/' => Divide(n1, n2),
                _ => 0
            };
            Console.WriteLine($"Result: {result}");

            // Circle Calculator with out
            Console.Write("\nEnter Circle Radius: ");
            double rad = double.Parse(Console.ReadLine());
            CalculateCircle(rad, out double area, out double circ);
            Console.WriteLine($"Area: {area:F2}, Circumference: {circ:F2}");
            Console.WriteLine("\n----------------------\n");
            #endregion

            #region Final Project: Student Grade Manager
            Console.WriteLine("--- Final Project: Student Grade Manager ---");
            int[] scores = new int[5];
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Enter score for Student {i + 1}: ");
                scores[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("\n--- Report ---");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Student {i + 1}: {scores[i]} -> Grade: {GetGrade(scores[i])}");
            }

            GetMinMax(scores, out int finalMin, out int finalMax);
            Console.WriteLine($"\nAverage: {CalculateAverage(scores)}");
            Console.WriteLine($"Highest Score: {finalMax}");
            Console.WriteLine($"Lowest Score: {finalMin}");
            #endregion

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}