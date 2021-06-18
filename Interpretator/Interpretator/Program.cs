using System.Collections.Generic;
using System.IO;
using System.Security.Policy;

namespace Interpretator
{
    class Program
    {
        public static void Main()
        {
            GetExpr();
            GetResult();
            System.Console.ReadKey();
        }
        private static string[] AllLines = File.ReadAllLines("expressions.txt");
        static void GetResult()
        {
            foreach(var rests in Expression.Variables)
            {
                System.Console.WriteLine(rests);
            }
        }
        static void GetExpr()
        {
            for (int i = 0; i < AllLines.Length; i++)
            {
                string[] current = AllLines[i].Split(new char[] { ' ', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (current[0].Contains(Commands.mov.ToString()))
                {
                    Expression.Variables.Add(current[1], int.Parse(current[2]));
                }
                else
                {
                    Counting(current, Expression.Variables);
                }
            }
        }
        static void Counting(string[] current, Dictionary<string, int> vars )
        {
            switch (current[0])
            {
                case"add":
                    new Add().Count(vars,current);
                    break;
                case"sub":
                    new Sub().Count(vars,current);
                    break;
                case"mul":
                    new Mul().Count(vars,current);
                    break;
                case"div":
                    new Div().Count(vars,current);
                    break;
                case "mov":
                    Expression.Variables.Add(current[1], int.Parse(current[3]));
                    break;
            }
        }
    }
    enum Commands {add, mul, sub, div, mov}

    abstract class Operations
    {
        public abstract double Count(Dictionary<string, int> vars, string[] current);
    }
    class Add : Operations
    {
        public override double Count(Dictionary<string, int> vars, string[] current)
        {
            if(int.TryParse(current[2], out int result))
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += int.Parse(current[2]);
                }
            }
            else
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += vars[current[2]];
                }
            }
            
            return 0;
        }
    }
    class Sub : Operations
    {
        public override double Count(Dictionary<string, int> vars, string[] current)
        {
            if (int.TryParse(current[2], out int result))
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += int.Parse(current[2]);
                }
            }
            else
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += vars[current[2]];
                }
            }

            return 0;
        }
    }
    class Mul : Operations
    {
        public override double Count(Dictionary<string, int> vars, string[] current)
        {
            if (int.TryParse(current[2], out int result))
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += int.Parse(current[2]);
                }
            }
            else
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += vars[current[2]];
                }
            }

            return 0;
        }
    }
    class Div : Operations
    {
        public override double Count(Dictionary<string, int> vars, string[] current)
        {
            if (int.TryParse(current[2], out int result))
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += int.Parse(current[2]);
                }
            }
            else
            {
                if (vars.ContainsKey(current[1]))
                {
                    return vars[current[1]] += vars[current[2]];
                }
            }

            return 0;
        }
    }
    class Expression
    {
        public static Dictionary<string, int> Variables { get; set; } = new Dictionary<string, int>();
    }
}