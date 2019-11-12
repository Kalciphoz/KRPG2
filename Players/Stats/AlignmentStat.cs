using Terraria.ModLoader.IO;

namespace KRPG2.Players.Stats
{
    public abstract class AlignmentStat : Stat
    {
        public int baseAmount = 0;
        public int bonusAmount = 0;

        public int Amount => baseAmount + bonusAmount;

        protected AlignmentStat(string unlocalizedName) : base(unlocalizedName)
        {
        }

        internal sealed override void ResetBonus()
        {
            bonusAmount = 0;
        }

        public sealed override TagCompound Save()
        {
            if (!DoSave) return new TagCompound();
            TagCompound tag = new TagCompound
            {
                {SAVE_KEY_AMOUNT, baseAmount}
            };

            return tag;
        }

        public sealed override void Load(TagCompound tag)
        {
            baseAmount = tag.GetInt(SAVE_KEY_AMOUNT);
        }
    }
}
