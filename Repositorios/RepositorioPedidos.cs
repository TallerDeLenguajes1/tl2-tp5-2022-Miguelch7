using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadeteriaMVC.Models;
using System.Data.SQLite;
using NLog;

public interface IRepositorioPedidos
{
  List<Pedido> ObtenerPedidos();
  List<Pedido> ObtenerPedidosPorCliente(int idCliente);
  List<Pedido> ObtenerPedidosPorCadete(int idCadete);
  Pedido? ObtenerPedidoPorNumeroPedido(int idPedido);
  void AgregarPedido(Pedido pedido);
  void EditarPedido(Pedido pedido);
  void EliminarPedido(int idPedido);
}

public class RepositorioPedidos : IRepositorioPedidos
{
  private readonly string ConnectionString;
  private readonly IConfiguration _configuration;
  private static readonly Logger logger = LogManager.GetCurrentClassLogger();

  public RepositorioPedidos(IConfiguration configuration)
  {
    this._configuration = configuration;
    this.ConnectionString = this._configuration.GetConnectionString("SQLite");
  }

  public List<Pedido> ObtenerPedidos()
  {
    try
    {
      List<Pedido> pedido = new List<Pedido>();

      string query = "SELECT * FROM pedido";

      using(SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        using(SQLiteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            bool realizado = reader.GetBoolean(2);
            int idCliente = reader.GetInt32(3);
            int idCadete = reader.GetInt32(4);
            
            pedido.Add(new Pedido(numeroPedido, observaciones, realizado, idCliente, idCadete));
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SQLiteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public List<Pedido> ObtenerPedidosPorCliente(int idCliente)
  {
    try
    {
      List<Pedido> pedido = new List<Pedido>();

      string query = "SELECT * FROM pedido WHERE id_cliente = @idCliente";

      using(SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idCliente", idCliente));

        using(SQLiteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            bool realizado = reader.GetBoolean(2);
            int idClientePedido = reader.GetInt32(3);
            int idCadete = reader.GetInt32(4);
            
            pedido.Add(new Pedido(numeroPedido, observaciones, realizado, idClientePedido, idCadete));
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SQLiteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public List<Pedido> ObtenerPedidosPorCadete(int idCadete)
  {
    try
    {
      List<Pedido> pedido = new List<Pedido>();

      string query = "SELECT * FROM pedido WHERE id_cadete = @idCadete";

      using(SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idCadete", idCadete));

        using(SQLiteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            bool realizado = reader.GetBoolean(2);
            int idCliente = reader.GetInt32(3);
            int idCadetePedido = reader.GetInt32(4);
            
            pedido.Add(new Pedido(numeroPedido, observaciones, realizado, idCliente, idCadetePedido));
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SQLiteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public Pedido? ObtenerPedidoPorNumeroPedido(int idPedido)
  {
    try
    {
      Pedido? pedido = null;

      string query = "SELECT * FROM pedido WHERE id = @idPedido";

      using(SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idPedido", idPedido));

        using (SQLiteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            bool realizado = reader.GetBoolean(2);
            int idCliente = reader.GetInt32(3);
            int idCadete = reader.GetInt32(4);
            
            pedido = new Pedido(numeroPedido, observaciones, realizado, idCliente, idCadete);
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SQLiteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public void AgregarPedido(Pedido pedido)
  {
    try
    {
      string query = "INSERT INTO pedido (observaciones, realizado, id_cliente, id_cadete) VALUES(@observaciones, @realizado, @idCliente, @idCadete)";

      using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@observaciones", pedido.Observaciones));
        command.Parameters.Add(new SQLiteParameter("@realizado", pedido.Realizado));
        command.Parameters.Add(new SQLiteParameter("@idCliente", pedido.IdCliente));
        command.Parameters.Add(new SQLiteParameter("@idCadete", pedido.IdCadete));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El pedido se agregó correctamente");
    }
    catch (SQLiteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
    catch (Exception ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public void EditarPedido(Pedido pedido)
  {
    try
    {
      string query = "UPDATE pedido SET observaciones = @observaciones, realizado = @realizado, id_cliente = @idCliente, id_cadete = @idCadete WHERE id = @idPedido";

      using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idPedido", pedido.NumeroPedido));
        command.Parameters.Add(new SQLiteParameter("@observaciones", pedido.Observaciones));
        command.Parameters.Add(new SQLiteParameter("@realizado", pedido.Realizado));
        command.Parameters.Add(new SQLiteParameter("@idCliente", pedido.IdCliente));
        command.Parameters.Add(new SQLiteParameter("@idCadete", pedido.IdCadete));
        
        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El pedido se actualizó correctamente");
    }
    catch (SQLiteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
    catch (Exception ex)
    {
      logger.Debug(ex.ToString());

      throw new Exception("ERROR!", ex);
    }
  }

  public void EliminarPedido(int idPedido)
  {
    try
    {
      string query = "DELETE FROM pedido WHERE id = @idPedido";

      using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idPedido", idPedido));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El pedido se eliminó correctamente");
    }
    catch (SQLiteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
    catch (Exception ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }
}