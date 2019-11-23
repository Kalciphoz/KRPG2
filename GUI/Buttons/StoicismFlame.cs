using KRPG2.RPG.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KRPG2.GUI.Buttons
{
    internal class StoicismFlame : StatAllocationButton
    {
        protected override Texture2D Texture => GetTexture("Flames_Stoicism");

        internal StoicismFlame (StatAllocationGUI gui, Stoicism stat, Vector2 position) : base(gui, stat, position) { }
    }
}
