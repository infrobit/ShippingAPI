using ShippingAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestesUnitarios.Db
{
    class ShippingInitializerSeed
    {
        public static IEnumerable<ShippingAPI.Model.Veiculo> GetPreconfiguredVeiculos()
        {
            return new List<ShippingAPI.Model.Veiculo>()
            {
                new ShippingAPI.Model.Veiculo {
                    NomeModelo = "Cargueiro Teste",
                    IdTipoVeiculo = 3,
                    ValorCubico = 22
                },
                new ShippingAPI.Model.Veiculo {
                    NomeModelo = "Trem Teste",
                    IdTipoVeiculo = 3,
                    ValorCubico = (decimal)11.15
                },
                new ShippingAPI.Model.Veiculo {
                    NomeModelo = "Caminhão Teste",
                    IdTipoVeiculo = 3,
                    ValorCubico = (decimal)21.15
                },
            };
        }
        public static IEnumerable<TipoVeiculo> GetPreconfiguredTipoVeiculo()
        {
            return new List<TipoVeiculo>()
            {
                new TipoVeiculo {
                    Tipo = "Tipo Teste1"
                },
                new TipoVeiculo {
                    Tipo = "Tipo Teste2"
                },
                new TipoVeiculo {
                    Tipo = "Tipo Teste3"
                },
                new TipoVeiculo {
                    Tipo = "Tipo Teste4"
                }
            };
        }
        public static IEnumerable<StatusCarga> GetPreconfiguredStatusCarga()
        {
            return new List<StatusCarga>()
            {
                new StatusCarga {
                    Status = "Cancelada"
                },
                new StatusCarga {
                    Status = "Em Percurso"
                },
                new StatusCarga {
                    Status = "Entregue"
                }
            };
        }
    }
}
