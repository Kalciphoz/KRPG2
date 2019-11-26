using KRPG2.Players;
using KRPG2.RPG.Stats;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net.Players
{
    public sealed class SyncStatPacket : ModPlayerNetworkPacket<K2Player>
    {
        public SyncStatPacket() { }

        public SyncStatPacket(Stat stat)
        {
            UnlocalizedName = stat.UnlocalizedName;
        }


        public string UnlocalizedName { get; set; }

        public float BaseAmount
        {
            get => ModPlayer.Character.GetStat(UnlocalizedName).BaseAmount;
            set => ModPlayer.Character.GetStat(UnlocalizedName).BaseAmount = value;
        }
    }
}