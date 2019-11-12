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
    public class Stoicism : AlignmentStat
    {
        public override void Update(Player player, K2Player k2Player, RPGCharacter character)
        {
            player.statLifeMax2 += 115 + Amount * 10 - player.statLifeMax;
            player.statDefense += Amount;
            character.minorStats[typeof(LifeRegen)].BonusAmount += 0.3f * Amount;
        }
    }
}
