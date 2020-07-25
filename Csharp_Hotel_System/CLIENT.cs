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
     * Create a class client to:
     * 1. add a new client
     * 2. edit client data 
     * 3. remove client
     * 4. get all client 
     * 
     */
    class CLIENT
    {
        CONNECT conn = new CONNECT();
        //create a function to instert a new client
        public bool insertClient(String fname, String lname, String phone, String country)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `client`(`first_name`, `last_name`, `phone`, `country`) VALUES (@fnm,@lnm,@phn,@cnt)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();

            //@fnm,@lnm,@phn,@cnt
            command.Parameters.Add("@fnm",MySqlDbType.VarChar).Value =fname;
            command.Parameters.Add("@lnm", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@cnt", MySqlDbType.VarChar).Value = country;

            conn.openConnection();

            //Phương thức ExecuteNonQuery nó chỉ trả về kết quả là số dòng dữ liệu bị ảnh hưởng
            //Thường dùng khi insert,removed,edit,...
            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }
        }

        // create a function to get the clients list (table)
        public DataTable getClients()
        {
            //Tạo đối tượng SqlCommand trong C# để truy vấn và cập nhật tới CSDL SQL Server
            MySqlCommand command = new MySqlCommand("SELECT * FROM `client`",conn.getConnection());

            //DataAdapter chính là cầu nối giữa Dataset và Datasource
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            //SelectCommand: Cho phép lấy thông tin từ nguồn dữ liệu về
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //create a function to edit a client data
        public bool editClient(int id, String fname, String lname, String phone, String country)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `client` SET `first_name`=@fnm,`last_name`=@lnm,`phone`=@phn,`country`=@cnt WHERE `id`=@cid";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            //@cid,@fnm,@lnm,@phn,@cnt
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fnm", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lnm", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@cnt", MySqlDbType.VarChar).Value = country;

            conn.openConnection();

            //Phương thức ExecuteNonQuery nó chỉ trả về kết quả là số dòng dữ liệu bị ảnh hưởng
            //Thường dùng khi insert,removed,edit,...
            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }

        }

    }
}
