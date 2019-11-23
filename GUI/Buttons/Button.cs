using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KRPG2.GFX;
using KRPG2.Players;
using KRPG2.RPG;

namespace KRPG2.GUI.Buttons
{
    public abstract class Button
    {
        public virtual bool Toggled => toggled;
        public bool Hover => Texture.Bounds.Contains(Main.MouseScreen - Position);

        protected readonly KRPG2 krpg2;
        protected Player Player => Main.LocalPlayer;
        protected K2Player K2Player => K2Player.Get();
        protected RPGCharacter Character => K2Player.Character;

        protected static float Scale => BaseGUI.Scale;

        protected abstract Texture2D Texture { get; }
        protected virtual Texture2D Texture_Pressed => null;
        protected virtual Texture2D Texture_Disabled => null;

        protected virtual Vector2 Position => position;
        private readonly Vector2 position;

        protected virtual bool Toggleable => false;
        protected virtual bool Enabled => true;
        protected virtual bool CanClick => Enabled;
        protected virtual bool CanRightClick => false;

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
            if (Hover)
            {
                Player.mouseInterface = true;
                if (Main.mouseLeft && CanClick)
                {
                    if (Toggleable || Texture_Pressed == null ? !oldMouseLeft : Main.mouseLeftRelease)
                    {
                        Main.PlaySound(SoundID.MenuTick);
                        Click();
                    }
                }
                else if (Main.mouseRight && CanRightClick && Main.mouseRightRelease)
                {
                    Main.PlaySound(SoundID.MenuTick);
                    RightClick();
                }
            }

            oldMouseLeft = Main.mouseLeft;
        }

        protected virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Enabled && Texture_Disabled != null)
                spriteBatch.Draw(Texture_Disabled, position, Scale);
            else if (Toggled && Texture_Pressed != null || Hover && Main.mouseLeft)
                spriteBatch.Draw(Texture_Pressed, position, Scale);
            else
                spriteBatch.Draw(Texture, position, Scale);
        }

        protected virtual void RightClick() { }

        protected abstract void Click();
    }
}
