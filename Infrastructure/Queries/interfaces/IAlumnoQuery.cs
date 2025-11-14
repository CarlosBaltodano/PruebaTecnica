using Core.Entities;
using Core.Shared;

namespace Infrastructure.Queries.interfaces
{
    public interface IAlumnoQuery
    {
        Task<IEnumerable<Alumno>> ListarAlumnosxAula(int aulaId);
        Task<IEnumerable<Alumno>> ListarAlumnosxDocente(int docenteId, int aulaId);
        Task<Response> AsignarAlumno(Alumno alumno);
        Task<Response> ActualizarAlumno(Alumno alumno);
        Task<Response> EliminarAlumno(int alumnoId);
    }
}
