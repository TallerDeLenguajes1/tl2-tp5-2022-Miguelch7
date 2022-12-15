using System.ComponentModel.DataAnnotations;
namespace SistemaCadeteriaMVC.ViewModels;

public class PedidoViewModel
{
  public int NumeroPedido { get; set; }
  
  [Display(Name = "Observaciones del pedido")]
  public string? Observaciones { get; set; }

  [Required]
  public bool Realizado { get; set; }

  [Display(Name = "Cadete")]
  [Required]
  public int IdCadete { get; set; }

  [Display(Name = "Cliente")]
  [Required]
  public int IdCliente { get; set; }
}
