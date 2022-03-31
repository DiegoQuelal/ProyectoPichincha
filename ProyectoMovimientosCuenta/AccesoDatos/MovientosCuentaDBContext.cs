using Microsoft.EntityFrameworkCore;
using ProyectoMovimientosCuenta.Modelos;

namespace ProyectoMovimientosCuenta.AccesoDatos
{
    public class MovientosCuentaDBContext:DbContext
    {
        public MovientosCuentaDBContext(DbContextOptions<MovientosCuentaDBContext> options)
    : base(options)
        { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimientos> Movimientos { get; set; }
    }
}
