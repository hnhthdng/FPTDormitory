namespace DormModel.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaximumNumberOfPeople { get; set; }
        public int CurrentNumberOfPeople { get; set; }
        public bool IsMaximum { get; set; }

        public virtual ICollection<FloorRoom> FloorRooms { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}