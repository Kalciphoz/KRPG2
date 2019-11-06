﻿using KRPG2.Players;
using Terraria;
using Terraria.ModLoader;

namespace KRPG2
{
    public class K2Item : GlobalItem
    {
        public override bool OnPickup(Item item, Player player)
        {
            return player.GetModPlayer<K2Player>().Inventory.lootLogic.OnPickup(item);
        }
    }
}
