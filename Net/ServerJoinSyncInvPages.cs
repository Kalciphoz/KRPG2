using System.IO;
using Terraria.ModLoader.IO;
using KRPG2.Inventory;

namespace KRPG2.Net
{
    internal class ServerJoinSyncInvPages : K2Message
    {
        public override bool resendPacket => true;

        public override Requirements reqs => Requirements.K2Player;

        public ServerJoinSyncInvPages(KRPG2 krpg2, InventoryHandler inv) : base(krpg2, w =>
        {
            for (int i = 1; i < inv.page.Length; i += 1)
                for (int j = 0; j < inv.page[i].item.Length; j += 1)
                    ItemIO.Send(inv.page[i].item[j], w, true, true);
        }) { }

        protected override void Read(BinaryReader reader, int sender)
        {
            if (!k2player.initialized) k2player.Init();

            for (int i = 1; i < k2player.inventory.page.Length; i += 1)
                for (int j = 0; j < k2player.inventory.page[i].item.Length; j += 1)
                    k2player.inventory.page[i].item[j] = ItemIO.Receive(reader, true, true);
        }
    }
}
