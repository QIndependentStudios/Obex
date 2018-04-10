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

        private static byte[] AdjustGuidEndian(IEnumerable<byte> bytes)
        {
            var b = bytes.ToArray();
            if (b.Length != 16)
                throw new ArgumentException("Guids must be exactly 16 bytes in length.");

            if (BitConverter.IsLittleEndian)
                b = new byte[]
                {
                    b[3], b[2], b[1], b[0],
                    b[5], b[4],
                    b[7], b[6],
                    b[8], b[9], b[10], b[11], b[12], b[13], b[14], b[15]
                };

            return b;
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

        public static byte[] GetBytes(string text, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            if (encoding == Encoding.Unicode)
                encoding = Encoding.BigEndianUnicode;

            return encoding.GetBytes(text);
        }

        public static byte[] GetBytes(Guid guid)
        {
            return AdjustGuidEndian(guid.ToByteArray());
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

        public static string ToString(byte[] bytes, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            if (encoding == Encoding.Unicode || encoding == Encoding.BigEndianUnicode)
                throw new ArgumentException($"Use {nameof(ToUnicodeString)}() when expecting unicode encoding.",
                    nameof(encoding));

            return encoding.GetString(bytes);
        }

        public static string ToUnicodeString(byte[] bytes)
        {
            return Encoding.BigEndianUnicode.GetString(bytes);
        }

        public static Guid ToGuid(byte[] bytes)
        {
            return new Guid(AdjustGuidEndian(bytes));
        }
    }
}
