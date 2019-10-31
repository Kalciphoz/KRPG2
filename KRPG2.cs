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

namespace KRPG2
{
	public class KRPG2 : Mod
	{
        public static Type[] assemblyTypes;

		public KRPG2()
        {
            assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            K2Networking.Init();
        }
    }
}