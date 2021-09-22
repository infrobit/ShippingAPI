using Microsoft.EntityFrameworkCore;
using ShippingAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShippingAPI.Service
{
    public class CargaService : GenericRepository<ShippingContext, Carga, int>, ICargaService<Carga>
    {

        public CargaService(ShippingContext context) : base(context)
        {
        }
        public override Carga FindById(int id) => ShippingContext.Set<Carga>()
                .Include(x => x.Veiculo)
                .Include(x => x.Status)
                .Where(x => x.Id == id).FirstOrDefault();

        public override IEnumerable<Carga> FindAll() => ShippingContext.Carga
                .Include(x => x.Veiculo)
                .Include(x => x.Status)
                .ToList();
    }
}
