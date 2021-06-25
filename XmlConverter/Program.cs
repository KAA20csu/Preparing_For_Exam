using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    public static List<Member> Members = new List<Member>();
    static void Main()
    {
        string[] FileInformation = File.ReadAllLines("membs.xml");
        GetInformation(FileInformation);
        UpdateInformation();
    }
    static void UpdateInformation()
    {
        List<string> newInformation = new List<string>();
        newInformation.Add($"<members>");
        for(int i = 0; i < Members.Count; i++)
        {
            newInformation.Add($"{new string(' ', 2)}<member name=\"{Members[i].Name}\">");
            foreach(var rolesprojs in Members[i].RoleName)
            {
                newInformation.Add($"{new string(' ', 4)}member role=\"{rolesprojs.RoleName}\" project=\"{rolesprojs.ProjectName}\"");
            }
            newInformation.Add($"{new string(' ', 2)}<\\member>");
        }
        newInformation.Add("<\\members>");
        File.WriteAllLines("updMembs.xml", newInformation);
    }
    static void GetInformation(string[] info)
    {
        string currentProject = "";
        for(int i = 0; i < info.Length; i++)
        {
            string[] str = info[i].Split(new char[] { '<', '>', '=', '"', ' '}, StringSplitOptions.RemoveEmptyEntries);
            if (IsProject(str) == true)
                currentProject = str[2];
            if(IsMember(str) == true)
            {
                if (ContainsMember(str[4]))
                {
                    FillMembersRoles(str[4], str[2], currentProject);
                }
                else
                {
                    Members.Add(new Member(new List<Role>(), str[4]));
                    FillMembersRoles(str[4], str[2], currentProject);
                }
            }
        }
    }
    static bool ContainsMember(string mName)
    {
        foreach(var member in Members)
        {
            if (member.Name == mName)
                return true;
        }
        return false;
    }
    static void FillMembersRoles(string mName, string roleName, string projectName)
    {
        foreach(var member in Members)
        {
            if(member.Name == mName)
            {
                member.RoleName.Add(new Role(roleName, projectName));
            }
        }
    }
    static bool IsMember(string[] str)
    {
        if (str[0] == "member")
            return true;
        return false;
    }
    static bool IsProject(string[] str)
    {
        if (str[0] == "project")
            return true;
        return false;
    }
}
class Member
{
    public List<Role> RoleName { get; } = new List<Role>(); // 2
    public string Name { get; } // 4
    public Member(List<Role> role, string name)
    {
        RoleName = role;
        Name = name;
    }
}
class Role
{
    public string RoleName { get; }
    public string ProjectName { get; }

    public Role(string rName, string pName)
    {
        RoleName = rName;
        ProjectName = pName;
    }
}