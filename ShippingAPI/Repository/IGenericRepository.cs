using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingAPI.Service
{
    public interface IGenericRepository<C, E, ID>
    {
        IEnumerable<E> FindAll();
        void Delete(ID id);
        E FindById(ID id);
        E Insert(E obj);
        void Update(E obj);
    }
}
