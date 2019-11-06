using KRPG2.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KRPG2
{
    public class K2NPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.boss)
                foreach (Player player in Main.player)
                    if (player != null && player.active)
                    {
                        K2Player k2player = player.GetModPlayer<K2Player>();

                        if (npc.type == NPCID.WallofFlesh)
                        {
                            if (k2player.Inventory.unlocked < 2) k2player.Inventory.unlocked = 2;
                        }

                        else if (k2player.Inventory.unlocked < 1) k2player.Inventory.unlocked = 1;
                    }
            
        }
    }
}
