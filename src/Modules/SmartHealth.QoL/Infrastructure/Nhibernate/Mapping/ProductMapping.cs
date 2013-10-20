using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using SmartHealth.Box.Domain.Models;

namespace SmartHealth.Box.Infrastructure.Nhibernate.Mapping
{
    using SmartHealth.Box.Domain.Models;

    public class ProductMapping : IAutoMappingOverride<Product>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Product> mapping)
        {
        }
    }
}
