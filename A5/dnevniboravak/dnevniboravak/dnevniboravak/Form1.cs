using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Collections;
namespace dnevniboravak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection konekcija;
        SqlCommand komanda;
        DataTable dt;
        SqlDataAdapter da;
        void Konekcija()
        {
            konekcija = new SqlConnection();
             konekcija.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //  konekcija.ConnectionString = @"Data Source=DESKTOP-TORV0OV\SQLEXPRESS;Initial Catalog=dnevniboravakdece;Integrated Security=True;";
            //konekcija.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dnevniboravakdece;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            komanda = new SqlCommand();

            komanda.Connection = konekcija;

            dt = new DataTable();

            da = new SqlDataAdapter();
            konekcija.Open();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Morate uneti naziv");
            }
            else
            {
                Konekcija();
               // komanda.CommandText = "SET IDENTITY_INSERT [dbo].[Aktivnosti] ON";
                
                komanda.CommandText = "INSERT INTO Aktivnosti (AktivnostID,NazivAktivnosti,Pocetak,Zavrsetak,Dan)VALUES (@Aktivnost_id,@Naziv,@Poc,@Zavr,@d)";
                //18-06-12 10:34:09 PM // datum mora da ti bude u ovakvom formatu kad unosis
         
                komanda.Parameters.AddWithValue("@Aktivnost_id", Convert.ToInt32(textBox1.Text));
                komanda.Parameters.AddWithValue("@Naziv", textBox2.Text);
                komanda.Parameters.AddWithValue("@Poc", maskedTextBox1.Text);
                komanda.Parameters.AddWithValue("@Zavr",maskedTextBox2.Text);
                komanda.Parameters.AddWithValue("@d", textBox3.Text);
               
                try
                {
                    //konekcija.Open();
                   // MessageBox.Show(komanda.CommandText);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("USPESNO UNETO");

                }
                catch
                {
                    MessageBox.Show("GRESKA!");
                }
                finally
                {
                    konekcija.Close();
                }
                Form1_Load(sender, e);

            }
        }

            private void Form1_Load(object sender, EventArgs e)
            {
                Konekcija();
           
                listView1.Items.Clear();
                //dodaju se kolone sa njihovim imenima i širinom
                listView1.Columns.Add("AktivnostID", 100);
                listView1.Columns.Add("NazivAktivnosti", 100);
                listView1.Columns.Add("Pocetak", 100);
                listView1.Columns.Add("Zavrsetak", 100);
                listView1.Columns.Add("Dan", 100);
                listView1.View = View.Details;
                listView1.GridLines = true;//omogućava grid-mrežu
                listView1.FullRowSelect = true;
                komanda.CommandText = "SELECT AktivnostID,NazivAktivnosti,Pocetak,Zavrsetak,Dan FROM Aktivnosti ";
                da.SelectCommand = komanda;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    string[] podaci = {
                    row[0].ToString(),row[1].ToString() , row[2].ToString(),row[3].ToString(), row[4].ToString() };
                    ListViewItem stavka = new ListViewItem(podaci);
                    listView1.Items.Add(stavka);
                }
            }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
        Form2 form2;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.Show();
        }
        Form3 form3;
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            form3 = new Form3();
            form3.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            
                textBox1.Text = dt.Rows[listView1.Items.IndexOf(listView1.SelectedItems[0])][0].ToString();
                textBox2.Text = dt.Rows[listView1.Items.IndexOf(listView1.SelectedItems[0])][1].ToString();
                textBox3.Text = dt.Rows[listView1.Items.IndexOf(listView1.SelectedItems[0])][4].ToString();
                maskedTextBox1.Text = dt.Rows[listView1.Items.IndexOf(listView1.SelectedItems[0])][2].ToString();
                maskedTextBox2.Text = dt.Rows[listView1.Items.IndexOf(listView1.SelectedItems[0])][3].ToString();
               
            
        }
    }
    } 
