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
    public abstract class AlignmentStat : Stat
    {
        public int baseAmount;
        public int bonusAmount = 0;
        public int Amount => baseAmount + bonusAmount;

        internal sealed override void ResetBonus()
        {
            bonusAmount = 0;
        }

        public sealed override TagCompound Save()
        {
            if (!DoSave) return new TagCompound();
            TagCompound tag = new TagCompound
            {
                {SAVE_KEY_AMOUNT, baseAmount}
            };

            return tag;
        }

        public sealed override void Load(TagCompound tag)
        {
            baseAmount = tag.GetInt(SAVE_KEY_AMOUNT);
        }
    }
}
