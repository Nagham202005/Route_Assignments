using System;
using System.Collections.Generic;

/* 
 * PART 01: THEORETICAL QUESTIONS
 * 
 * Q1: An interface is a contract that defines a set of signatures (methods, properties) without implementation. 
 * We use them to achieve loose coupling and polymorphism. 
 * Benefits: 1. Multiple inheritance support. 2. Improved testability (Mocking). 3. Code flexibility and maintenance.
 * 
 * Q2: 
 * a) Problem: Name collision. Currently, the single Greet() implementation satisfies both interfaces simultaneously.
 * b) Fix: Use "Explicit Interface Implementation" (e.g., void IEnglishSpeaker.Greet()).
 * c) No, you cannot call it directly on the object. You must cast the object to the specific interface first.
 * 
 * Q3: Shallow copy copies value types and references (pointing to same memory). Deep copy creates new instances 
 * for all reference-type fields. Use shallow for performance with value types; use deep when independence is required. 
 * Risk: In shallow copies, modifying a reference field in the copy changes the original object.
 * 
 * Q4: 
 * Output:
 * Dev - Testing
 * QA - Testing
 * Why: Title is a string (replaces reference), but Dept is a reference type. 
 * Since it's a shallow copy, both e1 and e2 point to the same Department object.
 */

namespace ConsoleApp1
{
    // --- PART 02: PRACTICAL IMPLEMENTATION ---

    // 1. Unified Printing Contract
    public interface IPrintable
    {
        void Print();
    }

    // 2. Booking & Cancellation Contract
    public interface IBookable
    {
        bool IsBooked { get; }
        void Book();
        void Cancel();
    }

    // Base Ticket Class
    public abstract class Ticket : IPrintable, IBookable, ICloneable
    {
        public int TicketId { get; set; }
        public string MovieName { get; set; }
        public double BasePrice { get; set; }
        public bool IsBooked { get; private set; }

        public abstract double CalculatePrice();

        public void Book()
        {
            if (IsBooked) Console.WriteLine($"Ticket {TicketId} is already booked.");
            else IsBooked = true;
        }

        public void Cancel()
        {
            if (!IsBooked) Console.WriteLine($"Ticket {TicketId} is not booked yet.");
            else IsBooked = false;
        }

        public abstract void Print();

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    // Concrete Ticket Types
    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public override double CalculatePrice() => BasePrice * 1.14;

        public override void Print()
        {
            Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | Standard | Seat: {SeatNumber} | Price: {BasePrice} | After Tax: {CalculatePrice()} | Booked: {(IsBooked ? "Yes" : "No")}");
        }
    }

    public class VipTicket : Ticket
    {
        public bool HasLoungeAccess { get; set; }
        public double AdditionalFee { get; set; }

        public override double CalculatePrice() => (BasePrice + AdditionalFee) * 1.14;

        public override void Print()
        {
            Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | VIP | Lounge: {(HasLoungeAccess ? "Yes" : "No")} | Fee: {AdditionalFee} | Price: {BasePrice} | After Tax: {CalculatePrice()} | Booked: {(IsBooked ? "Yes" : "No")}");
        }

        // Deep Copy Implementation
        public override object Clone()
        {
            VipTicket cloned = (VipTicket)this.MemberwiseClone();
            // In a more complex object, you would manually instantiate nested reference types here
            return cloned;
        }
    }

    public class ImaxTicket : Ticket
    {
        public bool Is3D { get; set; }

        public override double CalculatePrice() => (BasePrice + 50) * 1.14;

        public override void Print()
        {
            Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | IMAX | 3D: {(Is3D ? "Yes" : "No")} | Price: {BasePrice} | After Tax: {CalculatePrice()} | Booked: {(IsBooked ? "Yes" : "No")}");
        }
    }

    // Cinema Manager
    public class Cinema
    {
        private List<Ticket> tickets = new List<Ticket>();

        public void Open() => Console.WriteLine("=== Cinema Opened ===\n");
        public void Close() => Console.WriteLine("\n=== Cinema Closed ===");

        public void AddTicket(Ticket t) => tickets.Add(t);

        public void PrintAllTickets()
        {
            Console.WriteLine("--- All Tickets ---");
            foreach (var t in tickets) t.Print();
            Console.WriteLine();
        }
    }

    // Utility Method
    public static class BookingHelper
    {
        public static void PrintAll(IPrintable[] printables)
        {
            Console.WriteLine("--- BookingHelper.PrintAll ---");
            foreach (var item in printables)
            {
                item.Print();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Cinema myCinema = new Cinema();
            myCinema.Open();

            // Create Tickets
            StandardTicket t1 = new StandardTicket { TicketId = 1, MovieName = "Inception", BasePrice = 80, SeatNumber = "A5" };
            VipTicket t2 = new VipTicket { TicketId = 2, MovieName = "Avengers", BasePrice = 200, HasLoungeAccess = true, AdditionalFee = 50 };
            ImaxTicket t3 = new ImaxTicket { TicketId = 3, MovieName = "Dune", BasePrice = 130, Is3D = true };

            // Book and Add
            t1.Book();
            t2.Book();
            t3.Book();

            myCinema.AddTicket(t1);
            myCinema.AddTicket(t2);
            myCinema.AddTicket(t3);

            // Print all through Cinema
            myCinema.PrintAllTickets();

            // Clone VIP Ticket and prove independence
            Console.WriteLine("--- Clone Test ---");
            VipTicket clonedVip = (VipTicket)t2.Clone();
            clonedVip.TicketId = 4;
            clonedVip.MovieName = "Interstellar";
            clonedVip.Cancel(); // Status should be 'No' for clone, 'Yes' for original

            Console.Write("Original : "); t2.Print();
            Console.Write("Clone    : "); clonedVip.Print();
            Console.WriteLine();

            // Cancel and reprint
            Console.WriteLine("--- After Cancellation ---");
            t1.Cancel();
            t1.Print();
            Console.WriteLine();

            // Utility method print
            IPrintable[] printableItems = { t1, t2, t3 };
            BookingHelper.PrintAll(printableItems);

            myCinema.Close();

            // Prevent auto-close
            Console.ReadLine();
        }
    }
}