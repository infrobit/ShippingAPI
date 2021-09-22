using ShippingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestesUnitarios.Db
{
    class ShippingInitializer
    { 
        public static void Initialize(ShippingContext context)
        {
            if (context.Veiculo.Count() == 0)
            {
                context.Veiculo.AddRange(ShippingInitializerSeed.GetPreconfiguredVeiculos());
                context.SaveChanges();
            }

            if (context.TipoVeiculo.Count() == 0)
            {
                context.TipoVeiculo.AddRange(ShippingInitializerSeed.GetPreconfiguredTipoVeiculo());
                context.SaveChanges();
            }

            if (context.StatusCarga.Count() == 0)
            {
                context.StatusCarga.AddRange(ShippingInitializerSeed.GetPreconfiguredStatusCarga());
                context.SaveChanges();
            }
        }
    }
}
