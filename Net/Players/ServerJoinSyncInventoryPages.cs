using System.IO;
using KRPG2.Players;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net.Players
{
    public class ServerJoinSyncInventoryPages : ModPlayerNetworkPacket<K2Player>
    {
        protected override void PrePopulatePacket(ModPacket modPacket, ref int fromWho, ref int toWho)
        {
            for (int i = 1; i < ModPlayer.Inventory.Page.Length; i += 1)
                for (int j = 0; j < ModPlayer.Inventory.Page[i].item.Length; j += 1)
                    modPacket.WriteItem(ModPlayer.Inventory.Page[i].item[j], true, true);
        }


        protected override bool PreReceive(BinaryReader reader, int fromWho)
        {
            bool result = base.PreReceive(reader, fromWho);

            if (!ModPlayer.Initialized) ModPlayer.Init();

            for (int i = 1; i < ModPlayer.Inventory.Page.Length; i += 1)
                for (int j = 0; j < ModPlayer.Inventory.Page[i].item.Length; j += 1)
                    ModPlayer.Inventory.Page[i].item[j] = ItemIO.Receive(reader, true, true);

            return result;
        }

        public int UnlockedPages
        {
            get => ModPlayer.Inventory.Unlocked;
            set => ModPlayer.Inventory.Unlocked = value;
        }
    }
}