public class Branch
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public int ManagerId { get; set; }
    public Manager Manager { get; set; }
    public ICollection<Account> Accounts { get; set; }
}