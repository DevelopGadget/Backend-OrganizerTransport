
using MongoDB.Driver;
using OrganizerTransport.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganizerTransport.Interfaces
{
    interface ISaldo
    {
        Task<IEnumerable<Saldo>> Get();
        Task<Saldo> Get(string _id);
        Task Post(Saldo saldo);
        Task<ReplaceOneResult> Put(string _id, Saldo saldo);
        Task<DeleteResult> Delete(string _id);
    }
}
