using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteriaMVC.Models
{
    public class Cliente: Persona
    {
        public string? Referencia { get; set; }
        
        public Cliente(): base() {
            autonumerico++;
            this.Id = autonumerico;
        }

        public Cliente(string nombre, string direccion, string telefono, string referencia): base(nombre, direccion, telefono) {
            
            this.Referencia = referencia;
        }

        public Cliente(int id, string nombre, string direccion, string telefono, string referencia): base(id, nombre, direccion, telefono) {
            
            this.Referencia = referencia;
        }
    }
}