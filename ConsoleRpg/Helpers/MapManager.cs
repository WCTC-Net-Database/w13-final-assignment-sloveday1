using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System.Text;

namespace ConsoleRpg.Helpers;

public class MapManager
{
    private const int RoomNameLength = 5;
    private const int GridRows = 5;
    private const int GridCols = 5;
    private readonly string[,] _mapGrid;
    private readonly GameContext _context;
    private readonly OutputManager _outputManager;
    private Room _currentRoom;

    public MapManager(GameContext context, OutputManager outputManager)
    {
        _context = context;
        _outputManager = outputManager;
        _currentRoom = null;
        _mapGrid = new string[GridRows, GridCols];
    }

    public void LoadInitialRoom(int roomId)
    {
        // Fetch the initial room based on its ID
        _currentRoom = _context.Set<Room>().Include(r => r.North)
                                             .Include(r => r.South)
                                             .Include(r => r.East)
                                             .Include(r => r.West)
                                             .FirstOrDefault(r => r.Id == roomId);
    }

    public void DisplayMap()
    {
        var mapBuilder = new StringBuilder();
        mapBuilder.AppendLine("Map:");

        // Clear map grid
        for (var i = 0; i < GridRows; i++)
        {
            for (var j = 0; j < GridCols; j++)
            {
                _mapGrid[i, j] = "       ";
            }
        }

        // Place the current room and its neighbors
        if (_currentRoom != null)
        {
            var startRow = GridRows / 2;
            var startCol = GridCols / 2;
            PlaceRoom(_currentRoom, startRow, startCol);
        }

        // Build the map grid row by row
        for (var i = 0; i < GridRows; i++)
        {
            for (var j = 0; j < GridCols; j++)
            {
                if (_mapGrid[i, j].Contains("[") && _mapGrid[i, j].Contains("]"))
                {
                    var roomName = Markup.Escape(_mapGrid[i, j]);
                    if (_mapGrid[i, j] == $"[{_currentRoom.Name.Substring(0, RoomNameLength)}]")
                    {
                        mapBuilder.Append($"[yellow]{roomName,-7}[/]");
                    }
                    else
                    {
                        mapBuilder.Append($"[white]{roomName,-7}[/]");
                    }
                }
                else
                {
                    mapBuilder.Append($"[dim]{Markup.Escape(_mapGrid[i, j]),-7}[/]");
                }
            }

            mapBuilder.AppendLine();
        }

        _outputManager.UpdateMapContent(mapBuilder.ToString());
    }



    public void UpdateCurrentRoom(int roomId)
    {
        // Update current room based on new ID and refresh relationships
        _currentRoom = _context.Set<Room>().Include(r => r.North)
                                             .Include(r => r.South)
                                             .Include(r => r.East)
                                             .Include(r => r.West)
                                             .FirstOrDefault(r => r.Id == roomId);
    }

    private void PlaceRoom(Room room, int row, int col)
    {
        if (_mapGrid[row, col] != "       ")
        {
            return;
        }

        var roomName = room.Name.Length > RoomNameLength
            ? room.Name.Substring(0, RoomNameLength)
            : room.Name.PadRight(RoomNameLength);

        _mapGrid[row, col] = $"[{roomName}]";

        if (room.North != null && row > 1)
        {
            _mapGrid[row - 1, col] = "   |   ";
            PlaceRoom(room.North, row - 2, col);
        }

        if (room.South != null && row < GridRows - 2)
        {
            _mapGrid[row + 1, col] = "   |   ";
            PlaceRoom(room.South, row + 2, col);
        }

        if (room.East != null && col < GridCols - 2)
        {
            _mapGrid[row, col + 1] = "  ---  ";
            PlaceRoom(room.East, row, col + 2);
        }

        if (room.West != null && col > 1)
        {
            _mapGrid[row, col - 1] = "  ---  ";
            PlaceRoom(room.West, row, col - 2);
        }
    }

    internal void ViewRoom()
    {
        if (_currentRoom != null)
        {
            _outputManager.AddLogEntry("Room:");
            _outputManager.AddLogEntry($"Name: {_currentRoom.Name}");
            _outputManager.AddLogEntry($"Description: {_currentRoom.Description}");
            if (_currentRoom.Players.Count > 0)
            {
                foreach (var player in _currentRoom.Players)
            {
                _outputManager.AddLogEntry($"Player: {player.Name}");
            }
            } 
            else
            {
                _outputManager.AddLogEntry("No players in the room.");
            }
        }
    }

    internal void MoveToNextRoom()
    {
        if (_currentRoom != null)
        {
            var direction = _outputManager.GetUserInput("Enter the direction (n, s, e, w):");

            switch (direction.ToLower())
            {
                case "n":
                    if (_currentRoom.North != null)
                    {
                        UpdateCurrentRoom(_currentRoom.North.Id);
                        _outputManager.AddLogEntry("Moved to the North.");
                        DisplayMap();
                    }
                    else
                    {
                        _outputManager.AddLogEntry("No room to the North.");
                    }
                    break;
                case "s":
                    if (_currentRoom.South != null)
                    {
                        UpdateCurrentRoom(_currentRoom.South.Id);
                        _outputManager.AddLogEntry("Moved to the South.");
                        DisplayMap();
                    }
                    else
                    {
                        _outputManager.AddLogEntry("No room to the South.");
                    }
                    break;
                case "e":
                    if (_currentRoom.East != null)
                    {
                        UpdateCurrentRoom(_currentRoom.East.Id);
                        _outputManager.AddLogEntry("Moved to the East.");
                        DisplayMap();
                    }
                    else
                    {
                        _outputManager.AddLogEntry("No room to the East.");
                    }
                    break;
                case "w":
                    if (_currentRoom.West != null)
                    {
                        UpdateCurrentRoom(_currentRoom.West.Id);
                        _outputManager.AddLogEntry("Moved to the West.");
                        DisplayMap();
                    }
                    else
                    {
                        _outputManager.AddLogEntry("No room to the West.");
                    }
                    break;
                default:
                    _outputManager.AddLogEntry("Invalid direction.");
                    break;
            }
        }
    }

    internal void AddRoom(string roomName, string roomDescription)
    {
        var newRoom = new Room
        {
            Name = roomName,
            Description = roomDescription
        };

        _context.Rooms.Add(newRoom);
        _context.SaveChanges();
        _outputManager.AddLogEntry($"Added room: {newRoom.Name}");
    }
}
