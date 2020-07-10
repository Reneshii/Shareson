using System;
using System.IO;

namespace Shareson.Support
{
    public class ByteToFile
    {
        public static bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            { 
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        public static bool ByteArrayToFileAsString(byte[] byteArray,string fileName = null)
        {
            try
            {
                if(string.IsNullOrEmpty(fileName))
                {
                    fileName = $@"C:\Users\Reneshi\Downloads\Converted.txt";
                }
                var Str = BitConverter.ToString(byteArray, 0);
                var StrLength = Str.Length;


                File.WriteAllText(fileName, Str);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }
    }
}
