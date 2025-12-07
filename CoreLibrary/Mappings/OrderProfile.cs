using AutoMapper;
using CoreLibrary.DTOs.Orders.Response;
using Domain.Entities;

namespace CoreLibrary.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDtoResponse>();
        CreateMap<OrderGetLarge, OrderDtoResponse>();
        CreateMap<ExtrasDtoResponse, DTOs.Extras.Response.ExtrasDtoResponse>();
    }
}