using System;
using System.Collections.Generic;
using System.IO;

namespace GenTree
{
    class Program
    {
        private static List<Person> Obj { get; } = new List<Person>();
        static void Main()
        {
            GetPeopleList();
            Console.WriteLine("Введите данные первого человека:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите данные второго человека:");
            int id2 = int.Parse(Console.ReadLine());
            Console.WriteLine(GetRelatess(id,id2));
        }
        static string GetRelatess(int id, int id2)
        {
            foreach (var c in Obj)
            {
                if (id == c.Id)
                {
                    foreach (var b in c.PrsnRlts)
                    {
                        if (id2 == b.Second.Id)
                        {
                            return c.PrsnRlts[1].RltType.ToString();
                        }
                    }
                }
            }
            return "";
        }
        static void GetPeopleList()
        {
            string[] allPeople = File.ReadAllLines("Info.txt"); // Вся инфа
            int i = 1;
            for(; i<allPeople.Length; i++)
            {
                if(allPeople[i] != "")
                {
                    string[] current = allPeople[i].Split(";");
                    Obj.Add(new Person(int.Parse(current[0]), current[1], current[2], current[3]));
                    // 1 Иванов Иван 19.05.1974 и т.п
                }
                else
                {
                    i++;
                    break; // Парсим до пустой строки, считывая каждого человека. Добавляем в список.
                }
            }
            GetRelates(i, allPeople);
        }
        static void GetRelates(int i, string[] allPeople) // Чекаем всё ниже пустой строки (отношения)
        {
            for(;i<allPeople.Length; i++)
            {
                string[] ids = allPeople[i].Split("<->");
                string[] relates = ids[1].Split("=");
                // 1 2 sibling и т.д  
                int fPrsn = int.Parse(ids[0]);
                int sPrsn = int.Parse(relates[0]);
                string relType = relates[1];

                PersonRelates rlates = new PersonRelates();
                rlates.RltType = GetRelationType(relType);
                foreach (var crntPerson in Obj)
                {
                    if (crntPerson.Id == fPrsn)
                    {
                        rlates.First = crntPerson;
                        crntPerson.PrsnRlts.Add(rlates);
                    }
                    else if (crntPerson.Id == sPrsn)
                    {
                        rlates.Second = crntPerson;
                        crntPerson.PrsnRlts.Add(rlates);
                    }
                }
            }
        }
        private static RelationType GetRelationType(string relation)
        {
            return relation == "spouse"
                ? RelationType.spouse
                : relation == "sibling"
                    ? RelationType.sibling
                    : RelationType.parent;
        }
    }
    enum RelationType { sibling, spouse, parent}
    class PersonRelates
    {
        public Person First;
        public Person Second;
        public RelationType RltType;
    }
    class Person 
    {
        public int Id { get; }
        private string LastName { get; }
        private string FirstName { get; }
        private string BirthDate { get; }
        public List<PersonRelates> PrsnRlts { get; private set; } = new List<PersonRelates>();
        public Person(int id, string lName, string fName, string bDate)
        {
            Id = id;
            LastName = lName;
            FirstName = fName;
            BirthDate = bDate;
        }
    }
}
