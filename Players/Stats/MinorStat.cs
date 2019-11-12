using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace KRPG2.Players.Stats
{
    public abstract class MinorStat : Stat
    {
        public virtual bool IntegersOnly => false;

        private float _baseAmount;
        public float BaseAmount
        {
            get => _baseAmount;
            set
            {
                if (IntegersOnly && value != Math.Floor(value))
                    throw new Exception("This stat uses integer values only!");
                else
                    _baseAmount = value;
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
        public override bool DoSave => false;

        protected MinorStat(string unlocalizedName) : base(unlocalizedName)
        {
            BaseAmount = Default;
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
