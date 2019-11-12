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
    public abstract class Stat
    {
        protected const string
            SAVE_KEY_AMOUNT = "amount";

        public void Update(RPGCharacter character) => Update(character.Player, character.K2Player, character);

        internal abstract void ResetBonus();

        public virtual void Update(Player player, K2Player k2player, RPGCharacter character) { }

        public virtual bool DoSave => true;

        public abstract TagCompound Save();

        public abstract void Load(TagCompound tag);
    }
}
