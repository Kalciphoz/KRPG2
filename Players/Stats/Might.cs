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
    public class Might : AlignmentStat
    {
        public override void Update(Player player, K2Player k2Player, RPGCharacter character)
        {
            player.statManaMax2 += 19 + Amount * 3 - player.statManaMax;
            character.minorStats[typeof(Damage)].BonusAmount += 0.08f * Amount;
        }
    }
}
