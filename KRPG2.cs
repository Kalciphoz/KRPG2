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

namespace KRPG2
{
	public class KRPG2 : Mod
	{
        public Assembly assemblyTypes;

		public KRPG2()
        {
            assemblyTypes = Assembly.GetExecutingAssembly();
        }
    }
}