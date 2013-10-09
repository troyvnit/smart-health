using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinaSale.Infrastructure.Domain.DataInterfaces;
using VinaSale.Box.Domain.Dtos;
using VinaSale.Box.Domain.Models;

namespace VinaSale.Box.Domain.IRepository
{
    using VinaSale.Box.Domain.Dtos;
    using VinaSale.Box.Domain.Models;

    public interface IHealthSurveyRepository : IRepository<SaleInfo>
    {
        IList<SaleInfoDto> GetQolByDate(DateTime startDate, DateTime endDate, int userId);
        IList<SaleInfoDto> GetQolListByUser(int userId, int currentPage, int pageSize, string orderBy, string orderDirection, out int totalRecords);
        IList<SaleInfoDto> GetQolListByUserId(int userId, int currentPage, int pageSize, string orderBy, string direction, ref int totalRecords, bool isShowOlder);
    }
}
