using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace L.Util
{
    public class Des3
    {
        /// <summary>
        /// 加密模式
        /// Fill0xFF : 不足8字节，填充0xFF
        /// Fill0x00 : 不足8字节，填充0x00
        /// Ignore : 不足8字节的部分不加密
        /// </summary>
        public enum EncryptMode
        { 
            Fill0xFF,
            Fill0x00,
            Ignore
        }

        //加密矢量
        private static byte[] IV = { 0xB0, 0xA2, 0xB8, 0xA3, 0xDA, 0xCC, 0xDA, 0xCC };

        // 封闭构造方法
        private Des3() { }

        /// <summary>加密一块数据（8byte）</summary>
        /// <param name="keys">密匙 16字节</param>
        /// <param name="blockData">需要加密的数据 8字节</param>
        /// <returns>加密结果 8字节</returns>
        private static byte[] EncryptBlock(byte[] key, byte[] blockData)
        {
            TripleDESCryptoServiceProvider tdsc = new TripleDESCryptoServiceProvider();
            //指定密匙长度，默认为192位
            tdsc.KeySize = 128;
            //使用指定的key和IV（加密向量）
            tdsc.Key = key;
            tdsc.IV = IV;
            //加密模式，偏移
            tdsc.Mode = CipherMode.ECB;
            tdsc.Padding = PaddingMode.None;
            //进行加密转换运算
            ICryptoTransform ct = tdsc.CreateEncryptor();
            //8很关键，加密结果是8字节数组
            byte[] results = ct.TransformFinalBlock(blockData, 0, 8);

            return results;
        }

        /// <summary>
        /// 解密一块数据（8byte）
        /// </summary>
        /// <param name="key">密匙 16字节</param>
        /// <param name="blockData">已加密的数据 8字节</param>
        /// <returns>解密结果 8字节</returns>
        private static byte[] DecryptBlock(byte[] key, byte[] blockData)
        {
            TripleDESCryptoServiceProvider tdsc = new TripleDESCryptoServiceProvider();

            //指定密匙长度，默认为192位
            tdsc.KeySize = 128;
            //使用指定的key和IV（加密向量）
            tdsc.Key = key;
            tdsc.IV = IV;
            //加密模式，偏移
            tdsc.Mode = CipherMode.ECB;
            tdsc.Padding = PaddingMode.None;
            //进行加密转换运算
            ICryptoTransform ct = tdsc.CreateDecryptor();
            //8很关键，加密结果是8字节数组
            byte[] results = ct.TransformFinalBlock(blockData, 0, 8);

            return results;
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="key">密匙 16字节</param>
        /// <param name="data">需要加密的数据</param>
        /// <param name="mode">加密模式</param>
        /// <returns>加密结果</returns>
        public static byte[] EncryptBytes(byte[] key, byte[] data, EncryptMode mode)
        { 
            // 检查参数
            if (key == null || key.Length != 16) throw new ArgumentException("key length must equals 16 byte.");
            if (data == null || data.Length <= 0) throw new ArgumentException("data length must > 0");

            // 填充数据
            byte[] fillData = null;
            switch (mode)
            {
                case EncryptMode.Fill0xFF:
                    fillData = ReplenishBytes(data, 0xFF);
                    break;
                case EncryptMode.Fill0x00:
                    fillData = ReplenishBytes(data, 0x00);
                    break;
                case EncryptMode.Ignore:
                    fillData = data;
                    break;
            }

            // 按8字节一块进行分组
            byte[] block;
            List<byte[]> blocks = new List<byte[]>();
            for (int i = 0; i < fillData.Length / 8; i++)
            {
                block = new byte[8];
                Array.Copy(fillData, i * 8, block, 0, block.Length);
                blocks.Add(block);
            }

            // 如果不是整数倍，那么就有一块是不加密的部分
            byte[] ignoreBlock = new byte[0];
            if (blocks.Count * 8 < fillData.Length)
            {
                ignoreBlock = new byte[fillData.Length - blocks.Count * 8];
                Array.Copy(fillData, blocks.Count * 8, ignoreBlock, 0, ignoreBlock.Length);
            }

            // 存储加密结果的列表
            List<byte> result = new List<byte>(fillData.Length);
            // 依次加密每一块数据
            for (int i = 0; i < blocks.Count; i++)
            {
                result.AddRange(EncryptBlock(key, blocks[i]));
            }

            // 添加最后一块不加密的数据（如果为填充模式，则最后一块为空，不影响）
            result.AddRange(ignoreBlock);

            return result.ToArray();
        }

        /// <summary>
        /// 解密数据
        /// 如果数据长度不是8的整数倍
        /// 最后一块不进行解密操作
        /// </summary>
        /// <param name="key">密匙 16字节</param>
        /// <param name="data">需要解密的数据</param>
        /// <returns>解密结果</returns>
        public static byte[] DecryptBytes(byte[] key, byte[] data)
        {
            // 检查参数
            if (key == null || key.Length != 16) throw new ArgumentException("key length must equals 16 byte.");
            if (data == null || data.Length <= 0) throw new ArgumentException("data length must > 0");

            // 按8字节一块进行分组
            byte[] block;
            List<byte[]> blocks = new List<byte[]>();
            for (int i = 0; i < data.Length / 8; i++)
            {
                block = new byte[8];
                Array.Copy(data, i * 8, block, 0, block.Length);
                blocks.Add(block);
            }

            // 如果不是整数倍，那么就有一块是未加密的部分
            byte[] ignoreBlock = new byte[0];
            if (blocks.Count * 8 < data.Length)
            {
                ignoreBlock = new byte[data.Length - blocks.Count * 8];
                Array.Copy(data, blocks.Count * 8, ignoreBlock, 0, ignoreBlock.Length);
            }

            // 存储解密结果的列表
            List<byte> result = new List<byte>(data.Length);
            // 依次解密每一块数据
            for (int i = 0; i < blocks.Count; i++)
            {
                result.AddRange(DecryptBlock(key, blocks[i]));
            }

            // 添加最后一块解加密的数据（如果为没有未加密数据，则最后一块为空，不影响）
            result.AddRange(ignoreBlock);

            return result.ToArray();
        }

        /// <summary>
        /// 填充数据，如果数据不是8的倍数，则进行填充
        /// </summary>
        /// <param name="data">需要填充的源数据</param>
        /// <param name="repByte">填充的字节</param>
        /// <returns>填充后的数据</returns>
        private static byte[] ReplenishBytes(byte[] data, byte repByte)
        {
            if (data == null) return null;

            if (data.Length % 8 == 0)
            {
                return data;
            }
            // 找到最小公倍数
            int newLength = (data.Length / 8 + 1) * 8;
            byte[] newData = new byte[newLength];
            for (int i = 0; i < newData.Length; i++)
            {
                if (i < data.Length)
                {
                    newData[i] = data[i];
                }
                else
                {
                    newData[i] = repByte;
                }
            }

            return newData;
        }
    }
}
