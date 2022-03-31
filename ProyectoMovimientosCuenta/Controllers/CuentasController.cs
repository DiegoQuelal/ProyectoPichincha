using Microsoft.AspNetCore.Mvc;
using ProyectoMovimientosCuenta.AccesoDatos;
using ProyectoMovimientosCuenta.LogicaNegocio;

namespace ProyectoMovimientosCuenta.Controllers
{
    [Route("api")]
    public class CuentasController : Controller
    {
        private readonly MovientosCuentaDBContext context;
        public CuentasController(MovientosCuentaDBContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("EditaAgregaCuenta")]
        public void Post(Modelos.Cuenta Cuenta)
        {
            Cuenta cuenta = new Cuenta(context);
            cuenta.InsertarActualizarCuenta(Cuenta);
        }
        [HttpDelete]
        [Route("EliminaCuenta")]
        public void Delete(string NumeroCuenta)
        {
            Cuenta cuenta = new Cuenta(context);
            cuenta.EliminarCuenta(NumeroCuenta);
        }
    }
}
