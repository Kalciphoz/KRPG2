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

namespace KRPG2.GUI
{
    internal class GUIHandler
    {
        private readonly K2Player k2player;

        private readonly StatusBar statusBar;
        private readonly Hotbar hotbar;
        private readonly Inventory inventory;
        private  Buffs buffs;

        public GUIHandler(K2Player k2player)
        {
            this.k2player = k2player;

            statusBar = new StatusBar(k2player);
            hotbar = new Hotbar(k2player);
            inventory = new Inventory(k2player);
            buffs = new Buffs(k2player);

            Hotbar.ReplaceTextures();
        }
    }
}
