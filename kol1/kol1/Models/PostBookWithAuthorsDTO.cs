using System.ComponentModel.DataAnnotations;

namespace kol1.Models;

public class PostBookWithAuthorsDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Title { get; set; }
    
    public List<Author> Authors { get; set; }
}