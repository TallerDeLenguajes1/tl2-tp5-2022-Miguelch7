using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteriaMVC.Models
{
  public enum Rol
  {
    Adminsitrador,
    Encargado
  }

  public class Usuario
  {
    protected static int autonumerico = 0;
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Rol Rol { get; set; }

    public Usuario()
    {
      autonumerico++;
      this.Id = autonumerico;
    }

    public Usuario(string username, string password)
    {
      autonumerico++;
      this.Id = autonumerico;
      this.Username = username;
      this.Password = password;
      this.Rol = Rol.Encargado;
    }

    public Usuario(string username, string password, Rol rol)
    {
      autonumerico++;
      this.Id = autonumerico;
      this.Username = username;
      this.Password = password;
      this.Rol = rol;
    }

    public Usuario(int id, string username, string password)
    {
      this.Id = id;
      this.Username = username;
      this.Password = password;
      this.Rol = Rol.Encargado;
    }

    public Usuario(int id, string username, string password, Rol rol)
    {
      this.Id = id;
      this.Username = username;
      this.Password = password;
      this.Rol = rol;
    }
  }
}