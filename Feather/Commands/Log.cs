using Feather.Extesions;
using Feather.Resources;
using Feather.Structs;
using System;
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
                            $"\"{info.Maps[i].Name}\" from {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")} " +
                            $"{(info.Maps[i].D ? "[DELETED]" : "")}");
                    }

                    Console.WriteLine();
                }
                else
                {
                    string name = args[1];

                    string workspace = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", "INFO");
                    Info info = new Info(workspace);
                    List<Map> maps = new List<Map>();

                    for (int i = 0; i < info.Maps.Count; i++)
                    {
                        if (info.Maps[i]!.Name.ContainsLike(name))
                        {
                            maps.Add(info.Maps[i]!);
                        }
                    }

                    int idx = (int)Math.Ceiling(Math.Log10(info.Maps.Count));

                    for (int i = 0; i < maps.Count; i++)
                    {
                        string tempDir = Path.Combine(Path.GetTempPath(), "temp");

                        if (Directory.Exists(tempDir))
                        {
                            Directory.Delete(tempDir, true);
                        }

                        string zip = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", i.ToString());

                        if (!File.Exists(zip) && info.Maps.FirstOrDefault(x => x.Index == i) != null)
                        {
                            Console.WriteLine($"[{info.Maps[i].Time}] {info.Maps[i].Index.ToString().PadLeft(idx)}: " +
                                $"\"{info.Maps[i].Name}\" from {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")} " +
                                $"{(info.Maps[i].D ? "[DELETED]" : "")}");
                        }
                        else
                        {
                            ZipFile.ExtractToDirectory(zip, tempDir);

                            DirectoryInfo dirInfo = new DirectoryInfo(tempDir);

                            if (dirInfo.Exists)
                            {
                                Console.WriteLine($"[{info.Maps[i].Time}] {info.Maps[i].Index.ToString().PadLeft(idx)}: " +
                                    $"\"{info.Maps[i].Name}\" from {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")} " +
                                    $"{(info.Maps[i].D ? "[DELETED]" : "")}");

                                ShowDir(dirInfo, 1, new List<int>());

                                Directory.Delete(tempDir, true);
                            }
                            else
                            {
                                Console.WriteLine($"[{info.Maps[i].Time}] {info.Maps[i].Index.ToString().PadLeft(idx)}: " +
                                    $"\"{info.Maps[i].Name}\" from {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")} " +
                                    $"{(info.Maps[i].D ? "[DELETED]" : "")}");

                                Console.WriteLine(Messages.CommitEmpty);
                            }
                        }
                    }

                    Console.WriteLine();
                }
            }
            else if (args.Length == 3)
            {
                if (args[1] == Command.INDEX_FLAG)
                {
                    int index;
                    int to;

                    if (!int.TryParse(args[2], out index))
                    {
                        if (args[2].Contains('~'))
                        {
                            string[] indexes = args[2].Split('~');

                            if (indexes.Length != 2 ||
                                !int.TryParse(indexes[0], out index) || !int.TryParse(indexes[1], out to))
                            {
                                Program.ConsoleReturn(Messages.InvalidCommand, false);
                                return;
                            }
                        }
                        else
                        {
                            Program.ConsoleReturn(Messages.InvalidCommand, false);
                            return;
                        }
                    }
                    else
                    {
                        to = index;
                    }

                    string workspace = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", "INFO");
                    Info info = new Info(workspace);
                    int idx = (int)Math.Ceiling(Math.Log10(info.Maps.Count));

                    for (int i = index; i < to + 1; i++)
                    {
                        string tempDir = Path.Combine(Path.GetTempPath(), "temp");

                        if (Directory.Exists(tempDir))
                        {
                            Directory.Delete(tempDir, true);
                        }

                        string zip = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", i.ToString());

                        if (!File.Exists(zip) && info.Maps.FirstOrDefault(x=>x.Index == i) != null)
                        {
                            Console.WriteLine($"[{info.Maps[i].Time}] {info.Maps[i].Index.ToString().PadLeft(idx)}: " +
                                $"\"{info.Maps[i].Name}\" from {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")} " +
                                $"{(info.Maps[i].D ? "[DELETED]" : "")}");
                        }
                        else
                        {
                            ZipFile.ExtractToDirectory(zip, tempDir);

                            DirectoryInfo dirInfo = new DirectoryInfo(tempDir);

                            if (dirInfo.Exists)
                            {
                                Console.WriteLine($"[{info.Maps[i].Time}] {info.Maps[i].Index.ToString().PadLeft(idx)}: " +
                                    $"\"{info.Maps[i].Name}\" from {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")} " +
                                    $"{(info.Maps[i].D ? "[DELETED]" : "")}");

                                ShowDir(dirInfo, 1, new List<int>());

                                Directory.Delete(tempDir, true);
                            }
                            else
                            {
                                Console.WriteLine($"[{info.Maps[i].Time}] {info.Maps[i].Index.ToString().PadLeft(idx)}: " +
                                    $"\"{info.Maps[i].Name}\" from {(info.Maps[i].Parent != -1 ? info.Maps[i].Parent.ToString() : "ROOT")} " +
                                    $"{(info.Maps[i].D ? "[DELETED]" : "")}");

                                Console.WriteLine(Messages.CommitEmpty);
                            }
                        }
                    }

                    Console.WriteLine();
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