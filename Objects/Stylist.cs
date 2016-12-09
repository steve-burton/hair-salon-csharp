using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
	public class Stylist
	{
		private int _id;
		private string _stylistName;
		private string _stylistDetails;

		public Stylist(string stylistName, string stylistDetails, int id = 0)
		{
			_stylistName = stylistName;
			_stylistDetails = stylistDetails;
			_id = id;
		}

		public int GetId()
		{
			return _id;
		}
		public string GetStylistName()
		{
			return _stylistName;
		}
		public string GetStylistDetails()
		{
			return _stylistDetails;
		}
		public void Set(int id)
		{
			_id = id;
		}
		public void Set(string stylistName)
		{
			_stylistName = stylistName;
		}
		// public void Set(string stylistDetails)
		// {
		// 	_stylistDetails = stylistDetails;
		// }

		public override bool Equals(System.Object otherStylist)
		{
			if (!(otherStylist is Stylist))
			{
				return false;
			}
			else
			{
				Stylist newStylist = (Stylist) otherStylist;
				bool idEquality = (this.GetId() == newStylist.GetId());
				bool nameEquality = (this.GetStylistName() == newStylist.GetStylistName());
				bool detailsEquality = (this.GetStylistDetails() == newStylist.GetStylistDetails());
				return (idEquality && nameEquality && detailsEquality);
			}
		}

		public static List<Stylist> GetAll()
		{
			List<Stylist> allStylists = new List<Stylist>{};

			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				int stylistId = (rdr.GetInt32(0));
				string stylistName = (rdr.GetString(1));
				string stylistDetails = (rdr.GetString(2));

				Stylist newStylist = new Stylist(stylistName, stylistDetails, stylistId);
				allStylists.Add(newStylist);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
			return allStylists;
		}

		public void Save()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("INSERT INTO stylists (stylist_name, stylist_details) OUTPUT INSERTED.id VALUES (@StylistName, @StylistDetails);", conn);

			SqlParameter stylistNameParameter = new SqlParameter();
			stylistNameParameter.ParameterName = "@StylistName";
			stylistNameParameter.Value = this.GetStylistName();

			SqlParameter stylistDetailsParameter = new SqlParameter();
			stylistDetailsParameter.ParameterName = "@StylistDetails";
			stylistDetailsParameter.Value = this.GetStylistDetails();

			cmd.Parameters.Add(stylistNameParameter);
			cmd.Parameters.Add(stylistDetailsParameter);

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

		public static Stylist Find(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
			SqlParameter stylistIdParameter = new SqlParameter();
			stylistIdParameter.ParameterName = "@StylistId";
			stylistIdParameter.Value = id.ToString();
			cmd.Parameters.Add(stylistIdParameter);
			SqlDataReader rdr = cmd.ExecuteReader();

			int foundStylistId = 0;
			string foundStylistName = null;
			string foundStylistDetails = null;

			while(rdr.Read())
			{
				foundStylistId = rdr.GetInt32(0);
				foundStylistName = rdr.GetString(1);
				foundStylistDetails = rdr.GetString(2);
			}
			Stylist foundStylist = new Stylist(foundStylistName, foundStylistDetails, foundStylistId);

			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
			return foundStylist;
		}

		public List<Client> GetClients()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @ClientStylistId;", conn);
			SqlParameter stylistIdParameter = new SqlParameter();
			stylistIdParameter.ParameterName = "@ClientStylistId";
			stylistIdParameter.Value = this.GetId();
			cmd.Parameters.Add(stylistIdParameter);
			SqlDataReader rdr = cmd.ExecuteReader();

			List<Client> clients = new List<Client> {};
			while(rdr.Read())
			{
				int clientId = rdr.GetInt32(0);
				string clientName = rdr.GetString(1);
				string clientDetails = rdr.GetString(2);
				int clientStylistId = rdr.GetInt32(3);
				Client newClient = new Client(clientName, clientDetails, clientStylistId, clientId);
				clients.Add(newClient);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
			return clients;
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
