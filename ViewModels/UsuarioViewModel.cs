namespace SistemaCadeteriaMVC.ViewModels;
using System.ComponentModel.DataAnnotations;

public enum Rol
{
  Administrador,
  Encargado
}

public class UsuarioViewModel
{
  public int Id { get; set; }

  [Display(Name = "Username")]
  [Required(ErrorMessage = "Ingrese el nombre de usuario")]
  public string? Username { get; set; }

  [Display(Name = "Password")]
  [Required(ErrorMessage = "Ingrese su password")]
  public string? Password { get; set; }

  [Display(Name = "Rol")]
  [Required(ErrorMessage = "Ingrese el rol")]
  public Rol Rol { get; set; }

  public UsuarioViewModel() {}

  public UsuarioViewModel(int id, string username, string password)
  {
    this.Id = id;
    this.Username = username;
    this.Password = password;
    this.Rol = Rol.Encargado;
  }

  public UsuarioViewModel(int id, string username, string password, Rol rol)
  {
    this.Id = id;
    this.Username = username;
    this.Password = password;
    this.Rol = rol;
  }
}