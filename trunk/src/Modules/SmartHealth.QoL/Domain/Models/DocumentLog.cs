using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHealth.Infrastructure.Domain.Models;

namespace SmartHealth.Box.Domain.Models
{
    [Serializable]
    public class DocumentLog : Entity
    {
        public DocumentLog()
        {
            Document = new Document();
            CreatedDate = DateTime.Now;
        }

        public virtual string Email { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual Document Document { get; set; }
    }
}
