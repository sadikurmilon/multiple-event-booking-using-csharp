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
    public partial class Customer_info_frm : Form
    {
        Receipt_frm thrd_frm = new Receipt_frm();
        public static string store1;
        public Customer_info_frm()
        {
            InitializeComponent();
            data_base.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ryan\Documents\ticket_reserve.accdb;Persist Security Info=False;";
           
        }
        OleDbConnection data_base = new OleDbConnection();

        private void prcsd_bttn_Click(object sender, EventArgs e)
        {

            
            data_base.Open();
           
            double tax1 = Convert.ToDouble(Form1.price);
           
            tax1 = (tax1 * 0.13);
            tax = Convert.ToString(tax1);
            
            double price = Convert.ToDouble(Form1.price);
            price = tax1 + price;
            string price_tax = price.ToString();
            double seat_number;
            string seat = "";
            double st_ava = Convert.ToDouble(Form1.available_seat);
            if (Form1.sold_seat != "30")
            {
                seat_number = Double.Parse(Form1.sold_seat);
                seat_number = seat_number + 1;
                seat = seat_number.ToString();
                st_ava = st_ava - 1;
                
                OleDbCommand cmd2 = data_base.CreateCommand();
                cmd2.CommandText = "Update Event_info_tbl set number_of_seats_available=" + st_ava + ",number_of_seats_sold='" + seat + "'where Event_ID_Number=" + Form1.store + "";
                cmd2.Connection = data_base;
                cmd2.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("Sorry No more seat is available");
            }
           
            store1 = id_txtbox.Text;
            bool cnt = true;
           
            OleDbCommand cmd = data_base.CreateCommand();
            cmd.CommandText = "SELECT * from Customer_info";
            cmd.Connection = data_base;
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();
            while (reader.Read())
            {
               if (id_txtbox.Text == (reader["Customer_ID"]).ToString())
               {                
                   cnt = false;
               }
            }

            data_base.Close();
          
           
           if(cnt == true){
               if (!String.IsNullOrEmpty(id_txtbox.Text) && !String.IsNullOrEmpty(frstnme_txtbox.Text) && !String.IsNullOrEmpty(lstnme_txtbox.Text) && !String.IsNullOrEmpty(addrss_txtbox.Text) && !String.IsNullOrEmpty(crd_card_txtbox.Text) && !String.IsNullOrEmpty(crd_cvv_txtbox.Text))
               {
                   data_base.Open();
                   OleDbCommand cmde = data_base.CreateCommand();
                   cmde.Connection = data_base;
                   string query = "insert into Customer_info ( Customer_ID,firstname,lastname,address,telephone_number,credit_card_number,creditcard_cvv ) values('" + id_txtbox.Text + "','" + frstnme_txtbox.Text + "','" + lstnme_txtbox.Text + "','" + addrss_txtbox.Text + "','" + tlphn_txtbox.Text + "','" + crd_card_txtbox.Text + "','" + crd_cvv_txtbox.Text + "')";
                   cmde.CommandText = query;
                   cmde.ExecuteNonQuery();
                   OleDbCommand cmder = data_base.CreateCommand();
                   cmder.Connection = data_base;
                   string query1 = "insert into receipt_table (Customer_ID,Event_nme,Firstname,Lastname,location,telephone_nmbr,Seat_nmbr,price_with_tax,price_without_tax,Date_and_time,Address ) values('" + id_txtbox.Text + "','" + Form1.event_name1 + "','" + frstnme_txtbox.Text + "','" + lstnme_txtbox.Text + "','" + Form1.location + "','" + tlphn_txtbox.Text + "','" + seat + "' ,'" + price_tax + "','" + Form1.price + "','" + Form1.date_time + "','" + addrss_txtbox.Text + "')";
                   cmder.CommandText = query1;
                   cmder.ExecuteNonQuery();
                   data_base.Close();
                   thrd_frm.Show();
                   this.Hide();
           
               }
               else
               {
                   MessageBox.Show("Please fill all the boxes.It's required to purchase ticket");
               }
           }
           
           else if (cnt == false)
           {
               if (!String.IsNullOrEmpty(id_txtbox.Text))
               {
                   data_base.Open();
                   OleDbCommand cmd2 = data_base.CreateCommand();
                   cmd2.CommandText = "Update receipt_table set Event_nme='" + Form1.event_name1 + "',location='" + Form1.location + "',Seat_nmbr='" + seat + "',price_with_tax='" + price_tax + "',price_without_tax='" + Form1.price + "',Date_and_time='" + Form1.date_time + "'where Customer_ID=" + store1 + "";
                   cmd2.Connection = data_base;
                   cmd2.ExecuteNonQuery();
                   data_base.Close();         
                  MessageBox.Show("Your Customer ID shows that you are an old Customer.Thank you for purchasing ticket again.");
                  thrd_frm.Show();
                  this.Hide();
               }
               else
               {
                   MessageBox.Show("Please fill out the id box");
               }
           }    
        }
        public static string tax;
        
        private void back_bttn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 vf = new Form1();
            vf.Show();
            
        }
     
        private void Form2_Load(object sender, EventArgs e)
        {

            evnt_name_lbl.Text = Form1.event_name1;
           
           
        }
    }
}
