using System;
using System.Collections.Generic;
using System.IO;

namespace FileComparer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the path of first file: ");
            File firstFile = new File(System.IO.File.ReadAllLines(Console.ReadLine()));
            Console.WriteLine("Enter the path of second file: ");
            File secondFile = new File(System.IO.File.ReadAllLines(Console.ReadLine()));
            if (CompareSize(firstFile, secondFile))
            {
                CheckForDeletedOrAddedLines(firstFile.FileLines, secondFile.FileLines); 
                IsEdited(firstFile.FileLines, secondFile.FileLines);
            } 
            else IsEdited(firstFile.FileLines, secondFile.FileLines);

            Console.ReadKey();
        }
        static bool CompareSize(File first, File second) // Метка о том, есть ли вообще удалённые/добавленные строки
        {
            if (first.FileLines.Length > second.FileLines.Length || first.FileLines.Length < second.FileLines.Length)
                return true;
            return false;
        }
        static void IsEdited(string[] first, string[] second)
        {
            for (int i = 0; i < first.Length && i < second.Length; i++)
            {
                if (first[i].Equals(second[i]) || second[i].Equals(first[i]))
                {
                    Console.WriteLine(first[i]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(second[i]);
                    Console.ResetColor();
                }
                else
                {
                    if (!first[i].Equals(second[i]))
                    {
                        Console.WriteLine(second[i]);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(first[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(first[i]);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(second[i]);
                        Console.ResetColor();
                    }
                }
            }
        }
        static void CheckForDeletedOrAddedLines(string[] first, string[] second) // Если количество строк файлов различается,
                                                                                 // проверяем на удалённые/добавленные
        {
            for (int i = 0; i < first.Length && i < second.Length; i++)
            {
                if (first[i] == "" || second[i] == "")
                {
                    if (first[i] == "")
                    {
                        Console.WriteLine(second[i]);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(first[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(first[i]);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(second[i]);
                        Console.ResetColor();
                    }
                }
            }
        }
    }
    class File
    {
        public string[] FileLines { get; } = new string[] { };

        public File(string[] lines)
        {
            FileLines = lines;
        }
    }
}