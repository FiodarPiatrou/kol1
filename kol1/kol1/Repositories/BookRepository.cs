using kol1.Models;
using Microsoft.Data.SqlClient;

namespace kol1.Repositories;

public class BookRepository:IBookRepository
{
    private readonly IConfiguration _configuration;

    public BookRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> ExistsAsync(int idBook)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await connection.OpenAsync();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM book b where b.PK=@idBook";
        command.Parameters.AddWithValue("@idBook", idBook);
        var result =Convert.ToInt32( await command.ExecuteScalarAsync());
        if (result==1)
        {
            return true;
        }

        return false;
    }

    public async Task<GetAuthorsDTO> GetAuthorsByIdAsync(int idBook)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await connection.OpenAsync();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT PK, title, first_name,last_name  FROM books b " +
                              "join books_authors ba on b.PK=ba.FK_book" +
                              "JOIN authors a on ba.FK_author where b.PK=@idBook";
        command.Parameters.AddWithValue("@idBook", idBook);
        var reader = await command.ExecuteReaderAsync();
        var titleOrdinal = reader.GetOrdinal("title");
        var pkOrdinal = reader.GetOrdinal("PK");
        var firstNameOrdinal = reader.GetOrdinal("first_name");
        var lastNameOrdinal = reader.GetOrdinal("last_Name");
        GetAuthorsDTO authors = null;
        while (await reader.ReadAsync())
        {
            if (authors is not null)
            {
                authors.Authors.Add(new Author()
                {
                    FirstName = reader.GetString(firstNameOrdinal),
                    LastName = reader.GetString(lastNameOrdinal)
                });
            }
            else
            {
                authors = new GetAuthorsDTO()
                {
                    Id = reader.GetInt32(pkOrdinal),
                    Title = reader.GetString(titleOrdinal),
                    Authors = new List<Author>()
                };
            }
        }

        return authors;

    }

    public async Task<bool> AlreadyExists(PostBookWithAuthorsDTO bookWithAuthors)
    {
        throw new NotImplementedException();
    }

    public async Task InsertBookWithAuthors(PostBookWithAuthorsDTO bookWithAuthors)
    {
        throw new NotImplementedException();
    }
}