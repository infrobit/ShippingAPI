using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Model
{
    [Table("status_carga")]
    public class StatusCarga
    {
        public StatusCarga()
        {
            Cargas = new HashSet<Carga>();
        }
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Carga> Cargas { get; set; }
        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<Carga>()
                       .HasOne(d => d.Status)
                       .WithMany(s => s.Cargas)
                       .HasForeignKey(d => d.IdStatusCarga)
                       .IsRequired();
        }
    }
}
