using KRPG2.Players;
using Microsoft.Xna.Framework;
using Terraria;

namespace KRPG2.RPG.Stats
{
    public class Might : AlignmentStat
    {
        public Might(RPGCharacter character) : base(character, "Might") { }

        public override string DisplayName => "Might";
        public override Color StatColor => new Color(27, 65, 255);

        public override void Update()
        {
            Player.statManaMax2 += 19 + Amount * 3 - Player.statManaMax;
            character.MinorStats[typeof(Damage)].BonusAmount += 0.08f * Amount;
        }
    }
}
