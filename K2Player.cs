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
using KRPG2.GUI;
using KRPG2.Inventory;

namespace KRPG2
{
    public class K2Player : ModPlayer
    {
        public readonly RPGCharacter character;

        public InventoryHandler inventory;

        private GUIHandler guiHandler;

        public static List<Player> GetActivePlayers()
        {
            var list = new List<Player>();
            for (int i = 0; i < Main.player.Length; i += 1)
            {
                Player player = Main.player[i];
                if (player != null)
                    if (player.active)
                        list.Add(player);
            }
            return list;
        }

        public K2Player() : base()
        {
            character = new RPGCharacter(this);
        }

        public override void Initialize()
        {
        }

        public override void PostUpdate()
        {
            for (int i = 0; i < 40; i += 1)
                inventory.page[inventory.ActivePage].item[i] = player.inventory[i + 10];

            if (Main.mapTime % 60 == 0) API.FindRecipes();
        }

        public override TagCompound Save()
        {
            var tag = new TagCompound();
            tag.Add("inventory", inventory.Save());
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            if (Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer)
                guiHandler = new GUIHandler();

            inventory = new InventoryHandler(this);
            inventory.Load(tag.GetCompound("inventory"));
        }
    }
}
