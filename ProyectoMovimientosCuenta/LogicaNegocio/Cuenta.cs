using ProyectoMovimientosCuenta.AccesoDatos;
using System.Net;
using System.Web.Http;

namespace ProyectoMovimientosCuenta.LogicaNegocio
{
    public class Cuenta
    {
        private readonly MovientosCuentaDBContext context;
        public Cuenta(MovientosCuentaDBContext context)
        {
            this.context = context;
        }
        public Modelos.Cuenta ObtenerCuenta(string NumeroCuenta)
        {
            Modelos.Cuenta CuentaFitrada = context.Cuentas.FirstOrDefault(x => x.NumeroCuenta == NumeroCuenta);
            if(CuentaFitrada == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al obtener la Cuenta"),
                    ReasonPhrase = "La CuentaConsultada No existe"
                };
                throw new HttpResponseException(resp);
            }
            return CuentaFitrada;
        }
        public IEnumerable<Modelos.Cuenta> ObtenerCuentas(int ClienteId)
        {
            IEnumerable <Modelos.Cuenta> lstCuentas = context.Cuentas.Where(x => x.ClienteId == ClienteId);
            if (lstCuentas == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error NO se encontraron cuentas"),
                    ReasonPhrase = "NO se encontraron cuentas"
                };
                throw new HttpResponseException(resp);
            }
            return lstCuentas;
        }
        public void InsertarActualizarCuenta(Modelos.Cuenta Cuenta)
        {
            try
            {
                Modelos.Cuenta CuentaFitrada = context.Cuentas.FirstOrDefault(x => x.NumeroCuenta == Cuenta.NumeroCuenta);
                if (string.IsNullOrEmpty(CuentaFitrada?.NumeroCuenta))
                {
                    context.Add(Cuenta);
                    context.SaveChanges();
                }
                else
                {
                    CuentaFitrada.ClienteId = Cuenta.ClienteId;
                    CuentaFitrada.SaldoInicial = Cuenta.SaldoInicial;
                    CuentaFitrada.Estado = Cuenta.Estado;
                    CuentaFitrada.TipoCuenta = Cuenta.TipoCuenta;
                    context.Update(CuentaFitrada);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al insertar la Cuenta"),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }
        public void EliminarCuenta(string NumeroCuenta)
        {
            try
            {
                Modelos.Cuenta CuentaFitrada = context.Cuentas.FirstOrDefault(x => x.NumeroCuenta == NumeroCuenta);
                if (CuentaFitrada == null)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Error al Eliminar la Cuenta"),
                        ReasonPhrase = "No Encontró la cuenta a Eliminar"
                    };
                    throw new HttpResponseException(resp);
                }
                context.Cuentas.Remove(CuentaFitrada);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error al Eliminar la Cuenta"),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }
        public IEnumerable<Modelos.Reporte> GenerarReporte(DateTime Fechainicio, DateTime FechaFin, int IdCliente)
        {
            Cliente objCliente = new Cliente(context);
            Modelos.Cliente cliente = objCliente.ObtenerCliente(IdCliente);
            IEnumerable <Modelos.Cuenta> lstCuentas = ObtenerCuentas(cliente.ClienteId);
            List<Modelos.Reporte> lstReportes = new List<Modelos.Reporte>();
            foreach (Modelos.Cuenta cuenta in lstCuentas)
            {
                Movimientos movimientos = new Movimientos(context);
                IEnumerable<Modelos.Movimientos> lstMovimientos = movimientos.ObtenerMovimientos().Where(x => x.NumeroCuenta == cuenta.NumeroCuenta && x.Fecha>=Fechainicio && x.Fecha<=FechaFin);
                foreach(Modelos.Movimientos item in lstMovimientos)
                {
                    Modelos.Reporte Reporte = new Modelos.Reporte
                    {
                        Fecha = item.Fecha,
                        Cliente = cliente.Nombre,
                        NumeroCuenta = item.NumeroCuenta,
                        Tipo = cuenta.TipoCuenta,
                        SaldoInicial = cuenta.SaldoInicial,
                        Estado = cuenta.Estado,
                        Movimiento = item.Valor,
                        Saldo = item.Saldo
                    };
                    lstReportes.Add(Reporte);
                }
            }
            return lstReportes;
        }
    }
}
