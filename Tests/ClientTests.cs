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

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Client.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnTrueForSameNames()
    {
      Client firstClient = new Client("Susan", "Portland", 1);
      Client secondClient = new Client("Susan", "Portland", 1);

      Assert.Equal(firstClient, secondClient);
    }


    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
