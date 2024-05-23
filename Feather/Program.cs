using Feather.Commands;

namespace Feather
{
    public class Program
    {
        public static int Result { get; set; } = 0;

        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                ConsoleReturn("ver 0.0.0", true);
                return Result;
            }

            switch (args[0].ToLower())
            {
                case Command.HELP: new Help(args); break;
                case Command.INIT: new Init(args); break;
                case Command.DEL: new Del(args); break;
                case Command.GET: new Get(args); break;
                case Command.SHOW: new Show(args); break;
                case Command.COMMIT: new Commit(args); break;
                case Command.LOG: new Log(args); break;
            }

            return Result;
        }

        public static string GetPath(string path)
        {
            return new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, path)).FullName;
        }

        public static void ConsoleReturn(string message, bool successed)
        {
            Console.WriteLine(message);
            Result = successed ? 0 : -1;
        }

        public static string GetWorkspace(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);

            DirectoryInfo? get = info.GetDirectories().FirstOrDefault(x => x.Name == ".feather");

            if (get != null)
            {
                return info.FullName;
            }
            else
            {
                if (info.Parent != null)
                {
                    return GetWorkspace(info.Parent.FullName);
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}