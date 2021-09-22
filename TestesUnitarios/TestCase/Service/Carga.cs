using NUnit.Framework;
using ShippingAPI;
using ShippingAPI.Controllers;
using ShippingAPI.Enum;
using ShippingAPI.Model;
using ShippingAPI.Service;
using System;
using System.Linq;
using TestesUnitarios.Db;

namespace TestesUnitarios
{
    public class Carga : ShippingTestDB
    {
        private ICargaService<ShippingAPI.Model.Carga> _cargaService;
        private IVeiculoService<ShippingAPI.Model.Veiculo> _veiculoService;

        [SetUp]
        public void Setup()
        {
            _cargaService = new CargaService(ShippingContext);
            _veiculoService = new VeiculoService(ShippingContext);
        }

        [Test]
        public void FindAll()
        {
            Assert.Greater(_cargaService.FindAll().Count(), 0);
        }

        [Test]
        [TestCase(1)]
        public void GetById(int id)
        {
            Assert.NotNull(_cargaService.FindById(id));
        }

        [Test]
        [TestCase(437)]
        public void GetByIdNotFound(int id)
        {
            Assert.Null(_cargaService.FindById(id));
        }

        [Test]
        [TestCase(10, 5, 5, 3, "Jão Da Silva Junior")]
        [Order(1)]
        public void Insert(decimal altura, decimal largura, decimal comprimento, int idVeiculo, string responsavel)
        {
            var obj = _cargaService.Insert(new ShippingAPI.Model.Carga() 
            { 
                Altura = altura, 
                Largura = largura, 
                Comprimento = comprimento,
                ValorCarga = decimal.Round(altura * largura * comprimento * _veiculoService.FindById(idVeiculo).ValorCubico, 2),
                IdStatusCarga = (int)StatusCargaEnum.EM_PERCURSO,
                DataSaida = DateTime.Now,
                Responsavel = responsavel
            });
            Assert.Greater(obj.Id, 0);
        }

        [Test]
        [TestCase(1)]
        public void Confirm(int id)
        {
            var carga = _cargaService.FindById(id);
            carga.IdStatusCarga = (int)StatusCargaEnum.ENTREGUE;
            _cargaService.Update(carga);
            Assert.IsTrue(_cargaService.FindById(id).IdStatusCarga == (int)StatusCargaEnum.ENTREGUE);
        }

        [Test]
        [TestCase(1)]
        public void Cancel(int id)
        {
            var carga = _cargaService.FindById(id);
            carga.IdStatusCarga = (int)StatusCargaEnum.CANCELADO;
            _cargaService.Update(carga);
            Assert.IsTrue(_cargaService.FindById(id).IdStatusCarga == (long)StatusCargaEnum.CANCELADO);
        }
    }
}