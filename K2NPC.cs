using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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
                            if (k2player.inventory.unlocked < 2) k2player.inventory.unlocked = 2;
                        }

                        else if (k2player.inventory.unlocked < 1) k2player.inventory.unlocked = 1;
                    }
            
        }
    }
}
