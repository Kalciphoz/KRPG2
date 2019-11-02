﻿using System;
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

namespace KRPG2.GUI
{
    internal class StatusBar : BaseGUI
    {
        private RPGCharacter Character => k2player.character;

        public override bool Active => true;

        private Vector2 Origin => new Vector2(4f, 6f) * Scale;

        private Texture2D StatusFrame => GFX.GetGUI(krpg2, "StatusBar/StatusFrame");

        internal StatusBar(K2Player k2player) : base(k2player)
        {
        }

        protected override void Update()
        {
        }

        protected override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = Origin;

            spriteBatch.Draw(StatusFrame, position, Scale);
        }
    }
}