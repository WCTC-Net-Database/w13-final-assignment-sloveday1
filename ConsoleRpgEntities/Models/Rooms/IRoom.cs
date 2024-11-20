namespace ConsoleRpgEntities.Models.Rooms
{
    public interface IRoom
    {
        string Name { get; }
        IRoom? East { get; set; }
        IRoom? North { get; set; }
        IRoom? South { get; set; }
        IRoom? West { get; set; }
    }
}
