using CoreLibrary.DTOs.Orders.Requests;
using CoreLibrary.DTOs.Orders.Response;
using Domain.Wrappers;

namespace CoreLibrary.Interface.Services;

public interface IOrderService
{
    Task<Response<OrderDtoResponse>> InsertOrder(OrderDtoRequests orderDtoRequests);

    Task<Response<List<OrderDtoResponse>>> GetOrders();

    Task<Response<OrderDtoResponse>> UpdateOrder(OrderUpDto orderUpDto);

    Task<Response<Boolean>> RemoveOrder(int orderId);
}