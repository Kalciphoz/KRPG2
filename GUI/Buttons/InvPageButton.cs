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
using KRPG2.Net;

namespace KRPG2.GUI.Buttons
{
    internal class InvPageButton : Button
    {
        public override bool Toggled => K2Player.inventory.ActivePage == id && !K2Player.inventory.statPage;

        protected override Texture2D Texture => GetTexture($"Page{id}");
        protected override Texture2D Texture_Pressed => GetTexture($"Page{id}_Selected");
        protected override Texture2D Texture_Disabled => id == 0 ? null : GetTexture($"Page{id}_Disabled");

        protected override bool Toggleable => true;
        protected override bool Enabled => K2Player.inventory.unlocked >= id;

        private readonly int id;

        public InvPageButton(Vector2 position, int id) : base(position)
        {
            this.id = id;
        }

        public override void Click()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) K2Networking.SendPacket(krpg2, new ChangeInvPage(krpg2, id));
            K2Player.inventory.OpenPage(id);
        }
    }
}
