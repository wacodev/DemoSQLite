using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoSQLite.Abstractions
{
    public class BaseRepository<T> : IBaseRepository<T> where T : TableData, new()
    {
        SQLiteConnection connection;
        private const string DbFileName = "db_denuncias.db3";
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;
        private string DatabasePath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DbFileName);
            }
        }
        public string StatusMessage { get; set; }

        public BaseRepository()
        {
            connection = new SQLiteConnection(DatabasePath, Flags);
            connection.CreateTable<T>();
        }

        public void DeleteItem(T item)
        {
            try
            {
                connection.Delete(item);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            connection.Close();
        }

        public T? GetItem(int id)
        {
            try
            {
                return connection.Table<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return null;
        }

        public T? GetItem(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>().Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return null;
        }

        public List<T> GetItems()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return [];
        }

        public List<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return [];
        }

        public int SaveItem(T item)
        {
            int id = 0;

            try
            {
                int result = 0;

                if (item.Id != 0)
                {
                    result = connection.Update(item);
                    StatusMessage = $"{result} fila(s) actualizada(s)";

                    if (result > 0)
                    {
                        id = item.Id;
                    }
                }
                else
                {
                    result = connection.Insert(item);
                    StatusMessage = $"{result} fila(s) agregada(s)";

                    if (result > 0)
                    {
                        id = connection.ExecuteScalar<int>("SELECT last_insert_rowid()");
                    }
                }

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return id;
        }

        public void SaveItems(IEnumerable<T> items)
        {
            try
            {
                int result = 0;
                result = connection.InsertAll(items);
                StatusMessage = $"{result} fila(s) agregada(s)";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }
    }
}
