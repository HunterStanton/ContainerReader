using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("ContainerReader\nA program that prints infomation about Windows Containers.index files, used to store metadata about configuration/save game files for UWP apps and games.\nUsage: ContainerReader containers.index");
                return;
            }
            return;
        }
    }
}
