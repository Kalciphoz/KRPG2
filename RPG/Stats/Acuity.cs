using System;
using KRPG2.Players;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Acuity : AlignmentStat
    {
        public Acuity() : base("Acuity")
        {
            
        }

        protected override void Update(Player player, K2Player k2player, RPGCharacter character)
        {
            player.moveSpeed *= 1f + Math.Min(1.2f, Amount * 0.03f);
        }
    }
}
