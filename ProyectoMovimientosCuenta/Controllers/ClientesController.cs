using Microsoft.AspNetCore.Mvc;
using ProyectoMovimientosCuenta.AccesoDatos;
using ProyectoMovimientosCuenta.LogicaNegocio;

namespace ProyectoMovimientosCuenta.Controllers
{
    [Route("api")]
    public class ClientesController : Controller
    {
        private readonly MovientosCuentaDBContext context;
        public ClientesController(MovientosCuentaDBContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("EditaAgregaCliente")]
        public void Post(Modelos.Cliente Cliente)
        {
            Cliente cliente = new Cliente(context);
            cliente.InsertarActualizarCliente(Cliente);
        }
        [HttpDelete]
        [Route("EliminaCliente")]
        public void Delete(int ClienteId)
        {
            Cliente cliente = new Cliente(context);
            cliente.EliminarCliente(ClienteId);
        }
    }
}
