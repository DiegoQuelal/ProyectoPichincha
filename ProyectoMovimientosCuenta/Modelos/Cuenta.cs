using System.ComponentModel.DataAnnotations;

namespace ProyectoMovimientosCuenta.Modelos
{
    public class Cuenta
    {
        [Key]
        public string NumeroCuenta { get; set; }
        [Required(ErrorMessage = "El Campo TipoCuenta es requerido")]
        public string TipoCuenta { get; set; }
        [Required(ErrorMessage = "El Campo SaldoInicial es requerido")]
        public decimal SaldoInicial { get; set; }
        [Required(ErrorMessage = "El Campo Estado es requerido")]
        public bool Estado { get; set; }
        [Required(ErrorMessage = "El Campo ClienteId es requerido")]
        public int ClienteId { get; set; }
    }
}
