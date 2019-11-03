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
using Terraria.Localization;
using System.Reflection;

namespace KRPG2.GUI
{
    internal class BuffsGUI : BaseGUI
    {
        public override bool Active => !Player.ghost;

        private static MethodInfo DrawBuffIcon = typeof(Main).GetMethod("DrawBuffIcon", BindingFlags.NonPublic | BindingFlags.Static);

        protected override void Draw(SpriteBatch spriteBatch)
        {
            int num = -1;
            int num2 = 11;
            for (int i = 0; i < 22; i++)
            {
                if (Player.buffType[i] > 0)
                {
                    int b = Player.buffType[i];
                    int x = 320 + i * 38;
                    int num3 = 8;
                    if (i >= num2)
                    {
                        x = 32 + (i - num2) * 38;
                        num3 += 50;
                    }
                    num = (int)DrawBuffIcon.Invoke(null, new object[] { num, i, b, x, num3 }); // Main.DrawBuffIcon(num, i, b, x, num3);
                }
                else
                {
                    Main.buffAlpha[i] = 0.4f;
                }
            }
            if (num >= 0)
            {
                int num4 = Player.buffType[num];
                if (num4 > 0)
                {
                    Main.buffString = Lang.GetBuffDescription(num4);
                    int itemRarity = 0;
                    if (num4 == 26 && Main.expertMode)
                    {
                        Main.buffString = Language.GetTextValue("BuffDescription.WellFed_Expert");
                    }
                    if (num4 == 147)
                    {
                        Main.bannerMouseOver = true;
                    }
                    if (num4 == 94)
                    {
                        int num5 = (int)(Player.manaSickReduction * 100f) + 1;
                        Main.buffString = Main.buffString + num5 + "%";
                    }
                    if (Main.meleeBuff[num4])
                    {
                        itemRarity = -10;
                    }
                    BuffLoader.ModifyBuffTip(num4, ref Main.buffString, ref itemRarity);
                    Main.instance.MouseTextHackZoom(Lang.GetBuffName(num4), itemRarity, 0);
                }
            }
        }
    }
}
