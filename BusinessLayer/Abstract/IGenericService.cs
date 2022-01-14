using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        T TAdd(T t);

        void TDelete(T t);

        T TUpdate(T t);

        Task<List<T>> GetList();

        T TGetByID(int id);
    }
}
