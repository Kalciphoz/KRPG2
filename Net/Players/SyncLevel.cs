using WebmilioCommons.Networking.Packets;
using KRPG2.Players;

namespace KRPG2.Net.Players
{
    public class SyncLevel : ModPlayerNetworkPacket<K2Player>
    {
        public int Level
        {
            get => ModPlayer.Character.Level;
            set => ModPlayer.Character.Level = value;
        }

        public long XP
        {
            get => ModPlayer.Character.XP;
            set => ModPlayer.Character.XP = value;
        }
    }
}
