using KRPG2.Players;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Damage : MinorStat
    {
        protected override float Default => 1f;

        public override int StatPageColumn => STATPAGE_COLUMN_OFFENSIVE;

        public override string DisplayName => "Damage";

        public Damage(RPGCharacter character) : base(character, "Damage") { }

        public override void Update()
        {
            Player.allDamageMult *= Amount;
        }
    }
}
