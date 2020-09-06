// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Byte.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class Byte.
    /// </summary>
    public static class Byte
    {

        /// <summary>
        /// The placement byte.
        /// </summary>
        internal static byte PlacementByte = 45;

        /// <summary>
        /// A byte extension method that combines.
        /// </summary>
        /// <param name="b1">The b1 to act on.</param>
        /// <param name="b2">The second byte.</param>
        /// <returns>An int.</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static int Combine(this byte b1, byte b2)
        {
            int combined = b1 << 8 | b2;
            return combined;
        }

        /// <summary>
        /// A byte extension method that combine to u short.
        /// </summary>
        /// <param name="b1">The b1 to act on.</param>
        /// <param name="b2">The second byte.</param>
        /// <returns>An ushort.</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static ushort CombineToUShort(this byte b1, byte b2)
        {
            return BitConverter.ToUInt16(new[] { b2, b1 }, 0);
        }

        /// <summary>
        /// A byte extension method that combine to int 32.
        /// </summary>
        /// <param name="b1">The b1 to act on.</param>
        /// <param name="b2">The second byte.</param>
        /// <param name="b3">The third byte.</param>
        /// <param name="b4">The fourth byte.</param>
        /// <returns>An uint.</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static uint CombineToInt32(this byte b1, byte b2, byte b3, byte b4)
        {
            return BitConverter.ToUInt32(new[] { b4, b3, b2, b1 }, 0);
        }

        /// <summary>
        /// A byte extension method that combine to int 64.
        /// </summary>
        /// <param name="b1">The b1 to act on.</param>
        /// <param name="b2">The second byte.</param>
        /// <param name="b3">The third byte.</param>
        /// <param name="b4">The fourth byte.</param>
        /// <param name="b5">The fifth byte.</param>
        /// <param name="b6">The b 6.</param>
        /// <param name="b7">The b 7.</param>
        /// <param name="b8">The b 8.</param>
        /// <returns>An ulong.</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static ulong CombineToInt64(this byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, byte b8)
        {
            return BitConverter.ToUInt64(new[] { b8, b7, b6, b5, b4, b3, b2, b1 }, 0);
        }

        #region ACT Encoding 0101

        /// <summary>
        /// A byte[] extension method that converts an x to a ct string.
        /// </summary>
        /// <param name="x">.</param>
        /// <returns>x as a string.</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static string ToACTString(this byte[] x)
        {
            string _TmpReturn = "";
            bool _HasPadding = false;

            for (int pos = 0; pos < x.Length; pos = pos + 2)
            {
                byte First = 0;
                byte Second = PlacementByte;

                try
                {
                    First = x[pos];
                    Second = x[pos + 1];
                }
                catch
                {
                    try
                    {
                        First = x[pos];
                        Second = PlacementByte;
                        _HasPadding = true;
                    }
                    catch
                    {
                        return "Error Calculating ACTString";
                    }
                }

                byte _xOr = First.Xor(Second);

                _TmpReturn += First.ToBinaryString() + _xOr.ToBinaryString();
            }

            if (_HasPadding)
            {
                _TmpReturn += "PADDING";
            }

            return _TmpReturn;
        }

        /// <summary>
        /// A string extension method that initializes this object from the given from a ct string.
        /// </summary>
        /// <param name="x">.</param>
        /// <returns>A byte[].</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static byte[] FromACTString(this string x)
        {
            var _HasPadding = false;
            var _input = x;

            if (x.EndsWith("HASPADDING"))
            {
                _HasPadding = true;
                _input = _input.Substring(0, _input.Length - 10);
            }

            byte[] _ACTStringByteArrays = x.ToByteArrayFromBinaryString();

            var _TmpReturn = new List<byte>();

            for (var pos = 0; pos < _ACTStringByteArrays.Length; pos = pos + 2)
            {
                var FirstByte = _ACTStringByteArrays[pos];
                var SecondByte = _ACTStringByteArrays[pos + 1];
                var CalculatedByte = FirstByte.Xor(SecondByte);

                _TmpReturn.Add(FirstByte);
                _TmpReturn.Add(CalculatedByte);
            }

            // REMOVE PADDING BYTE
            if (_HasPadding)
            {
                _TmpReturn.RemoveAt(_TmpReturn.Count - 1);
            }

            return _TmpReturn.ToArray();
        }

        #endregion

        /// <summary>
        /// Xors the specified b.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Byte.</returns>
        public static byte Xor (this byte a, byte b)
        {
            return (byte)(a ^ b);
        }
        /// <summary>
        /// From binary string.
        /// </summary>
        /// <param name="x">.</param>
        /// <param name="PadIfNeeded">(Optional) true if pad if needed.</param>
        /// <returns>A byte[].</returns>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static byte[] FromBinaryString(string x, bool PadIfNeeded = true)
        {
            string input = x;
            int _Remainder = x.Length % 8;

            if (PadIfNeeded == false)
            {
                if (_Remainder != 0) { throw new Exception("Padding Is Off and String is Not divided by 8 evenly"); }
            }

            // PAD INPUT STRING TO THE BEGINNING  so 1101010 would become 01101010
            if (_Remainder != 0)
            {
                for (int pos = 0; pos < _Remainder; pos++)
                {
                    input = "0" + input;
                }
            }

            int numOfBytes = input.Length / 8;

            byte[] bytes = new byte[numOfBytes];

            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }

            return bytes;
        }

        /// <summary>
        /// A byte extension method that converts an x to a binary string.
        /// </summary>
        /// <param name="x">.</param>
        /// <returns>x as a string.</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static string ToBinaryString(this byte x)
        {
            return Convert.ToString(x, 2).PadLeft(8, '0');
        }

        /// <summary>
        /// A byte[] extension method that drop zero bytes.
        /// </summary>
        /// <param name="x">.</param>
        /// <returns>A byte[].</returns>
        /// <remarks>Mark Alicz, 12/18/2016.</remarks>
        public static byte[] DropZeroBytes(this byte[] x)
        {
            List<byte> _tmpReturn = new List<byte>();

            foreach (var b in x)
            {
                if (b != 0)
                {
                    _tmpReturn.Add(b);
                }
            }
            return _tmpReturn.ToArray();
        }

        /// <summary>
        /// Returns the Percentage Different From The Compare To
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="CompareTo">The compare to.</param>
        /// <returns>Decimal Percentage Similarity 100 is Perfect Similar</returns>
        public static decimal CompareArrays(this byte[] x, byte[] CompareTo)
        {
            int DifCount = 0;
            decimal DifferencePercent = 0;

            decimal _DifferencePerItem = ((decimal)100) / ((decimal)x.Length);

            for (int xpos = 0; xpos < x.Length; xpos++)
            {
                if (CompareTo.Length < xpos)
                {
                    break;
                }
                else
                {
                    if (x[xpos] == CompareTo[xpos])
                    {
                        DifferencePercent += _DifferencePerItem;
                    }
                    else
                    {
                        DifCount++;
                    }
                }
            }
            if (DifCount == 0) { return 100; }
            else { return DifferencePercent; }

        }
    }
}
