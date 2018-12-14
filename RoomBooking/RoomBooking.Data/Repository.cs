using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomBooking.Data.Entities;

namespace RoomBooking.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IAppDbContext _appDbContext;
        private DbSet<T> _dbset;

        public Repository(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbset = _appDbContext.Set(typeof(T)).Cast<T>();
        }

        public virtual IQueryable<T> Get()
        {
            return _dbset;
        }

        public virtual T Create(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbset.AddOrUpdate(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

    }

}
