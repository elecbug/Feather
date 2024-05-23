using Feather.Commands;

namespace Feather
{
    public class Program
    {
        public static int Result { get; set; } = 0;

        public static int Main(string[] args)
        {
            switch (args[0].ToLower())
            {
                case Command.HELP: new Help(args); break;
                case Command.INIT: new Init(args); break;
                case Command.DEL: new Del(args); break;
                case Command.PULL: new Pull(args); break;
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
    }
}