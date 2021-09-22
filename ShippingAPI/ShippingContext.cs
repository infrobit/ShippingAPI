using Microsoft.EntityFrameworkCore;
using ShippingAPI.Model;

namespace ShippingAPI
{
    public class ShippingContext : DbContext
    {
        public ShippingContext(DbContextOptions<ShippingContext> options) : base(options)
        {
        }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<TipoVeiculo> TipoVeiculo { get; set; }
        public DbSet<StatusCarga> StatusCarga { get; set; }
        public DbSet<Carga> Carga { get; set; }

        //Configuração de mapeamento das entidades colocado dentro de cada classe
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurando Carga
            ShippingAPI.Model.Carga.Configure(modelBuilder);
            //Configurando StatusCarga
            ShippingAPI.Model.StatusCarga.Configure(modelBuilder);
            //Configurando Veiculo
            ShippingAPI.Model.Veiculo.Configure(modelBuilder);
            //Configurando TipoVeiculo
            ShippingAPI.Model.TipoVeiculo.Configure(modelBuilder);
        }
    }
}
