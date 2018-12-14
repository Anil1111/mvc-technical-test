using System.Linq;
using RoomBooking.Data.Entities;

namespace RoomBooking.Data
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Get();
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}