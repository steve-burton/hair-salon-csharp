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

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      Client testClient = new Client("Susan", "Portland", 1);
      testClient.Save();

      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Client testClient = new Client("Susan", "Portland", 1);
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      Client testClient = new Client("Susan", "Portland", 1);
      testClient.Save();
      Client foundClient = Client.Find(testClient.GetId());

      Assert.Equal(testClient, foundClient);
    }

    [Fact]
    public void Test_Update_UpdatesClientDetailsInDatabase()
    {
      Client testClient = new Client("Susan", "Portland", 1);
      testClient.Save();
      string newClientDetails = ("Oregon City");

      testClient.Update(newClientDetails);

      string result = testClient.GetClientDetails();

      Assert.Equal(newClientDetails, result);
    }

    [Fact]
    public void Test_UpdateStylist_UpdatesClientStylistInDatabase()
    {
      Client testClient = new Client("Susan", "Portland", 1);
      testClient.Save();
      int newClientStylist = (2);

      testClient.UpdateStylist(newClientStylist);

      int result = testClient.GetClientStylistId();

      Assert.Equal(newClientStylist, result);
    }

    [Fact]
    public void Test_Delete_DeleteClientFromDatabase()
    {
      Client testClient1 = new Client("Susan", "Portland", 1);
      testClient1.Save();
      Client testClient2 = new Client("Sally", "NoPo", 1);
      testClient2.Save();

      testClient1.Delete();
      List<Client> resultClients = Client.GetAll();
      List<Client> testClientList = new List<Client> {testClient2};

      Assert.Equal(testClientList, resultClients);
    }


    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
