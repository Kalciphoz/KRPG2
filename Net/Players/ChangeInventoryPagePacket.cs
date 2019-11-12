using KRPG2.Players;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net.Players
{
    public class ChangeInventoryPagePacket : ModPlayerNetworkPacket<K2Player>
    {
        public int PageNumber
        {
            get => ModPlayer.Inventory.ActivePage;
            set => ModPlayer.Inventory.OpenPage(value);
        }
    }
}