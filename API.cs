using System.Collections.Generic;
using KRPG2.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using ReLogic.Graphics;

namespace KRPG2
{
    public static partial class API
    {
        public static void CraftItem(this Recipe r)
        {
            Main.CraftItem(r);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float scale)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle sourceRectangle, float scale)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public static void DrawStringWithShadow(this SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color color, float scale = 1f)
        {
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, position, color, 0f, Vector2.Zero, Vector2.One * scale);
        }

        public static bool Contains(this Rectangle rectangle, Vector2 point)
        {
            return rectangle.Contains((int)point.X, (int)point.Y);
        }

        public static void FindRecipes()
        {
            if (Main.dedServ) return;

            int num = Main.availableRecipe[Main.focusRecipe];
            float num2 = Main.availableRecipeY[Main.focusRecipe];
            
            for (int i = 0; i < Recipe.maxRecipes; i++)
            {
                Main.availableRecipe[i] = 0;
            }

            Main.numAvailableRecipes = 0;

            bool flag = Main.guideItem.type > 0 && Main.guideItem.stack > 0 && Main.guideItem.Name != "";
            if (flag)
            {
                for (int j = 0; j < Recipe.maxRecipes; j++)
                {
                    if (Main.recipe[j].createItem.type == 0)
                    {
                        break;
                    }
                    int num3 = 0;
                    while (num3 < Recipe.maxRequirements && Main.recipe[j].requiredItem[num3].type != 0)
                    {
                        if (Main.guideItem.IsTheSameAs(Main.recipe[j].requiredItem[num3]) || Main.recipe[j].useWood(Main.guideItem.type, Main.recipe[j].requiredItem[num3].type) || Main.recipe[j].useSand(Main.guideItem.type, Main.recipe[j].requiredItem[num3].type) || Main.recipe[j].useIronBar(Main.guideItem.type, Main.recipe[j].requiredItem[num3].type) || Main.recipe[j].useFragment(Main.guideItem.type, Main.recipe[j].requiredItem[num3].type) || Main.recipe[j].AcceptedByItemGroups(Main.guideItem.type, Main.recipe[j].requiredItem[num3].type) || Main.recipe[j].usePressurePlate(Main.guideItem.type, Main.recipe[j].requiredItem[num3].type))
                        {
                            Main.availableRecipe[Main.numAvailableRecipes] = j;
                            Main.numAvailableRecipes++;
                            break;
                        }
                        num3++;
                    }
                }
            }
            else
            {
                Dictionary<int, int> dictionary = new Dictionary<int, int>();
                Item item = null;
                Item[] inv = Main.player[Main.myPlayer].inventory;
                for (int k = 0; k < inv.Length; k++)
                {
                    item = inv[k];
                    if (item.stack > 0)
                    {
                        if (dictionary.ContainsKey(item.netID))
                        {
                            Dictionary<int, int> dictionary2;
                            int netID;
                            (dictionary2 = dictionary)[netID = item.netID] = dictionary2[netID] + item.stack;
                        }
                        else
                        {
                            dictionary[item.netID] = item.stack;
                        }
                    }
                }
                if (Main.player[Main.myPlayer].active)
                {
                    K2Player k2player = K2Player.Get();
                    for (int j = 0; j <= k2player.Inventory.Unlocked; j += 1)
                        if (j != k2player.Inventory.ActivePage)
                            foreach (Item i in k2player.Inventory.Page[j].item)
                            {
                                if (dictionary.ContainsKey(i.netID))
                                {
                                    Dictionary<int, int> dictionary2;
                                    int netID;
                                    (dictionary2 = dictionary)[netID = i.netID] = dictionary2[netID] + i.stack;
                                }
                                else
                                    dictionary[i.netID] = i.stack;
                            }
                }
                Item[] array = new Item[0];
                if (Main.player[Main.myPlayer].chest != -1)
                {
                    if (Main.player[Main.myPlayer].chest > -1)
                    {
                        array = Main.chest[Main.player[Main.myPlayer].chest].item;
                    }
                    else if (Main.player[Main.myPlayer].chest == -2)
                    {
                        array = Main.player[Main.myPlayer].bank.item;
                    }
                    else if (Main.player[Main.myPlayer].chest == -3)
                    {
                        array = Main.player[Main.myPlayer].bank2.item;
                    }
                    else if (Main.player[Main.myPlayer].chest == -4)
                    {
                        array = Main.player[Main.myPlayer].bank3.item;
                    }
                    for (int l = 0; l < 40; l++)
                    {
                        item = array[l];
                        if (item.stack > 0)
                        {
                            if (dictionary.ContainsKey(item.netID))
                            {
                                Dictionary<int, int> dictionary3;
                                int netID2;
                                (dictionary3 = dictionary)[netID2 = item.netID] = dictionary3[netID2] + item.stack;
                            }
                            else
                            {
                                dictionary[item.netID] = item.stack;
                            }
                        }
                    }
                }
                int num4 = 0;
                while (num4 < Recipe.maxRecipes && Main.recipe[num4].createItem.type != 0)
                {
                    bool flag2 = true;
                    if (flag2)
                    {
                        int num5 = 0;
                        while (num5 < Recipe.maxRequirements && Main.recipe[num4].requiredTile[num5] != -1)
                        {
                            if (!Main.player[Main.myPlayer].adjTile[Main.recipe[num4].requiredTile[num5]])
                            {
                                flag2 = false;
                                break;
                            }
                            num5++;
                        }
                    }
                    if (flag2)
                    {
                        for (int m = 0; m < Recipe.maxRequirements; m++)
                        {
                            item = Main.recipe[num4].requiredItem[m];
                            if (item.type == 0)
                            {
                                break;
                            }
                            int num6 = item.stack;
                            bool flag3 = false;
                            foreach (int current in dictionary.Keys)
                            {
                                if (Main.recipe[num4].useWood(current, item.type) || Main.recipe[num4].useSand(current, item.type) || Main.recipe[num4].useIronBar(current, item.type) || Main.recipe[num4].useFragment(current, item.type) || Main.recipe[num4].AcceptedByItemGroups(current, item.type) || Main.recipe[num4].usePressurePlate(current, item.type))
                                {
                                    num6 -= dictionary[current];
                                    flag3 = true;
                                }
                            }
                            if (!flag3 && dictionary.ContainsKey(item.netID))
                            {
                                num6 -= dictionary[item.netID];
                            }
                            if (num6 > 0)
                            {
                                flag2 = false;
                                break;
                            }
                        }
                    }
                    if (flag2)
                    {
                        bool flag4 = !Main.recipe[num4].needWater || Main.player[Main.myPlayer].adjWater || Main.player[Main.myPlayer].adjTile[172];
                        bool flag5 = !Main.recipe[num4].needHoney || Main.recipe[num4].needHoney == Main.player[Main.myPlayer].adjHoney;
                        bool flag6 = !Main.recipe[num4].needLava || Main.recipe[num4].needLava == Main.player[Main.myPlayer].adjLava;
                        bool flag7 = !Main.recipe[num4].needSnowBiome || Main.player[Main.myPlayer].ZoneSnow;
                        if (!flag4 || !flag5 || !flag6 || !flag7)
                        {
                            flag2 = false;
                        }
                    }
                    if (flag2 && RecipeHooks.RecipeAvailable(Main.recipe[num4]))
                    {
                        Main.availableRecipe[Main.numAvailableRecipes] = num4;
                        Main.numAvailableRecipes++;
                    }
                    num4++;
                }
            }
            for (int n = 0; n < Main.numAvailableRecipes; n++)
            {
                if (num == Main.availableRecipe[n])
                {
                    Main.focusRecipe = n;
                    break;
                }
            }
            if (Main.focusRecipe >= Main.numAvailableRecipes)
            {
                Main.focusRecipe = Main.numAvailableRecipes - 1;
            }
            if (Main.focusRecipe < 0)
            {
                Main.focusRecipe = 0;
            }
            float num7 = Main.availableRecipeY[Main.focusRecipe] - num2;
            for (int num8 = 0; num8 < Recipe.maxRecipes; num8++)
            {
                Main.availableRecipeY[num8] -= num7;
            }

        }
    }
}
