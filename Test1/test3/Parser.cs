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
            //sets a new path
            setPath("..//..//text1.txt");
            //gets that path
            var path = getPath();
            Console.WriteLine(path);
            //calls and awaits function, then writes it to the console
            Console.WriteLine(await getContent());
            Console.WriteLine(await getContentWithoutUnicode());
            Console.WriteLine();
            //adds new content to file and saves it
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
            //opens the needed file, and sets to a variable so it could be read or written 
            FileStream i = new FileStream(filePath, FileMode.Open);
            string output = "";
            int data;

            //cikles through the file data and gives the value to output variable
            while ((data = i.ReadByte()) > 0)
            {
                output += (char)data;
            }
            //closes file so it would not interfere with other functions
            i.Close();
            return output;
        }

        public static async Task<string> getContentWithoutUnicode()
        {
            //opens the needed file, and sets to a variable so it could be read or written 
            FileStream i = new FileStream(filePath, FileMode.Open);
            string output = "";
            int data;
            lock (requestLock)
            {
                //cikles through the file data and gives the value to output variable
                while ((data = i.ReadByte()) > 0)
                {
                    if (data < 0x80)
                    {
                        output += (char)data;
                    }
                }
            }
            //closes file so it would not interfere with other functions
            i.Close();
            return output;

        }
        public static void saveContent(string content)
        { 
            //opens the needed file, and sets to a variable so it could be read or written 
            FileStream o = new FileStream(filePath, FileMode.Open);
            string output = "";
            int data;
            //cikles through the file data so it wouldn't be deleted
            while ((data = o.ReadByte()) > 0)
            {
            }
            //writes new data to the file 
            for (int i = 0; i < content.Length; i += 1)
            {
                o.WriteByte((byte)content[i]);
            }
            //closes file so it would not interfere with other functions
            o.Close();
        }
    }

}
