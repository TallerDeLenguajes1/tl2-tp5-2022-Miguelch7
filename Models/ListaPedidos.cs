namespace SistemaCadeteriaMVC.Models;

public class ListaPedidos
{
  private static int autonumerico = 0;
  private List<Pedido> listadoPedidos = new List<Pedido>();
  
  public List<Pedido> ObtenerPedidos()
  {
    return listadoPedidos;
  }

  public void AsignarNumeroPedido(Pedido pedido)
  {
    autonumerico++;
    pedido.NumeroPedido = autonumerico;
  }

  public void AgregarPedido(Pedido pedido)
  {
    AsignarNumeroPedido(pedido);
    listadoPedidos.Add(pedido);
  }

  public void EliminarPedido(int numeroPedido)
  {
    Pedido? pedido = listadoPedidos.Find(p => p.NumeroPedido == numeroPedido);
    
    if(pedido != null) listadoPedidos.Remove(pedido);
  }

  public void EditarPedido(Pedido nuevoPedido)
  {
    Pedido? pedido = listadoPedidos.Find(p => p.NumeroPedido == nuevoPedido.NumeroPedido);

    if (pedido != null)
    {
      pedido.Observaciones = nuevoPedido.Observaciones;
      pedido.Realizado = nuevoPedido.Realizado;
      pedido.IdCliente = nuevoPedido.IdCliente;
      pedido.IdCadete = nuevoPedido.IdCadete;
    }
  }

}