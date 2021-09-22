using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Model
{
    [Table("veiculo")]
    public class Veiculo
    {
        public Veiculo()
        {
            Cargas = new HashSet<Carga>();
        }
        [Key]
        public int? Id { get; set; }
        [Column("nome_modelo")]
        public string NomeModelo { get; set; }
        [Column("valor_mcubico")]
        public Decimal ValorCubico { get; set; }

        [Column("id_tipo_veiculo")]
        public int IdTipoVeiculo { get; set; }
        public virtual TipoVeiculo TipoVeiculo { get; set; }
        public virtual ICollection<Carga> Cargas { get; set; }

        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<Carga>()
                      .HasOne(d => d.Veiculo)
                      .WithMany(s => s.Cargas)
                      .HasForeignKey(d => d.IdStatusCarga)
                      .IsRequired();
        }
    }
}
