using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Directories d = new Directories("C:\\");
            while (true)
            {
                Commands.Command(d);
            }
        }
    }
}