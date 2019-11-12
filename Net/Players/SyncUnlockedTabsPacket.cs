using KRPG2.Players;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net.Players
{
    public class SyncUnlockedTabsPacket : ModPlayerNetworkPacket<K2Player>
    {
        public int Unlocked
        {
            get => ModPlayer.Inventory.Unlocked;
            set => ModPlayer.Inventory.Unlocked = value;
        }
    }
}
