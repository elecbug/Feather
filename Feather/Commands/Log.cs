using Feather.Extesions;
using Feather.Structs;
using System.IO.Compression;

namespace Feather.Commands
{
    public class Log
    {
        public Log(string[] args)
        {
            try
            {
                Program.GetWorkspace(Program.GetPath(""));
            }
            catch
            {
                Program.ConsoleReturn(Messages.FeatherNotFound, false);
                return;
            }

            if (args.Length == 2)
            {
                if (args[1] == Command.ALL_FLAG)
                {
                    string workspace = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", "INFO");
                    Info info = new Info(workspace);

                    int idx = (int)Math.Ceiling(Math.Log10(info.Maps.Count));

                    for (int i = 0; i < info.Maps.Count; i++)
                    {
                        Console.WriteLine($"[{info.Maps[i].Time}] {info.Maps[i].Index.ToString().PadLeft(idx)}: " +
                            $"\"{info.Maps[i].Name}\" by {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")}");
                    }
                }
                else
                {
                    string name = args[1];

                    string workspace = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", "INFO");
                    Info info = new Info(workspace);
                    List<Map> maps = new List<Map>();

                    for (int i = 0; i < info.Maps.Count; i++)
                    {
                        if (info.Maps[i].Name.ContainsLike(name))
                        {
                            maps.Add(info.Maps[i]);
                        }
                    }

                    int idx = (int)Math.Ceiling(Math.Log10(info.Maps.Count));

                    for (int i = 0; i < maps.Count; i++)
                    {
                        Console.WriteLine($"[{maps[i].Time}] {maps[i].Index.ToString().PadLeft(idx)}: " +
                            $"\"{maps[i].Name}\" by {(maps[i].Parent != -1 ? maps[i].Parent.ToString() : "ROOT")}");
                    }
                }
            }
            else if (args.Length == 3)
            {
                if (args[1] == Command.INDEX_FLAG)
                {
                    int index;
                    
                    if (!int.TryParse(args[2], out index))
                    {
                        Program.ConsoleReturn(Messages.InvalidCommand, false);
                        return;
                    }

                    string tempDir = Path.Combine(Path.GetTempPath(), "temp");

                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }

                    string zip = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", index.ToString());

                    if (!File.Exists(zip))
                    {
                        Program.ConsoleReturn(Messages.CommitNotFound, false);
                        return;
                    }

                    ZipFile.ExtractToDirectory(zip, tempDir);

                    DirectoryInfo info = new DirectoryInfo(tempDir);

                    if (info.Exists)
                    {
                        Console.WriteLine(new DirectoryInfo(Program.GetWorkspace(Program.GetPath(""))).Name);
                        ShowDir(info, 1, new List<int>());

                        Directory.Delete(tempDir, true);
                    }
                    else
                    {
                        Console.WriteLine("This commit is empty");
                    }
                }
            }
            else
            {
                Program.ConsoleReturn(Messages.InvalidCommand, false);
                return;
            }
        }

        private void ShowDir(DirectoryInfo info, int whitespace, List<int> emptyLine)
        {
            DirectoryInfo[] dirs = info.GetDirectories();

            for (int i = 0; i < dirs.Length; i++)
            {
                for (int w = 0; w < whitespace - 1; w++)
                {
                    Console.Write((emptyLine.Where(x => x == w).Count() != 0 ? " " : "│"));
                }
                if (info.GetFiles().Length == 0)
                {
                    Console.Write("└");
                    emptyLine.Add(whitespace - 1);
                }
                else
                {
                    Console.Write("├");
                }

                Console.WriteLine(dirs[i].Name + "/");
                ShowDir(dirs[i], whitespace + 1, new List<int>(emptyLine));
            }

            FileInfo[] files = info.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                for (int w = 0; w < whitespace - 1; w++)
                {
                    Console.Write((emptyLine.Where(x => x == w).Count() != 0 ? " " : "│"));
                }
                if (i == files.Length - 1)
                    Console.Write("└");
                else
                    Console.Write("├");

                Console.WriteLine(files[i].Name);
            }
        }
    }
}