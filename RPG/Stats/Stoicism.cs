using KRPG2.Players;
using Microsoft.Xna.Framework;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Stoicism : AlignmentStat
    {
        public Stoicism(RPGCharacter character) : base(character, "Stoicism") { }

        public override string DisplayName => "Stoicism";
        public override Color StatColor => new Color(223, 0, 0);

        public override void Update()
        {
            Player.statLifeMax2 += 115 + Amount * 10 - Player.statLifeMax;
            Player.statDefense += Amount;
            character.MinorStats[typeof(LifeRegen)].BonusAmount += 0.3f * Amount;
        }
    }
}
