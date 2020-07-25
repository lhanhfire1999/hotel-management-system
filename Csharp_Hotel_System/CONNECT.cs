using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Csharp_Hotel_System
{
	/*
	 * This class will connect between our app and mysql database
	 * First you can download mysql connector and add it to this project
	 * download link: https://dev.mysql.com/downloads/connector/net/8.0.html
	 */
	class CONNECT
	{
		private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=Csharp_Hotel_DB");

		// create a function to return our connection 
		public MySqlConnection getConnection()
        {
			return connection;
        }

		// create a function to open the connection 
		public void openConnection()
        {
			if(connection.State==ConnectionState.Closed)
            {
				connection.Open();
            }
        }

		// create a function to close the connection 
		public void closeConnection()
		{
			if (connection.State == ConnectionState.Open)
			{
				connection.Close();
			}
		}
	}
}
