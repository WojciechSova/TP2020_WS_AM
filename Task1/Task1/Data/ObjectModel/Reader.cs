using System;

namespace Task1.Data
{
    public class Reader
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long PersonalID { get; set; }

        public Reader(string name, string surname, long personalId)
        {
            Name = name;
            Surname = surname;
            PersonalID = personalId;
        }

        public override string ToString()
        {
            return "Client: " + Name + " " + Surname + ", personal ID: " + PersonalID;
        }
    }
}