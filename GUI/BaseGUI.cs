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
    public class BaseGUI
    {
        private static List<BaseGUI> gui_elements = new List<BaseGUI>();
        public bool active { get; protected set; } = false;

        public static List<BaseGUI> GetGUIElements()
        {
            return gui_elements;
        }
    }
}
