using System;
using KRPG2.Players;
using Microsoft.Xna.Framework;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Acuity : AlignmentStat
    {
        public Acuity(RPGCharacter character) : base(character, "Acuity") { }

        public override string DisplayName => "Acuity";
        public override Color StatColor => new Color(0, 191, 31);

        public override void Update()
        {
            Player.moveSpeed *= 1f + Math.Min(1.2f, Amount * 0.03f);
        }
    }
}
