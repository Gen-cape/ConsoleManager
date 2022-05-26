using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    public static class Commands
    {
        public static string buff { get; set; }
        public static int opt { get; set; }
        public static void Command(Directories D)
        {
            re(D);
            string com = Console.ReadLine();
            if (com.Length < 4) return;
            if (com.StartsWith("cd")) cd(D, com.Substring(3));
            if (com.StartsWith("md")) md(D, com.Substring(3));
            if (com.StartsWith("mf")) mf(D, com.Substring(3));
            if (com.StartsWith("deldir")) deldir(D, com.Substring(7));
            if (com.StartsWith("delfile")) delfile(D, com.Substring(8));
            if (com.StartsWith("rename"))
            {
                string[] vs = com.Split(' ');
                rename(D, vs[1], vs[2]);
            }
            if (com.StartsWith("rendir"))
            {
                string[] vs = com.Split(' ');
                rendir(D, vs[1], vs[2]);
            }
            if (com.StartsWith("copyf")) copyf(D, com.Substring(6));
            if (com.StartsWith("pastef")) pastef(D);
            if (com.StartsWith("copyff"))
            {
                string[] vs = com.Split(' ');
                copyfilefull(D, vs[1], vs[2]);
            }
            if (com.StartsWith("copydir"))
            {
                string[] vs = com.Split(' ');
                copydir(D, vs[1], vs[2]);
            }
            if (com.StartsWith("movef"))
            {
                string[] vs = com.Split(' ');
                movef(D, vs[1], vs[2]);
            }
            if (com.StartsWith("movedir"))
            {
                string[] vs = com.Split(' ');
                movedir(D, vs[1], vs[2]);
            }
            if (com.StartsWith("find")) find(D, com.Substring(5));
            if (com.StartsWith("help") || com.StartsWith("HELP")) help();
            if (com.StartsWith("showdirs")) showdir(D);
            if (com.StartsWith("next")) next(D);
            if (com.StartsWith("prev")) prev(D);
            if (com.StartsWith("re")) re(D);
            if (com == "exit") Environment.Exit(0);
        }
        public static void cd(Directories D, string dir)
        {
            if (Directory.Exists(dir)) Directory.SetCurrentDirectory(dir);
            else if (Directory.Exists(D.curdir + '\\' + dir))
                Directory.SetCurrentDirectory(D.curdir + '\\' + dir);
            else if (D.curdir == "C:\\") return;
            else if (dir == "..")
            {
                var path = Path.GetDirectoryName(D.curdir);
                Directory.SetCurrentDirectory(path);
            }
            D.curdir = Directory.GetCurrentDirectory();
            opt = 0;
        }

        public static void md(Directories D, string name)
        {
            if (name != "")
                Directory.CreateDirectory(D.curdir + '\\' + name);
        }
        public static void mf(Directories D, string name)
        {
            if (name != "")
            {
                var fs = File.Create(D.curdir + '\\' + name);
                fs.Close();
            }
        }
        public static void deldir(Directories D, string name)
        {
            if (name != "")
            {
                var Dir = D.curdir + '\\' + name;
                if (Directory.Exists(Dir))
                {
                    string[] dirs = Directory.GetDirectories(Dir);
                    string[] files = Directory.GetDirectories(Dir);
                    if (dirs.Length == 0 && files.Length == 0)
                    {
                        Directory.Delete(Dir);
                    }
                    else
                    {
                        Console.WriteLine("Are you sure? (y/n)");
                        var ans = Console.ReadLine();
                        if (ans == "y") Directory.Delete(Dir);
                    }
                }
            }
        }
        public static void delfile(Directories D, String name)
        {
            if (name != "")
            {
                var Dir = D.curdir + '\\' + name;
                if (File.Exists(Dir))
                {
                    Console.WriteLine("Are you sure? (y/n)");
                    var ans = Console.ReadLine();
                    if (ans == "y") File.Delete(Dir);
                }
            }
        }
        public static void rename(Directories D, String name1, String name2)
        {
            var Dir = D.curdir + '\\';
            File.Move(Dir + name1, Dir + name2);
        }
        public static void rendir(Directories D, String name1, String name2)
        {
            Directory.Move(D.curdir + '\\' + name1, D.curdir + '\\' + name2);
        }
        public static void copyf(Directories D, String name) { buff = D.curdir + '\\' + name; }
        public static void pastef(Directories D) { File.Copy(buff, D.curdir + '\\' + buff.Split('\\').Last()); }
        public static void copyfilefull(Directories D, String source, String destination) { File.Copy(source, destination); }
        private static void copydir(Directories D, string sourceName, string targetPath)
        {
            string sourcePath = D.curdir + '\\' + sourceName;
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
        public static void movef(Directories D, String FromName, String ToPath) { File.Move(D.curdir + '\\' + FromName, ToPath); }
        public static void movedir(Directories D, String FromName, String ToPath) { Directory.Move(D.curdir + '\\' + FromName, ToPath); }
        public static void find(Directories D, String mask)
        {
            FileInfo ln;
            int i = 0;
            var Options = Directory.GetFiles(D.curdir, mask, SearchOption.AllDirectories);
            while (i != Options.Length)
            {
                String path = Options[i];
                ln = new FileInfo(path);
                Console.WriteLine(Path.GetFileName(path) + ' ' + ln.Length / 1024 / 1024 + " Mbytes");
                i++;
                if (i % 10 == 0 && i != 0)
                {
                    Console.WriteLine("\nDisplay more? next/prev/exit");
                    switch (Console.ReadLine().Split(' ')[0])
                    {
                        case "prev":
                            if (i >= 20) i -= 21;
                            break;
                        case "exit":
                            return;
                    }
                }
                Console.ReadKey();
            }
        }
        public static void showdir(Directories D)
        {
            foreach (var dir in Directory.GetDirectories(D.curdir))
            {
                Console.WriteLine(dir);
            }
            Console.ReadKey();
        }
        public static void re(Directories D) { UiManager.draw_base(D, opt); }
        public static void prev(Directories D) { opt -= Console.WindowHeight - 5; UiManager.draw_base(D, opt); }
        public static void next(Directories D) { opt += Console.WindowHeight - 5; UiManager.draw_base(D, opt); }
        public static void help()
        {
            Console.WriteLine(
            @"Добро пожаловать в базовую имплементацию консольного файлового менеджера Текущие команды:
            exit - выход
            cd <dir> - сменить директорию. В качестве директории можно указать:
    	    - полное имя каталога;
    	    - собственное имя подкаталога, находящегося в текущем каталоге;
    	    - собственное имя каталога, находящегося на диске С;
    	    - '..' для выхода из каталога на один уровень вверх.
            md <name> - создание каталога с именем name.
            mf <name> - создание файла с именем name.
            rename <name1, name2> - переименование файла со старого имени на новое
            deldir <name> - удаление каталога name в текущей директории
            delfile <name> - удаление файла name в текущем каталоге
            rename <name1, name2> - переименовать файл
            rendir <name1, name2> - переименовать директорию
            copyf <name> - скопировать файл, использовать paste для вставки
            pastef <name> - вставить результат copyf
            copyff <source, destination> - скопировать через абсолютные пути (новое имя тоже нужно задать)
            copydir <name, destination> - скопировать директорию в каталоге в абсолютный путь
            movef <FromName, ToPath> - переместить файл
            movedir <FromName, ToPath> - переместить директорию
            find <mask> - поиск по маске, next/prev/exit для навигации
            showdirs - показать субкаталоги
            help - помощь
            prev/next - показать следующие/предыдущие папки
            re - принудительный вызов отрисовки
            чтобы выйти пропишите любую команду кроме help или нажмите клавишу
            ПРИМЕЧАНИЕ: Если при старте вы хотите попасть в директорию запуска менеджера(а не C:\) то напишите cd ..
            ");
            Console.ReadKey();
        }
    }
}
