using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.Model;
using ShippingAPI.Service;
using ShippingAPI.Utils;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : Controller
    {
        private readonly IVeiculoService<Veiculo> _veiculoService;
        public VeiculoController(IVeiculoService<Veiculo> veiculoService)
        {
            _veiculoService = veiculoService;
        }
        /// <summary>
        /// Busca Veiculo pelo ID
        /// </summary>
        /// <param name="id">ID do Veiculo</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var veiculo = _veiculoService.FindById(id);
                _ = veiculo ?? throw new Exception("ERRO: Registro não localizado!");

                return Ok(veiculo.ToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        /// <summary>
        /// Busca por todos os veiculos
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                var veiculos = _veiculoService.FindAll().Select(x => x.ToDTO());
                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Insere um novo Veiculo
        /// </summary>
        /// <param name="nomeModelo">Nome ou modelo do Veiculo</param>
        /// <param name="valorCubico">Valor do transporte por metro cubico da carga</param>
        /// <param name="idTipoVeiculo">ID do Veiculo</param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post([FromForm] string nomeModelo, [FromForm] decimal valorCubico, [FromForm] int idTipoVeiculo)
        {
            try
            {
                var obj = _veiculoService.Insert(new Veiculo()
                {
                    NomeModelo = nomeModelo,
                    IdTipoVeiculo = idTipoVeiculo,
                    ValorCubico = valorCubico,
                });
                return Created(Path.Combine(HttpContext.Request.Path, $"{obj.Id}"), obj.ToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Deleta um registro de Veiculo pelo ID
        /// </summary>
        /// <param name="id">ID do veiculo</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _veiculoService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        /// <summary>
        /// Busca por todos Tipos de Veiculo
        /// </summary>
        /// <returns></returns>
        [HttpGet("tipo")]
        public IActionResult GetTipoVeiculo()
        {
            try
            {
                var obj = _veiculoService.FindAllTipoVeiculo()
                                         .Select(x => x.ToDTO());
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
