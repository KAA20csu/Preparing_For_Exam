using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace File_Encoder
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the command: ");
            string[] currentCommand = Console.ReadLine().Split(' ', '-');
            var smthng = currentCommand.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            switch (smthng[1])
            {
                case"encode":
                    Encoding.ExecuteCommand(smthng);
                    break;
                case"decode":
                    Decoding.ExecuteCommand(smthng);
                    break;
            }
        }
        public static Source GetSource(string[] cmnd, string srs)
        {
            return srs == "buffer" ? Source.buffer : Source.source;
        }
    }
    class Encoding
    {
        public static void ExecuteCommand(string[] cmnd)
        {
            string file = File.ReadAllText($"{cmnd[5]}");
            var encoded = System.Text.Encoding.UTF8.GetBytes(file);
            if (Program.GetSource(cmnd, cmnd[3]) == Source.source)
            {
                File.WriteAllText($"{cmnd[5]}.txt", System.Convert.ToBase64String(encoded));
            }
            else Clipboard.SetText(System.Convert.ToBase64String(encoded));
        }
    }

    class Decoding
    {
        public static void ExecuteCommand(string[] cmnd)
        {
            string file = File.ReadAllText($"{cmnd[5]}");
            var decoded = System.Convert.FromBase64String(file);
            if (Program.GetSource(cmnd, cmnd[3]) == Source.source)
            {
                File.WriteAllText($"{cmnd[5]}.txt", System.Text.Encoding.UTF8.GetString(decoded));
            }
            else Clipboard.SetText(System.Text.Encoding.UTF8.GetString(decoded));
        }
    }
    enum Commands { encode, decode }
    enum Source { buffer, source }
}