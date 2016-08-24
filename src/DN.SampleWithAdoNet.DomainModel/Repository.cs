using System.Collections.Generic;
using System.Data;

namespace DN.SampleWithAdoNet.DomainModel
{
    public abstract class Repository<TEntity> where TEntity : new()
    {
        protected AdoNetContext _context;

        public Repository(AdoNetContext context)
        {
            _context = context;
        }
        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<TEntity> items = new List<TEntity>();
                while (reader.Read())
                {
                    var item = new TEntity();
                    Map(reader, item);
                    items.Add(item);
                }
                return items;
            }
        }

        protected abstract void Map(IDataRecord record, TEntity entity);
    }
}
