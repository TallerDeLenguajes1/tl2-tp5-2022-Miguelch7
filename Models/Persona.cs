using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteriaMVC.Models
{
    public class Persona
    {
        protected static int autonumerico = 0;
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        
        public Persona() {
            autonumerico++;
            this.Id = autonumerico;
        }

        public Persona(string nombre, string direccion, string telefono) {
            autonumerico++;
            this.Id = autonumerico;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }

        public Persona(int id, string nombre, string direccion, string telefono) {
            this.Id = id;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }
    }
}