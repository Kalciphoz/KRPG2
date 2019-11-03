using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using KRPG2.Net;
using KRPG2.GUI;
using Terraria.UI;

namespace KRPG2
{
	public class KRPG2 : Mod
	{
        public static Type[] AssemblyTypes { get; private set; }

		public KRPG2()
        {
            AssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            K2Networking.Init();
        }

        public bool DrawInterface()
        {
            if (Main.netMode != NetmodeID.Server)
                BaseGUI.DrawGUIElements(Main.spriteBatch);

            return true;
        }

        public override void ModifyInterfaceLayers(List<Terraria.UI.GameInterfaceLayer> layers)
        {
            layers.Find(layer => layer.Name == "Vanilla: Resource Bars").Active = false;
            layers[layers.FindIndex(layer => layer.Name == "Vanilla: Inventory")] = new LegacyGameInterfaceLayer("kRPG", new GameInterfaceDrawMethod(DrawInterface), InterfaceScaleType.UI);
            layers.Find(layer => layer.Name == "Vanilla: Hotbar").Active = false;
        }

        public override void HandlePacket(BinaryReader reader, int sender) => K2Networking.HandlePacket(this, reader, sender);
    }
}