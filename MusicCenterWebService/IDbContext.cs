using System.Data;

namespace MusicCenterWebService
{
    public interface IDbContext
    {
        /// <summary>
        /// Opens connection with database
        /// </summary>
        void OpenConnection();

        /// <summary>
        /// Closes connection with database
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Reads from database
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>All rows matching search</returns>
        IDataReader Read(string sql);

        /// <summary>
        /// Reads from database
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>The single value that fits search</returns>
        IDataReader ReadValue(string sql);

        /// <summary>
        /// Inserts new row(s) into database
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Create(string sql);

        /// <summary>
        /// Deletes row(s) from database
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Delete(string sql);

        /// <summary>
        /// Updates row(s) in database
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Update(string sql);

        /// <summary>
        /// Start a transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit to transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Cancel the transaction
        /// </summary>
        void Rollback();
    }
}
