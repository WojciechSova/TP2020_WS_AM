using System;
using System.Collections.Generic;

namespace Task2.Data
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

        public override bool Equals(object obj)
        {
            return obj is Reader reader &&
                   Name == reader.Name &&
                   Surname == reader.Surname &&
                   PersonalID == reader.PersonalID;
        }
        public override int GetHashCode()
        {
            int hashCode = -1316942220;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + PersonalID.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "Client: " + Name + " " + Surname + ", personal ID: " + PersonalID + "\n";
        }
    }
}