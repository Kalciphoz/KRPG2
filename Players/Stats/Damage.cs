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
    public class Damage : MinorStat
    {
        protected override float Default => 1f;

        public override void Update(Player player, K2Player k2player, RPGCharacter character)
        {
            player.allDamageMult *= Amount;
        }
    }
}
