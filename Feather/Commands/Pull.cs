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

                    string tempDir = Path.Combine(Path.GetTempPath(), new DirectoryInfo(workspace).Name);

                    Info info = new Info(Path.Combine(workspace, ".feather", "INFO"));

                    if (index > info.Last)
                    {
                        Program.ConsoleReturn(Messages.CommitNotFound, false);
                        return;
                    }

                    int before = info.Current;
                    info.Current = index;

                    info.Save(Path.Combine(workspace, ".feather", "INFO"));

                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                        Directory.CreateDirectory(Path.Combine(tempDir, ".feather"));
                    }

                    string zip = Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather", index.ToString());
                    ZipFile.ExtractToDirectory(zip, tempDir);

                    CopyFilesRecursively(Path.Combine(Program.GetWorkspace(Program.GetPath("")), ".feather"), Path.Combine(tempDir, ".feather"));

                    try
                    {
                        DirectoryInfo[] dirs = new DirectoryInfo(workspace).GetDirectories();

                        Directory.Move(tempDir, workspace);

                        Program.ConsoleReturn(Messages.SuccessPull, true);
                        return;
                    }
                    catch
                    {
                        Program.ConsoleReturn(Messages.FailPull, false);
                        return;
                    }

                    //Directory.Move(Path.Combine(workspace, ".feather"), tempDir);

                    //DirectoryInfo[] dirs = new DirectoryInfo(workspace).GetDirectories();
                    //for (int i = 0; i < dirs.Length; i++)
                    //{
                    //    dirs[i].Delete(true);
                    //}

                    //FileInfo[] files = new DirectoryInfo(workspace).GetFiles();
                    //for (int i = 0; i < files.Length; i++)
                    //{
                    //    files[i].Delete();
                    //}

                    //string zip = Path.Combine(tempDir, index.ToString());

                    //ZipFile.ExtractToDirectory(zip, workspace);

                    //Directory.Move(tempDir, Path.Combine(workspace, ".feather"));

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