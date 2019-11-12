using System;
using System.Collections.Generic;
using KRPG2.GFX;
using KRPG2.Net.Players;
using KRPG2.Players;
using KRPG2.RPG.Leveling;
using KRPG2.RPG.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using WebmilioCommons.Extensions;

namespace KRPG2.RPG
{
    public sealed partial class RPGCharacter
    {
        public K2Player K2Player => K2Player.Get(Player);
        public Player Player { get; private set; }

        public long XP { get; set; }
        private int _level = 1;

        public Dictionary<Type, AlignmentStat> AlignmentStats { get; private set; }
        public Dictionary<Type, MinorStat> MinorStats { get; private set; }

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

            AlignmentStats = new Dictionary<Type, AlignmentStat>
            {
                { typeof(Stoicism), new Stoicism() },
                { typeof(Acuity), new Acuity() },
                { typeof(Might), new Might() }
            };

            MinorStats = new Dictionary<Type, MinorStat>
            {
                { typeof(LifeRegen), new LifeRegen() },
                { typeof(Damage), new Damage() }
            };
        }

        public void UpdateStats()
        {
            foreach (AlignmentStat stat in AlignmentStats.Values)
            {
                stat.Update(this);
            }

            Player.statLifeMax2 += Level * 5;
            float lifeMultiplier = 1f + (Player.statLifeMax - 100f) / 400f;
            Player.statLifeMax2 = (int)Math.Round(Player.statLifeMax2 * lifeMultiplier);

            Player.statManaMax2 += Level;
            float manaMultiplier = 1f + (Player.statManaMax - 20f) / 200f * 1.5f;
            Player.statManaMax2 = (int)Math.Round(Player.statManaMax2 * manaMultiplier);

            foreach (MinorStat stat in MinorStats.Values)
            {
                stat.Update(this);
            }
        }

        public void GainXP(int amount, bool first = true)
        {
            if (XPToLevel() == -1)
                return;

            if (first)
                K2Player.SendIfLocal(new GainXPPacket(amount));

            XP += amount;
            if (XP >= XPToLevel())
            {
                XP -= XPToLevel();
                LevelUp();
                GainXP(0, false);
            }

            if (first) 
                CombatText.NewText(Player.getRect(), new Color(127, 159, 255), amount + " XP");
        }

        public void LevelUp(int level = 1)
        {
            if (level < 1)
                throw new ArgumentException($"{nameof(level)} must be a positive integer.");

            Level += level;

            if (!Main.gameMenu) 
                LevelUpBling.Play(0.5f * Main.soundVolume, 0f, 0f);

            levelAnimation = 0;
        }

        public void SetLevel(int level, bool allowDowngrade = false)
        {
            if (level < Level && !allowDowngrade)
                return;

            Level = level;
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
                else 
                    Lighting.AddLight(Player.position, 0.4f, 0.4f, 0.4f);
                
                Main.spriteBatch.Draw(LevelUpAnimation, Player.Bottom - new Vector2(48, 108) - Main.screenPosition, new Rectangle(0, levelAnimation / 3 * 96, 96, 96), Color.White);
                levelAnimation += 1;
            }
        }

        public long XPToLevel() => XPToLevel(Level);

        public static long XPToLevel(int currentLevel)
        {
            long longLevel = currentLevel;

            if (LevelLocks.Contains(currentLevel))
                return -1;

            if (longLevel <= 5)
                return longLevel * 20;

            if (longLevel < 10)
                return longLevel * 40 - 100;

            if (longLevel == 10)
                return 2000;

            if (longLevel < 30)
                return longLevel * 100 - 800;

            if (longLevel == 30)
                return 12000;

            if (longLevel < 40)
                return longLevel * 200 - 4200;

            if (longLevel < 50)
                return longLevel * 400 - 12000;

            return -1;
        }
        
        public int Level
        {
            get => _level;
            set
            {
                if (_level == value) return;

                _level = value;
                K2Player.SendIfLocal(new SyncLevelPacket());
            }
        }
    }
}
