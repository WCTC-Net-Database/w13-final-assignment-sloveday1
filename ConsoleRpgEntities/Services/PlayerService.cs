using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Services;

public class PlayerService 
{
    private readonly IOutputService _outputService;
    private readonly AbilityService _abilityService;

     private readonly GameContext _context;

    public PlayerService(IOutputService outputService, AbilityService abilityService, GameContext context)
    {
        _outputService = outputService;
        _abilityService = abilityService;
        _context = context;
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

    public void ViewCharacters()
    {
        var players = _context.Players.ToList();
        if (players.Any())
        {
            _outputService.WriteLine("\nPlayers:");
            foreach (var player in players)
            {
                _outputService.WriteLine($"Character ID: {player.Id}, Name: {player.Name}, Health: {player.Health}, Experience: {player.Experience}");
            }
        }
        else
        {
            _outputService.WriteLine("No characters available.");
        }
    }

    public void AddCharacter(string name, int health, int experience)
    {
        var player = new Player
        {
            Name = name,
            Health = health,
            Experience = experience
        };

        _context.Players.Add(player);
        _context.SaveChanges();
        _outputService.WriteLine($"Added new player: {player.Name}");
    }

    public void UpdateCharacter(int playerId, string name, int health, int experience)
    {
        var playerToUpdate = _context.Players.Find(playerId);

        if (playerToUpdate != null)
        {
            playerToUpdate.Name = name;
            playerToUpdate.Health = health;
            playerToUpdate.Experience = experience;

            _context.SaveChanges();
            _outputService.WriteLine($"Updated player: {playerToUpdate.Name}");
        }
        else
        {
            _outputService.WriteLine("Character not found.");
        }
    }

    public void SearchCharacters(string search)
    {
        var players = _context.Players.Where(p => p.Name.Contains(search)).ToList();

        if (players.Any())
        {
            _outputService.WriteLine("\nPlayers:");
            foreach (var player in players)
            {
                _outputService.WriteLine($"Character ID: {player.Id}, Name: {player.Name}, Health: {player.Health}, Experience: {player.Experience}");
            }
        }
        else
        {
            _outputService.WriteLine("No characters found.");
        }
    }

    public void DeleteCharacter(int playerId)
    {
        var playerToDelete = _context.Players.Find(playerId);

        if (playerToDelete != null)
        {
            _context.Players.Remove(playerToDelete);
            _context.SaveChanges();
            _outputService.WriteLine($"Deleted player: {playerToDelete.Name}");
        }
        else
        {
            _outputService.WriteLine("Character not found.");
        }
    }

    public void AddAbilityToCharacter(int characterId, string abilityName, string abilityDescription, string abilityType)
    {
        var player = _context.Players.Find(characterId);

        if (player != null)
        {
            var ability = new Ability
            {
                Name = abilityName,
                Description = abilityDescription,
                AbilityType = abilityType
            };

            player.Abilities.Add(ability);
            _context.SaveChanges();
            _outputService.WriteLine($"Added ability {ability.Name} to {player.Name}");
        }
        else
        {
            _outputService.WriteLine("Character not found.");
        }
    }

    public void ViewCharacterAbilities(int id)
    {
        var player = _context.Players.Find(id);

        if (player != null)
        {
            if (player.Abilities.Any())
            {
                _outputService.WriteLine($"\nAbilities for {player.Name}:");
                foreach (var ability in player.Abilities)
                {
                    _outputService.WriteLine($"Ability ID: {ability.Id}, Name: {ability.Name}, Description: {ability.Description}, Type: {ability.AbilityType}");
                }
            }
            else
            {
                _outputService.WriteLine("No abilities available.");
            }
        }
        else
        {
            _outputService.WriteLine("Character not found.");
        }
    }
}
