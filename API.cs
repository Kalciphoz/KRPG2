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
using Terraria.UI.Chat;
using ReLogic.Graphics;

namespace KRPG2
{
    public static class API
    {
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float scale)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle sourceRectangle, float scale)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public static void DrawStringWithShadow(this SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color color, float scale = 1f)
        {
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, position, color, 0f, Vector2.Zero, Vector2.One * scale);
        }
    }
}
