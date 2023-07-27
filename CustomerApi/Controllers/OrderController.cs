
using CustomerApi.Models;
using CustomerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService) =>
        _orderService = orderService;

    [HttpGet]
    //public async Task<List<Order>> Get() =>
    //    await _orderService.GetAsync();
    public async Task<IActionResult> Get()
    {
        var orders = await _orderService.GetAsync();
        return Ok(orders);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Order>> Get(string id)
    {
        var order = await _orderService.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Order newOrder)
    {
        await _orderService.CreateAsync(newOrder);

        return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Order updatedOrder)
    {
        var order = await _orderService.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        updatedOrder.Id = order.Id;

        await _orderService.UpdateAsync(id, updatedOrder);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var order = await _orderService.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        await _orderService.RemoveAsync(id);

        return NoContent();
    }
}