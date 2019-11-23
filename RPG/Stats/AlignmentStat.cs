using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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

        internal sealed override void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
        {
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, StatPageLine, position, StatPageLineColor, scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, Amount.ToString(), position + new Vector2(120f, 0f) * scale, Color.White, scale);
        }

        public override int DisplayColumn => DISPLAY_COLUMN_OFFENSIVE;

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
