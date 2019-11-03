using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
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

        private Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI(krpg2, "StatusBar/" + texture);

        internal StatusBar(K2Player k2player) : base(k2player) { }

        protected override void Update() { }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = Origin;

            spriteBatch.Draw(Background, position, Scale);

            int currentLength = (int)Math.Round((decimal)Player.statLife / Player.statLifeMax2 * bar_life_length);
            Rectangle sourceRectangle = new Rectangle((int)bar_life_origin.X + bar_life_length - currentLength, (int)bar_life_origin.Y, currentLength, bar_life_thickness);
            spriteBatch.Draw(Bars, position + bar_life_origin * Scale, sourceRectangle, Scale);

            currentLength = (int)Math.Round((decimal)Player.statMana / Player.statManaMax2 * bar_mana_length);
            sourceRectangle = new Rectangle((int)bar_mana_origin.X + bar_mana_length - currentLength, (int)bar_mana_origin.Y, currentLength, bar_mana_thickness);
            spriteBatch.Draw(Bars, position + bar_mana_origin * Scale, sourceRectangle, Scale);

            currentLength = (int)Math.Round((decimal)Character.XP / Character.XPToLevel() * bar_xp_length);
            sourceRectangle = new Rectangle((int)bar_xp_origin.X + bar_xp_length - currentLength, (int)bar_xp_origin.Y, currentLength, bar_xp_thickness);
            spriteBatch.Draw(Bars, position + bar_xp_origin * Scale, sourceRectangle, Scale);

            spriteBatch.Draw(Frame, position, Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statLife} / {Player.statLifeMax2}", position + new Vector2(bar_life_origin.X * Scale + 24f * Scale, (bar_life_origin.Y + 4f) * Scale), Color.White, Scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, $"{Player.statMana} / {Player.statManaMax2}", position + new Vector2(bar_mana_origin.X * Scale + 24f * Scale, bar_mana_origin.Y * Scale), Color.White, 0.8f * Scale);
        }
    }
}
