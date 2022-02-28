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
            //Console.WriteLine(await getContentWithoutUnicode());
            //saveContent("232131231");
            //Console.WriteLine(getContent());
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
                    output += (char) data;
                }

                return output;
        }

        public static async Task<string> getContentWithoutUnicode()
        {
            lock (requestLock)
            {

                FileStream i = new FileStream(filePath, FileMode.Create);
                string output = "";
                int data;
                lock (i)
                {
                    while ((data = i.ReadByte()) > 0)
                    {
                        if (data < 0x80)
                        {
                            output += (char) data;
                        }
                    }
                }

                return output;
            }
        }
        public static void saveContent(string content)
        {
            FileStream o = new FileStream(filePath, FileMode.Create);
            lock (o)
            {
                for (int i = 0; i < content.Length; i += 1)
                {
                    o.WriteByte((byte)content[i]);
                }
            }
        }
    }

}
