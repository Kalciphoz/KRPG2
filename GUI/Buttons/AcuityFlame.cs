using KRPG2.RPG.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KRPG2.GUI.Buttons
{
    internal class AcuityFlame : StatAllocationButton
    {
        protected override Texture2D Texture => GetTexture("Flames_Acuity");

        protected override string EffectSummary => "Increases your movement speed, evasion, and crit multiplier";

        internal AcuityFlame(StatAllocationGUI gui, Acuity stat, Vector2 position) : base(gui, stat, position) { }
    }
}
