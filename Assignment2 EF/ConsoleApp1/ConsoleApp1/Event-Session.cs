public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } 
    public int MaxAttendees { get; set; }
    public ICollection<Session> Sessions { get; set; }
}

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int EventId { get; set; }
    public Event ParentEvent { get; set; }
}