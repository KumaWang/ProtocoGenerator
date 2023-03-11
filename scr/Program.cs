using System;
using System.IO;
using System.Text;
using System.Threading;

namespace iuiu.service
{
    class Program
    {
        static void Main(string[] args)
        {
            OneServer s = new OneServer(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));

            s.Start();

            Console.ReadLine();
        }

        

    }
}
