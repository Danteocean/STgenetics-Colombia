using Domain.Entities;

namespace CoreLibrary.Interface.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Sandwich> SandwichAsync { get; }
    IGenericRepository<Order> OrderAsync { get; }

    IGenericRepository<OrderItem> OrderItemAsync { get; }

    Task BeginTransactionAsync();

    Task CommitnAsync();

    Task RollbackAsync();
}