using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadeteriaMVC.Models;
using System.Data.SQLite;
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
    this.ConnectionString = this._configuration.GetConnectionString("SQLite");
  }

  public List<Cliente> ObtenerClientes()
  {
    try
    {
      List<Cliente> cliente = new List<Cliente>();

      string query = "SELECT * FROM cliente";

      using(SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        using(SQLiteDataReader reader = command.ExecuteReader())
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
    catch (SQLiteException ex)
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

      using(SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idCliente", idCliente));

        using (SQLiteDataReader reader = command.ExecuteReader())
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
    catch (SQLiteException ex)
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

      using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@nombre", cliente.Nombre));
        command.Parameters.Add(new SQLiteParameter("@direccion", cliente.Direccion));
        command.Parameters.Add(new SQLiteParameter("@telefono", cliente.Telefono));
        command.Parameters.Add(new SQLiteParameter("@referencia", cliente.Referencia));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cliente se agregó correctamente");
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

  public void EditarCliente(Cliente cliente)
  {
    try
    {
      string query = "UPDATE cliente SET nombre = @nombre, direccion = @direccion, telefono = @telefono, referencia = @referencia WHERE id = @idCliente";

      using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idCliente", cliente.Id));
        command.Parameters.Add(new SQLiteParameter("@nombre", cliente.Nombre));
        command.Parameters.Add(new SQLiteParameter("@direccion", cliente.Direccion));
        command.Parameters.Add(new SQLiteParameter("@telefono", cliente.Telefono));
        command.Parameters.Add(new SQLiteParameter("@referencia", cliente.Referencia));
        
        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cliente se actualizó correctamente");
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

  public void EliminarCliente(int idCliente)
  {
    try
    {
      string query = "DELETE FROM cliente WHERE id = @idCliente";

      using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();

        SQLiteCommand command = new SQLiteCommand(query, connection);

        command.Parameters.Add(new SQLiteParameter("@idCliente", idCliente));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cliente se eliminó correctamente");
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