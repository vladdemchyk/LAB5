using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static Queue<int> queue = new Queue<int>();
        static object lockObject = new object();

        static void Main(string[] args)
        {
            Thread producerThread = new Thread(Producer);
            Thread consumerThread = new Thread(Consumer);

            producerThread.Start();
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();
        }

        static void Producer()
        {
            Random random = new Random();

            while (true)
            {
                lock (lockObject)
                {
                    int number = random.Next(1, 100);
                    queue.Enqueue(number);
                    Console.WriteLine($"Producer added {number} to the queue");
                }

                Thread.Sleep(1000);
            }
        }

        static void Consumer()
        {
            while (true)
            {
                lock (lockObject)
                {
                    if (queue.Count > 0)
                    {
                        int number = queue.Dequeue();
                        Console.WriteLine($"Consumer removed {number} from the queue");
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }

    class TrafficLight
    {
        private static Semaphore semaphore = new Semaphore(2, 2);
        private static object lockObject = new object();
        private static int currentLight = 1;

        public void Run()
        {
            Thread light1Thread = new Thread(Light1);
            Thread light2Thread = new Thread(Light2);
            Thread light3Thread = new Thread(Light3);
            Thread light4Thread = new Thread(Light4);

            light1Thread.Start();
            light2Thread.Start();
            light3Thread.Start();
            light4Thread.Start();

            light1Thread.Join();
            light2Thread.Join();
            light3Thread.Join();
            light4Thread.Join();
        }

        static void Light1()
        {
            while (true)
            {
                lock (lockObject)
                {
                    if (currentLight == 1)
                    {
                        Console.WriteLine("Light 1 is green");
                        semaphore.WaitOne();
                        Thread.Sleep(5000);
                        semaphore.Release();
                        currentLight = 2;
                    }
                }

                Thread.Sleep(1000);
            }
        }

        static void Light2()
        {
            while (true)
            {
                lock (lockObject)
                {
                    if (currentLight == 2)
                    {
                        Console.WriteLine("Light 2 is green");
                        semaphore.WaitOne();
                        Thread.Sleep(5000);
                        semaphore.Release();
                        currentLight = 3;
                    }
                }

                Thread.Sleep(1000);
            }
        }

        static void Light3()
        {
            while (true)
            {
                lock (lockObject)
                {
                    if (currentLight == 3)
                    {
                        Console.WriteLine("Light 3 is green");
                        semaphore.WaitOne();
                        Thread.Sleep(5000);
                        semaphore.Release();
                        currentLight = 4;
                    }
                }

                Thread.Sleep(1000);
            }
        }

        static void Light4()
        {
            while (true)
            {
                lock (lockObject)
                {
                    if (currentLight == 4)
                    {
                        Console.WriteLine("Light 4 is green");
                        semaphore.WaitOne();
                        Thread.Sleep(5000);
                        semaphore.Release();
                        currentLight = 1;
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}