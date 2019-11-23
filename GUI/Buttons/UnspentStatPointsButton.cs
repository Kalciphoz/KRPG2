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

namespace KRPG2.GUI.Buttons
{
    internal abstract class UnspentStatPointsButton : Button
    {
        protected override string HoverText => Enabled ? "Click here to allocate stat points" : "You have no unspent stat points";

        public UnspentStatPointsButton(Vector2 position) : base(position) { }

        protected override bool CanClick => Character.UnspentStatPoints > 0;

        protected override void Click()
        {
            K2Player.OpenStatAllocationGUI();
        }
    }

    internal class UnspentPoints_StatusBar : UnspentStatPointsButton
    {
        protected override Texture2D Texture => GetTexture("UnspentStatusBar");

        public UnspentPoints_StatusBar(Vector2 position) : base(position) { }
    }

    internal class UnspentPoints_Inventory : UnspentStatPointsButton
    {
        protected override Texture2D Texture => GetTexture("UnspentInventory");

        public UnspentPoints_Inventory(Vector2 position) : base(position) { }
    }
}
