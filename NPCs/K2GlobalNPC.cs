using System;
using System.Collections.Generic;
using KRPG2.Players;
using KRPG2.RPG;
using KRPG2.RPG.Leveling;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Tinq;

namespace KRPG2.NPCs
{
    public class K2LevelingNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Initialized { get; private set; }
        public int Level { get; private set; }

        private int defLife;

        public override void NPCLoot(NPC npc)
        {
            if (npc.lifeMax < 10) return;
            if (npc.friendly) return;
            if (npc.townNPC) return;

            RPGCharacter character = K2Player.Get().Character;
            int baseExp = (int)Math.Round(Level * Math.Pow(defLife, 0.5) / 4 + 3);
            character.GainXP(baseExp * 2 / (Main.expertMode ? 3 : 2));

            if (npc.boss)
                Main.player.DoActive(player =>
                {
                    K2Player k2player = K2Player.Get(player);

                    if (npc.type == NPCID.WallofFlesh && k2player.Inventory.Unlocked < 2)
                        k2player.Inventory.Unlocked = 2;
                    else if (k2player.Inventory.Unlocked < 1)
                        k2player.Inventory.Unlocked = 1;
                });

            foreach (KeyValuePair<Predicate<NPC>, int> kvp in LevelLocks.levelPlateaus)
                if (kvp.Key(npc))
                    character.SetLevel(kvp.Value + 1);
        }

        public override bool PreAI(NPC npc)
        {
            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);

            if (npc.Hitbox.Contains(mousePosition))
                Main.hoverItemName = "kkekekekekkee";

            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
            if (!Initialized)
                Initialize(npc);
        }

        public void Initialize(NPC npc)
        {
            SetLevel(npc);
            defLife = npc.lifeMax;
            Initialized = true;
        }

        private void SetLevel(NPC npc)
        {
            int dmgLevel = npc.damage / (Main.expertMode ? 2 : 1) - 4;
            int lifeLevel = (int)Math.Round(npc.lifeMax / (Main.expertMode ? 14.0 : 7.0));
            int defenseLevel = npc.defense * 2;
            int total = dmgLevel + lifeLevel + defenseLevel;
            if (lifeLevel * 3 > total * 2)
            {
                total += 2;
                total -= lifeLevel;
                if (defenseLevel * 4 < total)
                {
                    Level = Math.Min(total - defenseLevel, 125);
                    return;
                }
            }
            else
                total -= Math.Min(Math.Min(dmgLevel, lifeLevel), defenseLevel);

            Level = (int)Math.Round(total / 2.0);
        }
    }
}
