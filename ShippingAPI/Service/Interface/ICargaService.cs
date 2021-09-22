using ShippingAPI.Model;
using System.Collections.Generic;

namespace ShippingAPI.Service
{
    public interface ICargaService<T> : IGenericRepository<ShippingContext, T, int>
    {
    }
}
