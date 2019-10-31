using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace KRPG2.Net
{
    public class K2Networking
    {
        private static Action<BinaryWriter, int> idWriter;
        private static Func<BinaryReader, int> idReader;

        private static Dictionary<Type, int> messageHandlerID;
        private static K2Message[] handlers;

        public static void HandlePacket(KRPG2 krpg2, BinaryReader reader, int sender)
        {
            int messageID = idReader(reader);

            if (messageID < 0 || messageID >= handlers.Length)
            {
                krpg2.LogError("MessageID out of bounds");
                return;
            }

            var handler = handlers[messageID];

            if (Main.netMode == NetmodeID.Server && !handler.CanBeSentByClients)
            {
                NetMessage.BootPlayer(sender, NetworkText.FromLiteral($"Your client has sent packet '{handler.GetType().Name}', which can only be sent by the server."));
                return;
            }

            if (handler.IsLogged) krpg2.Log($"Received message '{handler.GetType().Name}' from {(sender == 256 ? "server" : sender.ToString())}");

            try
            {
                handler.ReadPacket(reader, sender);
            }
            catch (Exception e)
            {
                krpg2.Log($"An error has occured while parsing message '{handler.GetType().Name}'.");
                krpg2.LogError(e);
            }
        }

        public static void SendPacket<T>(KRPG2 krpg2, T message, int toClient = -1, int ignoreClient = -1) where T : K2Message
        {
            try
            {
                SendPacket(krpg2, messageHandlerID[typeof(T)], w => message.WritePacket(w, out _), message.packetSize, toClient, ignoreClient);
            }
            catch (Exception e)
            {
                krpg2.Log($"An error has occured while sending message '{message.GetType().Name}'.");
                krpg2.LogError(e);
            }
        }

        public static void SendPacket(KRPG2 krpg2, int id, Action<BinaryWriter> writer, int capacity = 256, int toClient = -1, int ignoreClient = -1, Func<Player, bool> doSend = null)
        {
            ModPacket packet = krpg2.GetPacket(capacity);

            idWriter(packet, id);

            writer(packet);

            try
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    packet.Send();
                else if (toClient != -1)
                    packet.Send(toClient, ignoreClient);
                else
                {
                    foreach (Player player in K2Player.GetActivePlayers())
                    {
                        int i = player.whoAmI;

                        if (i != ignoreClient && Netplay.Clients[i].State >= 10 && (doSend == null || doSend(player)))
                            packet.Send();
                    }
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
