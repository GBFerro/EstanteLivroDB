using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace EstanteLivroDB
{
    internal class Person
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Birth Date")]
        public string BirthYear { get; set; }

        public Person(string name, string birthYear)
        {
            Name = name;
            BirthYear = birthYear;
        }

        public override string? ToString()
        {
            return $"Autor: {this.Name}\nData de Nascimento: {this.BirthYear}";
        }
    }
}
