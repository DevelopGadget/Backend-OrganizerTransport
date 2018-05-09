using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrganizerTransport.Interfaces;
using OrganizerTransport.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganizerTransport.Controllers
{
    [Route("[controller]")]
    public class DiaController : Controller
    {
        private readonly ISaldo _Saldo;

        public DiaController(ISaldo Saldo) => _Saldo = Saldo;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Saldo> saldos = await _Saldo.Get();
                List<List<Dia>> Dias = new List<List<Dia>>();
                if (saldos == null) return StatusCode(StatusCodes.Status406NotAcceptable, "No Hay Documentos");
                foreach (Saldo saldo in saldos)
                {
                    Dias.Add(saldo.Horario);
                }
                return Ok(JsonConvert.SerializeObject(Dias));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id.Length < 24) return StatusCode(StatusCodes.Status406NotAcceptable, "Id Invalid");
                Saldo saldo = await _Saldo.Get(id);
                if (saldo == null) return StatusCode(StatusCodes.Status406NotAcceptable, "No Hay Documentos");
                return Ok(JsonConvert.SerializeObject(saldo.Horario));
            }
            catch (Exception)
            {
                return BadRequest("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }

        // POST api/values
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id,[FromBody]Dia Horario)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id.Length < 24) return StatusCode(StatusCodes.Status406NotAcceptable, "Id Invalid");
                Saldo saldo = await _Saldo.Get(id);
                if (saldo == null) return StatusCode(StatusCodes.Status406NotAcceptable, "No Hay Documentos");
                if (!ModelState.IsValid) return StatusCode(StatusCodes.Status406NotAcceptable, ModelState);
                saldo.Horario.Add(Horario);
                var h = await _Saldo.Put(id, saldo);
                if (h.MatchedCount > 0) return Ok("Creado");
                else return StatusCode(StatusCodes.Status406NotAcceptable, "No Editado");
            }
            catch (Exception)
            {
                return BadRequest("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}/{Index}")]
        public async Task<IActionResult> Put(string id, int Index, [FromBody]Dia Horario)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || id.Length < 24) return StatusCode(StatusCodes.Status406NotAcceptable, "Id Invalid");
                Saldo saldo = await _Saldo.Get(id);
                if (saldo == null) return StatusCode(StatusCodes.Status406NotAcceptable, "No Hay Documentos");
                if (!ModelState.IsValid) return StatusCode(StatusCodes.Status406NotAcceptable, ModelState);
                for (int i = 0; i < saldo.Horario.Count; i++)
                {
                    if(i == Index)
                    {
                        saldo.Horario[Index] = Horario;
                        var h = await _Saldo.Put(id, saldo);
                        if (h.MatchedCount > 0) return Ok("Editado");
                        else return StatusCode(StatusCodes.Status406NotAcceptable, "No Editado");
                    } 
                }
                return StatusCode(StatusCodes.Status406NotAcceptable, "Index no encontrado");
            }
            catch (Exception)
            {
                return BadRequest("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }


    }
}
