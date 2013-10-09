using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinaSale.Core.Domain.IRepository;
using VinaSale.Core.Domain.Models;
using VinaSale.Infrastructure.Nhibernate;
using VinaSale.Box.Domain.Dtos;
using VinaSale.Box.Domain.IRepository;
using VinaSale.Box.Domain.Models;
using NHibernate;
using NHibernate.Transform;

namespace VinaSale.Box.Infrastructure.Nhibernate
{
    using VinaSale.Box.Domain.Dtos;
    using VinaSale.Box.Domain.IRepository;
    using VinaSale.Box.Domain.Models;

    public class HealthSurveyRepository : Repository<SaleInfo>, IHealthSurveyRepository
    {
        public IList<SaleInfoDto> GetQolByDate(DateTime startDate, DateTime endDate, int userId)
        {
            IQuery query = Session.GetNamedQuery("GetQolByDate")
            .SetDateTime("StartDate", startDate)
            .SetDateTime("EndDate", endDate)
            .SetInt32("UserId", userId);
            query.SetResultTransformer(Transformers.AliasToBean<SaleInfoDto>());
            IList<SaleInfoDto> listQOLDto = query.List<SaleInfoDto>();
            return listQOLDto;

        }

        public IList<SaleInfoDto> GetQolListByUser(int userId, int currentPage, int pageSize, string orderBy, string orderDirection, out int totalRecords)
        {
            totalRecords = 0;
            IQuery query = Session.GetNamedQuery("GetQolListByUser")
            .SetInt32("UserId", userId)
            .SetString("OrderBy", orderBy)
            .SetString("OrderDirection", orderDirection)
            .SetInt32("Page", currentPage)
            .SetInt32("PageSize", pageSize);
            query.SetResultTransformer(Transformers.AliasToBean<SaleInfoDto>());
            IList<SaleInfoDto> qolListDtos = query.List<SaleInfoDto>();
            if (qolListDtos.Any())
                totalRecords = qolListDtos[0].TotalRows;
            return qolListDtos;
        }

        public IList<SaleInfoDto> GetQolListByUserId(int userId, int currentPage, int pageSize, string orderBy, string direction, ref int totalRecords, bool isShowOlder)
        {
            totalRecords = 0;
            IQuery query = Session.GetNamedQuery("GetQolListByUserId")
            .SetInt32("PatientId", userId)
            .SetString("OrderBy", orderBy)
            .SetString("OrderDirection", direction)
            .SetInt32("Page", currentPage)
            .SetInt32("PageSize", pageSize)
            .SetBoolean("IsShowOlder", isShowOlder);
            query.SetResultTransformer(Transformers.AliasToBean<SaleInfoDto>());
            IList<SaleInfoDto> qolListDtos = query.List<SaleInfoDto>();
            if (qolListDtos.Any())
                totalRecords = qolListDtos[0].TotalRows;
            return qolListDtos;
        }
  
    }
}
