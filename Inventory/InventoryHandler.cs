using KRPG2.Net;
using KRPG2.Net.Players;
using KRPG2.Players;
using KRPG2.RPG;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using WebmilioCommons.Extensions;

namespace KRPG2.Inventory
{
    public class InventoryHandler
    {
        public const string 
            SAVE_KEY_UNLOCKED = "Unlocked",
            SAVE_KEY_PAGE = "Page";

        internal readonly ItemLootLogic lootLogic;

        // Inventory/Stat Pages
        public bool statPage;
        private int _activePage;

        private int _unlocked;

        public InventoryPage[] Page { get; } = new InventoryPage[3]
        {
            new InventoryPage(0),
            new InventoryPage(1),
            new InventoryPage(2)
        };

        public K2Player K2Player { get; }
        private Player Player => K2Player.player;
        private RPGCharacter Character => K2Player.Character;


        public InventoryHandler(K2Player k2player)
        {
            K2Player = k2player;
            lootLogic = new ItemLootLogic(this, k2player);
        }

        internal void OpenPage(int p)
        {
            for (int i = 0; i < K2Player.ACTUAL_INVENTORY_SIZE; i += 1)
                Player.inventory[i + K2Player.TOOLBAR_SIZE] = Page[p].item[i];

            ActivePage = p;
            statPage = false;

            API.FindRecipes();

            for (int slotIndex = 0; slotIndex < Main.realInventory; slotIndex += 1)
                if (Player.inventory[slotIndex].type >= ItemID.CopperCoin || Player.inventory[slotIndex].type <= ItemID.PlatinumCoin)
                    Player.DoCoins(slotIndex);
        }

        internal TagCompound Save()
        {
            TagCompound tag = new TagCompound
            {
                {SAVE_KEY_UNLOCKED, Unlocked}
            };

            for (int i = 0; i <= Unlocked; i += 1)
                tag.Add(SAVE_KEY_PAGE + i, Page[i].Save());

            return tag;
        }

        internal void Load(TagCompound tag)
        {
            Unlocked = tag.GetInt(SAVE_KEY_UNLOCKED);

            for (int i = 0; i <= Unlocked; i += 1)
                Page[i].Load(tag.GetCompound(SAVE_KEY_PAGE + i));

            OpenPage(0);
        }


        public int ActivePage
        {
            get => _activePage;
            set
            {
                if (value == _activePage) return;

                _activePage = value;
                K2Player.SendIfLocal(new ChangeInventoryPagePacket());
            }
        }
        
        public int Unlocked
        {
            get => _unlocked;
            set
            {
                if (value == _unlocked) return;

                _unlocked = value;
                K2Player.SendIfLocal(new SyncUnlockedTabsPacket());
            }
        }
    }
}
