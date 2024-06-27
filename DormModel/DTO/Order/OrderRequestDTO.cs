using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormModel.DTO.Order
{
    public class OrderRequestDTO
    {
        public string Id { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public string Description { get; set; }
        public float TotalPrice { get; set; }
    }
}
