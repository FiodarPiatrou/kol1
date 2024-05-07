using kol1.Models;

namespace kol1.Repositories;

public interface IBookRepository
{
    Task<bool> ExistsAsync(int idBook);
    Task<GetAuthorsDTO> GetAuthorsByIdAsync(int idBook);
    Task<bool> AlreadyExists(PostBookWithAuthorsDTO bookWithAuthors);
    Task InsertBookWithAuthors(PostBookWithAuthorsDTO bookWithAuthors);
}