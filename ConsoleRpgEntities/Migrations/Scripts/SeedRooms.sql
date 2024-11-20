SET IDENTITY_INSERT Rooms ON;

-- Insert independent rooms first (no foreign key dependencies)
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (0, 'Sunlit Clearing', 'Golden rays filter through the canopy above.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (1, 'Shadowy Alcove', 'The smell of damp moss fills the air.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (2, 'Forgotten Sanctuary', 'Ancient runes glow faintly on the walls.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (3, 'Echoing Hallway', 'Footsteps reverberate in the cavernous space.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (4, 'Shimmering Grotto', 'A pool of water reflects shimmering light patterns.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (5, 'Crystal Archway', 'Glittering crystals form a natural arch.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (6, 'Library of Whispers', 'Dusty tomes are scattered across the floor.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (7, 'Abandoned Shrine', 'Broken idols are strewn amidst the rubble.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (8, 'Luminous Cavern', 'The walls sparkle with embedded gemstones.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (9, 'Quiet Corridor', 'The silence is almost deafening here.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (10, 'Spiral Chamber', 'A spiral staircase winds down into darkness.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (11, 'Flickering Vault', 'Torchlight dances across the stone walls.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (12, 'Enchanted Meadow', 'Flowers emit a soft bioluminescent glow.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (13, 'Crumbling Tower', 'The tower leans precariously to one side.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (14, 'Sapphire Cavern', 'Deep blue stones line the cavern walls.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (15, 'Golden Chamber', 'A faint golden glow illuminates the room.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (16, 'Ancient Mine', 'Abandoned tools are scattered throughout.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (17, 'Amber Hall', 'The air is thick with the scent of pine resin.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (18, 'Twilight Cavern', 'Soft purple hues bathe the walls.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (19, 'Echoing Vault', 'A single drip echoes in the vast space.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (20, 'Ivory Hallway', 'Carvings of mythical creatures line the walls.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (21, 'Emerald Clearing', 'Vivid green light filters through leaves.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (22, 'Obsidian Chasm', 'The walls gleam like polished glass.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (23, 'Scarlet Alcove', 'A crimson hue saturates the room.', NULL, NULL, NULL, NULL);
INSERT INTO Rooms (Id, Name, Description, NorthId, SouthId, EastId, WestId) VALUES (24, 'Silver Vestibule', 'Soft moonlight reflects off metallic walls.', NULL, NULL, NULL, NULL);

-- Update relationships after rooms exist
UPDATE Rooms SET SouthId = 5, EastId = 1 WHERE Id = 0;
UPDATE Rooms SET SouthId = 6, EastId = 2, WestId = 0 WHERE Id = 1;
UPDATE Rooms SET SouthId = 7, EastId = 3, WestId = 1 WHERE Id = 2;
UPDATE Rooms SET SouthId = 8, EastId = 4, WestId = 2 WHERE Id = 3;
UPDATE Rooms SET WestId = 3 WHERE Id = 4;
UPDATE Rooms SET NorthId = 0, EastId = 6 WHERE Id = 5;
UPDATE Rooms SET NorthId = 1, SouthId = 11, EastId = 7, WestId = 5 WHERE Id = 6;
UPDATE Rooms SET NorthId = 2, SouthId = 12, EastId = 8, WestId = 6 WHERE Id = 7;
UPDATE Rooms SET NorthId = 3, SouthId = 13, EastId = 9, WestId = 7 WHERE Id = 8;
UPDATE Rooms SET NorthId = 4, SouthId = 14, WestId = 8 WHERE Id = 9;
UPDATE Rooms SET SouthId = 15, EastId = 11 WHERE Id = 10;
UPDATE Rooms SET NorthId = 6, SouthId = 16, EastId = 12, WestId = 10 WHERE Id = 11;
UPDATE Rooms SET NorthId = 7, EastId = 13, WestId = 11 WHERE Id = 12;
UPDATE Rooms SET NorthId = 8, EastId = 14, WestId = 12 WHERE Id = 13;
UPDATE Rooms SET NorthId = 9, WestId = 13 WHERE Id = 14;
UPDATE Rooms SET NorthId = 10, SouthId = 20 WHERE Id = 15;
UPDATE Rooms SET NorthId = 11, SouthId = 21, EastId = 17 WHERE Id = 16;
UPDATE Rooms SET SouthId = 22, EastId = 18, WestId = 16 WHERE Id = 17;
UPDATE Rooms SET SouthId = 23, EastId = 19, WestId = 17 WHERE Id = 18;
UPDATE Rooms SET WestId = 18 WHERE Id = 19;
UPDATE Rooms SET NorthId = 15, EastId = 21 WHERE Id = 20;
UPDATE Rooms SET NorthId = 16, EastId = 22, WestId = 20 WHERE Id = 21;
UPDATE Rooms SET NorthId = 17, EastId = 23, WestId = 21 WHERE Id = 22;
UPDATE Rooms SET NorthId = 18, EastId = 24, WestId = 22 WHERE Id = 23;
UPDATE Rooms SET WestId = 23 WHERE Id = 24;

SET IDENTITY_INSERT Rooms OFF;


-- Add player to room