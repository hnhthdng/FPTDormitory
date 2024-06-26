using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormModel.DTO.Room
{
    public class RoomRequestDTO
    {
        public string Name { get; set; }
        public int MaximumNumberOfPeople { get; set; }
        public int? CurrentNumberOfPeople { get; set; }
        public bool? IsMaximum { get; set; }
        public decimal Price { get; set; }
    }
}
