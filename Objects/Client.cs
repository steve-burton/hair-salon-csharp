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

		public static void DeleteAll()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
			cmd.ExecuteNonQuery();
			conn.Close();
		}
	}
}
