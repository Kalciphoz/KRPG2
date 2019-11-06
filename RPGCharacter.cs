using KRPG2.Players;
using Terraria;

namespace KRPG2
{
    public class RPGCharacter
    {
        public readonly K2Player k2player;
        public Player Player => k2player.player;

        public int Level { get; private set; } = 1;
        public long XP { get; private set; } = 0;

        public RPGCharacter(K2Player k2player)
        {
            this.k2player = k2player;
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
                return 5000;
            else if (Level < 30)
                return lv * 100 - 800;
            else if (Level == 30)
                return 12000;
            else if (Level < 40)
                return lv * 200 - 4200;
            else if (Level == 40)
                return 30000;
            else if (Level < 50)
                return lv * 400 - 12000;
            else
                return -1;
        }
    }
}
