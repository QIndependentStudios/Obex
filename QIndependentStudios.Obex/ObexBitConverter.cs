using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Converts values to and from network order binary format.
    /// </summary>
    public class ObexBitConverter
    {
        private const string Iso8601DateTimeFormat = "yyyyMMdd'T'HHmmss";

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
                b = new[]
                {
                    b[3], b[2], b[1], b[0],
                    b[5], b[4],
                    b[7], b[6],
                    b[8], b[9], b[10], b[11], b[12], b[13], b[14], b[15]
                };

            return b;
        }

        /// <summary>
        /// Converts a <see cref="ushort"/> value to network order binary format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(ushort value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts a <see cref="short"/> value to network order binary format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(short value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts a <see cref="uint"/> value to network order binary format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(uint value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts a <see cref="int"/> value to network order binary format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(int value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts a <see cref="ulong"/> value to network order binary format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(ulong value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts a <see cref="long"/> value to network order binary format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(long value)
        {
            return AdjustEndian(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts a <see cref="string"/> value to network order binary format based on encoding.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="encoding">The encoding of the text value.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(string value, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.ASCII;

            if (encoding.Equals(Encoding.Unicode))
                encoding = Encoding.BigEndianUnicode;

            return encoding.GetBytes(value);
        }

        /// <summary>
        /// Converts a <see cref="Guid"/> value to network order binary format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(Guid value)
        {
            return AdjustGuidEndian(value.ToByteArray());
        }
        
        /// <summary>
        /// Converts a <see cref="DateTime"/> value to network order binary format.
        /// </summary>
        /// <param name="guid">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static byte[] GetBytes(DateTime value)
        {
            var formattedDateTime = value.ToString(Iso8601DateTimeFormat, CultureInfo.InvariantCulture);
            return GetBytes(formattedDateTime, Encoding.ASCII);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="ushort"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static ushort ToUInt16(byte[] bytes)
        {
            return BitConverter.ToUInt16(AdjustEndian(bytes), 0);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="short"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static short ToInt16(byte[] bytes)
        {
            return BitConverter.ToInt16(AdjustEndian(bytes), 0);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="uint"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static uint ToUInt32(byte[] bytes)
        {
            return BitConverter.ToUInt32(AdjustEndian(bytes), 0);
        }

        /// <summary>
        /// Converts network order binary data to an <see cref="int"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static int ToInt32(byte[] bytes)
        {
            return BitConverter.ToInt32(AdjustEndian(bytes), 0);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="ulong"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static ulong ToUInt64(byte[] bytes)
        {
            return BitConverter.ToUInt64(AdjustEndian(bytes), 0);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="long"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static long ToInt64(byte[] bytes)
        {
            return BitConverter.ToInt64(AdjustEndian(bytes), 0);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="string"/> value based on the specified encoding.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <param name="encoding">The encoding to use for conversion.</param>
        /// <returns>The converted value.</returns>
        public static string ToString(byte[] bytes, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            if (encoding.Equals(Encoding.Unicode) || encoding.Equals(Encoding.BigEndianUnicode))
                throw new ArgumentException($"Use {nameof(ToUnicodeString)}() when expecting unicode encoding.",
                    nameof(encoding));

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="string"/> value based on Unicode encoding.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static string ToUnicodeString(byte[] bytes)
        {
            return Encoding.BigEndianUnicode.GetString(bytes);
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="Guid"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static Guid ToGuid(byte[] bytes)
        {
            return new Guid(AdjustGuidEndian(bytes));
        }

        /// <summary>
        /// Converts network order binary data to a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="bytes">The binary data to convert.</param>
        /// <returns>The converted value.</returns>
        public static DateTime ToDateTime(byte[] bytes)
        {
            var dateTime = ToString(bytes);
            return DateTime.ParseExact(dateTime, Iso8601DateTimeFormat, CultureInfo.InvariantCulture);
        }
    }
}
