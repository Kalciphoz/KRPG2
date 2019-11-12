using KRPG2.Players;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Might : AlignmentStat
    {
        public Might() : base("Might")
        {
        }

        protected override void Update(Player player, K2Player k2player, RPGCharacter character)
        {
            player.statManaMax2 += 19 + Amount * 3 - player.statManaMax;
            character.MinorStats[typeof(Damage)].BonusAmount += 0.08f * Amount;
        }
    }
}
