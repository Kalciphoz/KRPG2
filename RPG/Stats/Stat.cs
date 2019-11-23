using KRPG2.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader.IO;
using WebmilioCommons.Managers;

namespace KRPG2.RPG.Stats
{
    public abstract class Stat : IHasUnlocalizedName
    {
        public const int
            STATPAGE_COLUMN_NONE = 0,
            STATPAGE_COLUMN_OFFENSIVE = 1,
            STATPAGE_COLUMN_DEFENSIVE = 2,
            STATPAGE_COLUMN_MISCELLANEOUS = 3;

        protected const string
            SAVE_KEY_AMOUNT = "Amount";

        protected RPGCharacter character;
        protected K2Player K2Player => character.K2Player;
        protected Player Player => K2Player.player;

        protected Stat(RPGCharacter character, string unlocalizedName)
        {
            this.character = character;
            UnlocalizedName = unlocalizedName;
        }

        internal abstract void Draw(SpriteBatch spriteBatch, Vector2 position, float scale);

        internal abstract void ResetBonus();

        public virtual void Update() { }

        public abstract TagCompound Save();

        public abstract void Load(TagCompound tag);

        public string UnlocalizedName { get; }

        public virtual bool DoSave => true;

        public virtual bool DoDraw => StatPageColumn != STATPAGE_COLUMN_NONE;

        public abstract int StatPageColumn { get; }

        public abstract string DisplayName { get; }

        public virtual Color StatColor => Color.White;
    }
}
