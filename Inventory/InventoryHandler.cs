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
    public class InventoryHandler
    {
        internal readonly ItemLootLogic lootLogic;

        public readonly InventoryPage[] page = new InventoryPage[3]
        {
            new InventoryPage(0),
            new InventoryPage(1),
            new InventoryPage(2)
        };

        public int unlocked = 0;

        private K2Player k2player;
        private Player Player => k2player.player;
        private RPGCharacter Character => k2player.character;

        public int ActivePage { get; private set; } = 0;
        public bool statPage = false;

        public InventoryHandler(K2Player k2player)
        {
            this.k2player = k2player;
            lootLogic = new ItemLootLogic(this, k2player);
        }

        internal void OpenPage(int p)
        {
            for (int i = 0; i < 40; i += 1)
                Player.inventory[i + 10] = page[p].item[i];
            ActivePage = p;
            statPage = false;
            API.FindRecipes();
            for (int i = 0; i < 50; i += 1)
                if (Player.inventory[i].type == 71 || Player.inventory[i].type == 72 || Player.inventory[i].type == 73 || Player.inventory[i].type == 74)
                    Player.DoCoins(i);
        }

        internal TagCompound Save()
        {
            var tag = new TagCompound();
            tag.Add("unlocked", unlocked);
            for (int i = 0; i <= unlocked; i += 1)
                tag.Add("page" + i, page[i].Save());
            return tag;
        }

        internal void Load(TagCompound tag)
        {
            unlocked = tag.GetInt("unlocked");
            for (int i = 0; i <= unlocked; i += 1)
                page[i].Load(tag.GetCompound("page" + i));
            OpenPage(0);
        }
    }
}
