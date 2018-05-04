using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrganizerTransport.Interfaces;
using OrganizerTransport.Models;

namespace OrganizerTransport.Repositorios
{
    public class Saldo_Repositorio : ISaldo
    {
 
        private readonly ObjectContext context = null;
        public Saldo_Repositorio(IOptions<Settings> settings) => context = new ObjectContext(settings);

        public async Task<DeleteResult> Delete(string _id) => await context.Saldo.DeleteOneAsync(Builders<Saldo>.Filter.Eq("Id", _id));

        public async Task<IEnumerable<Saldo>> Get() => await context.Saldo.Find(x => true).ToListAsync();

        public async Task<Saldo> Get(string _id) => await context.Saldo.Find(Builders<Saldo>.Filter.Eq("Id", _id)).FirstOrDefaultAsync();

        public async Task Post(Saldo saldo) => await context.Saldo.InsertOneAsync(saldo);

        public async Task<ReplaceOneResult> Put(string _id, Saldo saldo) => await context.Saldo.ReplaceOneAsync(o => o.Id.Equals(_id), saldo);
    }
}
