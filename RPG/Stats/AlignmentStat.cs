using Terraria.ModLoader.IO;

namespace KRPG2.RPG.Stats
{
    public abstract class AlignmentStat : Stat
    {
        protected AlignmentStat(string unlocalizedName) : base(unlocalizedName)
        {
        }

        internal sealed override void ResetBonus()
        {
            BonusAmount = 0;
        }

        public sealed override TagCompound Save()
        {
            if (!DoSave) return new TagCompound();
            TagCompound tag = new TagCompound
            {
                {SAVE_KEY_AMOUNT, BaseAmount}
            };

            return tag;
        }

        public sealed override void Load(TagCompound tag)
        {
            BaseAmount = tag.GetInt(SAVE_KEY_AMOUNT);
        }

        public int BaseAmount { get; set; }
        public int BonusAmount { get; set; }

        public int Amount => BaseAmount + BonusAmount;
    }
}
