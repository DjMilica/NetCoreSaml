using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Saml2.Core.Encoders
{
    public interface ISamlEncoder
    {
        string Base64Encode(string data);
        string DeflateAndBase64Encode(string data);
        string Base64DecodeAndInflate(string data);
    }

    public class SamlEncoder : ISamlEncoder
    {
        public string Base64Encode(string data)
        {
            using MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(data ?? ""));

            byte[] dataByteArray = memoryStream.ToArray();

            return Convert.ToBase64String(dataByteArray);

        }

        public string DeflateAndBase64Encode(string data)
        {
            using MemoryStream streamToFlushCompresedData = new MemoryStream();
            using MemoryStream uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(data ?? ""));

            using DeflaterOutputStream deflateStream = new DeflaterOutputStream(streamToFlushCompresedData, new Deflater(Deflater.DEFLATED));

            uncompressedStream.CopyTo(deflateStream);
            streamToFlushCompresedData.Position = 0;
            deflateStream.Close();

            byte[] deflatedByteArray = streamToFlushCompresedData.ToArray();

            return Convert.ToBase64String(deflatedByteArray);
        }

        public string Base64DecodeAndInflate(string data)
        {
            byte[] base64DecodedData = Convert.FromBase64String(data);

            using MemoryStream streamToFlushDecompressedData = new MemoryStream();
            using MemoryStream compressedStream = new MemoryStream(base64DecodedData);
            using InflaterInputStream inflateStream = new InflaterInputStream(compressedStream, new Inflater(true));

            inflateStream.CopyTo(streamToFlushDecompressedData);
            streamToFlushDecompressedData.Position = 0;
            inflateStream.Close();

            byte[] inflatedByteArray = streamToFlushDecompressedData.ToArray();

            return Encoding.UTF8.GetString(inflatedByteArray);
        }
    }
}
