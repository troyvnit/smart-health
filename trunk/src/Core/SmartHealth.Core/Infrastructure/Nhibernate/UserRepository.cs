using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.IRepository;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Nhibernate;

namespace SmartHealth.Core.Infrastructure.Nhibernate
{
    public class UserRepository : Repository<User>, IUserRepository
    {
    }
}
