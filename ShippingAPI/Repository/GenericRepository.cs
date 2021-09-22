using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingAPI.Service
{
    public class GenericRepository<C, E, ID> : IGenericRepository<C, E, ID> where C : ShippingContext where E : class
    {
        public ShippingContext ShippingContext { get; private set; }
        public GenericRepository(C context)
        {
            ShippingContext = context;
        }

        public virtual void Delete(ID id)
        {
            E entity = FindById(id);

            _ = entity ?? throw new Exception("Não foi possível remover este registro. Registro não encontrado!");

            try
            {
                ShippingContext.Set<E>().Remove(entity);
                ShippingContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (null != ex.InnerException && ex.InnerException is SqlException sqlex && sqlex.Number == 547)
                {
                    throw new Exception("Não foi possível remover este registro. O mesmo se encontra associado a outro registro.");
                }
                else
                {
                    throw ex;
                }
            }
        }

        public virtual E FindById(ID id)
        {
            return ShippingContext.Set<E>().Find(id);
        }

        public virtual IEnumerable<E> FindAll()
        {
            return ShippingContext.Set<E>().ToList();
        }

        public virtual E Insert(E obj)
        {
            try
            {
                ShippingContext.Set<E>().Add(obj);
                ShippingContext.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException && ex.Message.Contains("because another instance with the same key value"))
                {
                    throw new Exception("Já existe um registro com o mesmo identificador.");
                }
                else
                {
                    throw ex;
                }
            }
            return obj;
        }

        public virtual void Update(E obj)
        {
            try
            {
                ShippingContext.Set<E>().Update(obj);
                ShippingContext.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException && ex.Message.Contains("cannot be modified"))
                {
                    throw new Exception("Identificador não pode ser modificado.");
                }
                else if (ex is DbUpdateConcurrencyException && ex.Message.Contains("Attempted to update"))
                {
                    throw new Exception("Não foi possível atualizar este registro. Registro não encontrado!");
                }
                else
                {
                    throw ex;
                }
            }
        }

    }
}
