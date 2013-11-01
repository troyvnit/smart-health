using SmartHealth.Infrastructure.Nhibernate;

namespace SmartHealth.Box.Infrastructure.Nhibernate
{
    using SmartHealth.Box.Domain.IRepository;
    using SmartHealth.Box.Domain.Models;

    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
    }
}
