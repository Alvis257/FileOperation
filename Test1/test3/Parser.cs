using System;
using System.IO;
using System.Threading.Tasks;

namespace test3
{
    static public class Parser
    {
        private static readonly object requestLock = new object();
        static public async Task Main(string[] args)
        {
            setPath("..//..//text1.txt");
            var path = getPath();
            Console.WriteLine(path);

            Console.WriteLine(await getContent());
            Console.WriteLine(await getContentWithoutUnicode());
            Console.WriteLine();
            saveContent("<new Content>");
            Console.WriteLine();
            Console.WriteLine(await getContent());
            Console.WriteLine(await getContentWithoutUnicode());
            Console.ReadKey();
        }

        private static string filePath;
        public static void setPath(string f)
        {
            filePath = f;
        }
        public static string getPath()
        {
            return filePath;
        }
        public static async Task<string> getContent()
        {

            FileStream i = new FileStream(filePath, FileMode.Open);
            string output = "";
            int data;
            while ((data = i.ReadByte()) > 0)
            {
                output += (char)data;
            }

            i.Close();
            return output;
        }

        public static async Task<string> getContentWithoutUnicode()
        {

            FileStream i = new FileStream(filePath, FileMode.Open);
            string output = "";
            int data;
            lock (requestLock)
            {
                while ((data = i.ReadByte()) > 0)
                {
                    if (data < 0x80)
                    {
                        output += (char)data;
                    }
                }
            }
            i.Close();
            return output;

        }
        public static void saveContent(string content)
        {
            FileStream o = new FileStream(filePath, FileMode.Open);
            string output = "";
            int data;
            while ((data = o.ReadByte()) > 0)
            {
            }

            for (int i = 0; i < content.Length; i += 1)
            {
                o.WriteByte((byte)content[i]);
            }

            o.Close();
        }
    }

}
