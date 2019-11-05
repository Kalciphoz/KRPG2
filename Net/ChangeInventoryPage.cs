using System.IO;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net
{
    public class ChangeInventoryPage : ModPlayerNetworkPacket<K2Player>
    {
        public int PageNumber
        {
            get => ModPlayer.inventory.ActivePage;
            set => ModPlayer.inventory.OpenPage(value);
        }
    }
}