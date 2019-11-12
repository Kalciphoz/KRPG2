using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KRPG2.Net;
using KRPG2.GUI;
using KRPG2.RPG.Leveling;
using Terraria.UI;
using WebmilioCommons.Networking;

namespace KRPG2
{
    public class KRPG2 : Mod
    {
        public KRPG2()
        {
            AssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

            Instance = this;
        }

        public override void Load()
        {
            LevelLocks.Load();
        }

        public override void Unload()
        {
            BaseGUI._guiElements.Clear();

            // Leveling
            LevelLocks.Unload();

            Instance = null;
        }


        public bool DrawInterface()
        {
            if (Main.netMode != NetmodeID.Server)
                BaseGUI.DrawGUIElements(Main.spriteBatch);

            return true;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (Main.netMode == NetmodeID.Server) return;

            layers.Find(layer => layer.Name == "Vanilla: Resource Bars").Active = false;
            layers[layers.FindIndex(layer => layer.Name == "Vanilla: Inventory")] = new LegacyGameInterfaceLayer("kRPG", new GameInterfaceDrawMethod(DrawInterface), InterfaceScaleType.UI);
            layers.Find(layer => layer.Name == "Vanilla: Hotbar").Active = false;
        }

        public override void HandlePacket(BinaryReader reader, int sender) => NetworkPacketLoader.Instance.HandlePacket(reader, sender);
        
        public static Type[] AssemblyTypes { get; private set; }

        public static KRPG2 Instance { get; private set; }
    }
}