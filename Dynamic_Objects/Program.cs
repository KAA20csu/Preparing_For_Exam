using System;
using System.Collections.Generic;
using System.IO;

namespace Dynamic_Objects
{
    class Program
    {
        public static List<ClassInfo> ClInfo { get; set; } = new List<ClassInfo>();
        public static List<Property> PrInfo { get; set; } = new List<Property>();
        public static string[] classInfo { get; } = File.ReadAllLines("Classes.txt");
        public static string[] PropInfo { get; } = File.ReadAllLines("Properties.txt");
        public static string[] Values { get; } = File.ReadAllLines("Objects.txt");
        public static List<Obj> Objects { get; set; } = new List<Obj>();
        static void Main()
        {
            GetInformation();
            GetProperties();
            int clId = 0;
            Dictionary<int, string> properts = null; ;
            for (int i = 1; i < Values.Length; i++)
            {
                foreach(var classes in ClInfo)
                {
                    clId = classes.Id;
                    foreach(var properties in PrInfo)
                    {
                        properts = properties.PropSet;
                    }
                }
                string[] information = Values[i].Split(";");
                Obj obj1 = new Obj(clId, i, properts, information[3]);
                Objects.Add(obj1);

            }

        }
        static void GetInformation()
        {
            for(int i = 1; i<classInfo.Length; i++)
            {
                string[] information = classInfo[i].Split(";");
                ClInfo.Add(new ClassInfo(int.Parse(information[0]), information[1]));
            }
        }
        static void GetProperties()
        {
            for(int i = 1; i<PropInfo.Length; i++)
            {
                string[] information = PropInfo[i].Split(";");
                Dictionary<int, string> propSet = new Dictionary<int, string>();
                propSet.Add(int.Parse(information[1]), information[2]);
                PrInfo.Add(new Property(propSet, information[3]));
            }
        }
    }
    public class ClassInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClassInfo(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class Property
    {
        public Dictionary<int,string> PropSet { get; set; }
        public string Type { get; set; }
        public Property(Dictionary<int, string> propset, string type)
        {
            PropSet = propset;
            Type = type;
        }
    }
    public class Obj
    {
        public int ClassId { get; set; }
        public int ObjId { get; set; }
        public Dictionary<int, string> Prop { get; set;}
        public string Value { get; set; }
        public Obj(int clId, int objId, Dictionary<int,string> props, string value)
        {
            ClassId = clId;
            ObjId = objId;
            Prop = props;
            Value = value;
        }
    }
}
