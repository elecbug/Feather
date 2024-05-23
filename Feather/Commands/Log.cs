using Feather.Structs;

namespace Feather.Commands
{
    public class Log
    {
        public Log(string[] args)
        {
            if (args.Length == 2)
            {
                if (args[1] == Command.ALL_FLAG)
                {
                    string workspace = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", "INFO");
                    Info info = new Info(workspace);

                    int idx = (int)Math.Ceiling(Math.Log10(info.Maps.Count));

                    for (int i = 0; i < info.Maps.Count; i++)
                    {
                        Console.WriteLine($"{info.Maps[i].Index.ToString().PadLeft(idx)}: {info.Maps[i].Name} {info.Maps[i].Time}");
                    }
                }
                else
                {

                }
            }
            else
            {
                Program.ConsoleReturn(Messages.InvalidCommand, false);
                return;
            }
        }
    }
}