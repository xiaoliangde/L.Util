using System;

namespace L.Util
{
    public static class DataConverter
    {
        /// <summary>
        /// 是否需要反转byte数组
        /// </summary>
        public static bool IsReverse { get; set; } = BitConverter.IsLittleEndian;

        /// <summary>
        /// 将字节数组转换为16位无符号整数
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">开始索引号</param>
        /// <returns>16位无符号整数</returns>
        public static UInt16 ToUInt16(this byte[] data, int startIndex)
        {
            UInt16 value;

            // 检查长度
            if (data.Length < startIndex + 2) throw new ArgumentOutOfRangeException("start index and length must in byte array.");

            lock (data)
            {
                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 2);

                // 转换byte数组
                value = BitConverter.ToUInt16(data, startIndex);

                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 2);
            }

            return value;
        }

        /// <summary>
        /// 将字节数组转换为32位有符号整数
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">开始索引号</param>
        /// <returns>32位有符号整数</returns>
        public static int ToInt32(this byte[] data, int startIndex)
        {
            int value;

            // 检查长度
            if (data.Length < startIndex + 4) throw new ArgumentOutOfRangeException("start index and length must in byte array.");

            lock (data)
            {
                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 4);

                // 转换byte数组
                value = BitConverter.ToInt32(data, startIndex);

                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 4);
            }

            return value;
        }

        /// <summary>
        /// 将字节数组转换为32位无符号整数
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">开始索引号</param>
        /// <returns>32位无符号整数</returns>
        public static uint ToUInt32(this byte[] data, int startIndex)
        {
            uint value;

            // 检查长度
            if (data.Length < startIndex + 4) throw new ArgumentOutOfRangeException("start index and length must in byte array.");

            lock (data)
            {
                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 4);

                // 转换byte数组
                value = BitConverter.ToUInt32(data, startIndex);

                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 4);
            }

            return value;
        }

        /// <summary>
        /// 将字节数组转换为64位有符号整数
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">开始索引号</param>
        /// <returns>64位有符号整数</returns>
        public static Int64 ToInt64(this byte[] data, int startIndex)
        {
            Int64 value;

            // 检查长度
            if (data.Length < startIndex + 8) throw new ArgumentOutOfRangeException("start index and length must in byte array.");

            lock (data)
            {
                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 8);

                // 转换byte数组
                value = BitConverter.ToInt64(data, startIndex);

                // 翻转byte数组中指定的节点
                if (IsReverse) Array.Reverse(data, startIndex, 8);
            }

            return value;
        }

        /// <summary>
        /// 将16位无符号整数转换为字节数组
        /// </summary>
        /// <param name="value">16位无符号整数</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(this UInt16 value)
        {
            byte[] data = new byte[2];

            data = BitConverter.GetBytes(value);

            if (IsReverse) Array.Reverse(data);

            return data;
        }



        /// <summary>
        /// 将32位有符号整数转换为字节数组
        /// </summary>
        /// <param name="value">32位无符号整数</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(this int value)
        {
            byte[] data = new byte[4];

            data = BitConverter.GetBytes(value);

            if (IsReverse) Array.Reverse(data);

            return data;
        }

        /// <summary>
        /// 将32位无符号整数转换为字节数组
        /// </summary>
        /// <param name="value">32位无符号整数</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(this uint value)
        {
            byte[] data = new byte[4];

            data = BitConverter.GetBytes(value);

            if (IsReverse) Array.Reverse(data);

            return data;
        }

        /// <summary>
        /// 将64位有符号整数转换为字节数组
        /// </summary>
        /// <param name="value">64位有符号整数</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(this Int64 value)
        {
            byte[] data = new byte[8];

            data = BitConverter.GetBytes(value);

            if (IsReverse) Array.Reverse(data);

            return data;
        }

        /// <summary>
        /// 判定数组是否相等
        /// 1.任意一个为空，则不相等
        /// 2.长度不同，则不相等
        /// 3.判断每一个元素
        /// </summary>
        public static bool Equals(this byte[] arr1, byte[] arr2)
        { 
            // 任意一个为空，则不相等
            if (arr1 == null || arr2 == null)
            {
                return false;
            }

            // 长度不同，则不相等
            if (arr1.Length != arr2.Length)
            {
                return false;
            }

            // 判断每一个元素
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    return false;
                }
            }

            return true;
        }


        private const double TwoPI = Math.PI * 2;
        public static double WrapAngleInTwoPI(double angle)
        {
            if (angle>=0 && angle< TwoPI)
            {
                return angle;
            }
            else if (angle < 0)
            {
                angle += TwoPI;
                WrapAngleInTwoPI(angle);
            }
            else if (angle >= TwoPI)
            {
                angle -= TwoPI;
                WrapAngleInTwoPI(angle);
            }
            return angle;
        }
    }
}
