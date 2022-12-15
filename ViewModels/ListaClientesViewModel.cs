using System.ComponentModel.DataAnnotations;
namespace SistemaCadeteriaMVC.ViewModels;

public class ListaClientesViewModel
{
  public List<ClienteViewModel>? ListadoClientes;

  public ListaClientesViewModel(List<ClienteViewModel> listadoClientes)
  {
    ListadoClientes = listadoClientes;
  }

  public int ContarClientes() 
  {
    return ListadoClientes!.Count();
  }
}