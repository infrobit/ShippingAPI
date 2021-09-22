using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.Enum;
using ShippingAPI.Model;
using ShippingAPI.Service;
using ShippingAPI.Utils;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargaController : Controller
    {
        private readonly ICargaService<Carga> _cargaService;
        private readonly IVeiculoService<Veiculo> _veiculoService;
        public CargaController(ICargaService<Carga> cargaService, IVeiculoService<Veiculo> veiculoService)
        {
            _cargaService = cargaService;
            _veiculoService = veiculoService;
        }

        /// <summary>
        /// Busca Carga pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var carga = _cargaService.FindById(id);
                _ = carga ?? throw new Exception("ERRO: Carga não localizada!");

                return Ok(carga.ToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca por todos os registros da Carga
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                var cargas = _cargaService.FindAll()
                                          .Select(x => x.ToDTO());
                return Ok(cargas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Insere uma nova Carga
        /// </summary>
        /// <param name="responsavel">Responsavel pela Carga</param>
        /// <param name="idVeiculo">ID do Veiculo que fará o transporte da Carga</param>
        /// <param name="altura">Altura da Carga</param>
        /// <param name="largura">Largura da Carga</param>
        /// <param name="comprimento">Comprimento da Carga</param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post([FromForm] string responsavel, 
                                  [FromForm] int idVeiculo,
                                  [FromForm] decimal altura,
                                  [FromForm] decimal largura,
                                  [FromForm] decimal comprimento)
        {
            try
            {
                var veiculo = _veiculoService.FindById(idVeiculo);
                var obj = _cargaService.Insert(new Carga()
                {
                    Responsavel = responsavel,
                    IdVeiculo = idVeiculo,
                    Altura = altura,
                    Largura = largura,
                    Comprimento = comprimento,
                    DataSaida = DateTime.Now,
                    ValorCarga = Math.Round(altura * largura * comprimento * veiculo.ValorCubico, 2)
                });
                return Created(Path.Combine(HttpContext.Request.Path, $"{obj.Id}"), obj.ToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Confirma a entrega da Carga
        /// </summary>
        /// <param name="id">ID da Carga</param>
        /// <returns></returns>
        [HttpPut("{id:int}/confirm")]
        public IActionResult Confirm(int id)
        {
            try
            {
                var carga = _cargaService.FindById(id);
                _ = carga ?? throw new Exception("ERRO: Carga não localizada!");

                carga.IdStatusCarga = (int)StatusCargaEnum.ENTREGUE;
                _cargaService.Update(carga);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancela a entrega da Carga
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}/cancel")]
        public IActionResult Cancel(int id)
        {
            try
            {
                var carga = _cargaService.FindById(id);
                _ = carga ?? throw new Exception("ERRO: Carga não localizada!");

                carga.IdStatusCarga = (int)StatusCargaEnum.CANCELADO;
                _cargaService.Update(carga);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
