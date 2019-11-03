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
using KRPG2.GFX;

namespace KRPG2.GUI
{
    internal partial class Inventory : BaseGUI
    {
        public override bool Active => Main.playerInventory;

        private const int bar_length = 192;
        private float bar_x => 314f * Scale;

        public Vector2 Origin => new Vector2(40f, 8f) * Scale;

        private Texture2D Frame => GetTexture("Frame");
        private Texture2D Panel => GetTexture("Panel");
        private Texture2D Separator => GetTexture("Separator");
        private Texture2D BarCovers => GetTexture("BarCovers");
        private Texture2D Life => GetTexture("Life");
        private Texture2D Mana => GetTexture("Mana");
        private Texture2D XP => GetTexture("XP");

        private Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI(krpg2, "Inventory/" + texture);

        public Inventory(K2Player k2player) : base(k2player) { }

        protected override void Update() { }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            Vanilla(true);
            spriteBatch.Draw(Frame, new Vector2((int)Origin.X, (int)Origin.Y), Scale);
            spriteBatch.Draw(Separator, new Vector2(Origin.X + 56 * Scale, Origin.Y + 354 * Scale), Scale);
            DrawHotbar(spriteBatch);

            int currentLength = (int)Math.Round((decimal)Player.statLife / Player.statLifeMax2 * bar_length);
            spriteBatch.Draw(Life, Origin + new Vector2(bar_x, 70 * Scale), new Rectangle(0, 0, currentLength, 20), Scale);
            currentLength = (int)Math.Round((decimal)Player.statMana / Player.statManaMax2 * bar_length);
            spriteBatch.Draw(Mana, Origin + new Vector2(bar_x, 98 * Scale), new Rectangle(0, 0, currentLength, 16), Scale);
            currentLength = (int)Math.Round((decimal)Character.XP / Character.XPToLevel() * bar_length);
            spriteBatch.Draw(XP, Origin + new Vector2(bar_x, 126 * Scale), new Rectangle(0, 0, currentLength, 8), Scale);
            spriteBatch.Draw(BarCovers, Origin + new Vector2(302, 68) * Scale, Scale);

            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statLife} / {Player.statLifeMax2}", Origin + new Vector2(bar_x + 16f * Scale, 72f * Scale), Color.White, 0.8f * Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statMana} / {Player.statManaMax2}", Origin + new Vector2(bar_x + 16f * Scale, 100f * Scale), Color.White, 0.6f * Scale);
        }
    }
}
