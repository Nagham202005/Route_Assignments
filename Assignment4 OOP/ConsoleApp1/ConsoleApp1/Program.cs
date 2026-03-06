/*

Q1: Relationships
a) University has Departments: Composition (Departments die if University is closed).
b) Driver uses a Car: Association.
c) Dog is an Animal: Inheritance (IS-A relationship).
d) Team has Players: Aggregation (Players still exist if Team is deleted).
e) Method receives Logger: Dependency.

Q2: Access Modifiers & Sealed
a) Child class in a different assembly can access a 'protected' field only via inheritance, 
   but not through an object instance from the outside.
b) 'protected internal' allows access within the same assembly OR child classes. 
   'private protected' allows access within the same assembly AND only for child classes.
c) 'sealed' on a class prevents inheritance. On a method, it prevents further overriding 
   in child classes (must be used with override).
d) Yes, an object can be created from a 'sealed' class using 'new'. It only blocks 
   inheritance, not instantiation.
*/

using System;

namespace MovieTicketSystem_Ass03
{
    public abstract class Ticket
    {
        private static int _ticketCounter = 0;
        private decimal _price;

        public int TicketId { get; }
        public string MovieName { get; set; }

        public virtual decimal Price
        {
            get => _price;
            set { if (value > 0) _price = value; }
        }

        public decimal PriceAfterTax => Price * 1.14m;

        public Ticket(string movieName, decimal price)
        {
            MovieName = movieName;
            Price = price;
            TicketId = ++_ticketCounter;
        }

        public override string ToString()
        {
            return $"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP | After Tax: {PriceAfterTax:F2} EGP";
        }

        public static int GetTotalTickets() => _ticketCounter;
    }

    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }
        public StandardTicket(string movieName, decimal price, string seat) : base(movieName, price)
        {
            SeatNumber = seat;
        }
        public override string ToString() => base.ToString() + $" | Seat: {SeatNumber}";
    }

    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; set; } = 50;
        public override decimal Price => base.Price + ServiceFee;

        public VIPTicket(string movieName, decimal price, bool lounge) : base(movieName, price)
        {
            LoungeAccess = lounge;
        }
        public override string ToString() => base.ToString() + $" | Lounge: {(LoungeAccess ? "Yes" : "No")} | Service Fee: {ServiceFee} EGP";
    }

    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }
        public override decimal Price => Is3D ? base.Price + 30 : base.Price;

        public IMAXTicket(string movieName, decimal price, bool is3D) : base(movieName, price)
        {
            Is3D = is3D;
        }
        public override string ToString() => base.ToString() + $" | IMAX 3D: {(Is3D ? "Yes" : "No")}";
    }

    public class Projector
    {
        public void Start() => Console.WriteLine("Projector started.");
        public void Stop() => Console.WriteLine("Projector stopped.");
    }

    public class Cinema
    {
        public string CinemaName { get; set; }
        private Projector _projector;
        private Ticket[] _tickets = new Ticket[20];
        private int _count = 0;

        public Cinema(string name)
        {
            CinemaName = name;
            _projector = new Projector();
        }

        public void OpenCinema()
        {
            Console.WriteLine($"\n========= {CinemaName} Opened =========");
            _projector.Start();
        }

        public void CloseCinema()
        {
            Console.WriteLine($"\n========= {CinemaName} Closed =========");
            _projector.Stop();
        }

        public void AddTicket(Ticket t)
        {
            if (_count < 20) _tickets[_count++] = t;
        }

        public void PrintAllTickets()
        {
            Console.WriteLine("\n========= All Tickets =========");
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_tickets[i].ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cinema myCinema = new Cinema("Route Cinema");
            myCinema.OpenCinema();

            Ticket t1 = new StandardTicket("Inception", 120, "A-5");
            Ticket t2 = new VIPTicket("Avengers", 200, true);
            Ticket t3 = new IMAXTicket("Dune", 180, false);

            myCinema.AddTicket(t1);
            myCinema.AddTicket(t2);
            myCinema.AddTicket(t3);

            myCinema.PrintAllTickets();

            Console.WriteLine("\n========= Statistics =========");
            Console.WriteLine($"Total Tickets Created: {Ticket.GetTotalTickets()}");

            myCinema.CloseCinema();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}