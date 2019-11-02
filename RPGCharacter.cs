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

namespace KRPG2
{
    public class RPGCharacter
    {
        public int level { get; private set; } = 1;
        private long XP = 0;

        public long ExperienceToLevel()
        {
            var lv = (long)level;
            if (level <= 5)
                return lv * 20;
            else if (level < 10)
                return lv * 40 - 100;
            else if (level == 10)
                return 2000;
            else if (level == 20)
                return 5000;
            else if (level < 30)
                return lv * 100 - 800;
            else if (level == 30)
                return 12000;
            else if (level < 40)
                return lv * 200 - 4200;
            else if (level == 40)
                return 30000;
            else if (level < 50)
                return lv * 400 - 12000;
            else
                return -1;
        }
    }
}
