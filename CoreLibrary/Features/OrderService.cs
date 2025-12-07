using AutoMapper;
using CoreLibrary.DTOs.Orders.Requests;
using CoreLibrary.DTOs.Orders.Response;
using CoreLibrary.Interface.Repositories;
using CoreLibrary.Interface.Services;
using Domain.Entities;
using Domain.Querys;
using Domain.Wrappers;
using Microsoft.Extensions.Configuration;

namespace CoreLibrary.Features;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private string ConnectionString => _configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("Missing DefaultConnection");

    public OrderService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<Response<OrderDtoResponse>> InsertOrder(OrderDtoRequests dto)
    {
        try
        {
      
            var sandwichResponse = await GetSandwichOrErrorAsync(dto.sandwichId);
            if (!sandwichResponse.Succeeded)
            {
                return new Response<OrderDtoResponse> { State = "Error", Message = "Sandwich not found or inactive", Succeeded = false };
            }

            var sandwich = sandwichResponse.Data;

          
            var extrasResponse = await GetExtrasOrErrorAsync(dto.ExtraIds);
            if (!extrasResponse.Succeeded)
            {
                return new Response<OrderDtoResponse> { State = "Error", Message = extrasResponse.Message, Succeeded = false };
            }            

            var extras = extrasResponse.Data;


            decimal subtotal = CalculateSubtotal(sandwich.price, extras);

   
            var discountPercent = await GetDiscountPercentAsync(extras);


            decimal total = CalculateTotal(subtotal, discountPercent.discountPercent);


            var order = await SaveOrderAsync(sandwich.sandwichId, subtotal, total, discountPercent);


            await SaveOrderItemsAsync(order.orderId, extras);


            var response = new OrderDtoResponse
            {
                orderId = order.orderId,
                subtotal = subtotal,
                totalPrice = total
            };

            return new Response<OrderDtoResponse>(response){ State = "Ok",Message = "Order created", Succeeded = true, Data = response };
        }
        catch (Exception ex)
        {
            return new Response<OrderDtoResponse>{State = "Error", Message = ex.Message, Succeeded = false };
        }
    }


    public async Task<Response<List<OrderDtoResponse>>> GetOrders()
    {
        try
        {
            GetOrder getOrder = new GetOrder(ConnectionString);
            var data = await getOrder.Get();

            if (data == null || data.Count == 0)
            {
                return new Response<List<OrderDtoResponse>>{ State = "Error", Message = "No Orders found", Succeeded = false };
            }
               
            var result = _mapper.Map<List<OrderDtoResponse>>(data);

            return new Response<List<OrderDtoResponse>>(result) { State = "Ok", Message = "Successful response", Succeeded = true };
        }
        catch (Exception ex)
        {

            return new Response<List<OrderDtoResponse>> { State = "Error", Message = ex.Message, Succeeded = false };
        }
    }

    public async Task<Response<OrderDtoResponse>> UpdateOrder(OrderUpDto orderDtoRequests)
    {
        try
        {
            GetOrder getOrder = new GetOrder(ConnectionString);

            var order = await getOrder.GetByIdAsync(orderDtoRequests.orderId);
            if (order.orderId == 0)
            {
                return new Response<OrderDtoResponse> { State = "Error", Message = "Order not found or inactive",Succeeded = false};
            }

            var sandwichResponse = await GetSandwichOrErrorAsync(orderDtoRequests.sandwichId);
            if (!sandwichResponse.Succeeded)
            {
                return new Response<OrderDtoResponse> { State = "Error", Message = sandwichResponse.Message, Succeeded = false };
            }
                

            var sandwich = sandwichResponse.Data;


            var extrasResponse = await GetExtrasOrErrorAsync(orderDtoRequests.ExtraIds);
            if (!extrasResponse.Succeeded)
            {
                return new Response<OrderDtoResponse> { State = "Error", Message = extrasResponse.Message, Succeeded = false };
            }
                
            var extras = extrasResponse.Data;

           
            decimal newSubtotal = CalculateSubtotal(sandwich.price, extras);


            var newDiscountPercent = await GetDiscountPercentAsync(extras);


            decimal newTotal = CalculateTotal(newSubtotal, newDiscountPercent.discountPercent);

            order.sandwichId = orderDtoRequests.sandwichId;
            order.subtotal = newSubtotal;
            order.discountRuleId = newDiscountPercent.discountRuleId;
            order.totalPrice = newTotal;
            order.updatedDate = DateTime.UtcNow;
            order.createdDate = order.createdDate;
            await _unitOfWork.OrderAsync.UpdateAsync(order);


            RemoveOrderItem removeOrderItem = new RemoveOrderItem(ConnectionString);

            if (!await removeOrderItem.Remove(orderDtoRequests.orderId))
            {
                return new Response<OrderDtoResponse> { State = "Error", Message = "Error remove orderItem", Succeeded = false };
            }

            await SaveOrderItemsAsync(orderDtoRequests.orderId, extras);


            var response = new OrderDtoResponse
            {
                orderId = order.orderId,
                subtotal = newSubtotal,
                totalPrice = newTotal
            };

            return new Response<OrderDtoResponse>(response) {State = "Ok", Message = "Order updated successfully", Succeeded = true, Data = response};
        }
        catch (Exception ex)
        {
            return new Response<OrderDtoResponse>{ State = "Error",Message = ex.Message,Succeeded = false};
        }
    }



    private async Task<Response<Sandwich>> GetSandwichOrErrorAsync(int sandwichId)
    {
        var getSandwich = new GetSandwich(ConnectionString);

        var sandwich = await getSandwich.GetById(sandwichId);

        if (sandwich == null || !sandwich.isActive)
        {
            return new Response<Sandwich> { Succeeded = false, Message = "Sandwich not found" };
        }
            

        return new Response<Sandwich>(sandwich);
    }

    private async Task<Response<List<Extras>>> GetExtrasOrErrorAsync(List<int> extraIds)
    {
        var getExtras = new GetExtras(ConnectionString);
        var extras = await getExtras.GetByIds(extraIds);

        if (extras.Count != extraIds.Count)
        {
            return new Response<List<Extras>> { Succeeded = false, Message = "Duplicate extras or not found." };
        }
      
        var distinctCategories = extras.Select(e => e.categoryId).Distinct().Count();
        if (distinctCategories != extras.Count)
        {
            return new Response<List<Extras>> { Succeeded = false, Message = "Extras of the same category cannot repeat" };
        }
           
        return new Response<List<Extras>>(extras);
    }

    private decimal CalculateSubtotal(decimal sandwichPrice, List<Extras> extras)
    {
        return sandwichPrice + extras.Sum(e => e.price);
    }


    private async Task<DiscountRule> GetDiscountPercentAsync(List<Extras> extras)
    {
        GetDiscount getDiscount = new GetDiscount(ConnectionString);

        var (rules, ruleCategories) = await getDiscount.GetActiveRulesWithCategories();

        var ruleToCategories = ruleCategories
            .GroupBy(rc => rc.discountRuleId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.categoryId).ToHashSet());

        var selectedCategories = extras.Select(e => e.categoryId).ToHashSet();

        var matchedRule = rules
            .OrderByDescending(r => r.discountPercent)
            .FirstOrDefault(r =>
                ruleToCategories.TryGetValue(r.discountRuleId, out var required)
                && required.IsSubsetOf(selectedCategories)
            );

        return matchedRule;
    }

    private decimal CalculateTotal(decimal subtotal, int discountPercent)
    {
        return subtotal - Math.Round(subtotal * discountPercent / 100m, 2);
    }

    private async Task<Order> SaveOrderAsync(int sandwichId, decimal subtotal, decimal total, DiscountRule discountPercent)
    {
        var order = new Order
        {
            sandwichId = sandwichId,
            subtotal = subtotal,
            totalPrice = total,
            discountRuleId = discountPercent.discountRuleId,
            createdDate = DateTime.UtcNow,
            updatedDate = DateTime.UtcNow,
            isActive = true
        };

        await _unitOfWork.OrderAsync.AddAsync(order);
        return order;
    }

    private async Task SaveOrderItemsAsync(int orderId, List<Extras> extras)
    {
        foreach (var extra in extras)
        {
            await _unitOfWork.OrderItemAsync.AddAsync(new OrderItem
            {
                orderId = orderId,
                extraId = extra.extraId,
                createdDate = DateTime.UtcNow,
                updatedDate = DateTime.UtcNow
            });
        }
    }

    public async Task<Response<Boolean>> RemoveOrder(int orderId)
    {

        try
        {
            GetOrder getOrder = new GetOrder(ConnectionString);
            var order = await getOrder.GetByIdAsync(orderId);
            if (order == null)
            {
                return new Response<Boolean>{ State = "Error", Message = "Order not found or inactive", Succeeded = false };
            }

            order.isActive = false;

            await _unitOfWork.OrderAsync.UpdateAsync(order);
            return new Response<Boolean> { State = "Ok", Message = "Order deleted successfully", Succeeded = true, Data = true };
        }
        catch (Exception ex)
        {
            return new Response<Boolean> { State = "Error", Message = ex.Message, Succeeded = false };
        }
    }
}

