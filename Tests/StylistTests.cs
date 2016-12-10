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

    [Fact]
    public void Test_Equal_ReturnTrueForSameNames()
    {
      Stylist firstStylist = new Stylist("Jenny", "Portland");
      Stylist secondStylist = new Stylist("Jenny", "Portland");

      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      Stylist testStylist = new Stylist("Jenny", "Portland");
      testStylist.Save();

      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Stylist testStylist = new Stylist("Jenny", "Portland");
      testStylist.Save();

      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      Stylist testStylist = new Stylist("Jenny", "Portland");
      testStylist.Save();

      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Test_GetClients_RetrievesAllClientsByStylist()
    {
      Stylist testStylist = new Stylist("Jenny", "Portland");
      testStylist.Save();

      Client firstClient = new Client("Susan", "Portland", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Sally", "Oregon City", testStylist.GetId());
      secondClient.Save();
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      Assert.Equal(testClientList, resultClientList);
    }

    [Fact]
    public void Test_Update_UpdatesStylistInDatabase()
    {
      Stylist testStylist = new Stylist("Jenny", "Portland");
      testStylist.Save();
      string newStylistDetails = ("Oregon City");

      testStylist.Update(newStylistDetails);

      string result = testStylist.GetStylistDetails();

      Assert.Equal(newStylistDetails, result);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
