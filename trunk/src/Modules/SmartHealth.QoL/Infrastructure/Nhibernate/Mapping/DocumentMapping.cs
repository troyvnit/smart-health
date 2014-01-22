using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using SmartHealth.Box.Domain.Models;

namespace SmartHealth.Box.Infrastructure.Nhibernate.Mapping
{
    using SmartHealth.Box.Domain.Models;

    public class DocumentMapping : IAutoMappingOverride<Document>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Document> mapping)
        {
            mapping.Map(a => a.Content).Length(4001);
        }
    }
}
