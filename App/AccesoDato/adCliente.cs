using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AccesoDato
{
  public sealed class adCliente
  {
    public static bool Grabar(Entidades.Cliente pEntidad)
    {
      using (var cn = new MySqlConnection(conexion.LeerCC))
      {
        // Contamos cuantos clientes existen segun el codcliente o nombre
        using (var cmd = new MySqlCommand(@"select ifnull(count(codcliente),0) from clientes where codcliente=@cod ;", cn))
        {
          //Asignar valores a los parametros
          cmd.Parameters.AddWithValue("cod", pEntidad.codcliente);
          cmd.Parameters.AddWithValue("cdis", pEntidad.ecategoria.codcategoria);
          cmd.Parameters.AddWithValue("nom", pEntidad.nombre);
          cmd.Parameters.AddWithValue("apellido", pEntidad.apellidos);
          cmd.Parameters.AddWithValue("dir", pEntidad.direccion);
          cmd.Parameters.AddWithValue("telef", pEntidad.telefono);

          cn.Open();
          // Ejecutamos el comando y verificamos si el resultado es mayor a cero actualizar, caso contrario insertar
          if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
          {
            // Si es mayor a cero, quiere decir que existe al menos un registro con los datos ingresados
            // Entonces antes de actualizar, hacer las siguientes comprobaciones
            if (pEntidad.codcliente == 0)
              throw new Exception("El cliente(a) ya esta registrado en el sistema, verifique los datos por favor!...");

            // Verifica si ya existe un registro con el mismo nombre de la categoria
            cmd.CommandText = @"select ifnull(count(codcliente),0) from clientes where codcliente<>@cod and telefono=@telef;";
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
              throw new Exception("No se puede grabar un valor duplicado, verifique los datos por favor!...");

            // Si las comprobaciones anteriores resultaron ser falsa, entonces actualizar
            cmd.CommandText = @"update clientes set codcategoria=@cdis, nombre=@nom,apellidos=@apellido, direccion=@dir, telefono=@telef where codcliente=@cod;";
          }
          else
            cmd.CommandText = @"insert into clientes (codcategoria, nombre, apellidos, direccion, telefono) values (@cdis, @nom, @apellido, @dir, @telef);";

          // Ejecutamos el comando que puede ser para update o insert
          return Convert.ToBoolean(cmd.ExecuteNonQuery());
        }
      }
    }

    public static bool Eliminar(Entidades.Cliente pEntidad)
    {
      using (var cn = new MySqlConnection(conexion.LeerCC))
      {
        // Como nadie depende de esta entidad (clientes) no habra comprobaciones de dependencia
        using (var cmd = new MySqlCommand(@"delete from clientes where codcliente=@cod;", cn))
        {
          cmd.Parameters.AddWithValue("cod", pEntidad.codcliente);

          cn.Open();
          // Ejecuta el comando
          return Convert.ToBoolean(cmd.ExecuteNonQuery());
        }
      }
    }

    public static List<Entidades.Cliente> Leer(string dato)
    {
      // Crea un obj. lista de tipo Clientes
      var lista = new List<Entidades.Cliente>();
      // Crear el objeto de conexion
      using (var cn = new MySqlConnection(conexion.LeerCC))
      {
        // crear el comando
        using (var cmd = new MySqlCommand("select codcliente, nomcategoria, nombre,apellidos, direccion, telefono from categorias inner join clientes on categorias.codcategoria = clientes.codcategoria where nombre like Concat(@nom, '%');", cn))
        {
          //Asignar valores a los parametros
          cmd.Parameters.AddWithValue("nom", dato);

          // Abrir el objeto de conexion
          cn.Open();
          using (var dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              // Crea un objeto del categoria
              var objCategoria = new Entidades.Categoria();
              var objCliente = new Entidades.Cliente();
              objCliente.codcliente = Convert.ToInt32(dr[dr.GetOrdinal("codcliente")]);

              // Aqui obtenemos el nombre de la categoria para luego ser enviado al objeto cliente}
              objCategoria.nomcategoria = Convert.ToString(dr[dr.GetOrdinal("nomcategoria")]);
              objCliente.ecategoria= objCategoria;

              objCliente.nombre = Convert.ToString(dr[dr.GetOrdinal("nombre")]);
              objCliente.apellidos = Convert.ToString(dr[dr.GetOrdinal("apellidos")]);
              objCliente.direccion = Convert.ToString(dr[dr.GetOrdinal("direccion")]);
              objCliente.telefono = Convert.ToString(dr[dr.GetOrdinal("telefono")]);
              // El objeto cliente es agregado a la lista
              lista.Add(objCliente);
              // marcamos a los objetos creamos como nulos
              objCategoria = null;
              objCliente = null;
            }
          }

          // Retorna una lista de datos
          return lista;
        }
      }
    }
  }
}
