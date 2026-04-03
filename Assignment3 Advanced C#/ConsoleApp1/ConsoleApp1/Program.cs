using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Exercise 1: Student Grade Manager (List)
            Console.WriteLine("--- Exercise 1: Student Grade Manager ---");
            // 1. Create Collection
            List<int> grades = new List<int> { 85, 92, 78, 95, 88, 70, 100, 65 };

            // 2. Print Count, First, Last
            Console.WriteLine($"Count: {grades.Count}, First: {grades[0]}, Last: {grades[grades.Count - 1]}");

            // 3. Sort and print
            grades.Sort();
            Console.WriteLine("Sorted Grades: " + string.Join(", ", grades));

            // 4. First grade above 90
            int firstAbove90 = grades.FirstOrDefault(g => g > 90);
            Console.WriteLine($"First grade > 90: {firstAbove90}");

            // 5. Grades below 75
            var failing = grades.Where(g => g < 75).ToList();
            Console.WriteLine("Failing grades: " + string.Join(", ", failing));

            // 6. Remove failing grades
            grades.RemoveAll(g => g < 75);
            Console.WriteLine("Grades after removing failing: " + string.Join(", ", grades));

            // 7. Check for 100
            bool hasFullMark = grades.Contains(100);
            Console.WriteLine($"Any grade equals 100? {hasFullMark}");

            // 8. Project to List<string>
            List<string> formattedGrades = grades.Select(g => $"Grade: {g}").ToList();
            formattedGrades.ForEach(Console.WriteLine);
            #endregion

            #region Exercise 2: Leaderboard (SortedList)
            Console.WriteLine("\n--- Exercise 2: Leaderboard ---");
            // SortedList keeps keys sorted automatically
            SortedList<int, string> leaderboard = new SortedList<int, string>();
            leaderboard.Add(500, "Ahmed");
            leaderboard.Add(200, "Sara");
            leaderboard.Add(800, "Ali");
            leaderboard.Add(350, "Mona");

            // 2. Print entries (sorted by score)
            foreach (var entry in leaderboard)
                Console.WriteLine($"{entry.Key} = {entry.Value}");

            // 3. First key and value
            Console.WriteLine($"Top Score: {leaderboard.Keys[leaderboard.Count - 1]}, Top Player: {leaderboard.Values[leaderboard.Count - 1]}");

            // 4. Check if 500 exists
            Console.WriteLine($"Score 500 exists? {leaderboard.ContainsKey(500)}");

            // 5. Safely get score 999
            if (leaderboard.TryGetValue(999, out string player))
                Console.WriteLine($"Player 999: {player}");
            else
                Console.WriteLine("Player 999: Not Found");

            // 6. Remove score 200
            leaderboard.Remove(200);
            Console.WriteLine("Updated Leaderboard count: " + leaderboard.Count);
            #endregion

            #region Exercise 3: Phone Book (Dictionary)
            Console.WriteLine("\n--- Exercise 3: Phone Book ---");
            Dictionary<string, string> phoneBook = new Dictionary<string, string> {
                {"Ali", "010111"}, {"Sara", "011222"}, {"Mona", "012333"}, {"Zaki", "015444"}
            };

            // 2. [] Syntax (Add or Update)
            phoneBook["Ali"] = "010999"; // Update

            // 3. Duplicate .Add() with Try-Catch
            try { phoneBook.Add("Sara", "011555"); }
            catch (ArgumentException ex) { Console.WriteLine($"Error: {ex.Message}"); }

            // 4. .TryAdd()
            bool added = phoneBook.TryAdd("Sara", "011555");
            Console.WriteLine($"TryAdd duplicate succeeded? {added}");

            // 5 & 6. Fallback Search
            string searchName = "Unknown";
            string number = phoneBook.ContainsKey(searchName) ? phoneBook[searchName] : "Not Found";
            Console.WriteLine($"{searchName}: {number}");

            // 7. Print Keys and Values
            Console.WriteLine("Names: " + string.Join(", ", phoneBook.Keys));
            Console.WriteLine("Numbers: " + string.Join(", ", phoneBook.Values));
            #endregion

            #region Exercise 4: Unique Email Validator (HashSet)
            Console.WriteLine("\n--- Exercise 4: Unique Email Validator ---");
            // Case-insensitive comparer ensures uniqueness regardless of Capital/Small letters
            HashSet<string> emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            emails.Add("ahmed@test.com");
            emails.Add("AHMED@test.com");
            emails.Add("sara@test.com");
            emails.Add("Sara@Test.Com");

            // Count will be 2 because duplicates are ignored due to case-insensitivity
            Console.WriteLine($"Stored unique emails count: {emails.Count}");

            // Set Operations
            HashSet<int> setA = new HashSet<int> { 1, 2, 3, 4, 5 };
            HashSet<int> setB = new HashSet<int> { 4, 5, 6, 7, 8 };

            var union = new HashSet<int>(setA); union.UnionWith(setB);
            Console.WriteLine("Union: " + string.Join(",", union));

            var intersect = new HashSet<int>(setA); intersect.IntersectWith(setB);
            Console.WriteLine("Intersection: " + string.Join(",", intersect));

            var except = new HashSet<int>(setA); except.ExceptWith(setB);
            Console.WriteLine("Except: " + string.Join(",", except));

            Console.WriteLine($"Is {{1,2}} subset of Set A? {new HashSet<int> { 1, 2 }.IsSubsetOf(setA)}");
            #endregion

            #region Exercise 5: Print Queue Simulator (Queue)
            Console.WriteLine("\n--- Exercise 5: Print Queue Simulator ---");
            Queue<string> printQueue = new Queue<string>();
            string[] docs = { "Report.pdf", "Invoice.pdf", "Letter.docx", "Resume.pdf", "Photo.jpg" };
            foreach (var doc in docs) printQueue.Enqueue(doc);

            Console.WriteLine($"Queue Count: {printQueue.Count}");
            Console.WriteLine($"Next to print (Peek): {printQueue.Peek()}");

            while (printQueue.Count > 0)
                Console.WriteLine($"Printing: {printQueue.Dequeue()}");

            bool canDequeue = printQueue.TryDequeue(out string result);
            Console.WriteLine($"TryDequeue on empty? {canDequeue}");
            #endregion

            #region Exercise 6: Browser History (Stack)
            Console.WriteLine("\n--- Exercise 6: Browser History (Undo) ---");
            Stack<string> history = new Stack<string>();
            history.Push("google.com");
            history.Push("github.com");
            history.Push("stackoverflow.com");
            history.Push("youtube.com");
            history.Push("claude.ai");

            Console.WriteLine($"Current Page (Peek): {history.Peek()}");

            for (int i = 0; i < 3; i++)
                Console.WriteLine($"Back from: {history.Pop()}");

            Console.WriteLine($"Current page after 3 backs: {history.Peek()}");

            history.Clear(); // Empty stack
            bool canPop = history.TryPop(out string popResult);
            Console.WriteLine($"TryPop on empty stack? {canPop}");
            #endregion

            Console.WriteLine("\nDone. Press any key to exit.");
            Console.ReadKey();
        }
    }
}