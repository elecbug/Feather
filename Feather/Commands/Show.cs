using Feather.Structs;
using System.IO.Compression;

namespace Feather.Commands
{
    public class Show
    {
        public Show(string[] args)
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
                int index;
                string file;

                if (args[1] == Command.INDEX_FLAG)
                {
                    if (!int.TryParse(args[2], out index))
                    {
                        Program.ConsoleReturn(Messages.InvalidCommand, false);
                        return;
                    }

                    file = args[3];
                }
                else if (args[2] == Command.INDEX_FLAG)
                {
                    if (!int.TryParse(args[3], out index))
                    {
                        Program.ConsoleReturn(Messages.InvalidCommand, false);
                        return;
                    }

                    file = args[1];
                }
                else
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

                try
                {
                    using (StreamReader stream = new StreamReader(Path.Combine(tempDir, file)))
                    {
                        Console.WriteLine(stream.ReadToEnd());
                    }
                }
                catch
                {
                    Program.ConsoleReturn(Messages.FileNotFound, false);
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