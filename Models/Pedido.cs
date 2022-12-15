using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteriaMVC.Models
{
    public class Pedido
    {
        private static int autonumerico = 0;
        public int NumeroPedido { get; set; }
        public string? Observaciones { get; set; }
        public bool Realizado { get; set; }
        public int IdCliente { get; set; }
        public int IdCadete { get; set; }

        public Pedido() {
            autonumerico++;
            this.NumeroPedido = autonumerico;
        }

        public Pedido(string observaciones, int idCliente, int idCadete) {
            autonumerico++;
            this.NumeroPedido = autonumerico;
            this.Observaciones = observaciones;
            this.Realizado = false;
            this.IdCliente = idCliente;
            this.IdCadete = idCadete;
        }

        public Pedido(string observaciones, bool realizado, int idCliente, int idCadete) {
            autonumerico++;
            this.NumeroPedido = autonumerico;
            this.Observaciones = observaciones;
            this.Realizado = realizado;
            this.IdCadete = idCadete;
            this.IdCliente = idCliente;
        }

        public Pedido(int numeroPedido, string observaciones, bool realizado, int idCliente, int idCadete) {
            this.NumeroPedido = numeroPedido;
            this.Observaciones = observaciones;
            this.Realizado = realizado;
            this.IdCadete = idCadete;
            this.IdCliente = idCliente;
        }
    }
}