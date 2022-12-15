namespace SistemaCadeteriaMVC.ViewModels;
using System.ComponentModel.DataAnnotations;

public class PedidosCadetesClientesViewModel
{
  public List<CadeteViewModel>? ListadoCadetes { get; set; }
  public List<ClienteViewModel>? ListadoClientes { get; set; }
  
  public int NumeroPedido { get; set; }

  [Display(Name = "Observaciones del pedido")]
  public string? Observaciones { get; set; }

  [Required]
  public bool Estado { get; set; }

  [Display(Name = "Cadete")]
  [Required]
  public int IdCadete { get; set; }

  [Display(Name = "Cliente")]
  [Required]
  public int IdCliente { get; set; }
}
