using KRPG2.Players;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Damage : MinorStat
    {
        protected override float Default => 1f;

        public override int DisplayColumn => DISPLAY_COLUMN_OFFENSIVE;

        protected override string StatPageLine => "Damage:";

        public Damage() : base("Damage") { }

        protected override void Update(Player player, K2Player k2player, RPGCharacter character)
        {
            player.allDamageMult *= Amount;
        }
    }
}
