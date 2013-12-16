using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using SmartHealth.Core.Domain.Models;

namespace SmartHealth.Core.Infrastructure.Nhibernate.Mapping
{
    public class UserMapping : IAutoMappingOverride<User>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<User> mapping)
        {
            mapping.Map(a => a.CreatedTime).Nullable();
            mapping.Map(a => a.DOB).Nullable();
            mapping.Map(a => a.LastLoginTime).Nullable();
            mapping.Map(a => a.ModifiedTime).Nullable();
        }
    }
}
