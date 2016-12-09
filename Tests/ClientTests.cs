using Xunit;
using System;
using System.Collections.Generic;
using HairSalon.Objects;

namespace  HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }


    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
