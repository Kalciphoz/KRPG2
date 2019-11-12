using KRPG2.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections;
using WebmilioCommons.Tinq;
using System;

namespace KRPG2
{
    public class K2NPC : GlobalNPC
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

                    if (npc.type == NPCID.WallofFlesh)
                    {
                        if (k2player.Inventory.Unlocked < 2) k2player.Inventory.Unlocked = 2;
                    }

                    else if (k2player.Inventory.Unlocked < 1) k2player.Inventory.Unlocked = 1;
                });

            if (npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.BrainofCthulhu || npc.type == NPCID.EaterofWorldsHead && !Main.npc.AnyActive(n => n.type == NPCID.EaterofWorldsBody))
                character.SetLevel(21);

            if (npc.type == NPCID.WallofFlesh || npc.type == NPCID.WallofFleshEye)
                character.SetLevel(41);

            if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.SkeletronPrime || npc.type == NPCID.Spazmatism && !Main.npc.AnyActive(n => n.type == NPCID.Retinazer) || npc.type == NPCID.Retinazer && !Main.npc.AnyActive(n => n.type == NPCID.Spazmatism))
                character.SetLevel(61);

            if (npc.type == NPCID.Plantera)
                character.SetLevel(81);

            if (npc.type == NPCID.MoonLordCore)
                character.SetLevel(100);
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
