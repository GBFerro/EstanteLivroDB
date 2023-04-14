using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EstanteLivroDB
{
    [BsonIgnoreExtraElements]
    internal class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("ISNB")]
        public string ISNB { get; set; }

        [BsonElement("Publisher")]
        public string Publisher { get; set; }

        [BsonElement("Publication Year")]
        public int RunYear { get; set; }

        [BsonElement("Author")]
        public Person Author { get; set; }

        public Book(string title, string iSNB, string publisher, int runYear, Person author)
        {
            Title = title;
            ISNB = iSNB;
            Publisher = publisher;
            RunYear = runYear;
            Author = author;
        }

        public override string? ToString()
        {
            return $"Título: {this.Title}\nAutor: {this.Author.Name}\nEditora: {this.Publisher}\nAno de Publicação: {this.RunYear}\nISNB: {this.ISNB}";
        }
    }
}
