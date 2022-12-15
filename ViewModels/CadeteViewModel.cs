namespace SistemaCadeteriaMVC.ViewModels;
using System.ComponentModel.DataAnnotations;

public class CadeteViewModel
{
  public int Id { get; set; }

  [Display(Name = "Nombre del cadete")]
  [Required]
  public string? Nombre { get; set; }

  [Display(Name = "Dirección del cadete")]
  [Required]
  public string? Direccion { get; set; }

  [Display(Name = "Teléfono del cadete")]
  [Required]
  public string? Telefono { get; set; }
}
