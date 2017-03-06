using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AccesoDato
{
  public sealed class adCategoria
  {
    public static bool Grabar(Entidades.Categoria pEntidad)
    {
      using (var cn = new MySqlConnection(conexion.LeerCC))
      {
        // Contamos cuantas categorias existen segun el codcategoria o nomcategoria
        using (var cmd = new MySqlCommand(@"select ifnull(count(codcategoria),0) from categorias where codcategoria=@cod;", cn))
        {
          cmd.Parameters.AddWithValue("cod", pEntidad.codcategoria);
          cmd.Parameters.AddWithValue("nom", pEntidad.nomcategoria);

          cn.Open();
          // Ejecutamos el comando y verificamos si el resultado es mayor a cero actualizar, caso contrario insertar
          if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
          {
            // Si es mayor a cero, quiere decir que existe al menos un registro con los datos ingresados
            // Entonces antes de actualizar, hacer las siguientes comprobaciones
            if (pEntidad.codcategoria == 0)
              throw new Exception("La categoria ya esta registrado en el sistema, verifique los datos por favor!...");

            // Verifica si ya existe un registro con el mismo nombre de la categoria
            cmd.CommandText = @"select ifnull(count(codcategoria),0) from categorias where codcategoria<>@cod and nomcategoria=@nom;";
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
              throw new Exception("No se puede Registrar un valor duplicado, verifique los datos por favor!...");

            // Si las comprobaciones anteriores resultaron ser falsa, entonces actualizar
            cmd.CommandText = @"update categorias set nomcategoria=@nom where codcategoria=@cod;";
          }
          else
            cmd.CommandText = @"insert into categorias (nomcategoria) values (@nom);";

          // Ejecutamos el comando que puede ser para update o insert
          return Convert.ToBoolean(cmd.ExecuteNonQuery());
        }
      }
    }

    public static bool Eliminar(Entidades.Categoria pEntidad)
    {
      using (var cn = new MySqlConnection(conexion.LeerCC))
      {
        // Contar la cantidad de clientes que existe en un determinado categoria
        using (var cmd = new MySqlCommand(@"select ifnull(count(codcliente),0) from clientes where codcategoria=@cod;", cn))
        {
          cmd.Parameters.AddWithValue("cod", pEntidad.codcategoria);

          cn.Open();
          // Si es mayor a cero, quiere decir que existen clientes en dicha categoria que intentamos eliminar
          if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            throw new Exception("No es posible eliminar el registro, ya que éste se encuentra en uso...");

          cmd.CommandText = "delete from categorias where codcategoria=@cod;";
          return Convert.ToBoolean(cmd.ExecuteNonQuery());
        }
      }
    }

    public static List<Entidades.Categoria> Leer(string dato)
    {
      // Crea un obj. lista de tipo Categoria
      var lista = new List<Entidades.Categoria>();
      // Crear el objeto de conexion
      using (var cn = new MySqlConnection(conexion.LeerCC))
      {
        // crear el comando
        using (var cmd = new MySqlCommand("select codcategoria, nomcategoria from categorias where nomcategoria like Concat(@nom, '%');", cn))
        {
          //Asignar valores a los parametros
          cmd.Parameters.AddWithValue("nom", dato);

          // Abrir el objeto de conexion
          cn.Open();
          using (var dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              // Crea un objeto de la categoria
              var objCategoria = new Entidades.Categoria();
              objCategoria.codcategoria = Convert.ToInt32(dr[dr.GetOrdinal("codcategoria")]);
              objCategoria.nomcategoria = Convert.ToString(dr[dr.GetOrdinal("nomcategoria")]);
              // El objeto categoria es agregado a la lista
              lista.Add(objCategoria);
              objCategoria = null;
            }
          }

          // Retorna una lista de datos
          return lista;
        }
      }
    }
  }
}
