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
    public partial class Receipt_frm : Form
    {
        public Receipt_frm()
        {
            InitializeComponent();
            data_base.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ryan\Documents\ticket_reserve.accdb;Persist Security Info=False;";
        }
        OleDbConnection data_base = new OleDbConnection();
        private void Receipt_frm_Load(object sender, EventArgs e)
        {
            data_base.Open();
            OleDbCommand cmd = data_base.CreateCommand();

            cmd.CommandText = "SELECT * from receipt_table where Customer_ID=" + Customer_info_frm.store1 + "";
            cmd.Connection = data_base;
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();
            event_nme_lbl.Text = (reader["Event_nme"]).ToString();
            
            lcation_lbl.Text = (reader["location"]).ToString();
            date_tme_lbl.Text = (reader["Date_and_time"]).ToString();
            tckt_prce_lbl.Text = (reader["price_without_tax"]).ToString();
            seat_lbl.Text = (reader["Seat_nmbr"]).ToString();            
            payer_lbl.Text = (reader["Firstname"]) +" "+ (reader["Lastname"]).ToString();
            phn_lbl.Text = (reader["telephone_nmbr"]).ToString();
            address_lbl.Text = (reader["Address"]).ToString();
            tax_lbl.Text = Customer_info_frm.tax;
            prce_tax_lbl.Text = (reader["price_with_tax"]).ToString();
            card_total_lbl.Text = (reader["price_with_tax"]).ToString();
            data_base.Close();
        }
        
        private void clse_bttn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
