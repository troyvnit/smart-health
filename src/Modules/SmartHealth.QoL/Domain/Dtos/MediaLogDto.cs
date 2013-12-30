using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class MediaLogDto
    {
        public MediaLogDto()
        {
            Media = new MediaDto();
        }
        public virtual int Id { get; set; }

        public virtual string Email { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual MediaDto Media { get; set; }
    }
}
