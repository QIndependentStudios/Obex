using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex
{
    public class ObexBitConverter
    {
        private const int GuidFirstLength = 4;
        private const int GuidSecondLength = 2;
        private const int GuidThirdLength = 2;

        private static byte[] AdjustEndian(IEnumerable<byte> bytes)
        {
            if (BitConverter.IsLittleEndian)
                bytes = bytes.Reverse();

            return bytes.ToArray();
        }

        public static byte[] GetBytes(ushort value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        public static byte[] GetBytes(short value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        public static byte[] GetBytes(uint value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        public static byte[] GetBytes(int value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        public static byte[] GetBytes(ulong value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        public static byte[] GetBytes(long value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        public static byte[] GetBytes(string text, bool isUnicode = false)
        {
            var encoding = isUnicode ? Encoding.BigEndianUnicode : Encoding.UTF8;
            return encoding.GetBytes(text);
        }

        public static byte[] GetBytes(Guid guid)
        {
            var b = guid.ToByteArray();
            return AdjustEndian(b.Take(GuidFirstLength))
                .Concat(AdjustEndian(b.Skip(GuidFirstLength).Take(GuidSecondLength)))
                .Concat(AdjustEndian(b.Skip(GuidFirstLength + GuidSecondLength).Take(GuidThirdLength)))
                .Concat(b.Skip(GuidFirstLength + GuidSecondLength + GuidThirdLength))
                .ToArray();
        }

        public static ushort ToUInt16(byte[] bytes)
        {
            return BitConverter.ToUInt16(AdjustEndian(bytes), 0);
        }

        public static short ToInt16(byte[] bytes)
        {
            return BitConverter.ToInt16(AdjustEndian(bytes), 0);
        }

        public static uint ToUInt32(byte[] bytes)
        {
            return BitConverter.ToUInt32(AdjustEndian(bytes), 0);
        }

        public static int ToInt32(byte[] bytes)
        {
            return BitConverter.ToInt32(AdjustEndian(bytes), 0);
        }

        public static ulong ToUInt64(byte[] bytes)
        {
            return BitConverter.ToUInt64(AdjustEndian(bytes), 0);
        }

        public static long ToInt64(byte[] bytes)
        {
            return BitConverter.ToInt64(AdjustEndian(bytes), 0);
        }

        public static string ToString(byte[] bytes, bool isUnicode = false)
        {
            var encoding = isUnicode ? Encoding.BigEndianUnicode : Encoding.UTF8;
            return encoding.GetString(bytes);
        }

        public static Guid ToGuid(byte[] bytes)
        {
            if (bytes.Length != 16)
                throw new ArgumentException("Guids must be exactly 16 bytes in length.");

            return new Guid(AdjustEndian(bytes.Take(GuidFirstLength))
                .Concat(AdjustEndian(bytes.Skip(GuidFirstLength).Take(GuidSecondLength)))
                .Concat(AdjustEndian(bytes.Skip(GuidFirstLength + GuidSecondLength).Take(GuidThirdLength)))
                .Concat(bytes.Skip(GuidFirstLength + GuidSecondLength + GuidThirdLength))
                .ToArray());
        }
    }
}
