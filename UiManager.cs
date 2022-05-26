using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class UiManager
    {
        public static void draw_base(Directories D, int opt = 0)
        {
            int width = 100;
            int height = 40;
            if (opt < 0) opt = 0;
            List<string> dirs = new List<string>(Directory.GetDirectories(D.curdir));
            List<string> files = new List<string>(Directory.GetFiles(D.curdir));
            Console.Clear();
            Console.Write("|  Директории");
            Console.SetCursorPosition(width / 2, 0);
            Console.Write("|");
            Console.SetCursorPosition(width - 8, 0);
            Console.WriteLine("Файлы  |");
            for (int i = 0; i < width; i++) { Console.Write("="); }
            Console.WriteLine();
            for (int i = 0; i < height; i++)
            {
                //if (i >= dirs.Count() || i >= files.Count()) { break; }
                Console.Write("| ");
                if (i + opt < dirs.Count ) Console.Write(dirs[i + opt].Split("\\")[^1]);
                Console.SetCursorPosition(width / 2, Console.GetCursorPosition().Top);
                Console.Write("|");
                if (i + opt < files.Count) Console.Write(files[i + opt].Split("\\")[^1]);
                Console.SetCursorPosition(width - 1, Console.GetCursorPosition().Top);
                Console.Write("|");
                Console.WriteLine();
            }
            for (int i = 0; i < width; i++) { Console.Write("="); }
            Console.WriteLine();
            Console.WriteLine("Текущая директория: " + D.curdir);
            Console.Write(">> ");
        }
    }
}
