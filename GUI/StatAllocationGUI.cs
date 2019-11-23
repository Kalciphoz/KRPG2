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
using KRPG2.GFX;
using KRPG2.GUI.Buttons;

namespace KRPG2.GUI
{
    internal class StatAllocationGUI : BaseGUI
    {
        private Texture2D DeerSkull => GetTexture("DeerSkull");

        private Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI(krpg2, texture);

        public StatAllocationGUI() : base()
        {
        }

        public int TotalAllocated
        {
            get
            {
                int total = 0;
                foreach (StatAllocationButton button in buttons)
                    total += button.Allocated;

                return total;
            }
        }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
