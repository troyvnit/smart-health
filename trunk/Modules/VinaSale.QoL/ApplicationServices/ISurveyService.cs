using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinaSale.Box.Domain.Dtos;
using VinaSale.Box.Domain.Models;

namespace VinaSale.Box.ApplicationServices
{
    public interface ISurveyService 
    {
        IList<QOLDto> GetQolByDate(DateTime startDate, DateTime endDate, int userId);
        IList<QOLDto> GetQolListByUser(int userId, int currentPage, int pageSize, string orderBy, string orderDirection, out int totalRecords);
        IList<QOLDto> GetQolListByUserId(int userId, int currentPage, int pageSize, string orderBy, string direction, ref int totalRecords, bool isShowOlder);
    }
}
