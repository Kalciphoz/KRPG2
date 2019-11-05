using Terraria;
using Terraria.ModLoader.IO;

namespace KRPG2.Inventory
{
    public class InventoryPage
    {
        public Item[] item = new Item[40];

        private readonly int id;

        public InventoryPage(int id)
        {
            this.id = id;
            for (int i = 0; i < item.Length; i += 1)
            {
                item[i] = new Item();
                item[i].SetDefaults();
            }
        }

        internal TagCompound Save()
        {
            var tag = new TagCompound();
            for (int i = 0; i < item.Length; i += 1)
                tag.Add($"item{i}", ItemIO.Save(item[i]));
            return tag;
        }

        internal void Load(TagCompound tag)
        {
            for (int i = 0; i < item.Length; i += 1)
                item[i] = ItemIO.Load(tag.GetCompound($"item{i}"));
        }
    }
}
