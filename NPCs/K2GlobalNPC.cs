using System;
using System.Collections.Generic;
using KRPG2.NPCs.Prefixes;
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
    public class K2GlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.lifeMax < 10) return;
            if (npc.friendly) return;
            if (npc.townNPC) return;

            RPGCharacter character = K2Player.Get().Character;
            int baseExp = (int)Math.Round(Level * Math.Pow(OriginalLife, 0.5) / 4 + 3);

            Prefix.NPCLoot(npc, character, ref baseExp);

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
            if (!Initialized)
                Initialize(npc);

            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);

            if (npc.Hitbox.Contains(mousePosition))
                Main.hoverItemName = "kkekekekekkee";

            return Prefix.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
            Prefix.PostAI(npc);
        }


        #region Prefix Hooking

        // Take methods out of here if they need to do more than just call the prefix.

        public override void AI(NPC npc) => Prefix.AI(npc);

        public override bool CheckActive(NPC npc) => Prefix.CheckAlive(npc);
        public override bool CheckDead(NPC npc) => Prefix.CheckDead(npc);

        public override bool? CanGoToStatue(NPC npc, bool toKingStatue) => Prefix.CanGoToStatue(npc, ref toKingStatue);

        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot) => Prefix.CanHitPlayer(npc, target, ref cooldownSlot);

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item) => Prefix.CanBeHitByItem(npc, player, item);

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile) => Prefix.CanBeHitByProjectile(npc, projectile);

        #endregion


        public void Initialize(NPC npc)
        {
            OriginalLife = npc.lifeMax;

            Prefix = NPCPrefixLoader.Instance.New<ExampleNPCPrefix>();

            Prefix.Initialize(npc);

            SetLevel(npc);

            if (!Prefix.Hidden)
                npc.GivenName = Prefix.NamePrefix + npc.GivenName;

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

            Level = Prefix.SetLevel(npc, (int)Math.Round(total / 2.0));
        }


        public override bool InstancePerEntity => true;


        public bool Initialized { get; private set; }

        public int OriginalLife { get; set; }

        public int Level { get; private set; }

        public NPCPrefix Prefix { get; private set; }
    }
}
