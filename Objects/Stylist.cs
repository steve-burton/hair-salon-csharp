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

				Stylist newStylist = new Stylist(stylistDetails, stylistName, stylistId);
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
