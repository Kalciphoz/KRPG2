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
using Terraria.UI;
using Terraria.Localization;
using System.Reflection;
using KRPG2.GFX;

namespace KRPG2.GUI
{
    internal class Hotbar : BaseGUI
    {
        public override bool Active => !Main.playerInventory && !Player.ghost;

        private static Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI((KRPG2)ModLoader.GetMod("KRPG2"), "Inventory/" + texture);

        internal Hotbar(K2Player k2player) : base(k2player) { }

        public static void ReplaceTextures()
        {
            var slot = GetTexture("Slot");
            Main.inventoryBackTexture = slot;
            Main.inventoryBack2Texture = slot;
            Main.inventoryBack3Texture = slot;
            Main.inventoryBack4Texture = slot;
            Main.inventoryBack5Texture = slot;
            Main.inventoryBack6Texture = slot;
            Main.inventoryBack7Texture = slot;
            Main.inventoryBack8Texture = slot;
            Main.inventoryBack9Texture = slot;
            Main.inventoryBack10Texture = GetTexture("Slot_Favorite");
            Main.inventoryBack11Texture = slot;
            Main.inventoryBack12Texture = slot;
            Main.inventoryBack13Texture = slot;
            Main.inventoryBack14Texture = GetTexture("Slot_Selected");
            Main.inventoryBack15Texture = slot;
            Main.inventoryBack16Texture = slot;
        }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            string text = "";
            if (Player.inventory[Player.selectedItem].Name != null && Player.inventory[Player.selectedItem].Name != "")
            {
                text = Player.inventory[Player.selectedItem].AffixName();
            }
            Vector2 vector = Main.fontMouseText.MeasureString(text) / 2;
            Main.spriteBatch.DrawStringWithShadow(Main.fontMouseText, text, new Vector2(Main.screenWidth - 240 - vector.X - 16f, 0f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 1f);
            int pos_X = Main.screenWidth - 480;
            for (int i = 0; i < 10; i++)
            {
                if (i == Player.selectedItem)
                {
                    if (Main.hotbarScale[i] < 1f)
                    {
                        Main.hotbarScale[i] += 0.05f;
                    }
                }
                else if ((double)Main.hotbarScale[i] > 0.75)
                {
                    Main.hotbarScale[i] -= 0.05f;
                }
                float num2 = Main.hotbarScale[i];
                int num3 = (int)(20f + 22f * (1f - num2));
                int a = (int)(75f + 150f * num2);
                Color lightColor = new Color(255, 255, 255, a);
                if (!Player.hbLocked && !PlayerInput.IgnoreMouseInterface && Main.mouseX >= pos_X && (float)Main.mouseX <= (float)pos_X + (float)Main.inventoryBackTexture.Width * Main.hotbarScale[i] && Main.mouseY >= num3 && (float)Main.mouseY <= (float)num3 + (float)Main.inventoryBackTexture.Height * Main.hotbarScale[i] && !Main.player[Main.myPlayer].channel)
                {
                    Player.mouseInterface = true;
                    Player.showItemIcon = false;
                    if (Main.mouseLeft && !Player.hbLocked && !Main.blockMouse)
                    {
                        Player.changeItem = i;
                    }
                    Main.hoverItemName = Player.inventory[i].AffixName();
                    if (Player.inventory[i].stack > 1)
                    {
                        object obj = Main.hoverItemName;
                        Main.hoverItemName = string.Concat(new object[]
                        {
                    obj,
                    " (",
                    Player.inventory[i].stack,
                    ")"
                        });
                    }
                    Main.rare = Player.inventory[i].rare;
                }
                float num4 = Main.inventoryScale;
                Main.inventoryScale = num2;
                ItemSlot.Draw(Main.spriteBatch, Player.inventory, 13, i, new Vector2((float)pos_X, (float)num3), Color.White);
                Main.inventoryScale = num4;
                pos_X += (int)((float)Main.inventoryBackTexture.Width * Main.hotbarScale[i]) + 4;
            }
            int selectedItem = Player.selectedItem;
            if (selectedItem >= 10 && (selectedItem != 58 || Main.mouseItem.type > 0))
            {
                float num5 = 1f;
                int num6 = (int)(20f + 22f * (1f - num5));
                int a2 = (int)(75f + 150f * num5);
                Color lightColor2 = new Color(255, 255, 255, a2);
                float num7 = Main.inventoryScale;
                Main.inventoryScale = num5;
                ItemSlot.Draw(Main.spriteBatch, Player.inventory, 13, selectedItem, new Vector2((float)pos_X, (float)num6), Color.White);
                Main.inventoryScale = num7;
            }
        }
    }
}
