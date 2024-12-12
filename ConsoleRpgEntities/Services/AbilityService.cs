using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;


namespace ConsoleRpgEntities.Services
{
    public class AbilityService
    {
        private readonly IOutputService _outputService;

        public AbilityService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void Activate(IAbility ability, IPlayer user, ITargetable target)
        {
            if (ability is ShoveAbility shoveAbility)
            {
                // Shove ability logic
                _outputService.WriteLine($"{user.Name} shoves {target.Name} back {shoveAbility.Distance} feet, dealing {shoveAbility.Damage} damage!");
            }else{
                _outputService.WriteLine($"{user.Name} uses {ability.Name} on {target.Name}!");
            }
        }
    }
}
