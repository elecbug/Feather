using System.IO.Compression;

namespace Feather.Commands
{
    public class Commit
    {
        public Commit(string[] args)
        {
            if (args.Length == 2) 
            {
                string name = args[1];

                Info info = new Info(Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", "INFO"));

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
                }
                catch 
                {
                    Program.ConsoleReturn(Messages.FeatherNotFound, false);
                }
                finally
                {
                    Program.ConsoleReturn(Messages.SuccessCommit, true);
                }
            }
            else
            {
                Program.ConsoleReturn(Messages.InvalidCommand, false);
            }
        }
    }
}