namespace SistemaCadeteriaMVC.Models;

public class ListaCadetes
{
  private static int autonumerico = 0;
  private static List<Cadete> listadoCadetes = new List<Cadete>();

  public List<Cadete> ObtenerCadetes()
  {
    return listadoCadetes;
  }

  public void AsignarId(Cadete cadete)
  {
    autonumerico++;
    cadete.Id = autonumerico;
  }

  public void AgregarCadete(Cadete cadete)
  {
    AsignarId(cadete);
    listadoCadetes.Add(cadete);
  }
  
  public void EliminarCadete(int idCadete)
  {
    Cadete? cadete = listadoCadetes.Find(c => c.Id == idCadete);

    if (cadete != null) listadoCadetes.Remove(cadete);
  }

  public void EditarCadete(Cadete nuevoCadete)
  {
    Cadete? cadete = listadoCadetes.Find(c => c.Id == nuevoCadete.Id);

    if (cadete != null)
    {
      cadete.Nombre = nuevoCadete.Nombre;
      cadete.Telefono = nuevoCadete.Telefono;
      cadete.Direccion = nuevoCadete.Direccion;
    }
  }

}