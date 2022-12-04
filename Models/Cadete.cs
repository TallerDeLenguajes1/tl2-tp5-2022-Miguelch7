using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteriaMVC.Models
{
    public class Cadete: Persona
    {
        public List<Pedido>? ListadoPedidos { get; set; }

        public Cadete() {
            autonumerico++;
            this.Id = autonumerico;
        }

        public Cadete(string nombre, string direccion, string telefono): base(nombre, direccion, telefono) {
            this.ListadoPedidos = new List<Pedido>();
        }

        public Cadete(int id, string nombre, string direccion, string telefono): base(id, nombre, direccion, telefono) {
            this.ListadoPedidos = new List<Pedido>();
        }
    }
}