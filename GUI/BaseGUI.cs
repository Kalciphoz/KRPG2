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

namespace KRPG2.GUI
{
    public abstract class BaseGUI
    {
        public virtual bool Active { get; protected set; } = false;

        private static List<BaseGUI> gui_elements = new List<BaseGUI>();

        protected readonly K2Player k2player;
        protected readonly KRPG2 krpg2;
        protected Player Player => k2player.player;
        protected RPGCharacter Character => k2player.character;

        protected virtual bool DoDraw => Active;

        protected float Scale => Main.UIScale * Math.Min(1f, Main.screenWidth / 1920f);

        public BaseGUI(K2Player k2player)
        {
            this.k2player = k2player;
            this.krpg2 = (KRPG2)ModLoader.GetMod("KRPG2");
            gui_elements.Add(this);
        }

        public static List<BaseGUI> GetGUIElements()
        {
            return gui_elements.ToList();
        }

        public static void UpdateGUIElements()
        {
            foreach (BaseGUI gui in gui_elements)
                if (gui.Active)
                    gui.Update();
        }

        public static void DrawGUIElements(SpriteBatch spriteBatch)
        {
            foreach (BaseGUI gui in gui_elements)
                if (gui.DoDraw)
                    gui.Draw(spriteBatch);
        }

        protected abstract void Update();

        protected abstract void Draw(SpriteBatch spriteBatch);
    }
}
