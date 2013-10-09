using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using VinaSale.Box.Domain.Models;

namespace VinaSale.Box.Infrastructure.Nhibernate.Mapping
{
    using VinaSale.Box.Domain.Models;

    public class SaleInfoMapping : IAutoMappingOverride<SaleInfo>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<SaleInfo> mapping)
        {
        }
    }
}
