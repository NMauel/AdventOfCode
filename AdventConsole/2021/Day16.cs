using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day16 : IPuzzleDay<long>
    {
        private readonly byte[] input = InputReader.ReadString("Day16.txt").ConvertHexToBytes();

        public long CalculateAnswerPuzzle1()
        {
            int offset = 0;
            Packet packet = DecodePacket(input, ref offset);

            return SumOfVersions(packet);
        }

        public long CalculateAnswerPuzzle2()
        {
            int offset = 0;
            Packet packet = DecodePacket(input, ref offset);

            return packet.Value;
        }
        
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
        private Packet DecodePacket(byte[] packetBytes, ref int offset)
        {
            var packet = new Packet();
            var decoder = new PacketBytesDecoder(packetBytes, offset);
            packet.Version = decoder.Take(3);
            packet.TypeID = decoder.Take(3);

            if (packet.TypeID == 4)
            {
                bool hasValue = true;
                while (hasValue)
                {
                    hasValue = decoder.Take(1) == 1;
                    packet.Value = (packet.Value << 4 ) | decoder.Take(4);
                }
                offset = decoder.Offset;
            }
            else
            {
                if(decoder.Take(1) == 1)
                {
                    var packetCnt = decoder.Take(11);
                    offset = decoder.Offset;
                    for (int p = 0; p < packetCnt; p++)
                        packet.Subpackets.Add(DecodePacket(packetBytes, ref offset));
                }
                else
                {
                    var subpacketsLength = decoder.Take(15);
                    offset = decoder.Offset;
                    while (offset - decoder.Offset < subpacketsLength)
                        packet.Subpackets.Add(DecodePacket(packetBytes, ref offset));
                }
            }

            return packet;
        }
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand

        private class Packet
        {
            private long _value;

            public int Version { get; set; }
            public int TypeID { get; set; }

            public List<Packet> Subpackets { get; } = new();

            public long Value
            {
                get
                {
                    if (Subpackets.Any())
                    {
                        switch (TypeID)
                        {
                            case 0:
                                return Subpackets.Sum(packet => packet.Value);
                            case 1:
                                return Subpackets.Select(packet => packet.Value).Aggregate(1L, (x, y) => x * y);
                            case 2:
                                return Subpackets.Min(packet => packet.Value);
                            case 3:
                                return Subpackets.Max(packet => packet.Value);
                            case 4:
                                return _value;
                            case 5:
                                return Subpackets.First().Value > Subpackets.Last().Value ? 1 : 0;
                            case 6:
                                return Subpackets.First().Value < Subpackets.Last().Value ? 1 : 0;
                            case 7:
                                return Subpackets.First().Value == Subpackets.Last().Value ? 1 : 0;
                        }
                    }
                    if (TypeID != 4)
                        throw new AccessViolationException("Code should never reach this place!; Wrong puzzle input provided!");
                    return _value;
                }
                set => _value = value;
            }
        }

        private class PacketBytesDecoder
        {
            private readonly ReadOnlyMemory<byte> packetMemory;
            //private readonly int packetLength;

            public int Offset { get; private set; }

            public PacketBytesDecoder(byte[] packetBytes, int offset)
            {
                packetMemory = new ReadOnlyMemory<byte>(packetBytes);
                //packetLength = packetMemory.Length * 8;
                Offset = offset;
            }

            public int Take(int nrOfBits)
            {
                var span = packetMemory.Span.Slice(Offset / 8);
                var shift = Offset % 8;
                var index = 0;
                int result = 0;
                var size = nrOfBits;
                while (size > 0)
                {
                    var chunk = span[index];
                    var chunkSize = 8;

                    if (shift > 0)
                    {
                        chunk = (byte)((byte)(chunk << shift) >> shift);
                        chunkSize -= shift;
                    }

                    if (size < (8 - shift))
                    {
                        chunk >>= (8 - shift - size);
                        chunkSize = size;
                    }

                    result = (result << chunkSize) | chunk;
                    size -= chunkSize;
                    shift = 0;
                    ++index;
                }

                Offset += nrOfBits;
                return result;
            }
        }

        private long SumOfVersions(Packet packet) => packet.Version + packet.Subpackets.Sum(packet => SumOfVersions(packet));
    }
}