using System;
using System.Threading;

namespace test2
{
    class Program
    {
        private static readonly object requestLock = new object();

        public class ExThread
        {
            public static void thread1()
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 2)
                    {
                        Thread.Sleep(60);
                    }

                    write1();
                    if (i == 0)
                    {
                        Thread.Sleep(2050);
                    }


                }
            }

            public static void thread2()
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                    {
                        Thread.Sleep(100);
                    }

                    write2();
                    if (i == 1)
                    {
                        Thread.Sleep(1400);
                    }
                }
            }

            public static void thread3()
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i == 0)
                    {
                        Thread.Sleep(350);
                    }

                    write3();

                    if (i == 6)
                    {
                        Thread.Sleep(1200);
                    }
                }
            }
        }

        static void write1()
        {
            Console.WriteLine("1");
            Thread.Sleep(100);
        }

        static void write2()
        {
            Console.WriteLine("2");
            Thread.Sleep(200);
        }

        static void write3()
        {
            Console.WriteLine("3");
            Thread.Sleep(300);
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
