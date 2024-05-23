using Feather.Structs;
using System.IO.Compression;

namespace Feather.Commands
{
    public class Get
    {
        public Get(string[] args)
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

            if (args.Length == 4)
            {
                if (args[1] == Command.INDEX_FLAG)
                {
                    string workspace = Program.GetWorkspace(Program.GetPath(""));
                    string path = Program.GetPath(args[3]);
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

                    if (Directory.Exists(path))
                    {
                        try
                        {
                            Directory.Delete(path, true);
                        }
                        catch
                        {
                            Program.ConsoleReturn(Messages.FailPull, false);
                            return;
                        }
                    }

                    Directory.CreateDirectory(path);
                    Directory.CreateDirectory(Path.Combine(path, ".feather"));

                    string zip = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", index.ToString());
                    ZipFile.ExtractToDirectory(zip, path);

                    CopyFilesRecursively(Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather"), Path.Combine(path, ".feather"));

                    DirectoryInfo infoFeather = new DirectoryInfo(Path.Combine(path, ".feather"));
                    infoFeather.Attributes |= FileAttributes.Hidden;

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