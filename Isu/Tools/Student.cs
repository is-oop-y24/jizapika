using System;

namespace Isu.Tools
{
    public class Student
    {
        private int _id = 0;
        private string _name;
        private GroupName _groupname;
        public Student(string name, GroupName groupname)
        {
            this._name = name;
            this._groupname = groupname;
        }

        public void Assign_id(int id)
        {
            this._id = id;
        }

        public int Get_id()
        {
            if (_id == 0) Console.WriteLine($"Студент {_name} не значится ни в каком вузе.");
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void RenameStudent(string name)
        {
            this._name = name;
        }

        public GroupName GetGroup()
        {
            return _groupname;
        }
    }
}