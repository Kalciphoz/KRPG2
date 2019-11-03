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
using KRPG2.GUI.Buttons;

namespace KRPG2.GUI
{
    public abstract class BaseGUI
    {
        public static float Scale => Main.UIScale * Math.Min(1f, Main.screenWidth / 1920f);

        public virtual bool Active { get; protected set; } = false;

        private static List<BaseGUI> gui_elements = new List<BaseGUI>();

        protected readonly KRPG2 krpg2;
        protected Player Player => Main.LocalPlayer;
        protected K2Player K2Player => Player.GetModPlayer<K2Player>();
        protected RPGCharacter Character => K2Player.character;

        protected virtual bool DoDraw => Active;

        protected List<Button> buttons = new List<Button>();

        public BaseGUI()
        {
            this.krpg2 = (KRPG2)ModLoader.GetMod("KRPG2");
            gui_elements.Add(this);
        }

        protected void AddButton(Button button)
        {
            buttons.Add(button);
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
                {
                    gui.Draw(spriteBatch);
                    foreach (Button button in gui.buttons)
                        button.Update(spriteBatch);
                }
        }

        protected virtual void Update() { }

        protected abstract void Draw(SpriteBatch spriteBatch);
    }
}
