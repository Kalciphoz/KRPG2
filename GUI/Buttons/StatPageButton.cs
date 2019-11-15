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
    public class StatPageButton : Button
    {
        public override bool Toggled => K2Player.Inventory.statPage;

        protected override Texture2D Texture => GetTexture("Stats");
        protected override Texture2D Texture_Pressed => GetTexture("Stats_Selected");

        public StatPageButton(Vector2 position) : base(position) { }

        public override void Click()
        {
            K2Player.Inventory.statPage = true;
        }
    }
}
