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
using Terraria.GameContent.Achievements;

namespace KRPG2.Inventory
{
    public class InventoryPage
    {
        public Item[] item = new Item[40];

        private readonly int id;

        public InventoryPage(int id)
        {
            this.id = id;
            for (int i = 0; i < item.Length; i += 1)
            {
                item[i] = new Item();
                item[i].SetDefaults();
            }
        }

        internal TagCompound Save()
        {
            var tag = new TagCompound();
            for (int i = 0; i < item.Length; i += 1)
                tag.Add($"item{i}", ItemIO.Save(item[i]));
            return tag;
        }

        internal void Load(TagCompound tag)
        {
            for (int i = 0; i < item.Length; i += 1)
                item[i] = ItemIO.Load(tag.GetCompound($"item{i}"));
        }
    }
}
