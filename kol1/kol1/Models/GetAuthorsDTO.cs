namespace kol1.Models;

public class GetAuthorsDTO
{
    
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Author> Authors { get; set; }
}