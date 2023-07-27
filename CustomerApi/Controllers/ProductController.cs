
using CustomerApi.Models;
using CustomerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService) =>
        _productService = productService;

    [HttpGet]
    //public async Task<IActionResult> Get() =>
    //    await _productService.GetAsync();

    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAsync();
        return Ok(products);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
        var product = await _productService.GetById(id);
        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product newProduct)
    {
        newProduct.CategoryName = null;
        await _productService.CreateAsync(newProduct);
        //return Ok("created successfully");
        return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Product updateProduct)
    {
        updateProduct.CategoryName = null;
        var product = await _productService.GetById(id);

        if (product is null)
        {
            return NotFound();
        }

        updateProduct.Id = product.Id;

        await _productService.UpdateAsync(id, updateProduct);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _productService.GetById(id);

        if (product is null)
        {
            return NotFound();
        }

        await _productService.RemoveAsync(id);

        return NoContent();
    }
}