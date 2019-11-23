using KRPG2.RPG.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KRPG2.GUI.Buttons
{
    internal class MightFlame : StatAllocationButton
    {
        protected override Texture2D Texture => GetTexture("Flames_Might");

        protected override string EffectSummary => "Increases your damage, mana, and mana regen";

        internal MightFlame(StatAllocationGUI gui, Might stat, Vector2 position) : base(gui, stat, position) { }
    }
}
