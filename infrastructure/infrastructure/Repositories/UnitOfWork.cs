using CoreLibrary.Interface.Repositories;
using Domain.Entities;
using infrastructure.Repositories.RepositoryAsync;
using infrastructure.Setting;
using Microsoft.EntityFrameworkCore.Storage;

namespace infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ServiceContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ServiceContext context) => _context = context;


    IGenericRepository<Sandwich> IUnitOfWork.SandwichAsync => new SandwichRepositoryAsync(_context);

    IGenericRepository<Order> IUnitOfWork.OrderAsync => new OrderRepositoryAsync(_context);

    IGenericRepository<OrderItem> IUnitOfWork.OrderItemAsync => new OrderItemRepositoryAsync(_context);


    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);
    }

    public async Task CommitnAsync()
    {
        try
        {
            await BeginTransactionAsync();
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            Dispose();
        }
    }

    private bool disposed = false;

    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task RollbackAsync()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        return Task.CompletedTask;
    }
}