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
        public Cliente? Cliente { get; set; }

        public Pedido() {
            autonumerico++;
            this.NumeroPedido = autonumerico;
        }

        public Pedido(Cliente cliente, string observaciones) {
            autonumerico++;
            this.NumeroPedido = autonumerico;
            this.Cliente = cliente;
            this.Observaciones = observaciones;
            this.Realizado = false;
        }

        public Pedido(Cliente cliente, string observaciones, bool realizado) {
            autonumerico++;
            this.NumeroPedido = autonumerico;
            this.Cliente = cliente;
            this.Observaciones = observaciones;
            this.Realizado = realizado;
        }
    }
}