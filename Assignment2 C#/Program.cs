namespace Route_Csharp_Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1: What will this print and explain what happens?
            double d = 9.99;
            int x = (int)d;
            Console.WriteLine(x);
            #endregion

            #region Q2: Fix integer division
            int n = 5;
            double d2 = n / 2.0;
            Console.WriteLine($"Result: {d2}");
            #endregion

            #region Q3: Read age as int
            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine($"Your age is: {age}");
            #endregion

            #region Q4: What happens with "12a"?
            #endregion

            #region Q5: TryParse (The safe way)
            string s5 = "12a";
            if (int.TryParse(s5, out int result))
            {
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Invalid");
            }
            #endregion

            #region Q6: Boxing and Unboxing
            object o = 10;
            int a = (int)o;
            Console.WriteLine($"Q6: {a + 1}");
            #endregion

            #region Q7: Unboxing Error handling
            object o7 = 10;
            if (o7 is int temp)
            {
                long h = temp;
                Console.WriteLine($"Q7: {h}");
            }
            #endregion

            #region Q8: Avoid exceptions (Print -1 if failed)
            object o8 = "Route";
            long x8 = (o8 is int result8) ? result8 : -1;
            Console.WriteLine($"Q8: {x8}");
            #endregion

            #region Q9: Null-conditional operator (?.)
            string? name = null;
            Console.WriteLine($"Q9: {name?.Length}");
            #endregion

            #region Q10: Null-coalescing operator (??)
            string? name2 = null;
            int length = name2?.Length ?? 0;
            Console.WriteLine($"Q10: {length}");
            #endregion

            #region Q11: Handling null before Parsing
            string? s11 = null;
            int x11 = int.Parse(s11 ?? "0");
            Console.WriteLine($"Q11: {x11}");
            #endregion

            #region Q12: Null-forgiving operator (!)
            string? s12 = null;
            Console.WriteLine($"Q12: Handling with ?. -> {s12?.Length}");
            #endregion

            #region Q13: Convert.ToInt32 with null
            string? s13 = null;
            int x13 = Convert.ToInt32(s13);
            Console.WriteLine($"Q13: {x13}");
            #endregion

            #region Q14: Compare Parse vs Convert
            string? s14 = null;
            Console.WriteLine("Q14: Parse throws error, Convert returns 0");
            #endregion

            #region Q15: Final Exercise (Guest or Name)
            string? user = null;
            string result15 = user?.ToUpper() ?? "Guest";
            Console.WriteLine($"Q15: {result15}");
            #endregion
        }
    }
}
