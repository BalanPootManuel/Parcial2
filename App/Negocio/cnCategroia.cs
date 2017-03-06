using System;
using System.Collections.Generic;

namespace Negocio
{
  public sealed class cnCategroia
  {
    public static bool Grabar(Entidades.Categoria pEntidad)
    {
      //  el nombre dela categoria no podra ser un valor nulo o vacio
      // Sera obligatorio ingresar dicho dato
      if (string.IsNullOrEmpty(pEntidad.nomcategoria.Trim()))
        throw new Exception("El nombre de la categoria no puede ser un valor nulo o vacio");

      return AccesoDato.adCategoria.Grabar(pEntidad);
    }

    public static bool Eliminar(Entidades.Categoria pEntidad)
    {
      return AccesoDato.adCategoria.Eliminar(pEntidad);
    }

    public static List<Entidades.Categoria> Listar(string dato)
    {
      return AccesoDato.adCategoria.Leer(dato);
    }
  }
}
