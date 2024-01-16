using Microsoft.EntityFrameworkCore;
using Udemy_RestWithASP_NET5.Model.Base;
using Udemy_RestWithASP_NET5.Model.Context;

namespace Udemy_RestWithASP_NET5.Repository.Generic {
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity {

        protected PostgreSQLContext _context;

        private DbSet<T> dataset;

        public GenericRepository(PostgreSQLContext context) {
            _context = context;
            dataset = _context.Set<T>();
        }

        public bool Exists(long id) {
            return dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindAll() {
            List<T> itens = new List<T>();

            return dataset.ToList();
        }

        public T FindByID(long id) {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }
        public T Create(T item) {
            try {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception) {

                throw;
            }
        }

        public void Delete(long id) {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null) {
                try {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception) {

                    throw;
                }
            }
        }
        public T Update(T item) {
            if (!Exists(item.Id)) return null;

            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));

            if (result != null) {
                try {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception) {

                    throw;
                }
            }

            return item;
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connectcion = _context.Database.GetDbConnection())
            {
                connectcion.Open();
                using (var command = connectcion.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
                return int.Parse(result);
        }
    }
}
