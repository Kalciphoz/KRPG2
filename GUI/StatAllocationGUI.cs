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
using KRPG2.GUI.Buttons;
using KRPG2.RPG.Stats;

namespace KRPG2.GUI
{
    internal class StatAllocationGUI : BaseGUI
    {
        private Vector2 Origin => new Vector2((Main.screenWidth - DeerSkull.Width) / 2f, 50f * Scale + 50f);

        private Texture2D DeerSkull => GetTexture("DeerSkull");

        private Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI(krpg2, texture);

        public StatAllocationGUI() : base()
        {
            AddButton(new StoicismFlame(this, (Stoicism)Character.AlignmentStats[typeof(Stoicism)], Origin + new Vector2(52f * Scale, -40f * Scale)));
            AddButton(new AcuityFlame(this, (Acuity)Character.AlignmentStats[typeof(Acuity)], Origin + new Vector2(172f * Scale, 0f)));
            AddButton(new MightFlame(this, (Might)Character.AlignmentStats[typeof(Might)], Origin + new Vector2(292f * Scale, -40f * Scale)));

            float buttonX = Main.screenWidth / 2f - 92f * Scale;
            AddButton(new ConfirmAllocationButton(this, new Vector2(buttonX, Main.screenHeight / 2f + 256f * Scale)));
            AddButton(new CancelAllocationButton(this, new Vector2(buttonX, Main.screenHeight / 2f + 320f * Scale)));
        }

        internal void OpenGUI()
        {
            foreach (StatAllocationButton button in buttons)
                button.Reset();

            Active = true;
        }

        public int TotalAllocated
        {
            get
            {
                int total = 0;
                foreach (StatAllocationButton button in buttons)
                    total += button.Allocated;

                return total;
            }
        }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DeerSkull, Origin, Scale);

            int remaining = Character.UnspentStatPoints;

            foreach (StatAllocationButton button in buttons)
                remaining -= button.Allocated;

            string remainingPointsText = "You have " + (remaining == 0 ? "no" : remaining.ToString()) + (remaining == 1 ? " point " : " points ") + "remaining";
            float textWidth = Main.fontMouseText.MeasureString(remainingPointsText).X * Scale;
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, remainingPointsText, Origin - new Vector2(textWidth / 2f - 200f, 38f * Scale + 38f), Color.White, Scale);

            if (buttons.Any(b => b is StatAllocationButton && b.Hover))
            {
                StatAllocationButton hoverButton = (StatAllocationButton)buttons.First(b => b.Hover);
                spriteBatch.Draw(GetTexture($"DeerSkull_Eyes_{hoverButton.stat.UnlocalizedName}"), Origin, Scale);
            }
        }

        internal void Confirm()
        {
            foreach (StatAllocationButton button in buttons)
                button.Allocate();

            Active = false;
        }

        internal void Cancel()
        {
            foreach (StatAllocationButton button in buttons)
                button.Reset();
            
            Active = false;
        }
    }
}
