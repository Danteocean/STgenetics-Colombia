using Domain.Entities;
using infrastructure.Setting;

namespace infrastructure.Repositories.RepositoryAsync;

public class OrderItemRepositoryAsync : GenericRepository<OrderItem>
{
    public OrderItemRepositoryAsync(ServiceContext ServiceContext) : base(ServiceContext) { }
}
