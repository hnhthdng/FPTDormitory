namespace DormModel.Model
{
    public class Dorm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //  Navigation properties
        public virtual ICollection<DormFloor> DormFloors { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
    }
}