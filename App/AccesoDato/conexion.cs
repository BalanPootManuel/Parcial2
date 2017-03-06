using System;

namespace AccesoDato
{
  public sealed class conexion
  {
    public static string LeerCC
    {
      get { return "Server=localhost; Port=3306; User Id=root; password=root; Persist Security Info=True; database=app"; }
    }



  }
}
