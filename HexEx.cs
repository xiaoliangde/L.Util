using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L.Util
{
    public static partial class EnumerableEx
    {
        /// <summary>
        /// 组合选择
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        public static string GetBufferHexString(this IEnumerable<byte> buffer)
        {
            var content = new StringBuilder();
            foreach (byte t in buffer) content.Append($"{t:X2} ");
            return content.ToString();
        }
        public static byte[] GetBytesFromHexString(this string hexString)
        {
            hexString = hexString.Trim(' ');
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++) returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        public static string GetBufferHexString<T>(this IEnumerable<T> buffer)
        {
            var content = new StringBuilder();
            foreach (T t in buffer) content.Append($"{t:X2} ");
            return content.ToString();
        }
    }
}
