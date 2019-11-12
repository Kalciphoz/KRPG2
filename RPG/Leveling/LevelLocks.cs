using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using WebmilioCommons.Tinq;

namespace KRPG2.RPG.Leveling
{
    public class LevelLocks
    {
        internal static Dictionary<Predicate<NPC>, int> levelPlateaus;
        internal static List<int> softXPCaps;

        internal static void Load()
        {
            levelPlateaus = new Dictionary<Predicate<NPC>, int>();
            softXPCaps = new List<int>();

            Add(npc => 
                npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.BrainofCthulhu || npc.type == NPCID.EaterofWorldsHead && !Main.npc.AnyActive(n => n.type == NPCID.EaterofWorldsBody),
                20);

            Add(npc => npc.type == NPCID.WallofFlesh || npc.type == NPCID.WallofFleshEye, 40);

            Add(npc =>
                npc.type == NPCID.TheDestroyer || npc.type == NPCID.SkeletronPrime || npc.type == NPCID.Spazmatism && !Main.npc.AnyActive(n => n.type == NPCID.Retinazer) || npc.type == NPCID.Retinazer && !Main.npc.AnyActive(n => n.type == NPCID.Spazmatism),
                60);

            Add(npc => npc.type == NPCID.Plantera, 80);
            Add(npc => npc.type == NPCID.MoonLordCore, 100);
        }

        internal static void Unload()
        {
            levelPlateaus = null;
            softXPCaps = new List<int>();
        }

        public static void Add(Predicate<NPC> unlockCondition, int lockAt)
        {
            levelPlateaus.Add(unlockCondition, lockAt);
            softXPCaps.Add(lockAt);
        }

        public static bool Contains(int level) => 
            softXPCaps.Contains(level);
    }
}