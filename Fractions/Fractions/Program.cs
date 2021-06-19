using System;
using System.Collections.Generic;

namespace Fractions
{
    //3/4+6/7
    class Program
    {
        private static char[] operators { get; } = new char[] {'+', '-', ':', '*'};
        static void Main(string[] args)
        {
            char op;
            Console.WriteLine("Введите выражение: ");
            string expression = Console.ReadLine();
            string[] fractions = expression.Split(operators);
            var fract1 = new Fraction(fractions[0]);
            var fract2 = new Fraction(fractions[1]);
            Console.WriteLine(ChooseOperation(fract1, fract2,expression).ToString());
        }
        static Fraction ChooseOperation(Fraction one, Fraction two, string expression)
        {
            if (expression.Contains("+"))
                return one + two;
            else if (expression.Contains("-"))
                return one - two;
            else if (expression.Contains("*"))
                return one * two;
            else if (expression.Contains(":"))
                return one / two;
            return one;
        }
    }
    class Fraction
    {
        private int Numerator { get; }
        private int Denominator { get; }
        public Fraction(string fraction)
        {
            string[] crntFraction = fraction.Split(new char[] { '/' });
            this.Numerator = int.Parse(crntFraction[0]);
            this.Denominator = int.Parse(crntFraction[1]);
        }
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
        public static Fraction operator +(Fraction first, Fraction second)
        {
            int num=0; int denum=0;
            if (first.Denominator != second.Denominator)
            {
                num = first.Numerator * second.Denominator + second.Numerator * first.Denominator;
                denum = first.Denominator * second.Denominator;
            }
            else
            {
                num = first.Numerator + second.Numerator;
                denum = first.Denominator;
            }
            return new Fraction($"{num}/{denum}");
        }
        public static Fraction operator -(Fraction first, Fraction second)
        {
            int num=0; int denum=0;
            if (first.Denominator != second.Denominator)
            {
                num = first.Numerator * second.Denominator - second.Numerator * first.Denominator;
                denum = first.Denominator * second.Denominator;
            }
            else
            {
                num = first.Numerator - second.Numerator;
                denum = first.Denominator;
            }
            return new Fraction($"{num}/{denum}");
        }
        public static Fraction operator *(Fraction first, Fraction second)
        {
            int num=0; int denum=0;
            num = first.Numerator * second.Numerator;
            denum = first.Denominator * second.Denominator;
            return new Fraction($"{num}/{denum}");
        }
        public static Fraction operator /(Fraction first, Fraction second)
        {
            int num=0; int denum=0;
            num = first.Numerator * second.Denominator;
            denum = first.Denominator * second.Numerator;
            return new Fraction($"{num}/{denum}");
        }
    }
    
}