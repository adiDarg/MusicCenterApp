
using MusicCenterFactories;

namespace MusicCenterWebService.Repositories
{
    /// <summary>
    /// Contains dbContext and modelFactory instance to be used by child objects
    /// </summary>
    public abstract class Repository
    {
        private DbContext _dbContext;
        private ModelFactory _modelFactory;
        public Repository()
        {
            _dbContext = DbContext.GetInstance();
            _modelFactory = new ModelFactory();
        }
        protected DbContext GetDbContext()
        {
            return _dbContext;
        }
        protected ModelFactory GetModelFactory()
        {
            return _modelFactory;
        }
    }
}
