using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Core.Domain.IRepository;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Nhibernate;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.IRepository;
using SmartHealth.Box.Domain.Models;
using NHibernate;
using NHibernate.Transform;

namespace SmartHealth.Box.Infrastructure.Nhibernate
{
    using SmartHealth.Box.Domain.Dtos;
    using SmartHealth.Box.Domain.IRepository;
    using SmartHealth.Box.Domain.Models;

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public IList<ProductDto> GetProductByDate(DateTime startDate, DateTime endDate, int userId)
        {
            IQuery query = Session.GetNamedQuery("GetProductByDate")
            .SetDateTime("StartDate", startDate)
            .SetDateTime("EndDate", endDate)
            .SetInt32("UserId", userId);
            query.SetResultTransformer(Transformers.AliasToBean<ProductDto>());
            IList<ProductDto> listQOLDto = query.List<ProductDto>();
            return listQOLDto;

        }

        public IList<ProductDto> GetProductListByUser(int userId, int currentPage, int pageSize, string orderBy, string orderDirection, out int totalRecords)
        {
            totalRecords = 0;
            IQuery query = Session.GetNamedQuery("GetProductListByUser")
            .SetInt32("UserId", userId)
            .SetString("OrderBy", orderBy)
            .SetString("OrderDirection", orderDirection)
            .SetInt32("Page", currentPage)
            .SetInt32("PageSize", pageSize);
            query.SetResultTransformer(Transformers.AliasToBean<ProductDto>());
            IList<ProductDto> qolListDtos = query.List<ProductDto>();
            if (qolListDtos.Any())
                totalRecords = qolListDtos[0].TotalRows;
            return qolListDtos;
        }

        public IList<ProductDto> GetProductListByUserId(int userId, int currentPage, int pageSize, string orderBy, string direction, ref int totalRecords, bool isShowOlder)
        {
            totalRecords = 0;
            IQuery query = Session.GetNamedQuery("GetProductListByUserId")
            .SetInt32("PatientId", userId)
            .SetString("OrderBy", orderBy)
            .SetString("OrderDirection", direction)
            .SetInt32("Page", currentPage)
            .SetInt32("PageSize", pageSize)
            .SetBoolean("IsShowOlder", isShowOlder);
            query.SetResultTransformer(Transformers.AliasToBean<ProductDto>());
            IList<ProductDto> qolListDtos = query.List<ProductDto>();
            if (qolListDtos.Any())
                totalRecords = qolListDtos[0].TotalRows;
            return qolListDtos;
        }
  
    }
}
