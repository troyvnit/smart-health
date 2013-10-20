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
        IList<ProductDto> GetProductByDate(DateTime startDate, DateTime endDate, int userId);
        IList<ProductDto> GetProductListByUser(int userId, int currentPage, int pageSize, string orderBy, string orderDirection, out int totalRecords);
        IList<ProductDto> GetProductListByUserId(int userId, int currentPage, int pageSize, string orderBy, string direction, ref int totalRecords, bool isShowOlder);
    }
}
