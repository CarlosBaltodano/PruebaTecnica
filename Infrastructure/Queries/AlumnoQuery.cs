using System.Data;
using Core.Entities;
using Core.Shared;
using Dapper;
using Infrastructure.Connection;
using Infrastructure.Queries.interfaces;

namespace Infrastructure.Queries
{
    public class AlumnoQuery : IAlumnoQuery
    {
        //private readonly string ConnectionString;
        private readonly ISqlConnection _conn;
        public AlumnoQuery(ISqlConnection conn)
        {
            //Deberá modificar la cadena de conexión antes de ejecutar

            //ConnectionString = ConfigurationExtensions.GetConnectionString(configuration, "PruebaTecnicaDB");

            _conn = conn;
        }

        public async Task<Response> AsignarAlumno(Alumno alumno)
        {
            using var conn = _conn.CreateConnection();
            var param = new DynamicParameters();

            param.Add("@Nombre", alumno.Nombre);
            param.Add("@Edad", alumno.Edad);
            param.Add("@AulaId", alumno.AulaId);

            var result = await conn.QueryFirstAsync<Response>("sp_Alumno_Asignar",param,commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<Alumno>> ListarAlumnosxAula(int aulaId)
        {

            using var conn = _conn.CreateConnection();
            var param = new DynamicParameters();

            param.Add("@AulaId", aulaId);

            return await conn.QueryAsync<Alumno>("sp_Alumno_ListarPorAula",param,commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Alumno>> ListarAlumnosxDocente(int docenteId, int aulaId)
        {

            using var conn = _conn.CreateConnection();
            var param = new DynamicParameters();

            param.Add("@DocenteId", docenteId);
            param.Add("@AulaId", aulaId);

            return await conn.QueryAsync<Alumno>("sp_Alumno_ListarPorDocente",param,commandType: CommandType.StoredProcedure);
        }

        public async Task<Response> ActualizarAlumno(Alumno alumno)
        {

            using var conn = _conn.CreateConnection();
            var param = new DynamicParameters();

            param.Add("@AlumnoId", alumno.AlumnoId);
            param.Add("@Nombre", alumno.Nombre);
            param.Add("@Edad", alumno.Edad);

            var result = await conn.QueryFirstAsync<Response>("sp_Alumno_Actualizar",param,commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<Response> EliminarAlumno(int alumnoId)
        {

            using var conn = _conn.CreateConnection();
            var param = new DynamicParameters();

            param.Add("@AlumnoId", alumnoId);

            var result = await conn.QueryFirstAsync<Response>("sp_Alumno_Eliminar",param,commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
