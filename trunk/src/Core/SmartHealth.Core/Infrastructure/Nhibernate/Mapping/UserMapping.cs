using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using VinaSale.Core.Domain.Models;

namespace VinaSale.Core.Infrastructure.Nhibernate.Mapping
{
    public class UserMapping : IAutoMappingOverride<User>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<User> mapping)
        {
        }
    }
}
