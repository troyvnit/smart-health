using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinaSale.Infrastructure.Domain.DataInterfaces;
using VinaSale.Core.Domain.Models;

namespace VinaSale.Core.Domain.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
