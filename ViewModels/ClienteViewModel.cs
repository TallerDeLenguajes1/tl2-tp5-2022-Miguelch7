namespace SistemaCadeteriaMVC.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ClienteViewModel
{
  public int Id { get; set; }

  [Display(Name = "Nombre del cliente")]
  [Required]
  public string? Nombre { get; set; }

  [Display(Name = "Dirección del cliente")]
  [Required]
  public string? Direccion { get; set; }

  [Display(Name = "Teléfono del cliente")]
  [Required]
  public string? Telefono { get; set; }

  [Display(Name = "Referencia del cliente")]
  public string? Referencia { get; set; }
}
