using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteriaMVC.Models
{
    public enum Estado
    {
        SinAsignar,
        Pendiente,
        EnCurso,
        Entregado
    }

    public class Pedido
    {
        private static int autonumerico = 0;
        public int NumeroPedido { get; set; }
        public string? Observaciones { get; set; }
        public Estado Estado { get; set; }
        public int IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public int IdCadete { get; set; }
        public string? NombreCadete { get; set; }

        public Pedido() {
            autonumerico++;
            this.NumeroPedido = autonumerico;
        }

        public Pedido(string observaciones, int idCliente, string? nombreCiente, int idCadete, string? nombreCadete) {
            autonumerico++;
            this.NumeroPedido = autonumerico;
            this.Observaciones = observaciones;
            this.Estado = Estado.SinAsignar;
            this.IdCliente = idCliente;
            this.NombreCliente = nombreCiente;
            this.IdCadete = idCadete;
            this.NombreCadete = nombreCadete;
        }

        public Pedido(string observaciones, Estado estado, int idCliente, string? nombreCiente, int idCadete, string? nombreCadete) {
            autonumerico++;
            this.NumeroPedido = autonumerico;
            this.Observaciones = observaciones;
            this.Estado = estado;
            this.IdCliente = idCliente;
            this.NombreCliente = nombreCiente;
            this.IdCadete = idCadete;
            this.NombreCadete = nombreCadete;
        }

        public Pedido(int numeroPedido, string observaciones, Estado estado, int idCliente, string? nombreCiente, int idCadete, string? nombreCadete) {
            this.NumeroPedido = numeroPedido;
            this.Observaciones = observaciones;
            this.Estado = estado;
            this.IdCliente = idCliente;
            this.NombreCliente = nombreCiente;
            this.IdCadete = idCadete;
            this.NombreCadete = nombreCadete;
        }
    }
}