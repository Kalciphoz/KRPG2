using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KRPG2.GUI.Buttons;
using KRPG2.Players;

namespace KRPG2.GUI
{
    public abstract class BaseGUI
    {
        public static float Scale => Main.UIScale * Math.Min(1f, Main.screenWidth / 1920f);

        public virtual bool Active { get; protected set; } = false;

        private static readonly List<BaseGUI> _guiElements = new List<BaseGUI>();

        protected readonly KRPG2 krpg2;
        protected Player Player => Main.LocalPlayer;
        protected K2Player K2Player => Player.GetModPlayer<K2Player>();
        protected RPGCharacter Character => K2Player.character;

        protected virtual bool DoDraw => Active;

        protected List<Button> buttons = new List<Button>();

        protected BaseGUI()
        {
            this.krpg2 = (KRPG2)ModLoader.GetMod("KRPG2");
            _guiElements.Add(this);
        }

        protected void AddButton(Button button)
        {
            buttons.Add(button);
        }

        public static List<BaseGUI> GetGUIElements()
        {
            return _guiElements.ToList();
        }

        public static void UpdateGUIElements()
        {
            foreach (BaseGUI gui in _guiElements)
                if (gui.Active)
                    gui.Update();
        }

        public static void DrawGUIElements(SpriteBatch spriteBatch)
        {
            if (Main.netMode == NetmodeID.Server)
                throw new Exception("Server attempted to draw GUI elements");
            
            foreach (BaseGUI gui in _guiElements)
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
