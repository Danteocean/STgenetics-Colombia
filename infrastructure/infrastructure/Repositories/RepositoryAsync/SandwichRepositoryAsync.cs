using Domain.Entities;
using infrastructure.Setting;

namespace infrastructure.Repositories.RepositoryAsync;

public class SandwichRepositoryAsync : GenericRepository<Sandwich>
{
    public SandwichRepositoryAsync(ServiceContext microServiceContext) : base(microServiceContext) { }
}