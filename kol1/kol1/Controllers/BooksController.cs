using kol1.Models;
using kol1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace kol1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BooksController: ControllerBase
{
    private readonly IBookRepository _bookRepository;
    
    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet("/{id}/authors")]
    public async Task<IActionResult> GetBookWithAuthors(int idBook)
    {
        if (!await _bookRepository.ExistsAsync(idBook)) return NotFound($"Book with given ID - {idBook} cannot be found");
        var authors = await _bookRepository.GetAuthorsByIdAsync(idBook);
        return Ok(authors);
    }
    [HttpPost("/authors")]
    public async Task<IActionResult> PostBookWithAuthors(PostBookWithAuthorsDTO bookWithAuthors)
    {
        if (!await _bookRepository.AlreadyExists(bookWithAuthors))
        {
            return BadRequest($"{bookWithAuthors} already exists");
        }

        await _bookRepository.InsertBookWithAuthors(bookWithAuthors);

        return Created();
    }

}