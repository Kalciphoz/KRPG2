using KRPG2.Players;
using Microsoft.Xna.Framework;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Stoicism : AlignmentStat
    {
        public Stoicism() : base("Stoicism") { }

        protected override string StatPageLine => "Stoicism:";
        protected override Color StatPageLineColor => new Color(223, 0, 0);

        protected override void Update(Player player, K2Player k2player, RPGCharacter character)
        {
            player.statLifeMax2 += 115 + Amount * 10 - player.statLifeMax;
            player.statDefense += Amount;
            character.MinorStats[typeof(LifeRegen)].BonusAmount += 0.3f * Amount;
        }
    }
}
