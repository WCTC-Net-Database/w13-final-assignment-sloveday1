using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Services;

public class PlayerService 
{
    private readonly IOutputService _outputService;
    private readonly AbilityService _abilityService;

    public PlayerService(IOutputService outputService, AbilityService abilityService)
    {
        _outputService = outputService;
        _abilityService = abilityService;
    }

    public void Attack(IPlayer player, ITargetable target)
    {
        if (player.Equipment?.Weapon == null)
        {
            _outputService.WriteLine($"{player.Name} has no weapon equipped!");
            return;
        }

        _outputService.WriteLine($"{player.Name} attacks {target.Name} with a {player.Equipment.Weapon.Name} dealing {player.Equipment.Weapon.Attack} damage!");
        target.Health -= player.Equipment.Weapon.Attack;
        _outputService.WriteLine($"{target.Name} has {target.Health} health remaining.");
    }

    public void UseAbility(IPlayer player, IAbility ability, ITargetable target)
    {
        if (player.Abilities?.Contains(ability) == true)
        {
            _abilityService.Activate(ability, player, target);
        }
        else
        {
            _outputService.WriteLine($"{player.Name} does not have the ability {ability.Name}!");
        }
    }

    public void EquipItemFromInventory(IPlayer player, Item item)
    {
        if (player.Inventory?.Items.Contains(item) == true)
        {
            player.Equipment?.EquipItem(item);
        }
        else
        {
            _outputService.WriteLine($"{player.Name} does not have the item {item.Name} in their inventory!");
        }
    }
}
