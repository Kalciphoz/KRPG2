using System;
using KRPG2.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Tinq;

namespace KRPG2.NPCs
{
    public class K2GlobalNPC : GlobalNPC
    {
        public int Level { get; private set; }

        public override bool InstancePerEntity { get; } = true;


        public override void SetDefaults(NPC npc)
        {
            Level = GetLevel(npc);
        }


        public override void NPCLoot(NPC npc)
        {
            if (npc.boss)
                Main.player.DoActive(player =>
                {
                    K2Player k2Player = K2Player.Get(player);

                    if (npc.type == NPCID.WallofFlesh && k2Player.Inventory.unlocked < 2)
                        k2Player.Inventory.unlocked = 2;
                    else if (k2Player.Inventory.unlocked < 1) 
                        k2Player.Inventory.unlocked = 1;
                });
        }


        public override bool PreAI(NPC npc)
        {
            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);

            if (npc.Hitbox.Contains(mousePosition))
                Main.hoverItemName = "kkekekekekkee";

            return base.PreAI(npc);
        }


        public static int GetLevel(NPC npc) => npc.defense * 4 + npc.damage * (Main.expertMode ? 2 : 1) / 3;
    }
}