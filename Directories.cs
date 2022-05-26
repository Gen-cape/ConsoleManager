using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Directories
    {
        public int? opt { get; set; }
        public string? curdir { get; set; }
        public Directories(string cur) { curdir = cur; opt = 0; }
    }
}
