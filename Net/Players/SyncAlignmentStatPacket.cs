﻿using System;
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
using KRPG2.RPG.Stats;

namespace KRPG2.Net.Players
{
    public class SyncAlignmentStatPacket : ModPlayerNetworkPacket<K2Player>
    {
        private readonly AlignmentStat stat;

        private IEnumerable<AlignmentStat> AlignmentStats => ModPlayer.Character.AlignmentStats.Values;

        public SyncAlignmentStatPacket(AlignmentStat stat) : base()
        {
            this.stat = stat;
        }

        protected override void PrePopulatePacket(ModPacket modPacket, ref int fromWho, ref int toWho)
        {
            modPacket.Write(stat.UnlocalizedName);
            modPacket.Write(stat.BaseAmount);
        }

        public override bool PostReceive(BinaryReader reader, int fromWho)
        {
            bool result = base.PostReceive(reader, fromWho);

            string name = reader.ReadString();
            AlignmentStat stat = AlignmentStats.FirstOrDefault(s => s.UnlocalizedName == name);
            if (stat != null)
                stat.BaseAmount = reader.ReadInt32();

            return result;
        }
    }
}
