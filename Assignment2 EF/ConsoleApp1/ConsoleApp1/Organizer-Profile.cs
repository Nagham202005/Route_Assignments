using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Organizer
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public bool IsVerified { get; set; }
    public Profile Profile { get; set; }
}

public class Profile
{
    [Key, ForeignKey("Organizer")]
    public int OrganizerId { get; set; }
    public string Biography { get; set; }
    public string Website { get; set; }
    public string LogoUrl { get; set; }
    public Organizer Organizer { get; set; }
}