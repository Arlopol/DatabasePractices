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

namespace DatabasePractices
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0; Data Source=Library.mdb");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand combo_Command = new OleDbCommand("select bookID from Book", connection);
            OleDbDataReader combo_Reader = combo_Command.ExecuteReader();

            while (combo_Reader.Read())
            {
                comboBox1.Items.Add(combo_Reader[0]);
            }

            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand book_Command = new OleDbCommand("select * from Book where bookID =" + comboBox1.SelectedItem.ToString(), connection);
            OleDbDataReader book_Reader = book_Command.ExecuteReader();

            book_Reader.Read();
            textBox1.Text = book_Reader.GetString(1);
            textBox2.Text = book_Reader[2].ToString();
            textBox3.Text = book_Reader["bookPage"].ToString();
            textBox4.Text = book_Reader.GetInt32(4).ToString();
            
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand add_Command = new OleDbCommand("insert into book(bookName, bookWriter, bookPage, bookPublishDate) values "+"('"+textBox1.Text+"','"+textBox2.Text+"', "+textBox3.Text+", "+textBox4.Text+") ", connection);
            MessageBox.Show(add_Command.ExecuteNonQuery() + " book added");
            connection.Close();
        }
    }
}
