using System.ComponentModel.DataAnnotations;
namespace SistemaCadeteriaMVC.ViewModels;

public enum Estado
{
  SinAsignar,
  Pendiente,
  EnCurso,
  Entregado
}

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

  [Display(Name = "Cliente del pedido")]
  [Required]
  public int IdCliente { get; set; }
}
