﻿using KRPG2.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader.IO;
using WebmilioCommons.Managers;

namespace KRPG2.RPG.Stats
{
    public abstract class Stat : IHasUnlocalizedName
    {
        protected const string
            SAVE_KEY_AMOUNT = "Amount";

        protected Stat(string unlocalizedName)
        {
            UnlocalizedName = unlocalizedName;
        }

        public void Update(RPGCharacter character) => Update(character.Player, character.K2Player, character);

        internal abstract void Draw(SpriteBatch spriteBatch, Vector2 position, float scale);

        internal abstract void ResetBonus();

        protected virtual void Update(Player player, K2Player k2player, RPGCharacter character) { }

        public abstract TagCompound Save();

        public abstract void Load(TagCompound tag);

        public string UnlocalizedName { get; }

        public virtual bool DoSave => true;

        public virtual bool DoDraw => true;

        protected abstract string StatPageLine { get; }

        protected virtual Color StatPageLineColor => Color.White;
    }
}
