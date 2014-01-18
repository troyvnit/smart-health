using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class DocumentLogDto
    {
        public DocumentLogDto()
        {
            Document = new DocumentDto();
        }
        public virtual int Id { get; set; }

        public virtual string Email { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DocumentDto Document { get; set; }
    }
}
