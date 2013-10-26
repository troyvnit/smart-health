using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.DataInterfaces;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;

namespace SmartHealth.Box.Domain.IRepository
{
    using SmartHealth.Box.Domain.Dtos;
    using SmartHealth.Box.Domain.Models;

    public interface IProductRepository : IRepository<Product>
    {
    }
}
