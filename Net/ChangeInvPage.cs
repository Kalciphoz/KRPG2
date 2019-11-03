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

namespace KRPG2.Net
{
    public class ChangeInvPage : K2Message
    {
        public override bool resendPacket => true;

        public override Requirements reqs => Requirements.K2Player;

        public ChangeInvPage(KRPG2 krpg2, int page) : base(krpg2, w => w.Write(page)) { }

        protected override void Read(BinaryReader reader, int sender) => k2player.inventory.OpenPage(reader.ReadInt32());
    }
}
