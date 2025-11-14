namespace Core.Entities
{
    public class Aula
    {
        public int AulaId { get; set; }
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
        public int DocenteId { get; set; }
    }
}
