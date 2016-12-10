using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
	public class Client
	{
		private int _id;
		private string _clientName;
		private string _clientDetails;
		private int _stylistId;

		public Client(string clientName, string clientDetails, int clientStylistId, int id = 0)
		{
			_clientName = clientName;
			_clientDetails = clientDetails;
			_stylistId = clientStylistId;
			_id = id;
		}

		public int GetId()
		{
			return _id;
		}
		public string GetClientName()
		{
			return _clientName;
		}
		public string GetClientDetails()
		{
			return _clientDetails;
		}
		public int GetClientStylistId()
		{
			return _stylistId;
		}
		public void SetId(int id)
		{
			_id = id;
		}
		public void SetClientName(string clientName)
		{
			_clientName = clientName;
		}
		public void SetClientDetails(string clientDetails)
		{
			_clientDetails = clientDetails;
		}
		public void SetClientStylistId(int clientStylistId)
		{
			_stylistId = clientStylistId;
		}

		public override bool Equals(System.Object otherClient)
		{
			if (!(otherClient is Client))
			{
				return false;
			}
			else
			{
				Client newClient = (Client) otherClient;
				bool idEquality = (this.GetId() == newClient.GetId());
				bool nameEquality = (this.GetClientName() == newClient.GetClientName());
				bool detailsEquality = (this.GetClientDetails() == newClient.GetClientDetails());
				bool clientStylistIdEquality = (this.GetClientStylistId() == newClient.GetClientStylistId());
				return (idEquality && nameEquality && detailsEquality && clientStylistIdEquality);
			}
		}

		public static List<Client> GetAll()
		{
			List<Client> allClients = new List<Client>{};

			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				string clientName = rdr.GetString(1);
				string clientDetails = rdr.GetString(2);
				int clientStylistId = rdr.GetInt32(3);
				int clientId = rdr.GetInt32(0);
				Client newClient = new Client(clientName, clientDetails, clientStylistId, clientId);
				allClients.Add(newClient);
			}
			if(rdr != null)
			{
				rdr.Close();
			}
			if(conn != null)
			{
				conn.Close();
			}
			return allClients;
		}

		public void Save()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("INSERT INTO clients (client_name, client_details, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientDetails, @ClientStylistId);", conn);

			SqlParameter clientNameParameter = new SqlParameter();
			clientNameParameter.ParameterName = "@ClientName";
			clientNameParameter.Value = this.GetClientName();

			SqlParameter clientDetailsParameter = new SqlParameter();
			clientDetailsParameter.ParameterName = "@ClientDetails";
			clientDetailsParameter.Value = this.GetClientDetails();

			SqlParameter clientStylistIdParameter = new SqlParameter();
			clientStylistIdParameter.ParameterName = "@ClientStylistId";
			clientStylistIdParameter.Value = this.GetClientStylistId();

			cmd.Parameters.Add(clientNameParameter);
			cmd.Parameters.Add(clientDetailsParameter);
			cmd.Parameters.Add(clientStylistIdParameter);

			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				this._id = rdr.GetInt32(0);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
		}

		public static Client Find(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
			SqlParameter clientIdParameter = new SqlParameter();
			clientIdParameter.ParameterName = "@ClientId";
			clientIdParameter.Value = id.ToString();
			cmd.Parameters.Add(clientIdParameter);
			SqlDataReader rdr = cmd.ExecuteReader();

			int foundClientId = 0;
			string foundClientName = null;
			string foundClientDetails = null;
			int foundClientStylistId = 0;
			while(rdr.Read())
			{
				foundClientId = rdr.GetInt32(0);
				foundClientName = rdr.GetString(1);
				foundClientDetails = rdr.GetString(2);
				foundClientStylistId = rdr.GetInt32(3);
			}
			Client foundClient = new Client(foundClientName, foundClientDetails, foundClientStylistId, foundClientId);

			if(rdr != null)
			{
				rdr.Close();
			}
			if(conn != null)
			{
				conn.Close();
			}
			return foundClient;
		}

		public void Update(string newClientDetails)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("UPDATE clients SET client_details = @NewClientDetails OUTPUT INSERTED.client_details WHERE id = @ClientId;", conn);

			SqlParameter newClientDetailsParameter = new SqlParameter();
			newClientDetailsParameter.ParameterName = "@NewClientDetails";
			newClientDetailsParameter.Value = newClientDetails;
			cmd.Parameters.Add(newClientDetailsParameter);

			SqlParameter clientIdParameter = new SqlParameter();
			clientIdParameter.ParameterName = "@ClientId";
			clientIdParameter.Value = this.GetId();
			cmd.Parameters.Add(clientIdParameter);
			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				this._clientDetails = rdr.GetString(0);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
		}

		public void UpdateStylist(int newClientStylist)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("UPDATE clients SET stylist_id = @NewClientStylist OUTPUT INSERTED.stylist_id WHERE id = @ClientId;", conn);

			SqlParameter newClientStylistParameter = new SqlParameter();
			newClientStylistParameter.ParameterName = "@NewClientStylist";
			newClientStylistParameter.Value = newClientStylist;
			cmd.Parameters.Add(newClientStylistParameter);

			SqlParameter clientIdParameter = new SqlParameter();
			clientIdParameter.ParameterName = "@ClientId";
			clientIdParameter.Value = this.GetId();
			cmd.Parameters.Add(clientIdParameter);
			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				this._stylistId = rdr.GetInt32(0);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
		}

		public void Delete()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);

			SqlParameter clientIdParameter = new SqlParameter();
			clientIdParameter.ParameterName = "@ClientId";
			clientIdParameter.Value = this.GetId();

			cmd.Parameters.Add(clientIdParameter);
			cmd.ExecuteNonQuery();

			if (conn != null)
			{
				conn.Close();
			}
		}

		public static void DeleteAll()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
			cmd.ExecuteNonQuery();
			conn.Close();
		}
	}
}
