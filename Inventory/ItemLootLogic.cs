using Terraria;
using Terraria.ID;
using Terraria.GameContent.Achievements;

namespace KRPG2.Inventory
{
    internal class ItemLootLogic
    {
        private readonly InventoryHandler inventory;
        private K2Player k2player;
        private Player Player => k2player.player;
        private RPGCharacter Character => k2player.character;

        internal ItemLootLogic(InventoryHandler inventory, K2Player k2player)
        {
            this.inventory = inventory;
            this.k2player = k2player;
        }

        public bool CanPickup(Item newItem)
        {
            if (ItemID.Sets.NebulaPickup[newItem.type] || newItem.type == ItemID.Heart || newItem.type == ItemID.CandyApple || newItem.type == ItemID.CandyCane || newItem.type == ItemID.Star || newItem.type == ItemID.SoulCake || newItem.type == ItemID.SugarPlum)
            {
                return true;
            }
            int startSlot = 50;

            if (newItem.type >= ItemID.CopperCoin && newItem.type <= ItemID.PlatinumCoin)
                startSlot = 54;

            for (int i = 0; i < startSlot; i++)
            {
                Item item = Player.inventory[i];
                if ((item.type == 0 || item.stack == 0) && (startSlot > 50 || inventory.ActivePage == 0) || item.type > 0 && item.stack > 0 && item.stack < item.maxStack && newItem.IsTheSameAs(item))
                    return true;
            }

            for (int i = 0; i <= inventory.unlocked; i++)
            {
                for (int j = 0; j < inventory.page[i].item.Length; j += 1)
                {
                    Item item = inventory.page[i].item[j];
                    if (item.type == 0 || item.stack == 0 || item.type > 0 && item.stack > 0 && item.stack < item.maxStack && newItem.IsTheSameAs(item))
                        return true;
                    
                }
            }

            return false;
        }

        public bool OnPickup(Item item)
        {
            if (Player.whoAmI == Main.myPlayer && (Player.inventory[Player.selectedItem].type != 0 || Player.itemAnimation <= 0))
            {
                if (ItemID.Sets.NebulaPickup[item.type])
                {
                    Main.PlaySound(7, (int)Player.position.X, (int)Player.position.Y, 1, 1f, 0f);
                    item = new Item();
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(102, -1, -1, null, Player.whoAmI, (float)item.buffType, Player.Center.X, Player.Center.Y, 0, 0, 0);
                        NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                    else
                    {
                        Player.NebulaLevelup(item.buffType);
                    }
                }
                if (item.type == ItemID.Heart || item.type == ItemID.CandyApple || item.type == ItemID.CandyCane)
                {
                    Main.PlaySound(7, (int)Player.position.X, (int)Player.position.Y, 1, 1f, 0f);
                    int healAmount = 10 + Character.Level / 2;
                    Player.statLife += healAmount;
                    if (Main.myPlayer == Player.whoAmI)
                    {
                        Player.HealEffect(healAmount);
                    }
                    if (Player.statLife > Player.statLifeMax2)
                    {
                        Player.statLife = Player.statLifeMax2;
                    }
                    item = new Item();
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
                else if (item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum)
                {
                    Main.PlaySound(7, (int)Player.position.X, (int)Player.position.Y, 1, 1f, 0f);
                    int healAmount = 5;// + Character.TotalStats(STAT.WITS);
                    Player.statMana += healAmount;
                    Player.statMana += healAmount;
                    if (Main.myPlayer == Player.whoAmI)
                    {
                        Player.ManaEffect(healAmount);
                    }
                    item = new Item();
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
                else
                {
                    item = GetItem(item);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }

            return false;
        }

        private Item GetItem(Item item)
        {
            bool isCoin = item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin;
            int startSlot = 50;
            if (item.noGrabDelay > 0)
                return item;
            
            int num2 = 0;
            if (item.uniqueStack && Player.HasItem(item.type))
                return item;
            
            if (isCoin)
            {
                num2 = -4;
                startSlot = 54;
            }

            if (((item.ammo > 0 || item.bait > 0) && !item.notAmmo) || item.type == ItemID.Wire)
            {
                item = Player.FillAmmo(Player.whoAmI, item);
                if (item.type == 0 || item.stack == 0)
                    return new Item();
            }

            for (int i = num2; i < 50; i++)
            {
                int num3 = i;
                if (num3 < 0) num3 = 54 + i;

                if (TryPlaceItem(ref item, ref Player.inventory[num3], isCoin, true))
                    return new Item();

                if (isCoin) Player.DoCoins(num3);
            }

            for (int i = 0; i <= inventory.unlocked; i += 1)
                if (k2player.inventory.ActivePage != i)
                {
                    InventoryPage page = inventory.page[i];
                    for (int j = 0; j < page.item.Length; j += 1)
                        if (TryPlaceItem(ref item, ref page.item[j], isCoin, true))
                            return new Item();
                }

            if (!isCoin && item.useStyle > 0)
                for (int j = 0; j < 10; j++)
                    if (TryPlaceItem(ref item, ref Player.inventory[j], isCoin, true))
                        return new Item();

            if (item.favorited)
            {
                for (int k = 0; k < startSlot; k++)
                    if (k2player.inventory.ActivePage == 0 || isCoin)
                        if (TryPlaceItem(ref item, ref Player.inventory[k], isCoin, false))
                        {
                            if (isCoin) Player.DoCoins(k);
                            return new Item();
                        }
            }

            else
            {
                for (int l = startSlot - 1; l >= 0; l--)
                    if (inventory.ActivePage == 0 || isCoin)
                        if (TryPlaceItem(ref item, ref Player.inventory[l], isCoin, false))
                        {
                            if (isCoin) Player.DoCoins(l);
                            return new Item();
                        }

                for (int i = 0; i <= inventory.unlocked; i += 1)
                    if (inventory.ActivePage != i)
                    {
                        InventoryPage page = inventory.page[i];
                        for (int j = 0; j < page.item.Length; j += 1)
                            if (TryPlaceItem(ref item, ref page.item[j], isCoin, false)) return new Item();
                    }
            }

            return item;
        }

        private bool TryPlaceItem(ref Item item, ref Item target, bool isCoin, bool incrementStack)
        {
            bool dispose = false;

            if (target.type == 0 && !incrementStack)
            {
                target = item;
                ItemText.NewText(item, item.stack);
                AchievementsHelper.NotifyItemPickup(Player, item);
                dispose = true;
            }
            else if (incrementStack && target.type > 0 && target.stack < target.maxStack && item.IsTheSameAs(target))
            {
                if (item.stack + target.stack <= target.maxStack)
                {
                    target.stack += item.stack;
                    ItemText.NewText(item, item.stack);
                    AchievementsHelper.NotifyItemPickup(Player, item);
                    dispose = true;
                }
                else
                {
                    AchievementsHelper.NotifyItemPickup(Player, item, target.maxStack - target.stack);
                    item.stack -= target.maxStack - target.stack;
                    ItemText.NewText(item, target.maxStack - target.stack);
                    target.stack = target.maxStack;
                }
            }
            
            Main.PlaySound(isCoin ? SoundID.CoinPickup : SoundID.Grab, (int)Player.position.X, (int)Player.position.Y, 1, 1f, 0f);
            if (Player.whoAmI == Main.myPlayer)
            {
                API.FindRecipes();
            }
            return dispose;
        }
    }
}
