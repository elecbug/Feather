using Feather.Structs;
using System.IO.Compression;

namespace Feather.Commands
{
    public class Pull
    {
        public Pull(string[] args)
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

                    Info info = new Info(Path.Combine(workspace, ".feather", "INFO"));

                    if (index > info.Last)
                    {
                        Program.ConsoleReturn(Messages.CommitNotFound, false);
                        return;
                    }

                    info.Current = index;

                    info.Save(Path.Combine(workspace, ".feather", "INFO"));

                    string beforeCurrent = Environment.CurrentDirectory;
                    Environment.CurrentDirectory = Program.GetWorkspace(Program.GetPath(""));

                    DirectoryInfo dirInfo = new DirectoryInfo(Program.GetWorkspace(Program.GetPath("")));
                    
                    foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                    {
                        if (dir.Name == ".feather")
                            continue;
                        else 
                            dir.Delete(true);
                    }
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    string zip = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", index.ToString());
                    ZipFile.ExtractToDirectory(zip, Environment.CurrentDirectory);

                    try
                    {
                        Environment.CurrentDirectory = beforeCurrent;
                    }
                    catch
                    {
                        Environment.CurrentDirectory = Program.GetWorkspace(Program.GetPath(""));
                    }

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

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
    }
}