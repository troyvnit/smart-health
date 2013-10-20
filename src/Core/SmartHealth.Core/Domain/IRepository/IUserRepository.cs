using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.DataInterfaces;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Core.Domain.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
