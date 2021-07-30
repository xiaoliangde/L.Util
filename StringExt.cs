using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace L.Util
{
    public static class StringExt
    {
        public static string GetMd5HexString(this string content)
        {
            MD5 md5 = MD5.Create();
            //需要将字符串转成字节数组
            byte[] buffer = Encoding.Default.GetBytes(content);
            //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
            byte[] md5buffer = md5.ComputeHash(buffer);
            var str = new StringBuilder();
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            foreach (byte b in md5buffer)
            {
                //得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                //但是在和对方测试过程中，发现我这边的MD5加密编码，经常出现少一位或几位的问题；
                //后来分析发现是 字符串格式符的问题， X 表示大写， x 表示小写， 
                //X2和x2表示不省略首位为0的十六进制数字；
                //str += b.ToString("x2");
                str.Append(b.ToString("x2"));
            }

            return str.ToString();
        }
        public static string ToContentString(this Stream stream)
        {
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            return text;
        }

        public static string ToBase64String(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return Convert.ToBase64String(bytes);
        }

        public static byte[] ToUtf8Buffer(this string content)
        {
            return Encoding.UTF8.GetBytes(content);
        }
        public static string ToISO_8859_1(this string srcText)
        {
            var dst = "";
            var src = srcText.ToCharArray();
            for (var i = 0; i < src.Length; i++)
            {
                var str = @"&#" + (int)src[i] + ";";
                dst += str;
            }
            return dst;
        }
        public static string FromISO_8859_1(this string srcText)
        {
            var dst = "";
            var src = srcText.Split(';');
            for (var i = 0; i < src.Length; i++)
            {
                if (src[i].Length > 0)
                {
                    var str = ((char)int.Parse(src[i].Substring(2))).ToString();
                    dst += str;
                }
            }
            return dst;
        }

        public static IList<byte> EscapeCode(this IList<byte> tmpbytes)
        {
            for (int i = 1; i < tmpbytes.Count - 1; i++)
            {
                if (tmpbytes[i] == 0x5b)
                {
                    tmpbytes[i] = (byte)0x5a;
                    tmpbytes.Insert(i + 1, (byte)0x01);
                    i++;
                }
                else if (tmpbytes[i] == 0x5a)
                {
                    tmpbytes[i] = (byte)0x5a;
                    tmpbytes.Insert(i + 1, (byte)0x02);
                    i++;
                }
                else if (tmpbytes[i] == 0x5d)
                {
                    tmpbytes[i] = (byte)0x5e;
                    tmpbytes.Insert(i + 1, (byte)0x01);
                    i++;
                }
                else if (tmpbytes[i] == 0x5e)
                {
                    tmpbytes[i] = (byte)0x5e;
                    tmpbytes.Insert(i + 1, (byte)0x02);
                    i++;
                }
            }
            return tmpbytes;
        }

        public static IList<byte> UnEscapeCode(this IList<byte> tmpbytes)
        {
            for (int i = 1; i < tmpbytes.Count - 1; i++)
            {
                if (tmpbytes[i] == (byte)0x5a && tmpbytes[i + 1] == (byte)0x01)
                {
                    tmpbytes[i] = (byte)0x5b;
                    tmpbytes.RemoveAt(i + 1);
                }
                else if (tmpbytes[i] == (byte)0x5a && tmpbytes[i + 1] == (byte)0x02)
                {
                    tmpbytes[i] = (byte)0x5a;
                    tmpbytes.RemoveAt(i + 1);
                }
                else if (tmpbytes[i] == (byte)0x5e && tmpbytes[i + 1] == (byte)0x01)
                {
                    tmpbytes[i] = (byte)0x5d;
                    tmpbytes.RemoveAt(i + 1);
                }
                else if (tmpbytes[i] == (byte)0x5e && tmpbytes[i + 1] == (byte)0x02)
                {
                    tmpbytes[i] = (byte)0x5e;
                    tmpbytes.RemoveAt(i + 1);
                }
            }
            return tmpbytes;
        }

        public static ushort CRC16_CCITT(this IList<byte> ucbuf, int offset, int iLen)
        {
            ushort crc = 0xFFFF;          // initial value
            ushort polynomial = 0x1021;   // 0001 0000 0010 0001  (0, 5, 12)

            for (int j = 0; j < iLen; ++j)
            {
                for (int i = 0; i < 8; i++)
                {
                    bool bit = ((ucbuf[j + offset] >> (7 - i) & 1) == 1);
                    bool c15 = ((crc >> 15 & 1) == 1);
                    crc <<= 1;
                    if (c15 ^ bit) crc ^= polynomial;
                }
            }

            crc &= 0xffff;

            return crc;
        }
        
        static readonly Random CodeRandom = new Random();
        private const int CharMin = 0X20;
        private const int CharMax = 0X7F;
        public static string GetGuidString(int len)//len must large than 15
        {
            var guidString = new StringBuilder();
            var dateChars = DateTime.Now.ToString("yyMMddHHmmssfff").ToCharArray();
            if(len < dateChars.Length)throw new Exception($"len must large than {dateChars.Length}");
            foreach (var t in dateChars) guidString.Append(t);
            for (var i = dateChars.Length-1; i < len; i++)guidString.Append((char)CodeRandom.Next(CharMin, CharMax));

            return guidString.ToString();
        }
        public static string GetRandomString(int len)
        {
            var randomString = new StringBuilder();
            if (len < 1) throw new Exception($"len must large than 0");
            for (var i = 0; i < len; i++) randomString.Append((char)CodeRandom.Next(CharMin, CharMax));

            return randomString.ToString();
        }
        public static string GetRandomLenthString(int minLen,int maxLen)
        {
            var len = CodeRandom.Next(minLen, maxLen);
            var randomString = new StringBuilder();
            if (len < 1) throw new Exception($"len must large than 0");
            for (var i = 0; i < len; i++) randomString.Append((char)CodeRandom.Next(CharMin, CharMax));

            return randomString.ToString();
        }

        private static readonly int GB = 1024 * 1024 * 1024;
        private static readonly int MB = 1024 * 1024;
        private static readonly int KB = 1024;
        public static string BytesToAutomatic(this long KSize)
        {
            if (KSize / GB >= 1) return Math.Round(KSize / (float)GB, 2) + "GB";
            if (KSize / MB >= 1) return Math.Round(KSize / (float)MB, 2) + "MB";
            if (KSize / KB >= 1) return Math.Round(KSize / (float)KB, 2) + "KB";
            return KSize + "Byte";
        }
    }
}
