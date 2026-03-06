namespace ConsoleApp1
{
    public enum TicketType { Standard, VIP, IMAX }
    public struct SeatLocation
    {
        public char Row { get; set; }
        public int Number { get; set; }
        public override string ToString() => $"{Row}-{Number}";
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema();
            Console.WriteLine("========== Ticket Booking ==========");
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"\nEnter data for Ticket {i}:");
                Console.Write("Movie Name: "); string name = Console.ReadLine();
                Console.Write("Ticket Type (0=Standard, 1=VIP, 2=IMAX): ");
                TicketType type = (TicketType)int.Parse(Console.ReadLine());
                Console.Write("Seat Row (A-Z): "); char row = char.Parse(Console.ReadLine().ToUpper());
                Console.Write("Seat Number: "); int num = int.Parse(Console.ReadLine());
                Console.Write("Price: "); double price = double.Parse(Console.ReadLine());

                cinema.AddTicket(new Ticket(name, type, new SeatLocation { Row = row, Number = num }, price));
            }
            Console.WriteLine("\n========== All Tickets ==========");
            for (int i = 0; i < 3; i++)
            {
                var t = cinema[i];
                if (t != null)
                    Console.WriteLine($"Ticket #{t.TicketId} | {t.MovieName} | {t.Type} | Seat: {t.Seat} | Price: {t.Price} EGP | After Tax: {t.PriceAfterTax:F1} EGP");
            }
            Console.WriteLine("\n========== Search by Movie ==========");
            Console.Write("Enter movie name to search: ");
            string search = Console.ReadLine();
            var found = cinema.GetMovieByMovieName(search);
            if (found != null) Console.WriteLine($"Found: Ticket #{found.TicketId} | {found.MovieName} | {found.Type} | Seat: {found.Seat} | Price: {found.Price} EGP");
            else Console.WriteLine("Not Found.");

            Console.WriteLine("\n========== Statistics ==========");
            Console.WriteLine($"Total Tickets Sold: {Ticket.GetTotalTicketsSold()}");
            Console.WriteLine($"Booking Reference 1: {BookingHelper.GenerateBookingReference()}");
            Console.WriteLine($"Booking Reference 2: {BookingHelper.GenerateBookingReference()}");

            double groupTotal = BookingHelper.CalcGroupDiscount(5, 80);
            Console.WriteLine($"\nGroup Discount (5 tickets x 80 EGP): {groupTotal} EGP (10% off applied)");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
    public class Ticket
    {
        private string _movieName;
        private double _price;
        private static int _ticketCounter = 0;
        public int TicketId { get; private set; }
        public TicketType Type { get; set; }
        public SeatLocation Seat { get; set; }

        public string MovieName
        {
            get => _movieName;
            set { if (!string.IsNullOrWhiteSpace(value)) _movieName = value; }
        }
        public double Price
        {
            get => _price;
            set { if (value > 0) _price = value; }
        }
        public double PriceAfterTax => Price * 1.14;
        public Ticket(string movieName, TicketType type, SeatLocation seat, double price)
        {
            _movieName = movieName;
            Type = type;
            Seat = seat;
            _price = price;
            TicketId = ++_ticketCounter;
        }
        public static int GetTotalTicketsSold() => _ticketCounter;
    }
    public class Cinema
    {
        private Ticket[] _tickets = new Ticket[20];
        public Ticket this[int index]
        {
            get => (index >= 0 && index < 20) ? _tickets[index] : null;
            set { if (index >= 0 && index < 20) _tickets[index] = value; }
        }
        public Ticket GetMovieByMovieName(string name)
        {
            foreach (var t in _tickets)
                if (t != null && t.MovieName.Equals(name, StringComparison.OrdinalIgnoreCase)) return t;
            return null;
        }
        public bool AddTicket(Ticket t)
        {
            for (int i = 0; i < _tickets.Length; i++)
            {
                if (_tickets[i] == null) { _tickets[i] = t; return true; }
            }
            return false;
        }
    }
    public static class BookingHelper
    {
        private static int _refCounter = 0;
        public static double CalcGroupDiscount(int count, double price)
        {
            double total = count * price;
            return count >= 5 ? total * 0.9 : total;
        }
        public static string GenerateBookingReference() => $"BK-{++_refCounter}";
    }
}
