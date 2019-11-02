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
        private static List<BaseGUI> gui_elements = new List<BaseGUI>();
        public bool Active { get; protected set; } = false;

        protected readonly K2Player k2player;
        protected Player Player => k2player.player;
        protected readonly KRPG2 krpg2;

        public BaseGUI(K2Player k2player)
        {
            this.k2player = k2player;
            this.krpg2 = (KRPG2)k2player.mod;
            gui_elements.Add(this);
        }

        public static List<BaseGUI> GetGUIElements()
        {
            return gui_elements.ToList();
        }

        protected abstract void Draw(SpriteBatch spriteBatch);

        protected float Scale => Main.UIScale * Math.Min(1f, Main.screenWidth / 1920 * 0.9f);
    }
}
