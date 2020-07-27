using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Csharp_Hotel_System
{
    class RESERVATION
    {
        CONNECT conn = new CONNECT();
        
        //get all reservation
        public DataTable getAllReserv()
        {
            //Tạo đối tượng SqlCommand trong C# để truy vấn và cập nhật tới CSDL SQL Server
            MySqlCommand command = new MySqlCommand("SELECT * FROM `reservations`", conn.getConnection());

            //DataAdapter chính là cầu nối giữa Dataset và Datasource
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            //SelectCommand: Cho phép lấy thông tin từ nguồn dữ liệu về
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // create a function to insert a new reservation
        public bool addReservation(int number, int clientid, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `reservations`(`roomNumber`, `clientid`, `DateIn`, `DateOut`) VALUES (@rnb,@cid,@din,@dout)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();

            //@rnb,@cid,@din,@dout
            command.Parameters.Add("@rnb", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientid;
            command.Parameters.Add("@din", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.Date).Value = dateOut;

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

        //Create a function to edit the selected reservation
        public bool editRes(int reservID, int number, int clientid, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `reservations` SET `roomNumber`=@rnb,`clientid`=@cid,`DateIn`=@din,`DateOut`=@dout WHERE `reservID`=@rvid";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            //@rvid,@rnb,@cid,@din,@dout
            command.Parameters.Add("@rvid", MySqlDbType.Int32).Value = reservID;
            command.Parameters.Add("@rnb", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientid;
            command.Parameters.Add("@din", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.Date).Value = dateOut;

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

        // create a function to delete the selected reservation
        // we only need the reservation id
        public bool removeReserv(int rsv_id)
        {
            MySqlCommand command = new MySqlCommand();
            String removeQuery = "DELETE FROM `reservations` WHERE `reservID`=@rvid";
            command.CommandText = removeQuery;
            command.Connection = conn.getConnection();

            //@rvid
            command.Parameters.Add("@rvid", MySqlDbType.Int32).Value = rsv_id;

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
