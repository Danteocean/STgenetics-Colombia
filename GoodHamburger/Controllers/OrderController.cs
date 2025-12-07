using CoreLibrary.DTOs.Orders.Requests;
using CoreLibrary.Interface.Services;
using Domain.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Controllers;

[ApiController]
[Route("Order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("GetOrders")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await _orderService.GetOrders());
    }

    [HttpPost("InsertOrder")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InsertOrder([FromBody] OrderDtoRequests orderDtoRequests)
    {
        return Ok(await _orderService.InsertOrder(orderDtoRequests));
    }

  

    [HttpPut("UpdateOrder")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderUpDto orderUpDto)
    {
        return Ok(await _orderService.UpdateOrder(orderUpDto));
    }

    [HttpDelete("RemoveOrder")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveOrder(int orderId)
    {
        return Ok(await _orderService.RemoveOrder(orderId));
    }
}
