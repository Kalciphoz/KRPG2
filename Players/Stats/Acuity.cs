using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace KRPG2.Players.Stats
{
    public class Acuity : AlignmentStat
    {
        public override void Update(Player player, K2Player k2Player, RPGCharacter character)
        {
            player.moveSpeed *= 1f + Math.Min(1.2f, Amount * 0.03f);
        }
    }
}
