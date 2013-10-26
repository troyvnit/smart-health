using System;

namespace SmartHealth.Box.Domain.Dtos
{
    public class FolderDto
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int ParentId { get; set; }

        public virtual bool HasChildren { get; set; }
    }
}
