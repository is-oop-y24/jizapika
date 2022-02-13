using System;

namespace Isu.Tools
{
    public class Student
    {
        private uint _id = 0;
        private string _name;
        private GroupName _groupname;
        public Student(string name, GroupName groupname, uint id)
        {
            this._name = name;
            this._groupname = groupname;
            this._id = id;
        }

        public uint Get_id()
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