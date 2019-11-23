using KRPG2.Net.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader.IO;
using WebmilioCommons.Extensions;

namespace KRPG2.RPG.Stats
{
    public abstract class AlignmentStat : Stat
    {
        protected AlignmentStat(RPGCharacter character, string unlocalizedName) : base(character, unlocalizedName) { }

        internal sealed override void ResetBonus()
        {
            BonusAmount = 0;
        }

        internal sealed override void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
        {
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, DisplayName, position, StatColor, scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, Amount.ToString(), position + new Vector2(120f, 0f) * scale, Color.White, scale);
        }

        public sealed override int StatPageColumn => STATPAGE_COLUMN_OFFENSIVE;

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

        private int _baseAmount;
        public int BaseAmount
        {
            get
            {
                return _baseAmount;
            }
            set
            {
                if (_baseAmount == value) return;

                _baseAmount = value;
                K2Player.SendIfLocal(new SyncAlignmentStatPacket(this));
            }
        }
        public int BonusAmount { get; set; }

        public int Amount => BaseAmount + BonusAmount;
    }
}
