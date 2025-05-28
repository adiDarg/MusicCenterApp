using MusicCenterFactories;
using System.Data;
using System.Data.OleDb;

namespace MusicCenterWebService
{
    public class DbContext : IDbContext
    {
        private OleDbCommand _command;
        private OleDbConnection _connection;
        private OleDbTransaction? _transaction;
        private static DbContext? _instance;
        private static Object _lock = new Object(); 
        private DbContext()
        {
            _connection = new OleDbConnection();
            _connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""C:\Users\Owner\Desktop\ביה''ס\מדמ''ח\MusicCenter\MusicCenterWebService\App_Data\musicCenterDatabase.accdb""";
            _command = _connection.CreateCommand();
        }
        public DbContext AddParameter(string name, string value)
        { 
            OleDbParameter oleDbParameter = new OleDbParameter(name, value);
            this._command.Parameters.Add(oleDbParameter);
            return this;
        }
        public void ClearParameters()
        {
            this._command.Parameters.Clear();
        }
        public static DbContext GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DbContext();
                    }
                }
            }
            return _instance;
        }
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
            _command.Transaction = _transaction;
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public int Create(string sql)
        {
            _command.CommandText = sql;
            return _command.ExecuteNonQuery();
        }

        public int Delete(string sql)
        {
            _command.CommandText = sql;
            return _command.ExecuteNonQuery();
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public IDataReader Read(string sql)
        {
            _command.CommandText = sql;
            return _command.ExecuteReader();
        }

        public IDataReader ReadValue(string sql)
        {
            _command.CommandText = sql;
            return _command.ExecuteReader();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public int Update(string sql)
        {
            _command.CommandText = sql;
            return _command.ExecuteNonQuery();
        }
    }
}
