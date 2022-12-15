using System.ComponentModel.DataAnnotations;
namespace SistemaCadeteriaMVC.ViewModels;

public class ListaPedidosViewModel
{
  public List<PedidoViewModel>? ListadoPedidos;

  public ListaPedidosViewModel(List<PedidoViewModel> listadoPedidos)
  {
    ListadoPedidos = listadoPedidos;
  }

  public int ContarPedidos() 
  {
    return ListadoPedidos!.Count();
  }
}