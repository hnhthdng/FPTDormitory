namespace DormModel.Model
{
    public class Dorm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //  Navigation properties
        public virtual ICollection<DormFloor> DormFloors { get; set; }
    }
}