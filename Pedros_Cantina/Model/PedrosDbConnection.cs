using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pedros_Cantina.Model
{
	public class PedrosDbConnection
	{
		SqlConnection _conn;
		public SqlConnection Connection
		{
			get { return _conn; }
			set { _conn = value; }
		}

		public PedrosDbConnection() 
		{
		}

		public void ConnectToDB()
		{
			//string ConnString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Pedros_Cantina;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

			var config = new ConfigurationBuilder()
			.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
			.Build();

			var connect = config["ConnectionSecret"];
			_conn = new SqlConnection(connect);
			_conn.Open();
					}

		public void DisconnectFromDB()
		{
			_conn.Close();
		}

		

	}
}
