﻿using Feather.Structs;
using System.IO.Compression;
using System.Text.Json;

namespace Feather.Commands
{
    public class Init
    {
        public Init(string[] args)
        {
            if (args.Length == 1)
            {
                CreateFeatherRepository(Environment.CurrentDirectory);
            }
            else if (args.Length == 2)
            {
                CreateFeatherRepository(Program.GetPath(args[1]));
            }
            else
            {
                Program.ConsoleReturn(Messages.InvalidCommand, false);
                return;
            }
        }

        private void CreateFeatherRepository(string dirPath)
        {
            // Console.WriteLine(new DirectoryInfo(dirPath).FullName);

            try
            {
                Program.GetWorkspace(dirPath);

                Program.ConsoleReturn(Messages.AlreadyFeather, false);
                return;
            }
            catch
            {
                if (!Directory.Exists(dirPath))
                {
                    Program.ConsoleReturn(Messages.InitDirNotFound, false);
                    return;
                }
                else
                {
                    ZipFile.CreateFromDirectory(dirPath, Path.Combine(Path.GetTempPath(), "0"));

                    DirectoryInfo dirInfo = Directory.CreateDirectory(Path.Combine(dirPath, ".feather"));
                    dirInfo.Attributes |= FileAttributes.Hidden;

                    File.Move(Path.Combine(Path.GetTempPath(), "0"), Path.Combine(dirPath, ".feather", "0"));

                    new Info(Path.Combine(dirPath, ".feather", "INFO"));

                    Program.ConsoleReturn(Messages.SuccessInit, true);
                    return;
                }
            }
        }
    }
}