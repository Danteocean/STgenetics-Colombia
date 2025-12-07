using Dapper;
using Domain.Entities;
using Npgsql;

namespace Domain.Querys;

public class GetOrder
{
    private String _conexion = "";

    public const String query = @"SELECT o.order_id orderId, o.sandwich_id sandwichId, s.name , o.discount_rule_id, 
	o.subtotal, o.total_price totalPrice,
    o.created_date, o.updated_date, o.is_active, oi.order_item_id, e.extra_id extraId, e.name nameExt,
    e.price priceExt 
    FROM ""order"" o 
    LEFT JOIN sandwich s  ON s.sandwich_id = o.sandwich_id
    LEFT JOIN order_item oi ON oi.order_id = o.order_id
    LEFT JOIN extra e ON e.extra_id = oi.extra_id
    WHERE o.is_active = TRUE ORDER BY o.order_id ;";

    public const String queryById = @"SELECT order_id orderId, sandwich_id sandwichId, subtotal,total_price totalPrice,is_active isActive,
    discount_rule_id discountRuleId FROM ""order"" WHERE is_active = TRUE AND  order_id = @id ";


    public GetOrder(String conexion)
    {
        _conexion = conexion;
    }

    public async Task<List<OrderGetLarge>> Get()
    {
        try
        {
            using var dbConnection = new Npgsql.NpgsqlConnection(_conexion);
            var result = dbConnection.QueryAsync<OrderRaw>(query).Result;
            return MapToOrderLarge(result);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error GetOrder: " + ex.Message);
            return new List<OrderGetLarge>();
        }
    }

    private List<OrderGetLarge> MapToOrderLarge(IEnumerable<OrderRaw> result)
    {
        return result
            .GroupBy(r => new { r.orderId, r.sandwichId, r.name, r.subtotal, r.totalPrice })
            .Select(g => new OrderGetLarge
            {
                orderId = g.Key.orderId,
                sandwichId = g.Key.sandwichId,
                name = g.Key.name,
                subtotal = g.Key.subtotal,
                totalPrice = g.Key.totalPrice,

                Extras = g
                    .Where(x => x.extraId.HasValue)
                    .Select(x => new ExtrasDtoResponse
                    {
                        extraId = x.extraId.Value,
                        nameExt = x.nameExt ?? "",
                        priceExt = x.priceExt ?? 0
                    })
                    .ToList()
            })
            .ToList();
    }


    public async Task<Order> GetByIdAsync(int id)
    {
        try
        {
            using var dbConnection = new NpgsqlConnection(_conexion);
            var parameters = new { id };
            return await dbConnection.QueryFirstOrDefaultAsync<Order>(queryById, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error GetOrderById: " + ex.Message);
            return new Order();
        }
        
    }
}