using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrganizerTransport.Interfaces;
using OrganizerTransport.Models;

namespace OrganizerTransport.Controllers
{
    [Route("[controller]")]
    public class SaldoController : Controller
    {
        private readonly ISaldo _Saldo;

        public SaldoController(ISaldo saldo) => this._Saldo = saldo;       

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Saldo> saldos = await _Saldo.Get();
                if (saldos == null)return StatusCode(StatusCodes.Status406NotAcceptable, "No Hay Documentos");
                return Ok(JsonConvert.SerializeObject(saldos));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id.Length < 24) return StatusCode(StatusCodes.Status406NotAcceptable, "Id Invalid");
                Saldo saldo = await _Saldo.Get(id);
                if(saldo == null) return StatusCode(StatusCodes.Status406NotAcceptable, "No Hay Documentos");
                return Ok(JsonConvert.SerializeObject(saldo));
            }
            catch (Exception)
            {
                return BadRequest("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Saldo saldo)
        {
            try
            {
                if (!ModelState.IsValid) return StatusCode(StatusCodes.Status406NotAcceptable, ModelState);
                await _Saldo.Post(saldo);
                return Ok("Creado");
            }
            catch (Exception)
            {
                return BadRequest("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Saldo saldo)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id.Length < 24) return StatusCode(StatusCodes.Status406NotAcceptable, "Id Invalid");
                if (!ModelState.IsValid) return StatusCode(StatusCodes.Status406NotAcceptable, ModelState);
                var h = await _Saldo.Put(id, saldo);
                if (h.MatchedCount > 0) return Ok("Editado");
                else return StatusCode(StatusCodes.Status406NotAcceptable, "No Editado");
            }
            catch (Exception)
            {
                return BadRequest("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id.Length < 24) return StatusCode(StatusCodes.Status406NotAcceptable, "Id Invalid");
                var h = await _Saldo.Delete(id);
                if (h.DeletedCount > 0) return Ok("Eliminado");
                else return StatusCode(StatusCodes.Status406NotAcceptable, "No Eliminado");
            }
            catch (Exception)
            {
                return BadRequest("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }
    }
}
