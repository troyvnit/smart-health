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

    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public IList<Article> GetList(string filter, int currentPage, int pageSize, string orderBy, string orderDirection, out int totalRecords)
        {
            currentPage = currentPage == 0 ? 1 : currentPage;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var query = this.GetQuery().Where(x => x.Title.Contains(filter)).Where(x => x.IsDeleted == false);
            switch (orderDirection)
            {
                case "ASC":
                    {
                        switch (orderBy)
                        {
                            case "Title":
                                query = query.OrderBy(x => x.Title);
                                break;
                            case "CreatedDate":
                                query = query.OrderBy(x => x.CreatedDate);
                                break;
                            default:
                                query = query.OrderBy(x => x.Title);
                                break;
                        }
                        break;
                    }
                case "DESC":
                    {
                        switch (orderBy)
                        {
                            case "Title":
                                query = query.OrderByDescending(x => x.Title);
                                break;
                            case "CreatedDate":
                                query = query.OrderByDescending(x => x.CreatedDate);
                                break;
                            default:
                                query = query.OrderByDescending(x => x.Title);
                                break;
                        }
                        break;
                    }
                default:
                    break;

            }

            totalRecords = query.Count();
            return query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
