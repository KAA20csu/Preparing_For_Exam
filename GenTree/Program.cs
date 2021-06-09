using System;
using System.Collections.Generic;
using System.IO;

namespace GenTree
{
    class Program
    {
        private static List<Person> Obj { get; } = new List<Person>();
        private static List<Relation> Relations { get; } = new List<Relation>();
        static void Main()
        {
            GetPeopleList();
            Console.WriteLine("Введите Id первого человека:");
            string frst = Console.ReadLine();
            Console.WriteLine("Введите Id второго человека:");
            string scnd = Console.ReadLine();
            Console.WriteLine(CheckRelates(frst,scnd));
        }
        static void GetPeopleList()
        {
            string[] allPeople = File.ReadAllLines("Info.txt");

            int i = 1;
            for(; i<allPeople.Length; i++)
            {
                if(allPeople[i] != "")
                {
                    string[] current = allPeople[i].Split(";");
                    Obj.Add(new Person(int.Parse(current[0]), current[1], current[2], current[3]));
                }
                else
                {
                    i++;
                    break;
                }
            }
            GetRelates(i, allPeople);
        }
        static string CheckRelates(string firstId, string secondId)
        {
            foreach(var relations in Relations)
            {
                if(int.Parse(firstId) == relations.FirstId && int.Parse(secondId) == relations.SecondId)
                {
                    return relations.Relate;
                }
            }
            return "Неопределённые отношения";
        }
        static void GetRelates(int i, string[] allPeople)
        {
            for (; i < allPeople.Length; i++)
            {
                string[] ids = allPeople[i].Split("<->");
                string[] relates = ids[1].Split("=");
                Relations.Add(new Relation(int.Parse(ids[0]), int.Parse(relates[0]), relates[1]));
            }
        }
    }
    class Person 
    {
        private int Id { get; }
        private string LastName { get; }
        private string FirstName { get; }
        private string BirthDate { get; }
        public Person(int id, string lName, string fName, string bDate)
        {
            Id = id;
            LastName = lName;
            FirstName = fName;
            BirthDate = bDate;
        }
    }
    
    class Relation
    {
        public enum Relations { parent, spouse, sibling }
        public int FirstId { get; }
        public int SecondId { get; }
        public string Relate { get; }
        public Relation(int first, int second, string relate)
        {
            FirstId = first;
            SecondId = second;
            Relate = relate;
        }

    }
}
