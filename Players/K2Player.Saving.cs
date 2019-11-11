using Terraria.ModLoader.IO;

namespace KRPG2.Players
{
    public sealed partial class K2Player
    {
        public const string SAVE_KEY_INVENTORY = "Inventory";
        public const string SAVE_KEY_CHARACTER = "Character";


        public override TagCompound Save()
        {
            if (!Initialized) Init();

            return new TagCompound
            {
                {SAVE_KEY_INVENTORY, Inventory.Save()},
                {SAVE_KEY_CHARACTER, Character.Save()}
            };
        }

        public override void Load(TagCompound tag)
        {
            Init();
            Inventory.Load(tag.GetCompound(SAVE_KEY_INVENTORY));
            Character.Load(tag.GetCompound(SAVE_KEY_CHARACTER));
        }
    }
}
