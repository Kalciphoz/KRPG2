namespace KRPG2.Players.Stats
{
    public class LifeRegen : MinorStat
    {
        protected override float Default => 0.5f;

        public LifeRegen() : base("LifeRegen")
        {
        }
    }
}
