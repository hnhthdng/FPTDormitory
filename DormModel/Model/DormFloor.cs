namespace DormModel.Model
{
    public class DormFloor
    {
        public int DormId { get; set; }
        public int FloorId { get; set; }

        public virtual Dorm Dorm { get; set; }
        public virtual Floor Floor { get; set; }
    }
}