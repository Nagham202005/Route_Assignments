using ConsoleApp1;

Console.WriteLine("Starting Bookstore System...");

using (AppDbContext context = new AppDbContext())
{
    context.Database.EnsureCreated();
}

Console.WriteLine("Database Created Successfully! Check your SQL Server.");