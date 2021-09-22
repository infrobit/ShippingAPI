using NUnit.Framework;
using ShippingAPI;
using ShippingAPI.Controllers;
using ShippingAPI.Model;
using ShippingAPI.Service;
using System.Linq;
using TestesUnitarios.Db;

namespace TestesUnitarios
{
    public class Veiculo : ShippingTestDB
    {
        private IVeiculoService<ShippingAPI.Model.Veiculo> _service;

        [SetUp]
        public void Setup()
        {
            _service = new VeiculoService(ShippingContext);
        }

        [Test]
        public void FindAll()
        {
            Assert.Greater(_service.FindAll().Count(), 0);
        }

        [Test]
        [TestCase(1)]
        public void FindById(int id)
        {
            Assert.NotNull(_service.FindById(id));
            Assert.Pass();
        }

        [Test]
        [TestCase(437)]
        public void GetByIdNotFound(int id)
        {
            Assert.Null(_service.FindById(id));
        }

        [Test]
        public void Insert()
        {
            var obj = _service.Insert(new ShippingAPI.Model.Veiculo() 
            { 
                IdTipoVeiculo = 3, 
                NomeModelo = "Veiculo Teste Insert", 
                ValorCubico = (long)44.6 
            });
            Assert.Greater(obj.Id, 0);
            Assert.Pass();
        }

        [Test]
        [TestCase(2)]
        public void Delete(int id)
        {
            _service.Delete(id);
            Assert.Pass();
        }
    }
}