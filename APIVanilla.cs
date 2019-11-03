using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Terraria.UI.Gamepad;
using Terraria.UI;
using Terraria.UI.Chat;
using System.Reflection;

namespace KRPG2
{
    public static partial class API
    {
        static FieldInfo inventoryGlowTime = typeof(ItemSlot).GetField("inventoryGlowTime", BindingFlags.NonPublic | BindingFlags.Static);
        static FieldInfo inventoryGlowHue = typeof(ItemSlot).GetField("inventoryGlowHue", BindingFlags.NonPublic | BindingFlags.Static);
        static FieldInfo inventoryGlowTimeChest = typeof(ItemSlot).GetField("inventoryGlowTimeChest", BindingFlags.NonPublic | BindingFlags.Static);
        static FieldInfo inventoryGlowHueChest = typeof(ItemSlot).GetField("inventoryGlowHueChest", BindingFlags.NonPublic | BindingFlags.Static);

        public static void ItemSlotDrawExtension(SpriteBatch spriteBatch, Item[] inv, int context, int slot, Vector2 position, Color overrideColor, Color lightColor = default, bool drawSelected = true)
        {
            Player player = Main.player[Main.myPlayer];
            Item item = inv[slot];
            float inventoryScale = Main.inventoryScale;
            Color color = Color.White;
            if (lightColor != Color.Transparent)
            {
                color = lightColor;
            }
            int num = -1;
            bool flag = false;
            int num2 = 0;
            if (PlayerInput.UsingGamepadUI)
            {
                switch (context)
                {
                    case 0:
                    case 1:
                    case 2:
                        num = slot;
                        break;
                    case 3:
                    case 4:
                        num = 400 + slot;
                        break;
                    case 5:
                        num = 303;
                        break;
                    case 6:
                        num = 300;
                        break;
                    case 7:
                        num = 1500;
                        break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        num = 100 + slot;
                        break;
                    case 12:
                        if (inv == player.dye)
                        {
                            num = 120 + slot;
                        }
                        if (inv == player.miscDyes)
                        {
                            num = 185 + slot;
                        }
                        break;
                    case 15:
                        num = 2700 + slot;
                        break;
                    case 16:
                        num = 184;
                        break;
                    case 17:
                        num = 183;
                        break;
                    case 18:
                        num = 182;
                        break;
                    case 19:
                        num = 180;
                        break;
                    case 20:
                        num = 181;
                        break;
                    case 22:
                        if (UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig != -1)
                        {
                            num = 700 + UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig;
                        }
                        if (UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall != -1)
                        {
                            num = 1500 + UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall + 1;
                        }
                        break;
                }
                flag = (UILinkPointNavigator.CurrentPoint == num);
                if (context == 0)
                {
                    int drawMode = player.DpadRadial.GetDrawMode(slot);
                    num2 = drawMode;
                    if (num2 > 0 && !PlayerInput.CurrentProfile.UsingDpadHotbar())
                    {
                        num2 = 0;
                    }
                }
            }
            Texture2D texture2D = Main.inventoryBackTexture;
            Color color2 = Main.inventoryBack;
            bool flag2 = false;
            if (item.type > 0 && item.stack > 0 && item.favorited && context != 13 && context != 21 && context != 22 && context != 14)
            {
                texture2D = Main.inventoryBack10Texture;
            }
            else if (item.type > 0 && item.stack > 0 && ItemSlot.Options.HighlightNewItems && item.newAndShiny && context != 13 && context != 21 && context != 14 && context != 22)
            {
                texture2D = Main.inventoryBack15Texture;
                float num3 = (float)Main.mouseTextColor / 255f;
                num3 = num3 * 0.2f + 0.8f;
                color2 = color2.MultiplyRGBA(new Color(num3, num3, num3));
            }
            else if (PlayerInput.UsingGamepadUI && item.type > 0 && item.stack > 0 && num2 != 0 && context != 13 && context != 21 && context != 22)
            {
                texture2D = Main.inventoryBack15Texture;
                float num4 = (float)Main.mouseTextColor / 255f;
                num4 = num4 * 0.2f + 0.8f;
                if (num2 == 1)
                {
                    color2 = color2.MultiplyRGBA(new Color(num4, num4 / 2f, num4 / 2f));
                }
                else
                {
                    color2 = color2.MultiplyRGBA(new Color(num4 / 2f, num4, num4 / 2f));
                }
            }
            else if (context == 0 && slot < 10)
            {
                texture2D = Main.inventoryBack9Texture;
            }
            else if (context == 10 || context == 8 || context == 16 || context == 17 || context == 19 || context == 18 || context == 20)
            {
                texture2D = Main.inventoryBack3Texture;
            }
            else if (context == 11 || context == 9)
            {
                texture2D = Main.inventoryBack8Texture;
            }
            else if (context == 12)
            {
                texture2D = Main.inventoryBack12Texture;
            }
            else if (context == 3)
            {
                texture2D = Main.inventoryBack5Texture;
            }
            else if (context == 4)
            {
                texture2D = Main.inventoryBack2Texture;
            }
            else if (context == 7 || context == 5)
            {
                texture2D = Main.inventoryBack4Texture;
            }
            else if (context == 6)
            {
                texture2D = Main.inventoryBack7Texture;
            }
            else if (context == 13)
            {
                byte b = 200;
                if (slot == Main.player[Main.myPlayer].selectedItem)
                {
                    texture2D = Main.inventoryBack14Texture;
                    b = 255;
                }
                color2 = new Color((int)b, (int)b, (int)b, (int)b);
            }
            else if (context == 14 || context == 21)
            {
                flag2 = true;
            }
            else if (context == 15)
            {
                texture2D = Main.inventoryBack6Texture;
            }
            else if (context == 22)
            {
                texture2D = Main.inventoryBack4Texture;
            }
            if (context == 0 && ((int[])inventoryGlowTime.GetValue(null))[slot] > 0 && !inv[slot].favorited)
            {
                float scale = Main.invAlpha / 255f;
                Color value = new Color(63, 65, 151, 255) * scale;
                Color value2 = Main.hslToRgb(((float[])inventoryGlowHue.GetValue(null))[slot], 1f, 0.5f) * scale;
                float num5 = (float)((int[])inventoryGlowTime.GetValue(null))[slot] / 300f;
                num5 *= num5;
                color2 = Color.Lerp(value, value2, num5 / 2f);
                texture2D = Main.inventoryBack13Texture;
            }
            if ((context == 4 || context == 3) && ((int[])inventoryGlowTimeChest.GetValue(null))[slot] > 0 && !inv[slot].favorited)
            {
                float scale2 = Main.invAlpha / 255f;
                Color value3 = new Color(130, 62, 102, 255) * scale2;
                if (context == 3)
                {
                    value3 = new Color(104, 52, 52, 255) * scale2;
                }
                Color value4 = Main.hslToRgb(((float[])inventoryGlowHue.GetValue(null))[slot], 1f, 0.5f) * scale2;
                float num6 = (float)((int[])inventoryGlowTimeChest.GetValue(null))[slot] / 300f;
                num6 *= num6;
                color2 = Color.Lerp(value3, value4, num6 / 2f);
                texture2D = Main.inventoryBack13Texture;
            }
            if (flag)
            {
                texture2D = Main.inventoryBack14Texture;
                color2 = Color.White;
            }
            if (!flag2)
            {
                spriteBatch.Draw(slot == Main.player[Main.myPlayer].selectedItem && drawSelected ? Main.inventoryBack14Texture : texture2D, position, null, overrideColor, 0f, default, inventoryScale, SpriteEffects.None, 0f);
            }
            int num7 = -1;
            switch (context)
            {
                case 8:
                    if (slot == 0)
                    {
                        num7 = 0;
                    }
                    if (slot == 1)
                    {
                        num7 = 6;
                    }
                    if (slot == 2)
                    {
                        num7 = 12;
                    }
                    break;
                case 9:
                    if (slot == 10)
                    {
                        num7 = 3;
                    }
                    if (slot == 11)
                    {
                        num7 = 9;
                    }
                    if (slot == 12)
                    {
                        num7 = 15;
                    }
                    break;
                case 10:
                    num7 = 11;
                    break;
                case 11:
                    num7 = 2;
                    break;
                case 12:
                    num7 = 1;
                    break;
                case 16:
                    num7 = 4;
                    break;
                case 17:
                    num7 = 13;
                    break;
                case 18:
                    num7 = 7;
                    break;
                case 19:
                    num7 = 10;
                    break;
                case 20:
                    num7 = 17;
                    break;
            }
            if ((item.type <= 0 || item.stack <= 0) && num7 != -1)
            {
                Texture2D texture2D2 = Main.extraTexture[54];
                Rectangle rectangle = texture2D2.Frame(3, 6, num7 % 3, num7 / 3);
                rectangle.Width -= 2;
                rectangle.Height -= 2;
                spriteBatch.Draw(texture2D2, position + texture2D.Size() / 2f * inventoryScale, new Rectangle?(rectangle), Color.White * 0.35f, 0f, rectangle.Size() / 2f, inventoryScale, SpriteEffects.None, 0f);
            }
            Vector2 vector = texture2D.Size() * inventoryScale;
            if (item.type > 0 && item.stack > 0)
            {
                Texture2D texture2D3 = /*item.modItem is ProceduralItem ? ((ProceduralItem)item.modItem).texture :*/ Main.itemTexture[item.type];
                Rectangle rectangle2;
                if (Main.itemAnimations[item.type] != null)
                {
                    rectangle2 = Main.itemAnimations[item.type].GetFrame(texture2D3);
                }
                else
                {
                    rectangle2 = texture2D3.Frame(1, 1, 0, 0);
                }
                Color newColor = color;
                float num8 = 1f;
                ItemSlot.GetItemLight(ref newColor, ref num8, item, false);
                float num9 = 1f;
                if (rectangle2.Width > 32 || rectangle2.Height > 32)
                {
                    if (rectangle2.Width > rectangle2.Height)
                    {
                        num9 = 32f / (float)rectangle2.Width;
                    }
                    else
                    {
                        num9 = 32f / (float)rectangle2.Height;
                    }
                }
                num9 *= inventoryScale;
                Vector2 position2 = position + vector / 2f - rectangle2.Size() * num9 / 2f;
                Vector2 origin = rectangle2.Size() * (num8 / 2f - 0.5f);
                if (ItemLoader.PreDrawInInventory(item, spriteBatch, position2, rectangle2, item.GetAlpha(newColor), item.GetColor(color), origin, num9 * num8))
                {
                    spriteBatch.Draw(texture2D3, position2, new Rectangle?(rectangle2), item.GetAlpha(newColor), 0f, origin, num9 * num8, SpriteEffects.None, 0f);
                    if (item.color != Color.Transparent)
                    {
                        spriteBatch.Draw(texture2D3, position2, new Rectangle?(rectangle2), item.GetColor(color), 0f, origin, num9 * num8, SpriteEffects.None, 0f);
                    }
                }
                ItemLoader.PostDrawInInventory(item, spriteBatch, position2, rectangle2, item.GetAlpha(newColor), item.GetColor(color), origin, num9 * num8);
                if (ItemID.Sets.TrapSigned[item.type])
                {
                    spriteBatch.Draw(Main.wireTexture, position + new Vector2(40f, 40f) * inventoryScale, new Rectangle?(new Rectangle(4, 58, 8, 8)), color, 0f, new Vector2(4f), 1f, SpriteEffects.None, 0f);
                }
                if (item.stack > 1)
                {
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, item.stack.ToString(), position + new Vector2(10f, 26f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
                }
                int num10 = -1;
                if (context == 13)
                {
                    if (item.DD2Summon)
                    {
                        for (int i = 0; i < 58; i++)
                        {
                            if (inv[i].type == 3822)
                            {
                                num10 += inv[i].stack;
                            }
                        }
                        if (num10 >= 0)
                        {
                            num10++;
                        }
                    }
                    if (item.useAmmo > 0)
                    {
                        int useAmmo = item.useAmmo;
                        num10 = 0;
                        for (int j = 0; j < 58; j++)
                        {
                            if (inv[j].ammo == useAmmo)
                            {
                                num10 += inv[j].stack;
                            }
                        }
                    }
                    if (item.fishingPole > 0)
                    {
                        num10 = 0;
                        for (int k = 0; k < 58; k++)
                        {
                            if (inv[k].bait > 0)
                            {
                                num10 += inv[k].stack;
                            }
                        }
                    }
                    if (item.tileWand > 0)
                    {
                        int tileWand = item.tileWand;
                        num10 = 0;
                        for (int l = 0; l < 58; l++)
                        {
                            if (inv[l].type == tileWand)
                            {
                                num10 += inv[l].stack;
                            }
                        }
                    }
                    if (item.type == 509 || item.type == 851 || item.type == 850 || item.type == 3612 || item.type == 3625 || item.type == 3611)
                    {
                        num10 = 0;
                        for (int m = 0; m < 58; m++)
                        {
                            if (inv[m].type == 530)
                            {
                                num10 += inv[m].stack;
                            }
                        }
                    }
                }
                if (num10 != -1)
                {
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, num10.ToString(), position + new Vector2(8f, 30f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale * 0.8f), -1f, inventoryScale);
                }
                if (context == 13)
                {
                    string text = string.Concat(slot + 1);
                    if (text == "10")
                    {
                        text = "0";
                    }
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, text, position + new Vector2(8f, 4f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
                }
                if (context == 13 && item.potion)
                {
                    Vector2 position3 = position + texture2D.Size() * inventoryScale / 2f - Main.cdTexture.Size() * inventoryScale / 2f;
                    Color color3 = item.GetAlpha(color) * ((float)player.potionDelay / (float)player.potionDelayTime);
                    spriteBatch.Draw(Main.cdTexture, position3, null, color3, 0f, default, num9, SpriteEffects.None, 0f);
                }
                if ((context == 10 || context == 18) && item.expertOnly && !Main.expertMode)
                {
                    Vector2 position4 = position + texture2D.Size() * inventoryScale / 2f - Main.cdTexture.Size() * inventoryScale / 2f;
                    Color white = Color.White;
                    spriteBatch.Draw(Main.cdTexture, position4, null, white, 0f, default, num9, SpriteEffects.None, 0f);
                }
            }
            else if (context == 6)
            {
                Texture2D trashTexture = Main.trashTexture;
                Vector2 position5 = position + texture2D.Size() * inventoryScale / 2f - trashTexture.Size() * inventoryScale / 2f;
                spriteBatch.Draw(trashTexture, position5, null, new Color(100, 100, 100, 100), 0f, default, inventoryScale, SpriteEffects.None, 0f);
            }
            if (context == 0 && slot < 10)
            {
                float num11 = inventoryScale;
                string text2 = string.Concat(slot + 1);
                if (text2 == "10")
                {
                    text2 = "0";
                }
                Color inventoryBack = Main.inventoryBack;
                int num12 = 0;
                if (Main.player[Main.myPlayer].selectedItem == slot)
                {
                    num12 -= 3;
                    inventoryBack.R = 255;
                    inventoryBack.B = 0;
                    inventoryBack.G = 210;
                    inventoryBack.A = 100;
                    num11 *= 1.4f;
                }
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, text2, position + new Vector2(6f, (float)(4 + num12)) * inventoryScale, inventoryBack, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
            }
            if (num != -1)
            {
                UILinkPointNavigator.SetPosition(num, position + vector * 0.75f);
            }
        }

        public static void APIQuickHeal(this Player player)
        {
            if (player.noItems)
            {
                return;
            }
            if (player.statLife == player.statLifeMax2 || player.potionDelay > 0)
            {
                return;
            }
            Item item = player.APIQuickHeal_GetItemToUse();
            if (item == null)
            {
                return;
            }
            Main.PlaySound(item.UseSound, player.position);
            if (item.potion)
            {
                if (item.type == 227)
                {
                    player.potionDelay = player.restorationDelayTime;
                    player.AddBuff(21, player.potionDelay, true);
                }
                else
                {
                    player.potionDelay = player.potionDelayTime;
                    player.AddBuff(21, player.potionDelay, true);
                }
            }
            ItemLoader.UseItem(item, player);
            player.statLife += item.healLife;
            player.statMana += item.healMana;
            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }
            if (player.statMana > player.statManaMax2)
            {
                player.statMana = player.statManaMax2;
            }
            if (item.healLife > 0 && Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(item.healLife, true);
            }
            if (item.healMana > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    player.ManaEffect(item.healMana);
                }
            }
            if (ItemLoader.ConsumeItem(item, player))
            {
                item.stack--;
            }
            if (item.stack <= 0)
            {
                item.TurnToAir();
            }
            API.FindRecipes();
        }

