using Rest.Api.Datastore;
using System;
using System.Collections;

namespace Rest.Api.Domain
{
    public interface IDBManager
    {
        IRepository<T> GetRepository<T>() where T : class;
    }

    public class DBManager : IDBManager
    {
        private readonly IDatabase _dbContext;
        private static Hashtable _repositories = new Hashtable();

        public DBManager(IDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repoType = typeof(Repository<>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(T)), _dbContext);
                _repositories.Add(type, repoInstance);
            }
            return (IRepository<T>)_repositories[type];
        }
    }
}