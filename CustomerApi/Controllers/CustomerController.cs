
using CustomerApi.Models;
using CustomerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService) =>
        _customerService = customerService;

    [HttpGet]
    public async Task<List<Customer>> Get() =>
        await _customerService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Customer>> Get(string id)
    {
        var book = await _customerService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Customer newBook)
    {
        await _customerService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Customer updatedBook)
    {
        var book = await _customerService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _customerService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _customerService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _customerService.RemoveAsync(id);

        return NoContent();
    }
}