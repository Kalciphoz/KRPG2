using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using KRPG2.Players.Stats;
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

            foreach (AlignmentStat stat in AlignmentStats.Values)
                if (stat.DoSave)
                    tag.Add(stat.UnlocalizedName, stat.Save());

            foreach (MinorStat stat in MinorStats.Values)
                if (stat.DoSave)
                    tag.Add(stat.UnlocalizedName, stat.Save());

            return tag;
        }

        internal void Load(TagCompound tag)
        {
            Level = tag.GetInt(SAVE_KEY_LEVEL);
            XP = tag.GetLong(SAVE_KEY_XP);

            foreach (AlignmentStat stat in AlignmentStats.Values)
                if (stat.DoSave)
                    stat.Load(tag.GetCompound(stat.UnlocalizedName));

            foreach (MinorStat stat in MinorStats.Values)
                if (stat.DoSave)
                    stat.Load(tag.GetCompound(stat.UnlocalizedName));
        }
    }
}
