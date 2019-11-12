using KRPG2.Players;
using Terraria;
using Terraria.ModLoader;

namespace KRPG2
{
    public class K2GlobalItem : GlobalItem
    {
        public override bool OnPickup(Item item, Player player)
        {
            return K2Player.Get(player).Inventory.lootLogic.OnPickup(item);
        }
    }
}
