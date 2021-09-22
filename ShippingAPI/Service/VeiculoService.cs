using Microsoft.EntityFrameworkCore;
using ShippingAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShippingAPI.Service
{
    public class VeiculoService : GenericRepository<ShippingContext, Veiculo, int>, IVeiculoService<Veiculo>
    {

        public VeiculoService(ShippingContext context) : base(context)
        {
        }

        public override IEnumerable<Veiculo> FindAll() => ShippingContext.Veiculo.Include(x => x.TipoVeiculo).ToList();
        public IEnumerable<TipoVeiculo> FindAllTipoVeiculo() => ShippingContext.TipoVeiculo.ToList();
    }
}
