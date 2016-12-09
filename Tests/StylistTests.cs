using Xunit;
using System;
using System.Collections.Generic;
using HairSalon.Objects;

namespace  HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Stylist.GetAll().Count;

      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
