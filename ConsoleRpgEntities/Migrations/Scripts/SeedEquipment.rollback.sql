-- Begin Transaction (optional but recommended)
BEGIN TRANSACTION;

-- 1. Remove the equipment from the player(s) who have the specific equipment
-- We'll find players who have an EquipmentId that references an Equipment with the Sword we inserted.

-- First, find the Id of the Sword item
DECLARE @SwordId INT;
SELECT @SwordId = Id FROM Items
WHERE Name = 'Sword' AND Attack = 5 AND Type = 'Weapon';

-- If the Sword exists, proceed
IF @SwordId IS NOT NULL
BEGIN
    -- Find the EquipmentId(s) where the WeaponId is @SwordId
    DECLARE @EquipmentId INT;
    SELECT @EquipmentId = Id FROM Equipments
    WHERE WeaponId = @SwordId;

    -- Remove the equipment from any players who have this EquipmentId
    IF @EquipmentId IS NOT NULL
    BEGIN
        UPDATE Players
        SET EquipmentId = NULL
        WHERE EquipmentId = @EquipmentId;

        -- Delete the equipment record
        DELETE FROM Equipments
        WHERE Id = @EquipmentId;
    END

    -- Delete the item record
    DELETE FROM Items
    WHERE Id = @SwordId;
END

-- Commit Transaction
COMMIT TRANSACTION;
