using System;

namespace SmartHealth.Box.Domain.Dtos
{
    public class MenuDto
    {
        public virtual int? Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Url { get; set; }

        public virtual int Priority { get; set; }

        public virtual string Icon { get; set; }

        public virtual int ParentId { get; set; }
    }
}
