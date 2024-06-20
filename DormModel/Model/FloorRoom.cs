namespace DormModel.Model
{
    public class FloorRoom
    {
        public Guid FloorId { get; set; }
        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Floor Floor { get; set; }
    }
}