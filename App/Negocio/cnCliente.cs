using System;
using System.Collections.Generic;

namespace Negocio
{
  public sealed class cnCliente
  {
    public static bool Grabar(Entidades.Cliente pEntidad)
    {
      //  el nombre del cliente no podra ser un valor nulo o vacio
      // Sera obligatorio ingresar dicho dato
      if (string.IsNullOrEmpty(pEntidad.nombre.Trim()))
        throw new Exception("El cliente no puede ser un valor nulo o vacio");

      return AccesoDato.adCliente.Grabar(pEntidad);
    }

    public static bool Eliminar(Entidades.Cliente pEntidad)
    {
      return AccesoDato.adCliente.Eliminar(pEntidad);
    }

    public static List<Entidades.Cliente> Listar(string dato)
    {
      return AccesoDato.adCliente.Leer(dato);
    }
  }
}
