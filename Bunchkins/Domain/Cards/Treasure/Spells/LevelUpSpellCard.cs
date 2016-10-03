using Bunchkins.Domain.Players;

namespace Bunchkins.Domain.Cards.Treasure.Spells
{
    class LevelUpSpellCard : TreasureSpellCard, IAnytimeSpell
    {
        public int Level { get; set; }
        public void Cast(Player player)
        {
            player.IncreaseLevel(Level);
        }
    }
}
