using ConsoleRpgEntities.Models.Characters;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities
{
    public class Ability : IAbility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AbilityType { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
