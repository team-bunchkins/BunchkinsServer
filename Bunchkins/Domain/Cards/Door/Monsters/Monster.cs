using System.ComponentModel;
using Bunchkins.Domain.Players;

namespace Bunchkins.Domain.Cards.Door.Monsters
{
    public abstract class Monster : DoorCard
    {
        public int Level { get; set; }

        [DefaultValue(1)]
        public int LevelGain { get; set; }

        public int TreasureGain { get; set; }

        public int MinPursuitLevel { get; set; }

        public abstract void BadStuff(Player player);
    }
}
