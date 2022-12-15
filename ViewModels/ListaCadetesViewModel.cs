using System.ComponentModel.DataAnnotations;
namespace SistemaCadeteriaMVC.ViewModels;

public class ListaCadetesViewModel
{
  public List<CadeteViewModel>? ListadoCadetes;

  public ListaCadetesViewModel(List<CadeteViewModel> listadoCadetes)
  {
    ListadoCadetes = listadoCadetes;
  }

  public int ContarCadetes() 
  {
    return ListadoCadetes!.Count();
  }
}