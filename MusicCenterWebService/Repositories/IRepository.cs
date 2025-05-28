namespace MusicCenterWebService.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Take every column from matching table, and complie into list
        /// </summary>
        /// <returns>List of every registered Instance of T object in database</returns>
        List<T> GetAll();

        /// <summary>
        /// Creates an entry in database according to existing istance of T object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Whether process was succesful</returns>
        public bool Create(T entity);

        /// <summary>
        /// Updates an entry in table according to existing instance of T object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Whether process was succesful</returns>
        public bool Update(T entity);

        /// <summary>
        /// Deletes entry in table according to existing instance of T object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Whether process was succesful</returns>
        public bool Delete(T entity);

        /// <summary>
        /// Searches database for entry with id entered
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object matching the id in table</returns>
        public T GetById(string id);
    }
}
