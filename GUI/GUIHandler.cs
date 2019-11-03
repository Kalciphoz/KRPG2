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
        private readonly StatusBar statusBar;
        private readonly Hotbar hotbar;
        private readonly InventoryGUI inventory;
        private readonly BuffsGUI buffs;

        private K2Player K2Player => Main.LocalPlayer.GetModPlayer<K2Player>();

        public GUIHandler()
        {
            statusBar = new StatusBar();
            hotbar = new Hotbar();
            inventory = new InventoryGUI();
            buffs = new BuffsGUI();

            Hotbar.ReplaceTextures();
        }
    }
}
