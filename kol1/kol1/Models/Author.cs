using System.ComponentModel.DataAnnotations;

namespace kol1.Models;

public class Author
{   
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
}