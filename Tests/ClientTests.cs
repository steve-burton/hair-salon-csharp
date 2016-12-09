using Xunit;
using System;
using System.Collections.Generic;
using Review.Objects;

namespace  HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=review_test;Integrated Security=SSPI;";
    }

    [Fact]
    
  }
}
