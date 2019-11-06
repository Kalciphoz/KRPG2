﻿using System.IO;
using KRPG2.Players;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net
{
    public class ServerJoinSyncInventoryPages : ModPlayerNetworkPacket<K2Player>
    {
        protected override void PrePopulatePacket(ModPacket modPacket, ref int fromWho, ref int toWho)
        {
            for (int i = 1; i < ModPlayer.Inventory.Page.Length; i += 1)
                for (int j = 0; j < ModPlayer.Inventory.Page[i].item.Length; j += 1)
                    modPacket.WriteItem(ModPlayer.Inventory.Page[i].item[j], true, true);

            base.Send(fromWho, toWho);
        }

        public override bool PostReceive(BinaryReader reader, int fromWho)
        {
            if (!ModPlayer.Initialized) ModPlayer.Init();

            for (int i = 1; i < ModPlayer.Inventory.Page.Length; i += 1)
                for (int j = 0; j < ModPlayer.Inventory.Page[i].item.Length; j += 1)
                    ModPlayer.Inventory.Page[i].item[j] = ItemIO.Receive(reader, true, true);

            return base.PostReceive(reader, fromWho);
        }
    }
}