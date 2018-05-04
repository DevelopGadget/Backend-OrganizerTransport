using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace OrganizerTransport.Models
{
    public class Saldo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public double SaldoGen { get; set; }
        public List<Dia> Horario { get; set; }
    }
}
