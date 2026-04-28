using (var context = new AppDbContext())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Console.WriteLine("EventHub Database Created Successfully with 3 different configurations!");
}