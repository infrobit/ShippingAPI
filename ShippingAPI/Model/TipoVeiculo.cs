using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Model
{
    [Table("tipo_veiculo")]
    public class TipoVeiculo
    {
        public TipoVeiculo()
        {
            Veiculos = new HashSet<Veiculo>();
        }
        [Key]
        public int Id { get; set; }
        public string Tipo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Veiculo> Veiculos { get; set; }
        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<Veiculo>()
                       .HasOne(d => d.TipoVeiculo)
                       .WithMany(s => s.Veiculos)
                       .HasForeignKey(d => d.IdTipoVeiculo)
                       .IsRequired();
        }
    }
}
