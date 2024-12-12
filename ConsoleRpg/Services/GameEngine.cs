using System.ComponentModel.Design.Serialization;
using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using Spectre.Console;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private readonly MenuManager _menuManager;
    private readonly MapManager _mapManager;
    private readonly PlayerService _playerService;
    private readonly OutputManager _outputManager;
    private Table _logTable;
    private Panel _mapPanel;

    private Player _player;
    private IMonster _goblin;

    public GameEngine(GameContext context, MenuManager menuManager, MapManager mapManager, PlayerService playerService, OutputManager outputManager)
    {
        _menuManager = menuManager;
        _mapManager = mapManager;
        _playerService = playerService;
        _outputManager = outputManager;
        _context = context;
    }

    public void Run()
    {
        if (_menuManager.ShowMainMenu())
        {
            SetupGame();
        }
    }

    private void GameLoop()
    {
        while (true)
        {
            _outputManager.AddLogEntry("1. Attack");
            _outputManager.AddLogEntry("2. Characters");
            _outputManager.AddLogEntry("3. Character Abilities");
            _outputManager.AddLogEntry("4. Room");
            _outputManager.AddLogEntry("5. Quit");
            var input = _outputManager.GetUserInput("Choose an action:");


            switch (input)
            {
                case "1":
                    AttackCharacter();
                    break;
                case "2":
                    CharacterInputs();
                    break;
                case "3":
                    CharacterAbilities();
                    break;
                case "4":
                    RoomInputs();
                    break;
                case "5":
                    _outputManager.AddLogEntry("Exiting game...");
                    Environment.Exit(0);
                    break;
                default:
                    _outputManager.AddLogEntry("Invalid selection. Please choose 1.");
                    break;
            }
        }
    }

    private void AttackCharacter()
    {
        if (_goblin is ITargetable targetableGoblin)
        {
            _playerService.Attack(_player, targetableGoblin);
            _playerService.UseAbility(_player, _player.Abilities.First(), targetableGoblin);
        }
    }

    private void SetupGame()
    {
        _player = _context.Players.FirstOrDefault();
        _outputManager.AddLogEntry($"{_player.Name} has entered the game.");

        // Load monsters into random rooms 
        LoadMonsters();

        // Load map
        _mapManager.LoadInitialRoom(1);
        _mapManager.DisplayMap();

        // Pause before starting the game loop
        Thread.Sleep(500);
        GameLoop();
    }

    private void LoadMonsters()
    {
        _goblin = _context.Monsters.OfType<Goblin>().FirstOrDefault();
    }

    private void CharacterInputs()
    {
        _outputManager.AddLogEntry("1. View Characters");
        _outputManager.AddLogEntry("2. Add New Character");
        _outputManager.AddLogEntry("3. Update Existing Character");
        _outputManager.AddLogEntry("4. Search Characters");
        _outputManager.AddLogEntry("5. Delete Character");
        _outputManager.AddLogEntry("6. Back");
        var characterInput = _outputManager.GetUserInput("Choose an action:");

        switch (characterInput)
        {
            case "1":
                _playerService.ViewCharacters();
                break;
            case "2":
                var name = _outputManager.GetUserInput("Character Name:");
                var health = Int32.Parse(_outputManager.GetUserInput("Characters Health:"));
                var experience = Int32.Parse(_outputManager.GetUserInput("Characters Experience:"));

                _playerService.AddCharacter(name, health, experience);
                break;
            case "3":
                _playerService.ViewCharacters();

                var playerId = Int32.Parse(_outputManager.GetUserInput("Enter the ID of the character to update:"));
                var updatename = _outputManager.GetUserInput("Enter the new name:");
                var updatehealth = Int32.Parse(_outputManager.GetUserInput("Enter the new health:"));
                var updateexperience = Int32.Parse(_outputManager.GetUserInput("Enter the new experience:"));

                _playerService.UpdateCharacter(playerId, updatename, updatehealth, updateexperience);
                break;
            case "4":
                var search = _outputManager.GetUserInput("Enter a character name to search for:");
                _playerService.SearchCharacters(search);
                break;
            case "5":
                _playerService.ViewCharacters();
                var deleteId = Int32.Parse(_outputManager.GetUserInput("Enter the ID of the character to delete:"));
                _playerService.DeleteCharacter(deleteId);
                break;
            case "6":
                break;
            default:
                _outputManager.AddLogEntry("Invalid selection. Please choose 1.");
                break;
        }
    }

    private void CharacterAbilities()
    {
        _outputManager.AddLogEntry("1. Add Ability To Character");
        _outputManager.AddLogEntry("2. Display Character Abilities");
        _outputManager.AddLogEntry("3. Back");
        var abilityInput = _outputManager.GetUserInput("Choose an action:");

        switch(abilityInput){
            
            case "1":
                _playerService.ViewCharacters();
                var characterId = Int32.Parse(_outputManager.GetUserInput("Enter the ID of the character to add an ability to:"));
                var abilityName = _outputManager.GetUserInput("Enter the name of the ability:");
                var abilityDescription = _outputManager.GetUserInput("Enter the description of the ability:");
                var abilityType = _outputManager.GetUserInput("Enter the type of the ability:");

                _playerService.AddAbilityToCharacter(characterId, abilityName, abilityDescription, abilityType);
                break;
            case "2":
                _playerService.ViewCharacters();
                var Id = Int32.Parse(_outputManager.GetUserInput("Enter the ID of the character to view abilities:"));
                _playerService.ViewCharacterAbilities(Id);
                break;
            default:
                _outputManager.AddLogEntry("Invalid selection. Please choose 1.");
                break;
        }
    }

    private void RoomInputs()
    {
        _outputManager.AddLogEntry("1. View Room");
        _outputManager.AddLogEntry("2. Move to Next Room");
        _outputManager.AddLogEntry("3. Add a New Room");
        _outputManager.AddLogEntry("3. Back");
        var roomInput = _outputManager.GetUserInput("Choose an action:");

        switch (roomInput)
        {
            case "1":
                _mapManager.ViewRoom();
                break;
            case "2":
                _mapManager.MoveToNextRoom();
                break;
            case "3":
                var roomName = _outputManager.GetUserInput("Enter the name of the room:");
                var roomDescription = _outputManager.GetUserInput("Enter the description of the room:");
                _mapManager.AddRoom(roomName, roomDescription);
                break;
            case "4":
                break;
            default:
                _outputManager.AddLogEntry("Invalid selection. Please choose 1.");
                break;
        }
    }

}
