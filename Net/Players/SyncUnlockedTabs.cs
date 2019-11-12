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
using WebmilioCommons.Networking.Packets;
using KRPG2.Players;

namespace KRPG2.Net.Players
{
    public class SyncUnlockedTabs : ModPlayerNetworkPacket<K2Player>
    {
        public int Unlocked
        {
            get => ModPlayer.Inventory.Unlocked;
            set => ModPlayer.Inventory.Unlocked = value;
        }
    }
}