        public static Item APIQuickHeal_GetItemToUse(this Player player)
        {
            int num = player.statLifeMax2 - player.statLife;
            Item result = null;
            int num2 = -player.statLifeMax2;
            for (int i = 0; i < 58; i++)
            {
                Item item = player.inventory[i];
                if (item.stack > 0 && item.type > 0 && item.potion && item.healLife > 0 && ItemLoader.CanUseItem(item, player))
                {
                    int num3 = item.healLife - num;
                    if (num2 < 0)
                    {
                        if (num3 > num2)
                        {
                            result = item;
                            num2 = num3;
                        }
                    }
                    else if (num3 < num2 && num3 >= 0)
                    {
                        result = item;
                        num2 = num3;
                    }
                }
            }

            /*K2Player k2player = player.GetModPlayer<K2Player>();
            for (int i = 0; i < k2player.inventories.Length; i += 1)
                if (k2player.activeInvPage != i)
                    for (int j = 0; j < k2player.inventories[i].Length; j += 1)
                    {
                        Item item = k2player.inventories[i][j];
                        if (item.stack > 0 && item.type > 0 && item.potion && item.healLife > 0 && ItemLoader.CanUseItem(item, player))
                        {
                            int num3 = item.healLife - num;
                            if (num2 < 0)
                            {
                                if (num3 > num2)
                                {
                                    result = item;
                                    num2 = num3;
                                }
                            }
                            else if (num3 < num2 && num3 >= 0)
                            {
                                result = item;
                                num2 = num3;
                            }
                        }
                    }*/

            return result;
        }

    }
}
