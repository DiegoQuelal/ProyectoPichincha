using ProyectoMovimientosCuenta.AccesoDatos;
using ProyectoMovimientosCuenta.Modelos;
using System.Net;
using System.Web.Http;

namespace ProyectoMovimientosCuenta.LogicaNegocio
{
    public class Cliente
    {
        private readonly MovientosCuentaDBContext context;
        public Cliente(MovientosCuentaDBContext context)
        {
            this.context = context;
        }
        public Modelos.Cliente ObtenerCliente(int ClienteId)
        {
            Modelos.Cliente ClienteFitrado = context.Clientes.FirstOrDefault(x => x.ClienteId == ClienteId);
            if (ClienteFitrado == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al obtener el Cliente"),
                    ReasonPhrase = "El cliente consultado No existe"
                };
                throw new HttpResponseException(resp);
            }
            return ClienteFitrado;
        }
        public void InsertarActualizarCliente(Modelos.Cliente Cliente)
        {
            try
            {
                Modelos.Cliente ClienteFitrado = context.Clientes.FirstOrDefault(x => x.ClienteId == Cliente.ClienteId);
                if (string.IsNullOrEmpty(ClienteFitrado?.Identificacion))
                {
                    context.Add(Cliente);
                    context.SaveChanges();
                }
                else
                {
                    ClienteFitrado.Estado = Cliente.Estado;
                    ClienteFitrado.Direccion = Cliente.Direccion;
                    ClienteFitrado.ClienteId = Cliente.ClienteId;
                    ClienteFitrado.Telefono = Cliente.Telefono;
                    ClienteFitrado.Genero = Cliente.Genero;
                    ClienteFitrado.Nombre = Cliente.Nombre;
                    ClienteFitrado.Contrasenia = Cliente.Contrasenia;
                    ClienteFitrado.Edad = Cliente.Edad;
                    context.Update(ClienteFitrado);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al inserta el Cliente"),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }
        public void EliminarCliente(int ClienteId)
        {
            try
            {
                Modelos.Cliente ClienteFitrado = context.Clientes.FirstOrDefault(x => x.ClienteId == ClienteId);
                if (ClienteFitrado == null)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Error al inserta el Cliente"),
                        ReasonPhrase = "No Encontró el cliente a Eliminar"
                    };
                    throw new HttpResponseException(resp);
                }
                context.Clientes.Remove(ClienteFitrado);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al inserta el Cliente"),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }
    }
}
