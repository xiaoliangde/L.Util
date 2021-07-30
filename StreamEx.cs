using System.Collections.Generic;
using System.IO;

namespace L.Util
{
    public static class StreamEx
    {
        public static void CopyTo(this Stream source,Stream destination,int copyLen, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            var counter = copyLen;
            int count;
            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                if(counter>=count)destination.Write(buffer, 0, count);
                else destination.Write(buffer, 0, counter);
                counter -= count;
                if(counter<=0)break;
            }
        }


        public static string ToStringWithDispose(this Stream stream)
        {
            using (stream)
            {
                using (StreamReader streamReader = new StreamReader(stream))
                    return streamReader.ReadToEnd();
            }
        }

        public static byte[] ToBufferBytes(this Stream stream,int bufferSize=4096)
        {
            if (!stream.CanRead)
                return (byte[])null;
            if (stream.CanSeek)
                stream.Seek(0L, SeekOrigin.Begin);
            List<byte> byteList = new List<byte>();
            byte[] buffer = new byte[bufferSize];
            int num;
            while ((num = stream.Read(buffer, 0, bufferSize)) > 0)
            {
                for (int index = 0; index < num; ++index)
                    byteList.Add(buffer[index]);
            }
            return byteList.ToArray();
        }
    }
}
