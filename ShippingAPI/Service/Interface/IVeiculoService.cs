using ShippingAPI.Model;
using System.Collections.Generic;

namespace ShippingAPI.Service
{
    public interface IVeiculoService<T> : IGenericRepository<ShippingContext, T, int>
    {
        public IEnumerable<TipoVeiculo> FindAllTipoVeiculo();
    }
}
