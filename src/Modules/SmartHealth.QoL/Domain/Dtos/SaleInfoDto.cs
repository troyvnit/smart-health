using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VinaSale.Box.Domain.Dtos
{
    public class SaleInfoDto
    {
        public SaleInfoDto()
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
