using KRPG2.GFX;
using KRPG2.Net;
using KRPG2.Players;
using KRPG2.Players.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using WebmilioCommons.Extensions;
using WebmilioCommons.Networking.Packets;

namespace KRPG2.Players
{
    public sealed partial class RPGCharacter
    {
        public K2Player K2Player => K2Player.Get(Player);
        public Player Player { get; private set; }

        public long XP { get; set; } = 0;
        private int _level = 1;

        public Dictionary<Type, AlignmentStat> alignmentStats;
        public Dictionary<Type, MinorStat> minorStats;

        private int levelAnimation = 60;

        private Texture2D LevelUpAnimation => GraphicsHandler.GetGFX(KRPG2.Instance, "LevelUp");
        private SoundEffect bling;
        private SoundEffect LevelUpBling
        {
            get
            {
                if (bling == null)
                    bling = KRPG2.Instance.GetSound("SFX/LevelUp");
                return bling;
            }
        }

        public RPGCharacter(Player player)
        {
            Player = player;
            alignmentStats = new Dictionary<Type, AlignmentStat>()
            {
                { typeof(Stoicism), new Stoicism() }
            };
            minorStats = new Dictionary<Type, MinorStat>()
            {
                { typeof(LifeRegen), new LifeRegen() }
            };
        }

        public void UpdateStats()
        {
            foreach (AlignmentStat stat in alignmentStats.Values)
            {
                stat.Update(this);
            }

            Player.statLifeMax2 += Level * 5;
            float lifeMultiplier = 1f + (Player.statLifeMax - 100f) / 400f;
            Player.statLifeMax2 = (int)Math.Round(Player.statLifeMax2 * lifeMultiplier);

            foreach (MinorStat stat in minorStats.Values)
            {
                stat.Update(this);
            }
        }

        public void GainXP(int amount, bool first = true)
        {
            if (XPToLevel() == -1)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient && first)
                K2Player.SendIfLocal(new GainXP(amount));

            XP += amount;
            if (XP >= XPToLevel())
            {
                XP -= XPToLevel();
                LevelUp();
                GainXP(0, false);
            }
            if (first) CombatText.NewText(Player.getRect(), new Color(127, 159, 255), amount + " XP");
        }

        public void LevelUp(int level = -1)
        {
            if (level == -1)
                Level += 1;
            else if (Level < level)
                Level = level;
            else
                throw new Exception("LevelUp(int) cannot be used to downgrade a character");

            if (!Main.gameMenu) LevelUpBling.Play(0.5f * Main.soundVolume, 0f, 0f);
            levelAnimation = 0;
        }

        public void DrawLevelAnimation(ref bool fullBright)
        {
            if (levelAnimation < 60)
            {
                if (levelAnimation < 24)
                {
                    fullBright = true;
                    Lighting.AddLight(Player.position, 0.9f, 0.9f, 0.9f);
                }
                else Lighting.AddLight(Player.position, 0.4f, 0.4f, 0.4f);
                Main.spriteBatch.Draw(LevelUpAnimation, Player.Bottom - new Vector2(48, 108) - Main.screenPosition, new Rectangle(0, levelAnimation / 3 * 96, 96, 96), Color.White);
                levelAnimation += 1;
            }
        }

        public long XPToLevel()
        {
            long lv = Level;

            if (Level <= 5)
                return lv * 20;
            else if (Level < 10)
                return lv * 40 - 100;
            else if (Level == 10)
                return 2000;
            else if (Level == 20)
                return -1;
            else if (Level < 30)
                return lv * 100 - 800;
            else if (Level == 30)
                return 12000;
            else if (Level < 40)
                return lv * 200 - 4200;
            else if (Level == 40)
                return -1;
            else if (Level < 50)
                return lv * 400 - 12000;
            else
                return -1;
        }
        
        public int Level
        {
            get => _level;
            set
            {
                if (_level == value) return;

                _level = value;
                if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
                    K2Player.SendIfLocal(new SyncLevel());
            }
        }
    }
}
