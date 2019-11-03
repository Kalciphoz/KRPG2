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
    internal class InvPageButton : Button
    {
        public override bool Toggled => k2player.inventory.ActivePage == id && !k2player.inventory.statPage;

        protected override Texture2D Texture => GetTexture($"Page{id}");
        protected override Texture2D Texture_Pressed => GetTexture($"Page{id}_Selected");
        protected override Texture2D Texture_Disabled => id == 0 ? null : GetTexture($"Page{id}_Disabled");

        protected override bool Toggleable => true;
        protected override bool Enabled => k2player.inventory.unlocked >= id;

        private int id;

        public InvPageButton(K2Player k2player, Vector2 position, int id) : base(k2player, position)
        {
            this.id = id;
        }

        public override void Click()
        {
            k2player.inventory.OpenPage(id);
        }
    }
}
