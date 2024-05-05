using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CargillTest.DAL
{
    public class DBHelper<T> where T : new()
    {
        private const string DEFAULT_DB_FILE_NAME = "CargillTest.db";
        private readonly SQLiteConnection _connection;

        public DBHelper(string? dbFileName = null)
        {
            if (string.IsNullOrWhiteSpace(dbFileName))
            {
                _connection = new SQLiteConnection(Path.Combine(Environment.CurrentDirectory, DEFAULT_DB_FILE_NAME));
            }
            else
            {
                _connection = new SQLiteConnection(Path.Combine(Environment.CurrentDirectory, dbFileName));
            }
            _connection.CreateTable<T>();
        }

        public int Insert(T item)
        {
            return _connection.Insert(item);
        }

        public int Update(T item)
        {
            return _connection.Update(item);
        }

        public List<T> QueryAll()
        {
            return _connection.Table<T>().ToList();
        }
    }
}
