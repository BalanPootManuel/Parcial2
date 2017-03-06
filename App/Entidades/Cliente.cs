using System;

namespace Entidades
{
  public class Cliente
  {
    public int codcliente { get; set; }
    // Esto indica que la entidad cliente esta relacionada al entidad Distrito
    public Categoria ecategoria { get; set; }
    public string nombre { get; set; }
    public string apellidos { get; set; }

    public string direccion { get; set; }
    public string telefono { get; set; }
  
    // Prop. de solo lectura para obtener el nombre de la categoria
    public string nombrecategoria
    {
      get { return ecategoria.nomcategoria; }
    }
  }
}
