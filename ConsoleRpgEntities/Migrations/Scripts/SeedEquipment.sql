-- 1. Insert a new sword into the Items table
INSERT INTO Items (Name, Type, Attack, Defense)
VALUES ('Sword', 'Weapon', 5, 0);

-- Get the Id of the newly inserted Item
DECLARE @SwordId INT = SCOPE_IDENTITY();

-- 2. Insert a new Equipment record with the WeaponId set to the new sword
INSERT INTO Equipments (WeaponId, ArmorId)
VALUES (@SwordId, NULL);

-- Get the Id of the newly inserted Equipment
DECLARE @EquipmentId INT = SCOPE_IDENTITY();

-- 3. Update the Player's EquipmentId to the new Equipment
-- Replace '1' with the actual Id of the player you want to update
DECLARE @PlayerId INT = 1; -- Replace with actual Player Id

UPDATE Players
SET EquipmentId = @EquipmentId
WHERE Id = @PlayerId;
