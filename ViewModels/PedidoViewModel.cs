using System.ComponentModel.DataAnnotations;
using SistemaCadeteriaMVC.Models;
namespace SistemaCadeteriaMVC.ViewModels;

public class PedidoViewModel
{
  public int NumeroPedido { get; set; }
  
  [Display(Name = "Observaciones del pedido")]
  public string? Observaciones { get; set; }

  [Required]
  [Display(Name = "Estado del pedido")]
  public Estado Estado { get; set; }

  [Display(Name = "Cadete del pedido")]
  [Required]
  public int IdCadete { get; set; }
  public string? NombreCadete { get; set; }

  [Display(Name = "Cliente del pedido")]
  [Required]
  public int IdCliente { get; set; }
  public string? NombreCliente { get; set; }
}
