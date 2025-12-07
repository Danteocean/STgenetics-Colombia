using Domain.Entities;
using infrastructure.Setting;

namespace infrastructure.Repositories.RepositoryAsync;

public class OrderRepositoryAsync : GenericRepository<Order>
{
    public OrderRepositoryAsync(ServiceContext ServiceContext) : base(ServiceContext) { }
}
