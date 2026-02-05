using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAssignment
{
    class Program
    {
        // Class-level field for scope demonstrations
        static int classField = 100;

        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           C# FUNDAMENTALS - ASSIGNMENT WITH ANSWERS                ║");
            Console.WriteLine("║                      20 Questions                                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝\n");

            #region Question 1: Regions
            // Q: What is the purpose of #region and #endregion directives in C#? 
            // How do they help in code organization?

            // Answer: 
            // The purpose of #region and #endregion is to allow developers to collapse and expand blocks of code.
            // It helps in code organization by hiding complex or long sections of code, 
            // making the file easier to read and navigate.

            Console.WriteLine("Question 1: Regions explained in code comments.");
            Console.WriteLine("\n" + new string('-', 70) + "\n");
            #endregion

            #region Question 2: Variable Declaration - Explicit vs Implicit
            // Q: What is the difference between explicit and implicit variable declaration?

            // EXPLICIT DECLARATION 
            int age = 25;
            string name = "Route";
            double salary = 5000.50;

            // IMPLICIT DECLARATION
            var myAge = 25;
            var myName = "Route";
            var mySalary = 5000.50;

            Console.WriteLine($"Question 2: Explicit Age: {age}, Implicit Age: {myAge}");
            #endregion

            #region Question 3: Constants
            // Q: Write the syntax for declaring a constant. Why use it instead of a variable?

            // Answer:
            // Syntax: const <data_type> <name> = <value>;
            // We use constants to prevent the value from being changed during execution 
            // and to improve code readability.

            const double Pi = 3.14159;
            const int DaysInWeek = 7;
            const string CompanyName = "Route Academy";

            Console.WriteLine($"Question 3: Constant PI: {Pi}");
            #endregion

            #region Question 4: Class-level vs Method-level Scope
            // Q: Explain the difference between class-level and method-level scope.

            // Answer:
            // 1. Class-level: Accessible by all methods in the class.
            // 2. Method-level: Accessible only inside the specific method where it is declared.

            int methodVariable = 10;
            Console.WriteLine($"Question 4: Class Field: {classField}, Method Variable: {methodVariable}");
            #endregion

            #region Question 5: Block-level Scope
            // Q: What is block-level scope?

            // Answer:
            // A variable is only accessible within the specific block { } where it was declared.
            {
                int blockVar = 50;
                Console.WriteLine($"Question 5: Inside block: {blockVar}");
            }
            // blockVar is not accessible here.
            #endregion

            #region Question 6: Variable Lifetime - Local vs Static
            // Q: Explain the lifetime of local variables vs static variables.

            // Answer:
            // 1. Local Variables: Exist only while the method is executing.
            // 2. Static Variables: Exist for the entire duration of the program.

            Console.WriteLine("Question 6: Lifetime theory explained in comments.");
            #endregion

            #region Question 7: Garbage Collector
            // Q: What is the Garbage Collector in C#?

            // Answer:
            // It is an automatic memory manager in .NET that reclaims memory occupied 
            // by objects that are no longer reachable or used.
            #endregion

            #region Question 8: Variable Shadowing
            // Q: What is variable shadowing in C#?

            // Answer:
            // It occurs when a variable in an inner scope has the same name as one in an outer scope.
            // C# does NOT allow shadowing local variables within nested blocks of the same method.
            #endregion

            #region Question 9: C# Naming Rules
            // 1. Must start with a letter or underscore.
            // 2. Cannot start with a digit.
            // 3. Only letters, digits, and underscores allowed.
            // 4. Case-sensitive.
            // 5. Cannot use reserved keywords.
            #endregion

            #region Question 10: Naming Conventions
            // (a) Local Variables: camelCase
            // (b) Class Names: PascalCase
            // (c) Constants: PascalCase
            #endregion

            #region Question 11: Error Types
            // 1. Syntax Error: Grammatical mistakes (e.g., missing semicolon).
            // 2. Runtime Error: Occurs during execution (e.g., division by zero).
            // 3. Logical Error: Incorrect logic producing wrong results.
            #endregion

            #region Question 12: Exception Handling Importance
            // Answer:
            // It prevents application crashes, provides user-friendly error messages,
            // and ensures proper resource cleanup.
            #endregion

            #region Question 13: try-catch-finally
            // Q: Write a code example demonstrating try-catch-finally.
            try
            {
                int result = 10 / int.Parse("0");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Question 13 Catch: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Question 13 Finally: This always executes.");
            }
            #endregion

            #region Question 14: Common Built-in Exceptions
            // 1. NullReferenceException
            // 2. IndexOutOfRangeException
            // 3. DivideByZeroException
            // 4. FormatException
            // 5. OverflowException
            #endregion

            #region Question 15: Multiple catch Blocks
            // Q: Why is the order important?
            // Answer: Specific exceptions must be caught before general exceptions.
            try
            {
                int x = 0;
                int y = 5 / x;
            }
            catch (DivideByZeroException) { Console.WriteLine("Question 15: Caught Specific."); }
            catch (Exception) { Console.WriteLine("Question 15: Caught General."); }
            #endregion

            #region Question 16: throw Keyword
            // Answer:
            // 'throw' preserves the original stack trace.
            // 'throw ex' resets the stack trace to the current line.
            #endregion

            #region Question 17: Stack and Heap Memory
            // Answer:
            // Stack: Fast, small, LIFO, stores value types.
            // Heap: Large, dynamic, managed by GC, stores reference types.
            #endregion

            #region Question 18: Value Types vs Reference Types
            // Value Type Example
            int val1 = 10;
            int val2 = val1;
            val2 = 20; // val1 remains 10

            // Reference Type Example
            int[] ref1 = { 1, 2, 3 };
            int[] ref2 = ref1;
            ref2[0] = 99; // ref1[0] becomes 99

            Console.WriteLine($"Question 18: ValueType: {val1}, ReferenceType: {ref1[0]}");
            #endregion

            #region Question 19: Object in C#
            // Answer:
            // 'object' is the base type to provide a unified type system.
            // Inherited methods: ToString(), Equals(), GetHashCode(), GetType().

            object obj = 42;
            Console.WriteLine($"Question 19: Object Type: {obj.GetType()}");
            #endregion

            Console.WriteLine("\nAssignment Completed. Press any key to exit...");
            Console.ReadKey();

        } // End of Main
    } // End of Program Class

    // Question 6 Helper Class (Outside Main)
    public class ExampleClass
    {
        public static int counter = 0;
        public void MyMethod()
        {
            int x = 10;
            Console.WriteLine(x);
        }
    }
} // End of Namespace