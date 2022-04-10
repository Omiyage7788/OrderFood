using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace FoodProject.Models
{
	public class DataConnection
	{
		static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderMealConnection"].ConnectionString);
		SqlCommand cmd = new SqlCommand("", conn);

		public SqlCommand Cmd {
			get
			{
				return cmd;
			}
			set
			{
				cmd = value;
			}
		}

		public void executeProc(string proc)
		{
			cmd.CommandText = proc;
			cmd.CommandType = CommandType.StoredProcedure;

			conn.Open();
			cmd.ExecuteNonQuery();

			conn.Close();
		}
	}
}