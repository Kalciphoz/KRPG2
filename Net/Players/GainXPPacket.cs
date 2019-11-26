using System.IO;
using KRPG2.Players;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Net.Players
{
    public class GainXPPacket : ModPlayerNetworkPacket<K2Player>
    {
        public GainXPPacket() : base() { }

        public GainXPPacket(int xp) : base()
        {
            this.XP = xp;
        }

        public int XP
        {
            get;
            set;
        }

        protected override bool PostReceive(BinaryReader reader, int fromWho)
        {
            ModPlayer.Character.GainXP(XP, false);
            return base.PostReceive(reader, fromWho);
        }
    }
}
