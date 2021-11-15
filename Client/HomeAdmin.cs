using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class HomeAdmin : Form
    {
        public ServiceReference1.Service1Client client;
        Server.Admin admin;
        public HomeAdmin(ServiceReference1.Service1Client Client, Server.Admin a)
        {
            client = Client;
            admin = a;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ClientiCensiti_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.ListaUtenti();
            dataGridView1.Visible = true;
        }
    }
}
