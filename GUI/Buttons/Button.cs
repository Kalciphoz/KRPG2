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
using KRPG2.GFX;

namespace KRPG2.GUI.Buttons
{
    public abstract class Button
    {
        public virtual bool Toggled => toggled;

        protected readonly KRPG2 krpg2;
        protected Player Player => Main.LocalPlayer;
        protected K2Player K2Player => Player.GetModPlayer<K2Player>();
        protected RPGCharacter Character => K2Player.character;

        protected static float Scale => BaseGUI.Scale;

        protected abstract Texture2D Texture { get; }
        protected virtual Texture2D Texture_Pressed => null;
        protected virtual Texture2D Texture_Disabled => null;

        protected virtual Vector2 Position => position;
        private readonly Vector2 position;

        protected virtual bool Toggleable => false;
        protected virtual bool Enabled => true;

        protected bool toggled = false;

        private bool oldMouseLeft = false;

        protected virtual Texture2D GetTexture(string texture) => GraphicsHandler.GetGUI(krpg2, "Buttons/" + texture);

        public Button(Vector2 position)
        {
            krpg2 = (KRPG2)ModLoader.GetMod("KRPG2");
            this.position = position;
        }

        public void Update(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch);
            if (Texture.Bounds.Contains(Main.MouseScreen - Position))
            {
                Player.mouseInterface = true;
                if (Main.mouseLeft && Enabled)
                    if (Toggleable ? !oldMouseLeft : Main.mouseLeftRelease)
                    {
                        Main.PlaySound(SoundID.MenuTick);
                        Click();
                    }
            }

            oldMouseLeft = Main.mouseLeft;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Enabled && Texture_Disabled != null)
                spriteBatch.Draw(Texture_Disabled, position, Scale);
            else if (Toggled && Texture_Pressed != null)
                spriteBatch.Draw(Texture_Pressed, position, Scale);
            else
                spriteBatch.Draw(Texture, position, Scale);
        }

        public abstract void Click();
    }
}
