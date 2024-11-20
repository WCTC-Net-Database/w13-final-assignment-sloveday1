using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Equipments;


namespace ConsoleRpgEntities.Services
{
    public class InventoryService
    {
        private readonly IOutputService _outputService;

        public InventoryService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void ListItems(List<Item> Items)
        {
            _outputService.WriteLine("Inventory:");
            foreach (var item in Items)
            {
                _outputService.WriteLine($"{item.Name}");
            }
        }
    }
}
