using Feather.Structs;
using System.IO.Compression;

namespace Feather.Commands
{
    public class Pull
    {
        public Pull(string[] args)
        {
            if (args.Length == 3)
            {
                if (args[1] == Command.INDEX_FLAG)
                {
                    string workspace = Program.GetWorkspace(Program.GetPath(""));
                    int index;

                    if (!int.TryParse(args[2], out index))
                    {
                        Program.ConsoleReturn(Messages.InvalidCommand, false);
                        return;
                    }
                    string tempDir = Path.Combine(Path.GetTempPath(), ".feather");

                    Info info = new Info(Path.Combine(workspace, ".feather", "INFO"));

                    if (index > info.Last)
                    {
                        Program.ConsoleReturn(Messages.CommitNotFound, false);
                        return;
                    }

                    info.Current = index;

                    info.Save(Path.Combine(workspace, ".feather", "INFO"));

                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }

                    Directory.Move(Path.Combine(workspace, ".feather"), tempDir);

                    DirectoryInfo[] dirs = new DirectoryInfo(workspace).GetDirectories();
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        dirs[i].Delete(true);
                    }

                    FileInfo[] files = new DirectoryInfo(workspace).GetFiles();
                    for (int i = 0; i < files.Length; i++)
                    {
                        files[i].Delete();
                    }

                    string zip = Path.Combine(tempDir, index.ToString());

                    ZipFile.ExtractToDirectory(zip, workspace);

                    Directory.Move(tempDir, Path.Combine(workspace, ".feather"));

                    Program.ConsoleReturn(Messages.SuccessPull, true);
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