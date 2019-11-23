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
using KRPG2.RPG.Stats;

namespace KRPG2.GUI.Buttons
{
    internal abstract class StatAllocationButton : Button
    {
        private const int
            frameTime = 5,
            frameCount = 8;

        public readonly AlignmentStat stat;
        public int Allocated { get; private set; } = 0;

        private readonly StatAllocationGUI gui;

        protected sealed override bool CanClick => gui.TotalAllocated < Character.UnspentStatPoints;
        protected sealed override bool CanRightClick => Allocated > 0;

        private int animationCounter = 0;

        private int FrameHeight => Texture.Height / frameCount;

        internal StatAllocationButton(StatAllocationGUI gui, AlignmentStat stat, Vector2 position) : base(position)
        {
            this.gui = gui;
            this.stat = stat;
        }

        internal void Allocate()
        {
            Character.AlignmentStats[stat.GetType()].BaseAmount += Allocated;
            Reset();
        }

        internal void Reset()
        {
            Allocated = 0;
        }

        protected sealed override void Draw(SpriteBatch spriteBatch)
        {
            if (animationCounter > frameCount * frameTime - 1)
                animationCounter = 0;
            int currentFrame = (int)Math.Floor((double)animationCounter / frameTime);
            int sourcePosY = FrameHeight * currentFrame;
            spriteBatch.Draw(Texture, Position, new Rectangle(0, sourcePosY, Texture.Width, FrameHeight), Color.White);
            animationCounter += 1;

            string displayedAmount = (Allocated + stat.BaseAmount).ToString();
            float width = Main.fontItemStack.MeasureString(displayedAmount).X;
            spriteBatch.DrawStringWithShadow(Main.fontItemStack, displayedAmount, Position + new Vector2(28f - width / 2, 36f) * Scale, Allocated > 0 ? Color.Lime : Color.White);

            if (Hover)
            {
                Vector2 position = new Vector2(Main.screenWidth / 2f - 96f, Main.screenHeight / 2f + 128f);
                spriteBatch.DrawStringWithShadow(Main.fontMouseText, stat.DisplayName, position, stat.StatColor);
                spriteBatch.DrawStringWithShadow(Main.fontMouseText, EffectSummary, position + new Vector2(-32f, 24f), Color.White);
            }
        }

        protected abstract string EffectSummary { get; }

        protected sealed override void Click()
        {
            Allocated += 1;
        }

        protected sealed override void RightClick()
        {
            Allocated -= 1;
        }
    }
}
