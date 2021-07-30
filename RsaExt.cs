using System;
using System.IO;
using System.Security.Cryptography;

namespace L.Util
{
    public static class RsaExt
    {
        public static byte[] EncryptEx(this RSACryptoServiceProvider rsaCryptography, byte[] plaintextData)
        {
            int maxBlockSize = rsaCryptography.KeySize / 8 - 11;    //加密块最大长度限制

            if (plaintextData.Length <= maxBlockSize)
                return rsaCryptography.Encrypt(plaintextData, false);//

            using (MemoryStream plaiStream = new MemoryStream(plaintextData))
            using (MemoryStream crypStream = new MemoryStream())
            {
                Byte[] buffer = new Byte[maxBlockSize];
                int blockSize = plaiStream.Read(buffer, 0, maxBlockSize);

                while (blockSize > 0)
                {
                    Byte[] toEncrypt = new Byte[blockSize];
                    Array.Copy(buffer, 0, toEncrypt, 0, blockSize);

                    Byte[] cryptograph = rsaCryptography.Encrypt(toEncrypt, false);
                    crypStream.Write(cryptograph, 0, cryptograph.Length);

                    blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                }

                return crypStream.ToArray();
            }
        }

        public static byte[] DecryptEx(this RSACryptoServiceProvider rsaCryptography, Byte[] ciphertextData)
        {
//            Byte[] ciphertextData = Convert.FromBase64String(ciphertext);
            var maxBlockSize = rsaCryptography.KeySize / 8;    //解密块最大长度限制

            if (ciphertextData.Length <= maxBlockSize)
                return rsaCryptography.Decrypt(ciphertextData, false);
//            return Encoding.UTF8.GetString(rsaCryptography.Decrypt(ciphertextData, true));

            using (MemoryStream crypStream = new MemoryStream(ciphertextData))
            using (MemoryStream plaiStream = new MemoryStream())
            {
                Byte[] buffer = new Byte[maxBlockSize];
                int blockSize = crypStream.Read(buffer, 0, maxBlockSize);

                while (blockSize > 0)
                {
                    Byte[] toDecrypt = new Byte[blockSize];
                    Array.Copy(buffer, 0, toDecrypt, 0, blockSize);

                    Byte[] plaintext = rsaCryptography.Decrypt(toDecrypt, false);
                    plaiStream.Write(plaintext, 0, plaintext.Length);

                    blockSize = crypStream.Read(buffer, 0, maxBlockSize);
                }

                return plaiStream.ToArray();
            }
        }
    }
}