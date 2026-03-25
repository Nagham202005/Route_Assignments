using System;
using System.Collections.Generic;
using System.Linq;

/* 
 * PART 01: THEORETICAL QUESTIONS
 * 
 * Q1: Abstraction is hiding internal details and showing only essential features. 
 *     Encapsulation is bundling data and methods into a single unit and restricting access.
 *     Example: A Car's Dashboard. Abstraction: You only see the steering wheel and pedals (essential features). 
 *     Encapsulation: The engine and gears are hidden under the hood (restricting access to internal complexity).
 * 
 * Q2: Differences: 
 *     1. Inheritance: A class can inherit from one abstract class but many interfaces.
 *     2. Members: Abstract classes can have fields and constructors; interfaces cannot.
 *     3. Implementation: Abstract classes can provide default logic for any member; interfaces (pre-C#8) only signatures.
 *     4. Access Modifiers: Abstract members can be protected/internal; interface members are public by default.
 *     Choice: Use Abstract Class for a common base with shared logic. Use Interface for defining a "can-do" behavior.
 * 
 * Q3: 
 *     a) No, 'Appliance' is abstract and cannot be instantiated directly.
 *     b) PowerConsumption: Abstract (must be defined by child). Status: Virtual (optional override). Label: Concrete (shared logic).
 *     c) "Standby", because Toaster does not override the Status() method.
 * 
 * Q4: 
 *     a) A class split across multiple files. Used for organizing large classes or separating generated code from manual code.
 *     b) A method declared in one partial part and optionally implemented in another. If not implemented, the compiler removes all calls to it.
 *     c) Adding methods to existing types without modifying them. Rules: Static class, Static method, 'this' keyword on first parameter.
 *     d) $20.00
 */

namespace ConsoleApp1
{
    // --- PART 02: PRACTICAL IMPLEMENTATION ---

    // 1. Abstract Base Class
    public abstract class Ticket
    {
        public int TicketId { get; set; }
        public string MovieName { get; set; }
        public double BasePrice { get; set; }
        public bool IsBooked { get; private set; }

        public void Book() => IsBooked = true;
        public void Cancel() => IsBooked = false;

        // Abstract method: Every ticket must calculate its price differently
        public abstract double CalculateFinalPrice();

        // Virtual method: Shared logic but can be customized
        public virtual void PrintDetails()
        {
            Console.Write($"[Ticket #{TicketId}] {MovieName} | ");
        }
    }

    // Concrete Ticket Types
    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }
        public override double CalculateFinalPrice() => BasePrice * 1.14; // 14% Tax
        public override void PrintDetails()
        {
            base.PrintDetails();
            Console.WriteLine($"Standard | Seat: {SeatNumber} | Price: {BasePrice} | Final: {CalculateFinalPrice():F2} | Booked: {(IsBooked ? "Yes" : "No")}");
        }
    }

    public class VipTicket : Ticket
    {
        public bool HasLoungeAccess { get; set; }
        public double AdditionalFee { get; set; }
        public override double CalculateFinalPrice() => (BasePrice + AdditionalFee) * 1.14;
        public override void PrintDetails()
        {
            base.PrintDetails();
            Console.WriteLine($"VIP | Lounge: {(HasLoungeAccess ? "Yes" : "No")} | Fee: {AdditionalFee} | Price: {BasePrice} | Final: {CalculateFinalPrice():F2} | Booked: {(IsBooked ? "Yes" : "No")}");
        }
    }

    public class ImaxTicket : Ticket
    {
        public bool Is3D { get; set; }
        public override double CalculateFinalPrice() => (BasePrice + 50) * 1.14;
        public override void PrintDetails()
        {
            base.PrintDetails();
            Console.WriteLine($"IMAX | 3D: {(Is3D ? "Yes" : "No")} | Price: {BasePrice} | Final: {CalculateFinalPrice():F2} | Booked: {(IsBooked ? "Yes" : "No")}");
        }
    }

    // 2. Partial Cinema Class - Part 1: Management
    public partial class Cinema
    {
        private List<Ticket> tickets = new List<Ticket>();

        public void Open()
        {
            Console.WriteLine("=== Cinema Opened ===");
            Console.WriteLine(" Projector ON\n");
        }

        public void Close()
        {
            Console.WriteLine("\n Projector OFF");
            Console.WriteLine("=== Cinema Closed ===");
        }

        public void AddTicket(Ticket t) => tickets.Add(t);
    }

    // 2. Partial Cinema Class - Part 2: Reporting
    public partial class Cinema
    {
        public void PrintAllTickets()
        {
            Console.WriteLine("--- All Tickets (from Cinema.Reporting) ---");
            foreach (var t in tickets) t.PrintDetails();
            Console.WriteLine();
        }

        public Ticket[] GetTicketsArray() => tickets.ToArray();
    }

    // 3. Extension Methods
    public static class TicketExtensions
    {
        public static string GetReceipt(this Ticket t)
        {
            return "========== RECEIPT ==========\n" +
                   $" Movie   : {t.MovieName}\n" +
                   $" Type    : {t.GetType().Name}\n" +
                   $" Price   : {t.BasePrice}\n" +
                   $" Final   : {t.CalculateFinalPrice():F2}\n" +
                   $" Status  : {(t.IsBooked ? "Booked" : "Cancelled")}\n" +
                   "=============================";
        }

        public static double CalculateTotalRevenue(this Ticket[] tickets)
        {
            return tickets.Sum(t => t.CalculateFinalPrice());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Cinema myCinema = new Cinema();
            myCinema.Open();

            // a. Proof that abstract class cannot be instantiated
            // Ticket t = new Ticket("Test", 100); // ERROR: Cannot create instance of abstract type 'Ticket'
            Console.WriteLine("// Ticket t = new Ticket(\"Test\", 100); // ERROR: Cannot create instance of abstract type 'Ticket'\n");

            // b. Create concrete tickets
            StandardTicket t1 = new StandardTicket { TicketId = 1, MovieName = "Inception", BasePrice = 80, SeatNumber = "A5" };
            VipTicket t2 = new VipTicket { TicketId = 2, MovieName = "Avengers", BasePrice = 200, HasLoungeAccess = true, AdditionalFee = 50 };
            ImaxTicket t3 = new ImaxTicket { TicketId = 3, MovieName = "Dune", BasePrice = 130, Is3D = true };

            t1.Book();
            t2.Book();
            t3.Book();

            // c. Add to Cinema and use Partial Reporting file
            myCinema.AddTicket(t1);
            myCinema.AddTicket(t2);
            myCinema.AddTicket(t3);
            myCinema.PrintAllTickets();

            // d. Polymorphism demonstration
            Console.WriteLine("--- Polymorphism: Final Price per Ticket ---");
            Ticket[] ticketArray = myCinema.GetTicketsArray();
            foreach (var t in ticketArray)
            {
                Console.WriteLine($"{t.GetType().Name} => Final Price: {t.CalculateFinalPrice():F2}");
            }

            // e. Extension Method: Receipt
            Console.WriteLine("\n--- Extension Method: Receipt ---");
            Console.WriteLine(t2.GetReceipt());

            // f. Extension Method: Total Revenue
            Console.WriteLine("\n--- Extension Method: Total Revenue ---");
            double total = ticketArray.CalculateTotalRevenue();
            Console.WriteLine($"Total Revenue: {total:F2}");

            // g. Close Cinema
            myCinema.Close();

            Console.ReadLine(); // Keep window open
        }
    }
}