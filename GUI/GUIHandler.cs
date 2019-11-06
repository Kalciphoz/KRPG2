using KRPG2.Players;
using Terraria;

namespace KRPG2.GUI
{
    internal class GUIHandler
    {
        private readonly StatusBar statusBar;
        private readonly Hotbar hotbar;
        private readonly InventoryGUI inventory;
        private readonly BuffsGUI buffs;

        private K2Player K2Player => Main.LocalPlayer.GetModPlayer<K2Player>();

        public GUIHandler()
        {
            BaseGUI._guiElements.Clear();

            statusBar = new StatusBar();
            hotbar = new Hotbar();
            inventory = new InventoryGUI();
            buffs = new BuffsGUI();

            Hotbar.ReplaceTextures();
        }
    }
}
