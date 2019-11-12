using System.IO;
using KRPG2.Players;
using KRPG2.RPG.Stats;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net.Players
{
    public class ServerJoinSyncPacket : ModPlayerNetworkPacket<K2Player>
    {
        protected override void PrePopulatePacket(ModPacket modPacket, ref int fromWho, ref int toWho)
        {
            for (int i = 1; i < ModPlayer.Inventory.Page.Length; i += 1)
                for (int j = 0; j < ModPlayer.Inventory.Page[i].item.Length; j += 1)
                    modPacket.WriteItem(ModPlayer.Inventory.Page[i].item[j], true, true);

            foreach (AlignmentStat stat in ModPlayer.Character.AlignmentStats.Values)
                if (stat.DoSave)
                    modPacket.Write(stat.baseAmount);

            foreach (MinorStat stat in ModPlayer.Character.MinorStats.Values)
                if (stat.DoSave)
                    modPacket.Write(stat.BaseAmount);
        }


        protected override bool PreReceive(BinaryReader reader, int fromWho)
        {
            bool result = base.PreReceive(reader, fromWho);

            if (!ModPlayer.Initialized) ModPlayer.Init();

            for (int i = 1; i < ModPlayer.Inventory.Page.Length; i += 1)
                for (int j = 0; j < ModPlayer.Inventory.Page[i].item.Length; j += 1)
                    ModPlayer.Inventory.Page[i].item[j] = ItemIO.Receive(reader, true, true);

            foreach (AlignmentStat stat in ModPlayer.Character.AlignmentStats.Values)
                if (stat.DoSave)
                    stat.baseAmount = reader.ReadInt32();

            foreach (MinorStat stat in ModPlayer.Character.MinorStats.Values)
                if (stat.DoSave)
                    stat.BaseAmount = reader.ReadSingle();

            return result;
        }

        public int UnlockedPages
        {
            get => ModPlayer.Inventory.Unlocked;
            set => ModPlayer.Inventory.Unlocked = value;
        }
    }
}