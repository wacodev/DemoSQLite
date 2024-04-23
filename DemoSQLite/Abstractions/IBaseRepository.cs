using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoSQLite.Abstractions
{
    public interface IBaseRepository<T> : IDisposable where T : TableData, new()
    {
        int SaveItem(T item);

        void SaveItems(IEnumerable<T> items);

        T? GetItem(int id);

        T? GetItem(Expression<Func<T, bool>> predicate);

        List<T> GetItems();

        List<T> GetItems(Expression<Func<T, bool>> predicate);

        void DeleteItem(T item);
    }
}
