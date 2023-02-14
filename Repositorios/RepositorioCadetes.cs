using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadeteriaMVC.Models;
using Microsoft.Data.Sqlite;
using NLog;

public interface IRepositorioCadetes
{
  List<Cadete> ObtenerCadetes();
  Cadete? ObtenerCadetePorId(int idCadete);
  void AgregarCadete(Cadete cadete);
  void EditarCadete(Cadete cadete);
  void EliminarCadete(int idCadete);

}

public class RepositorioCadetes : IRepositorioCadetes
{
  private readonly string ConnectionString;
  private readonly IConfiguration _configuration;
  private static readonly Logger logger = LogManager.GetCurrentClassLogger();

  public RepositorioCadetes(IConfiguration configuration)
  {
    this._configuration = configuration;
    this.ConnectionString = this._configuration.GetConnectionString("Sqlite");
  }

  public List<Cadete> ObtenerCadetes()
  {
    try
    {
      List<Cadete> cadetes = new List<Cadete>();

      string query = "SELECT * FROM cadete";

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
            
            cadetes.Add(new Cadete(id, nombre, direccion, telefono));
          }
        }

        connection.Close();
      }

      return cadetes;
    }
    catch (SqliteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public Cadete? ObtenerCadetePorId(int idCadete)
  {
    try
    {
      Cadete? cadete = null;

      string query = "SELECT * FROM cadete WHERE id = @idCadete";

      using(SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

        using (SqliteDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            int id = reader.GetInt32(0);
            string nombre = reader[1].ToString()!;
            string direccion = reader[2].ToString()!;
            string telefono = reader[3].ToString()!;
            
            cadete = new Cadete(id, nombre, direccion, telefono);
          }
        }

        connection.Close();
      }

      return cadete;
    }
    catch (SqliteException ex)
    {
      logger.Debug(ex.ToString());
      throw new Exception("ERROR!", ex);
    }
  }

  public void AgregarCadete(Cadete cadete)
  {
    try
    {
      string query = $"INSERT INTO cadete (nombre, direccion, telefono) VALUES(@nombre, @direccion, @telefono)";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@nombre", cadete.Nombre));
        command.Parameters.Add(new SqliteParameter("@direccion", cadete.Direccion));
        command.Parameters.Add(new SqliteParameter("@telefono", cadete.Telefono));

        command.ExecuteNonQuery();
        
        connection.Close();
      }

      logger.Info("Se agregó el cadete exitosamente");
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

  public void EditarCadete(Cadete cadete)
  {
    try
    {
      string query = "UPDATE cadete SET nombre = @nombre, direccion = @direccion, telefono = @telefono WHERE id = @idCadete";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@idCadete", cadete.Id));
        command.Parameters.Add(new SqliteParameter("@nombre", cadete.Nombre));
        command.Parameters.Add(new SqliteParameter("@direccion", cadete.Direccion));
        command.Parameters.Add(new SqliteParameter("@telefono", cadete.Telefono));
        
        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cadete se actualizó correctamente");
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

  public void EliminarCadete(int idCadete)
  {
    try
    {
      string query = "DELETE FROM cadete WHERE id = @idCadete";

      using (SqliteConnection connection = new SqliteConnection(ConnectionString))
      {
        connection.Open();

        SqliteCommand command = new SqliteCommand(query, connection);

        command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

        command.ExecuteNonQuery();

        connection.Close();
      }

      logger.Info("El cadete se eliminó correctamente");
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