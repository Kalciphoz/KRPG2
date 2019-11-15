using System;
using KRPG2.RPG;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Loaders;

namespace KRPG2.NPCs.Prefixes
{
    public abstract class NPCPrefix : IAssociatedToMod
    {
        protected NPCPrefix(int rarity, string namePrefix, bool hidden = false)
        {
            Rarity = rarity;

            NamePrefix = namePrefix;

            Hidden = hidden;
        }


        public virtual void Initialize(NPC npc) { }

        public virtual int SetLevel(NPC npc, int level) => level;


        public virtual void AI(NPC npc) { }

        public virtual bool PreAI(NPC npc) => true;
        public virtual void PostAI(NPC npc) { }

        public virtual void NPCLoot(NPC npc, RPGCharacter rpgCharacter, ref int baseExp) { }

        public virtual bool CheckAlive(NPC npc) => true;
        public virtual bool CheckDead(NPC npc) => true;

        public virtual bool? CanGoToStatue(NPC npc, ref bool toKingStatue) => null;

        public virtual bool CanHitPlayer(NPC npc, Player player, ref int cooldownSlot) => true;

        public virtual bool? CanBeHitByItem(NPC npc, Player player, Item item) => null;
        public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile) => null;


        public float Rarity { get; }

        public string NamePrefix { get; }

        public bool Hidden { get; }

        public Mod Mod { get; set; }
    }
}