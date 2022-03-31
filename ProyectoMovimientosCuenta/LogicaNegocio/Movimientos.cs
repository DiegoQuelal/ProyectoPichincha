using ProyectoMovimientosCuenta.AccesoDatos;
using System.Net;
using System.Web.Http;

namespace ProyectoMovimientosCuenta.LogicaNegocio
{
    public class Movimientos
    {
        private readonly MovientosCuentaDBContext context;
        public Movimientos(MovientosCuentaDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<Modelos.Movimientos> ObtenerMovimientos()
        {
            IEnumerable<Modelos.Movimientos> lstMovimientos = context.Movimientos.ToList();
            if (lstMovimientos == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("No se Encontraron Movimientos"),
                    ReasonPhrase = "No exiten Movimientos"
                };
                throw new HttpResponseException(resp);
            }
            return lstMovimientos;
        }
        public void InsertarActualizarMovimentos(Modelos.Movimientos Movimientos)
        {
            Cuenta cuenta = new Cuenta(context);
            Modelos.Cuenta CuentaFiltrada = cuenta.ObtenerCuenta(Movimientos?.NumeroCuenta);
            Modelos.Movimientos MovimientoFitrado = context.Movimientos.FirstOrDefault(x => x.IdMovimiento == Movimientos.IdMovimiento);
            IEnumerable<Modelos.Movimientos> ValidarCupo = ObtenerMovimientos().Where(x => x.Valor < 0 && x.Fecha.ToString("dd/mm/yyyy") == System.DateTime.Now.ToString("dd/mm/yyyy"));
            decimal valor = ValidarCupo.Sum(x => x.Valor);
            Movimientos.Saldo = CuentaFiltrada.SaldoInicial + Movimientos.Valor;
            if(Movimientos.Saldo <= 0)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Saldo No disponible"),
                    ReasonPhrase = "Fondos Insuficientes"
                };
                throw new HttpResponseException(resp);
            }
            if (valor < -1000)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Cupo diario Excedido"),
                    ReasonPhrase = "Cupo diario Excedido"
                };
                throw new HttpResponseException(resp);
            }
            try
            {
                if (MovimientoFitrado?.IdMovimiento == 0)
                {
                    context.Add(MovimientoFitrado);
                    context.SaveChanges();
                }
                else
                {
                    context.Update(MovimientoFitrado);
                    context.SaveChanges();
                }
                CuentaFiltrada.SaldoInicial = Movimientos.Saldo;
                cuenta.InsertarActualizarCuenta(CuentaFiltrada);
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al insertar el Movimiento de la Cuenta"),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }
        public void EliminarMovimentos(int Movimiento)
        {
            try
            {
                Modelos.Movimientos MovimientoFitrado = context.Movimientos.FirstOrDefault(x => x.IdMovimiento == Movimiento);
                if (MovimientoFitrado == null)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Error al Eliminar el Movimiento"),
                        ReasonPhrase = "No Encontró el movimiento a Eliminar"
                    };
                    throw new HttpResponseException(resp);
                }
                context.Movimientos.Remove(MovimientoFitrado);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al Eliminar el Movimiento"),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }
    }
}
