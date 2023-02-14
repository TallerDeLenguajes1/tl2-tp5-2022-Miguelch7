using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadeteriaMVC.Models;
using Microsoft.Data.Sqlite;
using NLog;

public interface IRepositorioClientes
{
  List<Cliente> ObtenerClientes();
  Cliente? ObtenerClientePorId(int idCliente);
  void AgregarCliente(Cliente cliente);
  void EditarCliente(Cliente cliente);
  void EliminarCliente(int idCliente);

}

public class RepositorioClientes : IRepositorioClientes
{
  private readonly string ConnectionString;
  private readonly IConfiguration _configuration;
  private static readonly Logger logger = LogManager.GetCurrentClassLogger();

  public RepositorioClientes(IConfiguration configuration)
  {
    this._configuration = configuration;
    this.ConnectionString = this._configuration.GetConnectionString("Sqlite");
  }

  public List<Cliente> ObtenerClientes()
  {
    try
    {
      List<Cliente> cliente = new List<Cliente>();

      string query = "SELECT * FROM cliente";

      using(SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        using(SqliteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int id = reader.GetInt32(0);
            string nombre = reader[1].ToString()!;
            string direccion = reader[2].ToString()!;
            string telefono = reader[3].ToString()!;
            string referencia = reader[4].ToString()!;
            
            cliente.Add(new Cliente(id, nombre, direccion, telefono, referencia));
          }
        }

        connection.Close();
      }

      return cliente;
    }
    catch (SqliteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public Cliente? ObtenerClientePorId(int idCliente)
  {
    try
    {
      Cliente? cliente = null;

      string query = "SELECT * FROM cliente WHERE id = @idCliente";

      using(SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@idCliente", idCliente));

        using (SqliteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int id = reader.GetInt32(0);
            string nombre = reader[1].ToString()!;
            string direccion = reader[2].ToString()!;
            string telefono = reader[3].ToString()!;
            string referencia = reader[4].ToString()!;
            
            cliente = new Cliente(id, nombre, direccion, telefono, referencia);
          }
        }

        connection.Close();
      }

      return cliente;
    }
    catch (SqliteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public void AgregarCliente(Cliente cliente)
  {
    try
    {
      string query = "INSERT INTO cliente (nombre, direccion, telefono, referencia) VALUES(@nombre, @direccion, @telefono, @referencia)";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@nombre", cliente.Nombre));
        command.Parameters.Add(new SqliteParameter("@direccion", cliente.Direccion));
        command.Parameters.Add(new SqliteParameter("@telefono", cliente.Telefono));
        command.Parameters.Add(new SqliteParameter("@referencia", cliente.Referencia));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cliente se agregó correctamente");
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

  public void EditarCliente(Cliente cliente)
  {
    try
    {
      string query = "UPDATE cliente SET nombre = @nombre, direccion = @direccion, telefono = @telefono, referencia = @referencia WHERE id = @idCliente";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@idCliente", cliente.Id));
        command.Parameters.Add(new SqliteParameter("@nombre", cliente.Nombre));
        command.Parameters.Add(new SqliteParameter("@direccion", cliente.Direccion));
        command.Parameters.Add(new SqliteParameter("@telefono", cliente.Telefono));
        command.Parameters.Add(new SqliteParameter("@referencia", cliente.Referencia));
        
        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cliente se actualizó correctamente");
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

  public void EliminarCliente(int idCliente)
  {
    try
    {
      string query = "DELETE FROM cliente WHERE id = @idCliente";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@idCliente", idCliente));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cliente se eliminó correctamente");
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