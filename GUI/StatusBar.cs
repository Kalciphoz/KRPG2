using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using KRPG2.GFX;

namespace KRPG2.GUI
{
    internal class StatusBar : BaseGUI
    {
        public override bool Active => !Main.playerInventory && !Player.ghost;

        private readonly Vector2 bar_life_origin = new Vector2(278f, 50f);
        private const int bar_life_length = 302;
        private const int bar_life_thickness = 28;

        private readonly Vector2 bar_mana_origin = new Vector2(254f, 92f);
        private const int bar_mana_length = 286;
        private const int bar_mana_thickness = 18;

        private readonly Vector2 bar_xp_origin = new Vector2(370f, 122f);
        private const int bar_xp_length = 138;
        private const int bar_xp_thickness = 6;

        private readonly Vector2 bubbles_origin = new Vector2(284f, 134f);
        private const int bubbles_length = 132;
        private const int bubbles_thickness = 22;

        private Vector2 Origin => new Vector2(4f, 6f) * Scale;

        private Texture2D Frame => GetTexture("Frame");
        private Texture2D Background => GetTexture("Background");
        private Texture2D Bars => GetTexture("Bars");
        private Texture2D Bubbles => GetTexture("Bubbles");
        private Texture2D BubblesLava => GetTexture("Bubbles_Lava");

        private Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI(krpg2, "StatusBar/" + texture);

        protected override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = Origin;

            spriteBatch.Draw(Background, position, Scale);

            DrawBar(spriteBatch, bar_life_origin, bar_life_length, bar_life_thickness, (decimal)Player.statLife / Player.statLifeMax2);
            DrawBar(spriteBatch, bar_mana_origin, bar_mana_length, bar_mana_thickness, (decimal)Player.statMana / Player.statManaMax2);
            DrawBar(spriteBatch, bar_xp_origin, bar_xp_length, bar_xp_thickness, (decimal)Character.XP / Character.XPToLevel());

            spriteBatch.Draw(Frame, position, Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statLife} / {Player.statLifeMax2}", position + new Vector2(bar_life_origin.X * Scale + 24f * Scale, (bar_life_origin.Y + 4f) * Scale), Color.White, Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statMana} / {Player.statManaMax2}", position + new Vector2(bar_mana_origin.X * Scale + 24f * Scale, bar_mana_origin.Y * Scale), Color.White, 0.8f * Scale);

            DrawBubbles(spriteBatch);
        }

        private void DrawBar(SpriteBatch spriteBatch, Vector2 origin, int length, int thickness, decimal fullness)
        {
            int drawLength = (int)Math.Round(fullness * length);
            Rectangle sourceRectangle = new Rectangle((int)origin.X + length - drawLength, (int)origin.Y, drawLength, thickness);
            spriteBatch.Draw(Bars, Origin + origin, sourceRectangle, Scale);
        }

        private void DrawBubbles(SpriteBatch spriteBatch)
        {
            if (Player.lavaTime < Player.lavaMax)
            {
                int currentBubbles = (int)Math.Round((decimal)bubbles_length * Player.lavaTime / Player.lavaMax);
                spriteBatch.Draw(Bubbles, Origin + bubbles_origin * Scale, new Rectangle(0, 0, currentBubbles, bubbles_thickness), Scale);
            }
            if (Player.breath < Player.breathMax)
            {
                int currentBubbles = (int)Math.Round((decimal)bubbles_length * Player.breath / Player.breathMax);
                spriteBatch.Draw(BubblesLava, Origin + bubbles_origin * Scale, new Rectangle(0, 0, currentBubbles, bubbles_thickness), Scale);
            }
        }
    }
}
