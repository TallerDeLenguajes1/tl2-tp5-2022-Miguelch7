using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaCadeteriaMVC.ViewModels;
using Microsoft.Data.Sqlite;
using NLog;

public interface IRepositorioUsuarios
{
  UsuarioViewModel Login(string username, string password);

}

public class RepositorioUsuarios : IRepositorioUsuarios
{
  private readonly string ConnectionString;
  private readonly IConfiguration _configuration;
  private static readonly Logger logger = LogManager.GetCurrentClassLogger();

  public RepositorioUsuarios(IConfiguration configuration)
  {
    this._configuration = configuration;
    this.ConnectionString = this._configuration.GetConnectionString("Sqlite");
  }

  public UsuarioViewModel Login(string username, string password)
  {
    UsuarioViewModel? user = null;

    SqliteConnection connection = new SqliteConnection(ConnectionString);
    SqliteCommand command = new();
    connection.Open();

    command.Connection = connection;

    try
    {
      command.CommandText = "SELECT * FROM usuario WHERE username = $username AND password = $password";

      command.Parameters.AddWithValue("$username", username);
      command.Parameters.AddWithValue("$password", password);

      var reader = command.ExecuteReader();

      if (reader.Read() && !reader.IsDBNull(0))
      {
        int id = reader.GetInt32(0);
        string userName = reader.GetString(1);
        string userPassword = reader.GetString(2);
        Rol rol = (Rol) reader.GetInt32(3);

        user = new UsuarioViewModel(id, userName, userPassword, rol);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine("Ha ocurrido un error", ex.Message);
    }

    connection.Close();

    return user!;
  }
}