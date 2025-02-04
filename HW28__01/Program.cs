using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace HW28__01
{
    class Program
    {
        private static object fileLock = new object();
        private static object processLock = new object();

        static void Main()
        {
            Thread writerThread = new Thread(WriteRandomNumbersToFile);
            Thread primeFilterThread = new Thread(ExtractPrimeNumbers);
            Thread prime7FilterThread = new Thread(ExtractPrimesEndingWith7);

            writerThread.Start();
            writerThread.Join();

            primeFilterThread.Start();
            primeFilterThread.Join();

            prime7FilterThread.Start();
            prime7FilterThread.Join();

            Console.WriteLine("Обробка завершена");
        }

        static void WriteRandomNumbersToFile()
        {
            lock (fileLock)
            {
                Random random = new Random();
                using (StreamWriter writer = new StreamWriter("TXT.txt"))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        writer.WriteLine(random.Next(1, 1000));
                    }
                }
                Console.WriteLine("Згенеровано числа у TXT.txt");
            }
        }

        static void ExtractPrimeNumbers()
        {
            lock (fileLock)
            {
                if (File.Exists("TXT.txt"))
                {
                    var primes = File.ReadLines("TXT.txt")
                        .Select(int.Parse)
                        .Where(IsPrime)
                        .ToList();

                    lock (processLock)
                    {
                        File.WriteAllLines("primes.txt", primes.Select(n => n.ToString()));
                        Console.WriteLine("Прості числа записані у primes.txt");
                    }
                }
            }
        }

        static void ExtractPrimesEndingWith7()
        {
            lock (processLock)
            {
                if (File.Exists("primes.txt"))
                {
                    var primes7 = File.ReadLines("primes.txt")
                        .Select(int.Parse)
                        .Where(n => n % 10 == 7)
                        .ToList();

                    File.WriteAllLines("primes_with_7.txt", primes7.Select(n => n.ToString()));
                    Console.WriteLine("Прості числа, що закінчуються на 7, записані у primes_with_7.txt");
                }
            }
        }

        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
