using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace KRPG2.Config
{
    public sealed class K2ClientConfiguration : ModConfig
    {
        [Label("Display Enemies' Levels")]
        [DefaultValue(true)]
        public bool DisplayEnemiesLevels { get; set; }


        public override ConfigScope Mode { get; } = ConfigScope.ClientSide;
    }
}