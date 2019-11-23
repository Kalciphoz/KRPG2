using KRPG2.Net.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader.IO;
using WebmilioCommons.Extensions;

namespace KRPG2.RPG.Stats
{
    public abstract class MinorStat : Stat
    {
        protected virtual bool IntegersOnly => false;
        public override bool DoSave => false;

        private float _baseAmount;
        public float BaseAmount
        {
            get => _baseAmount;
            set
            {
                if (_baseAmount == value) return;

                if (IntegersOnly && value != Math.Floor(value))
                    throw new Exception("This stat uses integer values only!");
                else
                {
                    _baseAmount = value;
                    K2Player.SendIfLocal(new SyncMinorStatPacket(this));
                }
            }
        }
        private float _bonusAmount = 0f;
        public float BonusAmount
        {
            get => _bonusAmount;
            set
            {
                if (IntegersOnly && value != Math.Floor(value))
                    throw new Exception("This stat uses integer values only!");
                else
                    _bonusAmount = value;
            }
        }
        public float Amount => BaseAmount + BonusAmount;
        protected virtual float Default => 0f;

        protected virtual bool Percentage => true;

        protected MinorStat(RPGCharacter character, string unlocalizedName) : base(character, unlocalizedName)
        {
            BaseAmount = Default;
        }

        protected virtual string ValueDisplay
        {
            get
            {
                int rounded = (int)Math.Round(Amount * 100);
                return Percentage ? rounded.ToString() + "%" : (rounded / 100.0).ToString();
            }
        }

        internal sealed override void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
        {
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, DisplayName, position, StatColor, scale);
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, ValueDisplay, position + new Vector2(120f, 0f) * scale, Color.White, scale);
        }

        internal sealed override void ResetBonus()
        {
            BonusAmount = 0f;
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
            BaseAmount = tag.GetFloat(SAVE_KEY_AMOUNT);
        }
    }
}
