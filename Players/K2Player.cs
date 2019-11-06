using System.Collections.Generic;
using KRPG2.GUI;
using KRPG2.Inventory;
using KRPG2.Net;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons.Extensions;

namespace KRPG2.Players
{
    public sealed partial class K2Player : ModPlayer
    {
        public const int 
            TOOLBAR_SIZE = 10,
            ACTUAL_INVENTORY_SIZE = Main.realInventory - TOOLBAR_SIZE; // There are 10 slots in the hotbar.

        private GUIHandler _guiHandler;

        public readonly RPGCharacter character;
        public InventoryHandler Inventory { get; private set; }

        public bool Initialized { get; private set; }


        public static K2Player Get() => Get(Main.LocalPlayer);
        public static K2Player Get(Player player) => player.GetModPlayer<K2Player>();


        public static List<Player> GetActivePlayers()
        {
            List<Player> list = new List<Player>();

            for (int i = 0; i < Main.player.Length; i += 1)
            {
                Player player = Main.player[i];

                if (player != null && player.active)
                    list.Add(player);
            }

            return list;
        }

        public K2Player() : base()
        {
            character = new RPGCharacter(this);
        }

        public void Init()
        {
            if (!Main.dedServ && player.whoAmI == Main.myPlayer)
                _guiHandler = new GUIHandler();

            Inventory = new InventoryHandler(this);

            Initialized = true;
        }

        public override void PostUpdate()
        {
            if (!Initialized) Init();

            for (int i = 0; i < ACTUAL_INVENTORY_SIZE; i += 1)
                Inventory.Page[Inventory.ActivePage].item[i] = player.inventory[i + 10];

            if (Main.playerInventory && Main.mapTime % 60 == 0)
                API.FindRecipes();
        }

        public override void OnEnterWorld(Terraria.Player player)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                this.SendIfLocal(new ServerJoinSyncInventoryPages());
        }
    }
}
