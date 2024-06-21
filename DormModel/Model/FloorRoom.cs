namespace DormModel.Model
{
    public class FloorRoom
    {
        public int FloorId { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Floor Floor { get; set; }
    }
}