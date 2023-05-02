using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Models;
using BookStoreAPI.Services;

namespace BookStoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService) => _bookService = bookService;

    // GET: api/Books
    [HttpGet]
    public async Task<List<Book>> Get() => await _bookService.GetAsync();

    // GET: api/Books/5
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _bookService.GetAsync(id);

        if(book is null)
        {
            return NotFound();
        }

        return book;
    }

    // POST: api/Books
    [HttpPost]
    public async Task<IActionResult> Post(Book newBook)
    {
        await _bookService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    // PUT: api/Books/5
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Book updateBook)
    {
        var book = await _bookService.GetAsync(id);
        if(book is null)
        {
            return NotFound();
        }

        updateBook.Id = book.Id;
        await _bookService.UpdateAsync(id, updateBook);

        return NoContent();
    }

    // DELETE: api/Books/5
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _bookService.GetAsync(id);

        if(book is null)
        {
            return NotFound();
        }

        await _bookService.RemoveAsync(id);

        return NoContent();
    }
}
