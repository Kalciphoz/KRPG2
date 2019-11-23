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
    internal class CancelAllocationButton : Button
    {
        protected override Texture2D Texture => GetTexture("Cancel");
        protected override Texture2D Texture_Pressed => GetTexture("Cancel_Depressed");

        private readonly StatAllocationGUI gui;

        public CancelAllocationButton(StatAllocationGUI gui, Vector2 position) : base(position)
        {
            this.gui = gui;
        }

        protected override void Click()
        {
            gui.Cancel();
        }
    }
}
