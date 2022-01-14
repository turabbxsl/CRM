using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDAL<T> where T:class
    {

        T Insert(T t);

        void Delete(T t);

        T Update(T t);

        Task<List<T>> GetListAll();

        Task<List<T>> GetListAll(Expression<Func<T, bool>> filter);
        T GetByID(int id);


    }
}
