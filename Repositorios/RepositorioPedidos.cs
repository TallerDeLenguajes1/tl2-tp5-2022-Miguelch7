using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadeteriaMVC.Models;
using Microsoft.Data.Sqlite;
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
    this.ConnectionString = this._configuration.GetConnectionString("Sqlite");
  }

  public List<Pedido> ObtenerPedidos()
  {
    try
    {
      List<Pedido> pedido = new List<Pedido>();

      string query = "SELECT * FROM pedido";

      using(SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);
        RepositorioCadetes repositorioCadetes = new RepositorioCadetes(_configuration);
        RepositorioClientes repositorioClientes = new RepositorioClientes(_configuration);

        using(SqliteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            Estado estado = (Estado) reader.GetInt32(2);
            int idCliente = reader.GetInt32(3);
            int idCadete = reader.GetInt32(4);
            string? nombreCadete = repositorioCadetes.ObtenerCadetePorId(idCadete)?.Nombre;
            string? nombreCliente = repositorioClientes.ObtenerClientePorId(idCliente)?.Nombre;
            
            pedido.Add(new Pedido(numeroPedido, observaciones, estado, idCliente, nombreCliente, idCadete, nombreCadete));
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SqliteException ex)
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

      using(SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        RepositorioCadetes repositorioCadetes = new RepositorioCadetes(_configuration);
        RepositorioClientes repositorioClientes = new RepositorioClientes(_configuration);

        command.Parameters.Add(new SqliteParameter("@idCliente", idCliente));

        using(SqliteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            Estado estado = (Estado) reader.GetInt32(2);
            int idClientePedido = reader.GetInt32(3);
            int idCadete = reader.GetInt32(4);
            string? nombreCadete = repositorioCadetes.ObtenerCadetePorId(idCadete)?.Nombre;
            string? nombreCliente = repositorioClientes.ObtenerClientePorId(idCliente)?.Nombre;
            
            pedido.Add(new Pedido(numeroPedido, observaciones, estado, idCliente, nombreCliente, idCadete, nombreCadete));
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SqliteException ex)
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

      using(SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        RepositorioCadetes repositorioCadetes = new RepositorioCadetes(_configuration);
        RepositorioClientes repositorioClientes = new RepositorioClientes(_configuration);

        command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

        using(SqliteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            Estado estado = (Estado) reader.GetInt32(2);
            int idCliente = reader.GetInt32(3);
            int idCadetePedido = reader.GetInt32(4);
            string? nombreCadete = repositorioCadetes.ObtenerCadetePorId(idCadete)?.Nombre;
            string? nombreCliente = repositorioClientes.ObtenerClientePorId(idCliente)?.Nombre;
            
            pedido.Add(new Pedido(numeroPedido, observaciones, estado, idCliente, nombreCliente, idCadete, nombreCadete));
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SqliteException ex)
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

      string query = "SELECT * FROM pedido WHERE numero_pedido = @idPedido";

      using(SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        RepositorioCadetes repositorioCadetes = new RepositorioCadetes(_configuration);
        RepositorioClientes repositorioClientes = new RepositorioClientes(_configuration);

        command.Parameters.Add(new SqliteParameter("@idPedido", idPedido));

        using (SqliteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int numeroPedido = reader.GetInt32(0);
            string observaciones = reader[1].ToString()!;
            Estado estado = (Estado) reader.GetInt32(2);
            int idCliente = reader.GetInt32(3);
            int idCadete = reader.GetInt32(4);
            string? nombreCadete = repositorioCadetes.ObtenerCadetePorId(idCadete)?.Nombre;
            string? nombreCliente = repositorioClientes.ObtenerClientePorId(idCliente)?.Nombre;
            
            pedido = new Pedido(numeroPedido, observaciones, estado, idCliente, nombreCliente, idCadete, nombreCadete);
          }
        }

        connection.Close();
      }

      return pedido;
    }
    catch (SqliteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public void AgregarPedido(Pedido pedido)
  {
    try
    {
      string query = "INSERT INTO pedido (observaciones, estado, id_cliente, id_cadete) VALUES(@observaciones, @estado, @idCliente, @idCadete)";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@observaciones", pedido.Observaciones));
        command.Parameters.Add(new SqliteParameter("@estado", pedido.Estado));
        command.Parameters.Add(new SqliteParameter("@idCliente", pedido.IdCliente));
        command.Parameters.Add(new SqliteParameter("@idCadete", pedido.IdCadete));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El pedido se agregó correctamente");
    }
    catch (SqliteException ex)
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
      string query = "UPDATE pedido SET observaciones = @observaciones, estado = @estado, id_cliente = @idCliente, id_cadete = @idCadete WHERE numero_pedido = @numeroPedido";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@observaciones", pedido.Observaciones));
        command.Parameters.Add(new SqliteParameter("@estado", pedido.Estado));
        command.Parameters.Add(new SqliteParameter("@idCliente", pedido.IdCliente));
        command.Parameters.Add(new SqliteParameter("@idCadete", pedido.IdCadete));
        command.Parameters.Add(new SqliteParameter("@numeroPedido", pedido.NumeroPedido));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El pedido se actualizó correctamente");
    }
    catch (SqliteException ex)
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
      string query = "DELETE FROM pedido WHERE numero_pedido = @idPedido";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@idPedido", idPedido));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El pedido se eliminó correctamente");
    }
    catch (SqliteException ex)
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