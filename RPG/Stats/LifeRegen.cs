namespace KRPG2.RPG.Stats
{
    public class LifeRegen : MinorStat
    {
        protected override float Default => 0.5f;

        public override int StatPageColumn => STATPAGE_COLUMN_DEFENSIVE;

        public override string DisplayName => "Life regen";

        public LifeRegen(RPGCharacter character) : base(character, "LifeRegen") { }
    }
}
