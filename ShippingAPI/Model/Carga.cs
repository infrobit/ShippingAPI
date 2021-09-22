using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingAPI.Model
{
    [Table("carga")]
    public class Carga 
    {
        [Key]
        public int? Id { get; set; }
        [Column("responsavel")]
        public string Responsavel { get; set; }
        [Column("altura")]
        public decimal Altura { get; set; }
        [Column("largura")]
        public decimal Largura { get; set; }
        [Column("comprimento")]
        public decimal Comprimento { get; set; }
        [Column("data_saida")]
        public DateTime DataSaida { get; set; }
        [Column("valor_carga")]
        public decimal ValorCarga { get; set; }

        [Column("id_veiculo")]
        public int IdVeiculo { get; set; }
        [Column("id_status_carga")]
        public int IdStatusCarga { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual StatusCarga Status { get; set; }

        public static void Configure(ModelBuilder builder)
        {

        }
    }
}
