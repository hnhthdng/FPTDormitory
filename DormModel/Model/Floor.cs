namespace DormModel.Model
{
    public class Floor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DormFloor> DormFloors { get; set; }
        public virtual ICollection<FloorRoom> FloorRooms { get; set; }
    }
}