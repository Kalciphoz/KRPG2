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
    internal class ConfirmAllocationButton : Button
    {
        protected override Texture2D Texture => GetTexture("Confirm");
        protected override Texture2D Texture_Pressed => GetTexture("Confirm_Depressed");

        private readonly StatAllocationGUI gui;

        public ConfirmAllocationButton(StatAllocationGUI gui, Vector2 position) : base(position)
        {
            this.gui = gui;
        }

        protected override void Click()
        {
            gui.Confirm();
        }
    }
}
