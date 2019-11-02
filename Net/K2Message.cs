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
using Terraria.Net;

namespace KRPG2.Net
{
    public abstract class K2Message
    {
        public enum Requirements
        {
            None = 0,

            Player = 1,
            K2Player = 2 | Player,

            PlayerItem = 4,
            PlayerK2Item = 8 | PlayerItem,

            Item = 16,
            K2Item = 32 | Item,

            NPC = 64,
            K2NPC = 128 | NPC,
            
            Projectile = 256,
            K2Projectile = 512 | Projectile
        }

        public readonly int packetSize;

        public int id;

        public abstract bool resendPacket { get; }
        public abstract Requirements reqs { get; }

        public virtual bool isLogged => false;
        public virtual bool canBeSentByClients => true;
        public virtual bool playerIsNotTheSender => false;

        protected readonly KRPG2 krpg2;

        private readonly Action<BinaryWriter> writeAction;

        protected Player player;
        protected K2Player k2player;

        protected Item playerItem;
        protected K2Item playerK2item;

        protected Item item;
        protected K2Item k2item;

        protected NPC npc;
        protected K2NPC k2npc;

        protected Projectile projectile;
        protected K2Projectile k2projectile;

        protected K2Message(KRPG2 krpg2, Action<BinaryWriter> writeAction, Player player = null, NPC npc = null, Item item = null, Projectile projectile = null, int packetSize = 256)
        {
            this.krpg2 = krpg2;
            this.packetSize = packetSize;
            this.writeAction = writeAction;

            this.player = player;
            this.npc = npc;
            this.item = item;
            this.projectile = projectile;
        }

        public void WritePacket(BinaryWriter writer, out int packetHeaderSize, bool skipAction = false)
        {
            packetHeaderSize = 0;

            void Write(bool doWrite, Entity entity, string argName, bool byteId, ref int size)
            {
                if (doWrite)
                {
                    if (entity == null)
                        throw new ArgumentNullException();
                    else if (byteId)
                        writer?.Write((byte)entity.whoAmI);
                    else
                        writer?.Write((ushort)entity.whoAmI);
                }
                else if (entity != null)
                    throw new ArgumentException($"Unexpected argument: '{argName}'.");
            }

            if (playerIsNotTheSender || Main.netMode == NetmodeID.Server)
                Write(reqs.HasFlag(Requirements.Player), player, nameof(player), true, ref packetHeaderSize);

            Write(reqs.HasFlag(Requirements.Item), item, nameof(item), false, ref packetHeaderSize);
            Write(reqs.HasFlag(Requirements.NPC), npc, nameof(npc), false, ref packetHeaderSize);
            Write(reqs.HasFlag(Requirements.Projectile), projectile, nameof(projectile), false, ref packetHeaderSize);

            if (!skipAction && writer != null) writeAction?.Invoke(writer);
        }

        public void ReadPacket(BinaryReader reader, int sender)
        {
            bool isFromServer = sender == 256;

            bool? TryRead<T>(bool isNeeded, T[] array, out T resultEntity, bool byteIndex = false, int? forcedIndex = null) where T : Entity
            {
                resultEntity = null;

                if (isNeeded)
                {
                    int index = forcedIndex ?? (byteIndex ? reader.ReadByte() : reader.ReadUInt16());
                    if (array.IndexInRange(index) && (resultEntity = array[index]) != null && resultEntity.active) return true;

                    krpg2.LogWarning("Could not read {typeof(TEntity).Name} with id {index}");
                    return false;
                }
                return null;
            }

            switch (TryRead(reqs.HasFlag(Requirements.Player), Main.player, out player, true, (isFromServer || playerIsNotTheSender) ? (int?)null : sender))
            {
                case false: return;

                case true:
                    if (reqs.HasFlag(Requirements.K2Player))
                        k2player = player.GetModPlayer<K2Player>();

                    switch (TryRead(reqs.HasFlag(Requirements.PlayerItem), player.inventory, out playerItem))
                    {
                        case false: return;

                        case true:
                            if (reqs.HasFlag(Requirements.PlayerK2Item))
                                playerK2item = (K2Item)item.GetGlobalItem<K2Item>();
                            break;
                    }
                    break;
            }

            switch (TryRead(reqs.HasFlag(Requirements.Item), Main.item, out item))
            {
                case false: return;

                case true:
                    if (reqs.HasFlag(Requirements.K2Item))
                        k2item = (K2Item)item.GetGlobalItem<K2Item>();
                    break;
            }

            switch (TryRead(reqs.HasFlag(Requirements.NPC), Main.npc, out npc))
            {
                case false: return;

                case true:
                    if (reqs.HasFlag(Requirements.K2NPC))
                        k2npc = (K2NPC)npc.GetGlobalNPC<K2NPC>();
                    break;
            }

            switch (TryRead(reqs.HasFlag(Requirements.Projectile), Main.projectile, out projectile))
            {
                case false: return;

                case true:
                    if (reqs.HasFlag(Requirements.K2Projectile))
                        k2projectile = (K2Projectile)projectile.GetGlobalProjectile<K2Projectile>();
                    break;
            }

            long messageSpecificDataStart = reader.BaseStream.Position;

            Read(reader, sender);

            //Packet resending
            if (Main.netMode == NetmodeID.Server && resendPacket)
            {
                var stream = reader.BaseStream;
                if (!stream.CanSeek)
                    throw new Exception($"Fatal multiplayer exception: '{stream.GetType().Name}' does not support seeking.");

                WritePacket(null, out int packetHeaderSize);
                long pos = stream.Position;
                int messageSpecificDataSize = (int)(pos - messageSpecificDataStart);
                int packetSize = packetHeaderSize + messageSpecificDataSize;

                Action<BinaryWriter> a = w =>
                {
                    //Rewrite entities (the way player is written will change depending if we are them or not)
                    WritePacket(w, out _, true);

                    if (messageSpecificDataSize > 0)
                    {
                        long sPos = w.BaseStream.Position;
                        stream.Seek(messageSpecificDataStart, SeekOrigin.Begin);
                        stream.CopyTo(w.BaseStream, messageSpecificDataSize);
                        stream.Seek(pos, SeekOrigin.Begin);

                        w.BaseStream.Seek(sPos + messageSpecificDataSize, SeekOrigin.Begin);
                    }
                };
                K2Networking.SendPacket(krpg2, id, a, packetSize, ignoreClient: sender);
            }

            ClearEntityFields();
        }

        private void ClearEntityFields()
        {
            player = null;
            k2player = null;
            playerItem = null;
            playerK2item = null;
            item = null;
            k2item = null;
            npc = null;
            k2npc = null;
            projectile = null;
            k2projectile = null;
        }

        protected abstract void Read(BinaryReader reader, int sender);
    }
}
