using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormModel.Model
{
    public class RoomOrder
    {
        public int OrderId { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Order Order { get; set; }
    }
}
