using System.IO.Compression;
using Feather.Structs;

namespace Feather.Commands
{
    public class Commit
    {
        public Commit(string[] args)
        {
            if (args.Length == 2) 
            {
                string workspace = Program.GetWorkspace(Program.GetPath(""));
                string name = args[1];

                Info info = new Info(Path.Combine(workspace, ".feather", "INFO"));

                Map map = new Map()
                {
                    Name = name,
                    Index = info.Last + 1,
                    Parent = info.Current,
                };

                info.Maps.Add(map);
                info.Last = map.Index;
                info.Current = map.Index;

                try
                {
                    string tempDir = Path.Combine(Path.GetTempPath(), ".feather");

                    info.Save(Path.Combine(workspace, ".feather", "INFO"));

                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }

                    Directory.Move(Path.Combine(workspace, ".feather"), tempDir);

                    ZipFile.CreateFromDirectory(workspace, 
                        Path.Combine(Path.GetTempPath(), ".feather", map.Index.ToString()));

                    Directory.Move(tempDir, Path.Combine(workspace, ".feather"));
                    
                    Program.ConsoleReturn(Messages.SuccessCommit, true);
                    return;
                }
                catch 
                {
                    Program.ConsoleReturn(Messages.FeatherNotFound, false);
                    return;
                }
            }
            else if (args.Length == 4)
            {
                string name;
                int parent;

                if (args[1] == Command.PARENT_FLAG)
                {
                    name = args[3];

                    if (!int.TryParse(args[2], out parent))
                    {
                        Program.ConsoleReturn(Messages.InvalidCommand, false);
                        return;
                    }
                }
                else if (args[2] == Command.PARENT_FLAG)
                {
                    name = args[1];

                    if (!int.TryParse(args[3], out parent))
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

                Info info = new Info(Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", "INFO"));

                if (parent > info.Last)
                {
                    Program.ConsoleReturn(Messages.CommitNotFound, false);
                    return;
                }

                Map map = new Map()
                {
                    Name = name,
                    Index = info.Last + 1,
                    Parent = parent,
                };

                info.Maps.Add(map);
                info.Last = map.Index;
                info.Current = map.Index;

                try
                {
                    string workspace = Program.GetWorkspace(Program.GetPath(""));
                    string tempDir = Path.Combine(Path.GetTempPath(), ".feather");

                    info.Save(Path.Combine(workspace, ".feather", "INFO"));

                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }

                    Directory.Move(Path.Combine(workspace, ".feather"), tempDir);

                    ZipFile.CreateFromDirectory(workspace,
                        Path.Combine(Path.GetTempPath(), ".feather", map.Index.ToString()));

                    Directory.Move(tempDir, Path.Combine(workspace, ".feather"));

                    Program.ConsoleReturn(Messages.SuccessCommit, true);
                    return;
                }
                catch
                {
                    Program.ConsoleReturn(Messages.FeatherNotFound, false);
                    return;
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