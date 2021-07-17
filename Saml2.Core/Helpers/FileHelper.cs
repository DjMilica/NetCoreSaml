using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Saml2.Core.Helpers
{
    public class FileHelper
    {
        public static string Read(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            using StreamReader reader = new StreamReader(fileStream);
            return reader.ReadToEnd();
        }
    }
}
