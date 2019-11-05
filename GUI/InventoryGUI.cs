using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using KRPG2.GFX;
using KRPG2.GUI.Buttons;

namespace KRPG2.GUI
{
    internal partial class InventoryGUI : BaseGUI
    {
        public override bool Active => Main.playerInventory;

        private const int barLength = 192;
        private float BarX => 314f * Scale;

        public Vector2 Origin => new Vector2(40f, 8f) * Scale;

        private Texture2D Frame => GetTexture("Frame");
        private Texture2D Panel => GetTexture("Panel");
        private Texture2D Separator => GetTexture("Separator");
        private Texture2D BarCovers => GetTexture("BarCovers");
        private Texture2D Life => GetTexture("Life");
        private Texture2D Mana => GetTexture("Mana");
        private Texture2D XP => GetTexture("XP");

        private Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI(krpg2, "Inventory/" + texture);

        public InventoryGUI() : base()
        {
            AddButton(new InvPageButton(Origin + new Vector2(174f, 102f), 0));
            AddButton(new InvPageButton(Origin + new Vector2(206f, 102f), 1));
            AddButton(new InvPageButton(Origin + new Vector2(238f, 102f), 2));
        }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            Vanilla(!K2Player.inventory.statPage);
            if (K2Player.inventory.statPage) DrawStatPage(spriteBatch);
            spriteBatch.Draw(Frame, new Vector2((int)Origin.X, (int)Origin.Y), Scale);
            spriteBatch.Draw(Separator, new Vector2(Origin.X + 56 * Scale, Origin.Y + 354 * Scale), Scale);
            DrawHotbar(spriteBatch);

            int currentLength = (int)Math.Round((decimal)Player.statLife / Player.statLifeMax2 * barLength);
            spriteBatch.Draw(Life, Origin + new Vector2(BarX, 70 * Scale), new Rectangle(0, 0, currentLength, 20), Scale);
            currentLength = (int)Math.Round((decimal)Player.statMana / Player.statManaMax2 * barLength);
            spriteBatch.Draw(Mana, Origin + new Vector2(BarX, 98 * Scale), new Rectangle(0, 0, currentLength, 16), Scale);
            currentLength = (int)Math.Round((decimal)Character.XP / Character.XPToLevel() * barLength);
            spriteBatch.Draw(XP, Origin + new Vector2(BarX, 126 * Scale), new Rectangle(0, 0, currentLength, 8), Scale);
            spriteBatch.Draw(BarCovers, Origin + new Vector2(302, 68) * Scale, Scale);

            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statLife} / {Player.statLifeMax2}", Origin + new Vector2(BarX + 16f * Scale, 72f * Scale), Color.White, 0.8f * Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statMana} / {Player.statManaMax2}", Origin + new Vector2(BarX + 16f * Scale, 100f * Scale), Color.White, 0.6f * Scale);
        }

        private void DrawStatPage(SpriteBatch spriteBatch)
        {
            var panel_origin = Origin + new Vector2(56, 146) * Scale;
            spriteBatch.Draw(Panel, panel_origin, Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, (Character.XPToLevel() - Character.XP).ToString() + " XP to level", panel_origin + new Vector2(24f, 24f) * Scale, Color.White, 0.8f * Scale);
        }
    }
}
