using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters.Monsters;

namespace ConsoleRpgEntities.Services
{
    public class MonsterService
    {
        private readonly IOutputService _outputService;

        public MonsterService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void Attack(IMonster monster, ITargetable target)
        {
            // Goblin-specific attack logic
            if (monster is Goblin goblin)
            {

                _outputService.WriteLine($"{monster.Name} sneaks up and attacks {target.Name}!");
            }
        }
    }
}
