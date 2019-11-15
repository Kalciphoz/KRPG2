using Terraria;

namespace KRPG2.NPCs.Prefixes
{
    public class ExampleNPCPrefix : NPCPrefix
    {
        public ExampleNPCPrefix() : base(1, "Example")
        {
        }

        public override bool PreAI(NPC npc)
        {
            npc.life = 1;

            return base.PreAI(npc);
        }
    }
}