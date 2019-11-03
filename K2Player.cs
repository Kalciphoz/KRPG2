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

namespace KRPG2
{
    public class K2Player : ModPlayer
    {
        public readonly RPGCharacter character;

        public InventoryHandler inv;

        private GUIHandler guiHandler;
        private bool initializedGUI = false;

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

        public override void PostUpdate()
        {
            if (Main.netMode != NetmodeID.Server)
                if (!initializedGUI)
                {
                    guiHandler = new GUIHandler(this);
                    initializedGUI = true;
                }
        }
    }
}
