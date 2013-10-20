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
        }
    }
}
