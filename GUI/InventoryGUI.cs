using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using KRPG2.GFX;
using KRPG2.GUI.Buttons;
using KRPG2.RPG.Stats;

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
            AddButton(new StatPageButton(Origin + new Vector2(142f, 102f)));
            AddButton(new InvPageButton(Origin + new Vector2(174f, 102f), 0));
            AddButton(new InvPageButton(Origin + new Vector2(206f, 102f), 1));
            AddButton(new InvPageButton(Origin + new Vector2(238f, 102f), 2));
        }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            Vanilla(!K2Player.Inventory.statPage);
            if (K2Player.Inventory.statPage) DrawStatPage(spriteBatch);
            spriteBatch.Draw(Frame, new Vector2((int)Origin.X, (int)Origin.Y), Scale);
            spriteBatch.Draw(Separator, new Vector2(Origin.X + 56 * Scale, Origin.Y + 354 * Scale), Scale);
            DrawHotbar(spriteBatch);
            
            StatusBar.DrawNumerals(spriteBatch, Character, new Vector2(132f, 60f) * Scale);

            int currentLength = (int)Math.Round((decimal)Player.statLife / Player.statLifeMax2 * barLength);
            spriteBatch.Draw(Life, Origin + new Vector2(BarX, 70 * Scale), new Rectangle(0, 0, currentLength, 20), Scale);
            currentLength = (int)Math.Round((decimal)Player.statMana / Player.statManaMax2 * barLength);
            spriteBatch.Draw(Mana, Origin + new Vector2(BarX, 98 * Scale), new Rectangle(0, 0, currentLength, 16), Scale);
            if (Character.XPToLevel() > 0)
            {
                currentLength = (int)Math.Round((decimal)Character.XP / Character.XPToLevel() * barLength);
                spriteBatch.Draw(XP, Origin + new Vector2(BarX, 126 * Scale), new Rectangle(0, 0, currentLength, 8), Scale);
            }
            spriteBatch.Draw(BarCovers, Origin + new Vector2(302, 68) * Scale, Scale);

            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statLife} / {Player.statLifeMax2}", Origin + new Vector2(BarX + 16f * Scale, 72f * Scale), Color.White, 0.8f * Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statMana} / {Player.statManaMax2}", Origin + new Vector2(BarX + 16f * Scale, 100f * Scale), Color.White, 0.6f * Scale);
        }

        private void DrawStatPage(SpriteBatch spriteBatch)
        {
            var panel_origin = Origin + new Vector2(56, 146) * Scale;
            spriteBatch.Draw(Panel, panel_origin, Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, (Character.XPToLevel() - Character.XP).ToString() + " XP to level", panel_origin + new Vector2(24f, 24f) * Scale, Color.White, 0.8f * Scale);
            var lines_origin = panel_origin + new Vector2(24f, 40f);
            int pos = 0;
            foreach (Stat stat in Character.AlignmentStats.Values.Where(s => s.DoDraw))
            {
                stat.Draw(spriteBatch, lines_origin + new Vector2(0f, pos * 18f) * Scale, 0.8f * Scale);
                pos += 1;
            }
            foreach (Stat stat in Character.MinorStats.Values.Where(s => s.DoDraw && s.DisplayColumn == Stat.DISPLAY_COLUMN_OFFENSIVE))
            {
                stat.Draw(spriteBatch, lines_origin + new Vector2(0f, pos * 18f) * Scale, 0.8f * Scale);
                pos += 1;
            }
            pos = 0;
            foreach (Stat stat in Character.MinorStats.Values.Where(s => s.DoDraw && s.DisplayColumn == Stat.DISPLAY_COLUMN_DEFENSIVE))
            {
                stat.Draw(spriteBatch, lines_origin + new Vector2(160f, pos * 18f) * Scale, 0.8f * Scale);
                pos += 1;
            }
            pos = 0;
            foreach (Stat stat in Character.MinorStats.Values.Where(s => s.DoDraw && s.DisplayColumn == Stat.DISPLAY_COLUMN_MISCELLANEOUS))
            {
                stat.Draw(spriteBatch, lines_origin + new Vector2(320f, pos * 18f) * Scale, 0.8f * Scale);
                pos += 1;
            }
        }
    }
}
