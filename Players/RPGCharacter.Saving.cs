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

namespace KRPG2.Players
{
    public sealed partial class RPGCharacter
    {
        public const string
            SAVE_KEY_LEVEL = "Level",
            SAVE_KEY_XP = "XP";

        internal TagCompound Save()
        {
            TagCompound tag = new TagCompound
            {
                {SAVE_KEY_LEVEL, Level},
                {SAVE_KEY_XP, XP }
            };

            return tag;
        }

        internal void Load(TagCompound tag)
        {
            Level = tag.GetInt(SAVE_KEY_LEVEL);
            XP = tag.GetLong(SAVE_KEY_XP);
        }
    }
}
