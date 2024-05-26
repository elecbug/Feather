using Feather.Resources;
using Feather.Structs;
using System.IO.Compression;

namespace Feather.Commands
{
    public class Del
    {
        public Del(string[] args)
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

                    Console.Write("Delete the version " + index + "? [y/N] ");

                    if (Console.ReadLine()!.ToLower() != "y")
                    {
                        return;
                    }

                    Info info = new Info(Path.Combine(workspace, ".feather", "INFO"));

                    if (index > info.Last)
                    {
                        Program.ConsoleReturn(Messages.CommitNotFound, false);
                        return;
                    }

                    if (index == info.Current)
                    {
                        Program.ConsoleReturn(Messages.DoNotDelCurrent, false);
                        return;
                    }

                    try
                    {
                        int foundIndex = info.Maps.FindIndex(x => x != null && x.Index == index);
                        int parent = info.Maps[foundIndex]!.Parent;

                        foreach (Map x in info.Maps.Where(x => x != null && x.Parent == foundIndex).ToList())
                        {
                            x!.Parent = parent;
                        }

                        info.Maps[foundIndex].D = true;
                    }
                    catch
                    {
                        Program.ConsoleReturn(Messages.CommitNotFound, false);
                        return;
                    }

                    info.Save(Path.Combine(workspace, ".feather", "INFO"));

                    string featherVer = Path.Combine(workspace, ".feather", index.ToString());
                    File.Delete(featherVer);

                    Program.ConsoleReturn(Messages.SuccessDelete, true);
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