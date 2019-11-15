using System;
using KRPG2.Players;
using Microsoft.Xna.Framework;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Acuity : AlignmentStat
    {
        public Acuity() : base("Acuity") { }

        protected override string StatPageLine => "Acuity:";
        protected override Color StatPageLineColor => new Color(0, 191, 31);

        protected override void Update(Player player, K2Player k2player, RPGCharacter character)
        {
            player.moveSpeed *= 1f + Math.Min(1.2f, Amount * 0.03f);
        }
    }
}
