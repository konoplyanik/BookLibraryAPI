using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer.Service.Contract;
using ServiceLayer.Service.UoW;

namespace ServiceLayer.Service.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _db;

        public DbSet<T> Set { get; private set; }

        public Repository(AppDbContext db)
        {
            _db = db;
            var set = _db.Set<T>();
            set.Load();

            Set = set;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task<T>.Run(() => Set);
        }
        public async Task<T> Get(int id)
        {
            return await Task<T>.Run(() => Set.Find(id));
        }

        public async Task Create(T item)
        {
            await Task.Run(() => Set.Add(item));
        }

        public async Task Update(T item)
        {
            await Task.Run(() => Set.Update(item));
        }
        public async Task Delete(T item)
        {
            await Task.Run(() => Set.Remove(item));
        }
    }
}
