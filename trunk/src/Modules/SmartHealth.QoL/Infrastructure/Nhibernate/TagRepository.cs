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

    public class TagRepository : Repository<Tag>, ITagRepository
    {
    }
}
