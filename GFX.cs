using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KRPG2
{
    public static class GFX
    {
        private const string PATH_GUI = "GFX/GUI/";

        private static Dictionary<string, Texture2D> texturesByPath = new Dictionary<string, Texture2D>();

        public static Texture2D GetGUI(KRPG2 krpg2, string filename)
        {
            return GetTexture(krpg2, PATH_GUI + filename);
        }

        private static Texture2D GetTexture(KRPG2 krpg2, string path)
        {
            if (Main.netMode == NetmodeID.Server)
                throw new Exception("Server attempted to retrieve texture.");

            if (!texturesByPath.ContainsKey(path))
            {
                Texture2D texture;
                try
                {
                    texture = krpg2.GetTexture(path);
                }
                catch (Exception e)
                {
                    krpg2.LogError($"Could not retrieve texture '{path}'. " + e.ToString());
                    throw e;
                }
                texturesByPath.Add(path, texture);
            }
            return texturesByPath[path];
        }
    }
}
