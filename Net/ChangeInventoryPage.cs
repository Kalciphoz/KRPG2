using System.IO;
using KRPG2.Players;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net
{
    public class ChangeInventoryPage : ModPlayerNetworkPacket<K2Player>
    {
        public int PageNumber
        {
            get => ModPlayer.Inventory.ActivePage;
            set => ModPlayer.Inventory.OpenPage(value);
        }
    }
}