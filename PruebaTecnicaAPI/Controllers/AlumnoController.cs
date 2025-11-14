using Core.Entities;
using Core.Shared;
using Infrastructure.Queries.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoQuery _query;
        public AlumnoController(IAlumnoQuery query)
        {
            _query = query;
        }

        [Route("saludar")]
        [HttpGet]
        public ActionResult Saludo()
        {
            return Ok("Saludos Inlearning");
        }

        [HttpGet("aula/{aulaId}")]
        public async Task<IActionResult> ListarAlumnosxAula(int aulaId)
        {
            var data = await _query.ListarAlumnosxAula(aulaId);

            return Ok(new Response {Msg = $"{data.Count()} alumnos encontrados.",  Rpta = 0, Data = data });
        }

        [HttpGet("docente/{docenteId}/{aulaId}")]
        public async Task<IActionResult> ListarAlumnosxDocente(int docenteId, int aulaId)
        {
            var data = await _query.ListarAlumnosxDocente(docenteId, aulaId);
            return Ok(new Response { Msg = $"{data.Count()} alumnos encontrados.", Rpta = 0, Data = data });
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarAlumno([FromBody] Alumno alumno)
        {
            var resp = await _query.AsignarAlumno(alumno);
            return resp.Rpta == 0 ? Ok(resp) : BadRequest(resp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAlumno(int id, [FromBody] Alumno alumno)
        {
            alumno.AlumnoId = id;

            var resp = await _query.ActualizarAlumno(alumno);
            return resp.Rpta == 0 ? Ok(resp) : BadRequest(resp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAlumno(int id)
        {
            var resp = await _query.EliminarAlumno(id);
            return resp.Rpta == 0 ? Ok(resp) : BadRequest(resp);
        }
    }
}
