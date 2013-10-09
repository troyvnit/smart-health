using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinaSale.Core.Domain.IRepository;
using VinaSale.Core.Domain.Models;
using VinaSale.Infrastructure.Nhibernate;

namespace VinaSale.Core.Infrastructure.Nhibernate
{
    public class UserRepository : Repository<User>, IUserRepository
    {
    }
}
