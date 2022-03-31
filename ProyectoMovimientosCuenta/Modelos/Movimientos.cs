using System.ComponentModel.DataAnnotations;

namespace ProyectoMovimientosCuenta.Modelos
{
    public class Movimientos
    {
        [Key]
        public int IdMovimiento { get; set; }
        [Required(ErrorMessage = "El Campo Fecha es requerido")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El Campo Tipo es requerido")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "El Campo Valor es requerido")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "El Campo Saldo es requerido")]
        public decimal Saldo { get; set; }
        [Required(ErrorMessage = "El Campo NumeroCuenta es requerido")]
        public string NumeroCuenta { get; set; }
        [Required(ErrorMessage = "El Campo NumeroCuenta es requerido")]
        public string Movimiento { get; set; }
    }
}
