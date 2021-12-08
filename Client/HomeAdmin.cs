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
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            {
                button1.HeaderText = "Modifica";
                button1.Text = "Modifica";
                button1.UseColumnTextForButtonValue = true;
                button1.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                button1.FlatStyle = FlatStyle.Standard;
                button1.CellTemplate.Style.BackColor = Color.RoyalBlue;
            }
            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            {
                button2.HeaderText = "Elimina";
                button2.Text = "Elimina";
                button2.UseColumnTextForButtonValue = true;
                button2.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                button2.FlatStyle = FlatStyle.Standard;
                button2.CellTemplate.Style.BackColor = Color.IndianRed;

            }

            dataGridView1.Columns.Add(button1);
            dataGridView1.Columns.Add(button2);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        public void EliminaPrenotazione(int row)
        {
            dataGridView1.Rows.RemoveAt(row);
        }

        //Bottone per ottenere lista clienti
        private void ClientiCensiti_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.DataSource = client.ListaUtenti();
            dataGridView1.Visible = true;
        }

        private void Storico_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.DataSource = client.StoricoBiglietti();
            dataGridView1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var np = new NuovaPartita(this, client);
            np.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.ListaPartite();
            dataGridView1.Columns[0].DisplayIndex = 7;
            dataGridView1.Columns[1].DisplayIndex = 7;
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Visible = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.RowIndex != senderGrid.Rows.Count - 1)
            {
                if (senderGrid.Columns[e.ColumnIndex].Index == 0)
                {
                    var ModificaPartita = new NuovaPartita(this, client, senderGrid.Rows[e.RowIndex]);
                    ModificaPartita.Show();
                }
                else
                {
                    if (client.EliminaPartita((int)senderGrid.Rows[e.RowIndex].Cells["Codice"].Value))
                    {
                        MessageBox.Show("Partita rimossa con successo", "Success", MessageBoxButtons.OK);
                        button4.PerformClick();
                    }
                    else
                        MessageBox.Show("Qualcosa è andato storto!!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(OpenLoginForm)); //you create a new thread
            MessageBox.Show("Admin è stato disconnesso con successo", "Success", MessageBoxButtons.OK);
            this.Close(); //you close your current form (for example a menu)
            t.Start();  //you start the thread
        }

        public static void OpenLoginForm()
        {
            var client = new ServiceReference1.Service1Client();
            Application.Run(new LogIn(client)); //run your new form
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
