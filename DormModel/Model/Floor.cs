using DormModel.Model;
using Microsoft.EntityFrameworkCore;

namespace DormModel.Model
{
    public class Floor
    {
            public int Id { get; set; }
            public string Name { get; set; }
        public virtual ICollection<DormFloor> DormFloors { get; set; }
        public virtual ICollection<FloorRoom> FloorRooms { get; set; }
        public virtual ICollection<Dorm> Dorms { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
