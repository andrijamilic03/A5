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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection konekcija;
        SqlCommand komanda;
        DataTable dt;
     //   DataTable dt1;
        SqlDataAdapter da;
        void Konekcija()
        {
            konekcija = new SqlConnection();
            konekcija.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db;Integrated Security=True;";
            // konekcija.ConnectionString = @"Data Source=DESKTOP-TORV0OV\SQLEXPRESS;Initial Catalog=dnevniboravakdece;Integrated Security=True;";
            // konekcija.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dnevniboravakdece;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            komanda = new SqlCommand();

            komanda.Connection = konekcija;

            dt = new DataTable();
           // dt1 = new DataTable();
            da = new SqlDataAdapter();
        }
        string[] x = { "ponedeljak", "utorak", "sreda", "četvrtak", "petak", "subota", "nedelja" };
        private void button1_Click(object sender, EventArgs e)
        {
            Konekcija();
            string upit = @"select a.Dan, count(d.DeteId) as [Broj dece], 
                            case 
	                            when a.Dan = 'ponedeljak' Then 1
	                            when a.Dan = 'utorak' Then 2
	                            when a.Dan = 'sreda' Then 3
	                            when a.Dan = N'cetvrtak' Then 4
	                            when a.Dan = 'petak' Then 5
	                            when a.Dan = 'subota' Then 6
	                            when a.Dan = 'nedelja' Then 7
                            end as DanBroj
                            from Aktivnosti as a 
                               inner join Registar_Aktivnosti as ra on a.AktivnostID = ra.AktivnostID
                               inner join Dete as d on d.DeteID=ra.DeteID 
                               group by a.Dan
                               union
                            select  'subota', 0 , 6 
                               Union
                            select  'nedelja', 0 , 7    
                               order by [DanBroj] asc
                            ";
            komanda.CommandText = upit;
            
           
            
            da.SelectCommand = komanda;
            da.Fill(dt);


            
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["DanBroj"].Visible = false;
            chart1.Legends[0].Enabled = false;
            chart1.Series[0].IsValueShownAsLabel = true;

            foreach (DataRow row in dt.Rows)
            {
                string dan = row[0].ToString();
                chart1.Series[0].Points.AddXY(dan, row[1]);
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
