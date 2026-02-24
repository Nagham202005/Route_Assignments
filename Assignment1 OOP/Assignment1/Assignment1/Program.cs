using System;

namespace MovieBookingAssignment
{
    #region Part 01: Theoretical Answers
    /*
     Q1: Class vs Struct behavior:
         - Class: Reference Type (Stored on Heap). If you copy it, both variables point to the same object.
         - Struct: Value Type (Stored on Stack). If you copy it, you get a completely new independent copy.

     Q2: Public vs Private:
         - Public: Accessible from anywhere in the project.
         - Private: Accessible only inside the class it was defined in (Encapsulation).

     Q3 & Q4: Class Library:
         - It's a project that produces a .dll file, not an .exe. We use it to share code between different projects.
         - Steps: Right-click Solution -> Add New Project -> Class Library (.NET) -> Write code -> Build -> Add Reference in the main project.
    */
    #endregion

    #region Part 02: Supporting Types (Enums & Structs)

    // 1. Ticket Type represented by Enum
    public enum TicketType
    {
        Standard, // 0
        VIP,      // 1
        IMAX      // 2
    }

    // 2. Seat Location represented by Struct (Small, simple data container)
    public struct SeatLocation
    {
        public char Row;
        public int Number;

        public SeatLocation(char row, int number)
        {
            Row = row;
            Number = number;
        }

        public override string ToString() => $"{Row}{Number}";
    }
    #endregion

    #region Part 02: Ticket Class
    public class Ticket
    {
        // Properties
        public string MovieName { get; set; }
        public TicketType Type { get; set; }
        public SeatLocation Seat { get; set; }
        private double price; // Private field as requested

        // 3. Constructors
        // Full info Constructor
        public Ticket(string movieName, TicketType type, SeatLocation seat, double price)
        {
            MovieName = movieName;
            Type = type;
            Seat = seat;
            this.price = price;
        }

        // Default Constructor (chains to the full one using : this)
        // Handles: default type Standard, seat A1, price 50
        public Ticket(string movieName) : this(movieName, TicketType.Standard, new SeatLocation('A', 1), 50)
        {
        }

        // 4. Methods
        // a. Calculate Total after tax (Original price stays unchanged)
        public double CalcTotal(double taxPercent)
        {
            double taxAmount = price * (taxPercent / 100);
            return price + taxAmount;
        }

        // b. Apply Discount
        public void ApplyDiscount(ref double discountAmount)
        {
            if (discountAmount > 0 && discountAmount <= price)
            {
                price -= discountAmount;
                discountAmount = 0; // Discount consumed
            }
        }

        // c. Print Ticket Info
        public void PrintTicket(double taxPercent)
        {
            Console.WriteLine($"Movie      : {MovieName}");
            Console.WriteLine($"Type       : {Type}");
            Console.WriteLine($"Seat       : {Seat}");
            Console.WriteLine($"Price      : {price:F2}");
            Console.WriteLine($"Total ({taxPercent}% tax) : {CalcTotal(taxPercent):F2}");
        }
    }
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            #region User Interaction Logic
            Console.WriteLine("--- Movie Ticket Booking System ---");

            // Reading Data
            Console.Write("Enter Movie Name: ");
            string movie = Console.ReadLine();

            Console.Write("Enter Ticket Type (0 = Standard, 1 = VIP, 2 = IMAX): ");
            TicketType type = (TicketType)int.Parse(Console.ReadLine());

            Console.Write("Enter Seat Row (A, B, C...): ");
            char row = char.Parse(Console.ReadLine().ToUpper());

            Console.Write("Enter Seat Number: ");
            int seatNum = int.Parse(Console.ReadLine());

            Console.Write("Enter Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Enter Discount Amount: ");
            double discount = double.Parse(Console.ReadLine());

            // Create Ticket Object
            Ticket myTicket = new Ticket(movie, type, new SeatLocation(row, seatNum), price);

            // Output 1: Initial Ticket Info
            Console.WriteLine("\n===== Ticket Info =====");
            myTicket.PrintTicket(14); // 14% tax example

            // Apply Discount
            Console.WriteLine("\n===== After Discount =====");
            double tempDiscount = discount; // to show before/after
            myTicket.ApplyDiscount(ref discount);

            Console.WriteLine($"Discount Before : {tempDiscount:F2}");
            Console.WriteLine($"Discount After  : {discount:F2}");
            myTicket.PrintTicket(14);

            #endregion

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}