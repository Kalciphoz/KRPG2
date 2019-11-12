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
using WebmilioCommons.Networking.Packets;
using KRPG2.Players;

namespace KRPG2.Net
{
    public class GainXP : ModPlayerNetworkPacket<K2Player>
    {
        public GainXP() : base() { }

        public GainXP(int xp) : base()
        {
            this.XP = xp;
        }

        public int XP
        {
            get;
            set;
        }

        public override bool PostReceive(BinaryReader reader, int fromWho)
        {
            ModPlayer.Character.GainXP(XP, false);
            return base.PostReceive(reader, fromWho);
        }
    }
}
