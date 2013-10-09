using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VinaSale.Infrastructure.Domain.Models;

namespace VinaSale.Box.Domain.Models
{
    [Serializable]
    public sealed class SaleInfo : Entity
    {
        public SaleInfo()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUserId { get; set; }
    }
}
