using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using WebmilioCommons.Loaders;

namespace KRPG2.NPCs.Prefixes
{
    public sealed class NPCPrefixLoader : SingletonLoader<NPCPrefixLoader, NPCPrefix>
    {
        private Dictionary<float, List<Type>> _typeByCommonness = new Dictionary<float, List<Type>>();


        protected override void PostAdd(Mod mod, NPCPrefix item)
        {
            if (item.Rarity <= 0)
                return;

            float weight = GetWeight(item.Rarity);
            GetOrCreateCommonnessList(weight).Add(item.GetType());

            TotalWeight += weight;
        }

        private List<Type> GetOrCreateCommonnessList(float percentage)
        {
            if (!_typeByCommonness.ContainsKey(percentage))
                _typeByCommonness.Add(percentage, new List<Type>());

            return _typeByCommonness[percentage];
        }

        private static float GetWeight(float rarity) => 1 / rarity;


        public NPCPrefix GetWeightedRandom()
        {
            return null;
        }


        public override void PreLoad()
        {
            _typeByCommonness = new Dictionary<float, List<Type>>();
        }

        public override void Unload()
        {
            base.Unload();

            _typeByCommonness = new Dictionary<float, List<Type>>();
        }


        public float TotalWeight { get; private set; }
    }
}