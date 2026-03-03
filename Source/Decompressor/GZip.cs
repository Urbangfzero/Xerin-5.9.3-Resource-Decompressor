#region GZip

using System.IO.Compression;

namespace Xerin593ResourceDecompressor.Decompressor
{
    internal class GZip
    {
        #region Decompress

        public static byte[] Decompress(byte[] A_0)
        {
            byte[] array;
            using (MemoryStream memoryStream = new MemoryStream(A_0))
            {
                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    using (MemoryStream memoryStream2 = new MemoryStream())
                    {
                        gzipStream.CopyTo(memoryStream2);
                        array = memoryStream2.ToArray();
                    }
                }
            }
            return array;
        }

        #endregion Decompress
    }
}

#endregion