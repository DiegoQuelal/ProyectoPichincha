namespace ProyectoMovimientosCuenta.Modelos
{
    public class Reporte
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal Saldo { get; set; }
    }
}
