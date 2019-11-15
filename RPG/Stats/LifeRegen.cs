namespace KRPG2.RPG.Stats
{
    public class LifeRegen : MinorStat
    {
        protected override float Default => 0.5f;

        protected override string StatPageLine => "Life regen:";

        public LifeRegen() : base("LifeRegen") { }
    }
}
