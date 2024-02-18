namespace CONSUMIR_API.Models
{
    public class Resultado_API
    {
        public List<model_API> Lista {  get; set; }
        public int Identificacion { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public int UserId { get; set; }
    }
}
