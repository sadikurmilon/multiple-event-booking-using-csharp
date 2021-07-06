using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Final_Project
{
   
    public partial class Form1 : Form
    {
       
        

        public Form1()
        {
            InitializeComponent();
            data_base.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ryan\Documents\ticket_reserve.accdb;Persist Security Info=False;";
        }
         OleDbConnection data_base = new OleDbConnection();
            
        private void Form1_Load(object sender, EventArgs e)
        {
           
           
        }


        private void src_bttn_Click(object sender, EventArgs e)
        {
            if (src_txt_box.Text == "1" || src_txt_box.Text == "3" || src_txt_box.Text == "2")
            {
                data_base.Open();
                OleDbCommand cmd = data_base.CreateCommand();

                cmd.CommandText = "SELECT * from Event_info_tbl where Event_ID_Number=" + src_txt_box.Text + "";
                cmd.Connection = data_base;                
                OleDbDataReader reader = cmd.ExecuteReader();
                reader.Read();
                evnt_nme_lbl.Text = (reader["Event_Description"]).ToString();
                lction_lbl.Text = (reader["Location"]).ToString();
                dt_tm_lbl.Text = (reader["date_and_time"]).ToString();
                prce_lbl.Text = (reader["Price"]).ToString();
                st_avlbl_lbl.Text = (reader["number_of_seats_available"]).ToString();
                st_sld_lbl.Text = (reader["number_of_seats_sold"]).ToString();
               
                if (evnt_nme_lbl.Text == "JUSTIN BEIBER CONCERT")
                {
                    jb_imagebox.Visible = true;
                }
                else
                {
                    jb_imagebox.Visible = false;
                }
                if (evnt_nme_lbl.Text == "RAPTORS VS GOLDEN STATE")
                {
                    RVSGIMAGEBOX.Visible = true;
                }
                else
                {
                    RVSGIMAGEBOX.Visible = false;
                }
                if (evnt_nme_lbl.Text == "BARCELONA VS REAL MADRID")
                {
                    BRVSRMIMAGEBOX.Visible = true;
                }
                else
                {
                    BRVSRMIMAGEBOX.Visible = false;
                }
               
                data_base.Close();
            }
            else
            {
                MessageBox.Show("please choose the right event id");
            }

        }
        Customer_info_frm scnd_frm = new Customer_info_frm();
        public static string event_name1;
        public static string store;
        public static string price;
        public static string location;
        public static string date_time;
        public static string available_seat;
        public static string sold_seat;
        private void prcsd_bttn_Click(object sender, EventArgs e)
        {
            data_base.Open();
            OleDbCommand cmde = data_base.CreateCommand();
            cmde.CommandText = "SELECT * from Event_info_tbl where Event_ID_Number=" + inpt_choice_txtbox.Text + "";
            cmde.Connection = data_base;
            OleDbDataReader reader = cmde.ExecuteReader();
            reader.Read();
            event_name1 = (reader["Event_Description"]).ToString();
            available_seat = (reader["number_of_seats_available"]).ToString();
            sold_seat = (reader["number_of_seats_sold"]).ToString();
            location = (reader["Location"]).ToString();
            price = (reader["Price"]).ToString();
            date_time = (reader["date_and_time"]).ToString();

            data_base.Close();
           
            store = inpt_choice_txtbox.Text;
            if (inpt_choice_txtbox.Text == "1" || inpt_choice_txtbox.Text == "3" || inpt_choice_txtbox.Text == "2")
            {
               
                scnd_frm.Show();
                this.Hide();
               
            }
            else
            {
                MessageBox.Show("please choose the right event id");

            }
        }
    }
}
