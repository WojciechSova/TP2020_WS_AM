using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task2.Data
{
    public class Reader
    {
        public Guid ReaderGuid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long PersonalID { get; set; }

        [JsonConstructor]
        public Reader(string name, string surname, long personalId)
        {
            this.ReaderGuid = Guid.NewGuid();
            Name = name;
            Surname = surname;
            PersonalID = personalId;
        }

        public Reader(String readerGuid, string name, string surname, String personalID)
        {
            ReaderGuid = Guid.Parse(readerGuid);
            Name = name;
            Surname = surname;
            PersonalID = Convert.ToInt64(personalID);
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

        public void GetObjectData(SerializationInfo info, StreamingContext context, int index)
        {
            info.AddValue("ReaderId_" + index.ToString() + "_", ReaderGuid);
            info.AddValue("Name_" + index.ToString() + "_", Name);
            info.AddValue("Surname_" + index.ToString() + "_", Surname);
            info.AddValue("PersonalId_" + index.ToString() + "_", PersonalID);
        }
    }
}