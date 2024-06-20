namespace DormModel.Model
{
    public class DormFloor
    {
        public Guid DormId { get; set; }
        public Guid FloorId { get; set; }

        public virtual Dorm Dorm { get; set; }
        public virtual Floor Floor { get; set; }
    }
}