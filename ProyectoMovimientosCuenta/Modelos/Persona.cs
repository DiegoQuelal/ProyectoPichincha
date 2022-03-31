using System.ComponentModel.DataAnnotations;

namespace ProyectoMovimientosCuenta.Modelos
{
    public class Persona
    {
        [Key]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "El Campo Nombre es requerido")]
        public string Nombre { get;set;}
        [Required(ErrorMessage = "El Campo Genero es requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El Campo Edad es requerido")]
        public int Edad { get; set; }
        [Required(ErrorMessage = "El Campo Direccion es requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El Campo Telefono es requerido")]
        public string Telefono { get; set; }
    }
}
