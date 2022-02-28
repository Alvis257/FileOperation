using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test2
{
    class Program
    {
        private static readonly object requestLock = new object();

        public class ExThread
        {
            public static async void thread1()
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 2)
                    {
                        await Task.Delay(50);
                    }

                    write1();

                    if (i == 0)
                    {
                        await Task.Delay(140);
                    }


                }
            }


            public static async void thread2()
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                    {
                        await Task.Delay(10);
                    }
                    if (i == 5)
                    {
                        await Task.Delay(50);
                    } 
                   
                    write2();
                    if (i == 3)
                    {
                        await Task.Delay(70);
                    }
                    if (i == 1)
                    {
                        await Task.Delay(90); 
                    }
                   
                }
            }

            public static async void thread3()
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i == 0)
                    {
                        await Task.Delay(30);
                    }
                    if (i == 7)
                    {
                        await Task.Delay(100);
                    }

                    write3();

                    if (i == 5)
                    {
                        await Task.Delay(120);
                    }
                    
                }
            }
        }

        static async Task write1()
        {
            Console.WriteLine("1");
            await Task.Delay(100);
        }

        static async Task write2()
        {
            Console.WriteLine("2");
            await Task.Delay(200);
        }

        static async Task write3()
        {
            Console.WriteLine("3");
            await Task.Delay(300);
        }


        static void Main(string[] args)
        {
            Thread a = new Thread(ExThread.thread1);
            Thread b = new Thread(ExThread.thread2);
            Thread c = new Thread(ExThread.thread3);
            a.Start();
            b.Start();
            c.Start();

            Console.ReadKey();
        }
    }
}

