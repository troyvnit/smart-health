using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHealth.Box.Domain.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
        }
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public Double PCS { get; set; }
        public Double MCS { get; set; }
        public Double NumberOfPatients { get; set; }
        public int TotalRows { get; set; }
    }
}
