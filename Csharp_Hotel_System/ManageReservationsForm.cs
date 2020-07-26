using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csharp_Hotel_System
{
    public partial class ManageReservationsForm : Form
    {
        public ManageReservationsForm()
        {
            InitializeComponent();
        }
        ROOM room = new ROOM();
        RESERVATION reservation = new RESERVATION();
        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            // display room's type
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";

            // display room's number depending on the selected type
            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            comboBoxRoomNumber.DataSource = room.roomByType(type);
            comboBoxRoomNumber.DisplayMember = "number";
            comboBoxRoomNumber.ValueMember = "number";

            // show all reservations in the datagridview
            dataGridView1.DataSource = reservation.getAllReserv();
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxReservId.Text = "";
            textBoxClientID.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            dateTimePickerIN.Value = DateTime.Now;
            dateTimePickerOUT.Value = DateTime.Now;
        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // display room's number depending on the selected type
                int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
                comboBoxRoomNumber.DataSource = room.roomByType(type);
                comboBoxRoomNumber.DisplayMember = "number";
                comboBoxRoomNumber.ValueMember = "number";

            }
            catch(Exception )
            {
                //do nothing
            }
        }

        private void buttonAddReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int clientID = Convert.ToInt32(textBoxClientID.Text);
                int roomNumber = Convert.ToInt32(comboBoxRoomNumber.SelectedValue);
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                //date in must be = or > today date
                //date out must be = or > date in
                if(dateIn < DateTime.Now)
                {
                    MessageBox.Show("The Date In Must Be > To Today Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut < dateIn)
                {
                    MessageBox.Show("The Date Out Must Be = or > To Date In", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (reservation.addReservation(roomNumber, clientID, dateIn, dateOut))
                    {
                        // set the room free column to No
                        // you can add a message if the room is editor
                        room.setRoomFreeToNo(roomNumber);
                        dataGridView1.DataSource = reservation.getAllReserv();
                        MessageBox.Show("New Reservation Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        buttonClear.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Reservation NOT Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void buttonEditReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int reservID = Convert.ToInt32(textBoxReservId.Text);
                int clientID = Convert.ToInt32(textBoxClientID.Text);
                int roomNumber = Convert.ToInt32(comboBoxRoomNumber.SelectedValue);
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                //date in must be = or > today date
                //date out must be = or > date in
                if (dateIn < DateTime.Now)
                {
                    MessageBox.Show("The Date In Must Be > To Today Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut < dateIn)
                {
                    MessageBox.Show("The Date Out Must Be = or > To Date In", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                     
                    if (reservation.editRes(reservID,roomNumber, clientID, dateIn, dateOut))
                    {
                        // set the room free column to No
                        // you can add a message if the room is edited
                        dataGridView1.DataSource = reservation.getAllReserv();
                        room.setRoomFreeToNo(roomNumber);
                        MessageBox.Show(" Reservation Data Updated", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        buttonClear.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Reservation Data Not Updated", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxReservId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            // we need to select the combo of room's type
            // we need firest to know the type of the room

            // get the room id 
            int roomId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());

            // select the room type from the combobox
            comboBoxRoomType.SelectedValue = room.getRoomType(roomId);

            // select the room number from the combobox
            // if you need to set a room to a reservation
            // you have to set the free column to 'Yes'
            comboBoxRoomNumber.SelectedValue = roomId;

            textBoxClientID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            
        }

        
    }
}


/*
 * 
 * -------------------------FUNCTION ADD BUG(RUN TIME)------------------------------------------
 * 1.The client 1000 and 9999 don't exist in the table client
 * we need to add foreign key to prevet the type of problem
 * 2.- After user add reservation, app can to update table rooms-> column free, and set it to NO
 *   - The room 123 is not avaible on 26/7/2020 so we need pervent that
 * 3 fix dates OUT>IN
 * DEBUG
 * 1. Add the foreign keys for the client & the room
 * ALTER TABLE rooms add CONSTRAINT fk_type_id FOREIGN KEY (type) REFERENCES rooms_category(category_id) on UPDATE CASCADE on DELETE CASCADE;
 * ALTER TABLE reservations add CONSTRAINT fk_room_number FOREIGN KEY (roomNumber) REFERENCES rooms(number) on UPDATE CASCADE on DELETE CASCADE;
 * ALTER TABLE reservations add CONSTRAINT fk_client_id FOREIGN KEY (clientid) REFERENCES client(id) on UPDATE CASCADE on DELETE CASCADE;
 * 2. MySqlCommand command = new MySqlCommand("SELECT * FROM `rooms` WHERE `type`=@typ and `free` ='Yes'", conn.getConnection()); ->room.roomByType
 * 3.
 * - date in must be = or > today date
 * - date out must be = or > date in
 */
