using System.ComponentModel.DataAnnotations;

namespace ProyectoMovimientosCuenta.Modelos
{
    public class Cliente : Persona
    {
        [Required(ErrorMessage = "El Campo ClienteId es requerido")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El Campo Contrasenia es requerido")]
        public string Contrasenia { get; set; }
        [Required(ErrorMessage = "El Campo Estado es requerido")]
        public bool Estado { get; set; }
    }
}
