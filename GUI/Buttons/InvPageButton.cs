using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using KRPG2.Net;
using WebmilioCommons.Extensions;

namespace KRPG2.GUI.Buttons
{
    internal class InvPageButton : Button
    {
        public override bool Toggled => K2Player.Inventory.ActivePage == id && !K2Player.Inventory.statPage;

        protected override Texture2D Texture => GetTexture($"Page{id}");
        protected override Texture2D Texture_Pressed => GetTexture($"Page{id}_Selected");
        protected override Texture2D Texture_Disabled => id == 0 ? null : GetTexture($"Page{id}_Disabled");

        protected override bool Toggleable => true;
        protected override bool Enabled => K2Player.Inventory.unlocked >= id;

        private readonly int id;

        public InvPageButton(Vector2 position, int id) : base(position)
        {
            this.id = id;
        }

        public override void Click()
        {
            K2Player.Inventory.OpenPage(id);
        }
    }
}
