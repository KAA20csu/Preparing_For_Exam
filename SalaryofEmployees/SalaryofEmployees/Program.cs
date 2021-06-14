using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace SalaryofEmployees
{
    class Program
    {
        private static List<Employee> Employees { get; } = new List<Employee>();
        private static List<WorkPlace> WorkPlaces { get; } = new List<WorkPlace>();
        static void Main()
        {
            GetEmployeesData();
            CheckDirectors();
        }
        static void GetEmployeesData()
        {
            string[] information = File.ReadAllLines("sals.txt");
            for (int i = 0; i < information.Length; i++)
            {
                bool isDir;
                string[] current = information[i].Split(";");
                if (current.Length == 4)
                {
                     isDir = Convert.ToBoolean(current[3]);    
                }
                else
                {
                    isDir = false;
                }
                Employees.Add(new Employee(current[0], current[1], int.Parse(current[2]), isDir));
            }
            GetWorkPlaces();
        }
        static void GetWorkPlaces()
        {
            var groupedEmps = Employees.GroupBy(e => e.WorkPlace);
            foreach (var wEmp in groupedEmps)
            {
                WorkPlace wp = new WorkPlace(wEmp.Key);
                foreach (var emps in wEmp)
                {
                    wp.Workers.Add(emps);
                }
                WorkPlaces.Add(wp);
            }
            CheckDirectors();
        }
        static void CheckDirectors()
        {
            foreach (var crntWP in WorkPlaces)
            {
                var dircts = crntWP.Workers.Count(e => e.IsDirector == true);
                if (dircts > 2 || dircts < 1) throw new Exception("Некорректные данные!");
            }
            CountSalary();
            GetTheRichestHead();
        }

        static void CountSalary()
        {
            foreach (var crntWP in WorkPlaces)
            {
                var emps = crntWP.Workers.Where(e => e.IsDirector == false);
                string wpName = crntWP.Name;
               double avg = emps.Average(e => e.Salary);
               Console.WriteLine(wpName + " " + avg);
            }
        }
        static void GetTheRichestHead()
        {
            List<Employee> directors = new List<Employee>();
            foreach (var crntWP in WorkPlaces)
            {
                var emps = crntWP.Workers.Where(e => e.IsDirector == true);
                foreach (var drcts in emps)
                {
                    directors.Add(drcts);
                }
            }
            var richest = directors.Max(e=>e.Salary);
            Console.WriteLine("Самый высокооплачиваемый: "+ richest);
        }
    }
    class WorkPlace
    {
        public string Name { get; }
        public List<Employee> Workers { get; } = new List<Employee>();
        public WorkPlace(string name)
        {
            Name = name;
        }
    }
    class Employee
    {
        public string Name { get; }
        public string WorkPlace { get; }
        public int Salary { get; }
        public bool IsDirector { get; }

        public Employee(string name, string wp, int sal, bool isDirector)
        {
            Name = name;
            WorkPlace = wp;
            Salary = sal;
            IsDirector = isDirector;
        }
    }
}