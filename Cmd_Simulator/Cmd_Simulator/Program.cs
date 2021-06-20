using System;
using System.IO;
using System.Collections.Generic;

namespace Cmd_Simulator
{
    class Program
    {
        static void Main()
        {
            string currentPath = Directory.GetCurrentDirectory();
            DirectoryInfo crnt = new DirectoryInfo(currentPath);
            var folders = crnt.GetDirectories();
            var files = crnt.GetFiles();
            Command.GetFiles(files);
            Command.GetFolders(folders);
            Console.WriteLine("Write command: ");
            string[] command = Console.ReadLine().Split(" ");
            if (command[0] == Commands.Cd.ToString() && command[1] == ".")
            {
                new CdCommand().ExecuteCommand(crnt);
            }
            if (command[0] == Commands.New.ToString())
            {
                var newFile = new NewCommand(command[1]);
                newFile.ExecuteCommand(crnt);
            }
            if (command[0] == Commands.Delete.ToString())
            {
                var newFile = new DeleteCommand(command[1]);
                newFile.ExecuteCommand(crnt);
            }
        }
    }
    enum  Commands
    {
        Cd, 
        New,
        Delete
    }
    abstract class Command
    {
        public abstract void ExecuteCommand(DirectoryInfo files);
        public static void GetFiles(FileInfo[] files)
        {
            foreach (var dir in files)
            {
                Console.WriteLine(dir.Name);
            }
        }
        public static void GetFolders(DirectoryInfo[] folders)
        {
            foreach (var folder in folders)
            {
                Console.WriteLine(folder.Name);
            }
        }
    }
    class CdCommand : Command
    {
        public override void ExecuteCommand(DirectoryInfo files)
        {
            files = files.Parent;
            var filess = files.GetFiles();
            var folders = files.GetDirectories();
            Command.GetFiles(filess);
            Command.GetFolders(folders);
        }
    }
    class NewCommand : Command
    {
        public string Name { get; }

        public NewCommand(string name)
        {
            Name = name;
        }
        public override void ExecuteCommand(DirectoryInfo files)
        {
            File.Create($"{files}/{Name}");
        }
    }
    class DeleteCommand : Command
    {
        public string Name { get; }

        public DeleteCommand(string name)
        {
            Name = name;
        }
        public override void ExecuteCommand(DirectoryInfo files)
        {
            File.Delete($"{files}/{Name}");
        }
    }
}