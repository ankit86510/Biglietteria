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
    public partial class HomeUtente : Form
    {
        public ServiceReference1.Service1Client client;
        Server.Utente utente;
        Carrello cart;
        
        //Costruttore
        //Istanzia oggetto Home, oggetto carello e prepara la DataGridView per ospitare la lista partite prenotabili
        public HomeUtente(ServiceReference1.Service1Client Client, Server.Utente u)
        {
            client = Client;
            utente = u;
            cart = new Carrello(client, utente);
            InitializeComponent();
            dataGridView1.DataSource = client.ListaPartite();
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Carrello";
                buttons.Text = "Aggiungi nel Carrello";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;
            }
            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            {
                button2.HeaderText = "Cancella Prenotazione";
                button2.Text = "Cancella Prenotazione";
                button2.UseColumnTextForButtonValue = true;
                button2.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                button2.FlatStyle = FlatStyle.Standard;
                button2.CellTemplate.Style.BackColor = Color.IndianRed;

            }
            dataGridView1.Columns.Add(buttons);
            dataGridView1.Columns.Add(button2);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        //Gestione evento on-click per il pulasnte Partite
        //Mostra lista partite in programma chiamando funzione servizio ListaPartite
        private void Partite_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.ListaPartite();
            dataGridView1.Columns["Codice"].DisplayIndex = 0;
            dataGridView1.Columns[0].DisplayIndex = 7;
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Visible = true;
        }

        //Gestione evento cambio indice per il ComboBox1
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = comboBox1.Text;
            label2.Visible = true;
            comboBox2.DataSource = client.RicercaPer(comboBox1.Text, string.Empty)[0];
            comboBox2.DisplayMember = client.RicercaPer(comboBox1.Text, string.Empty)[0].Columns[0].ColumnName.ToString();
            comboBox2.Visible = true;

        }

        //Gestione evento cambio indice per il ComboBox2
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Width = comboBox2.Items[comboBox1.SelectedIndex].ToString().Length * 7;
            dataGridView1.DataSource = client.RicercaPer(comboBox1.Text, comboBox2.Text)[1];
            dataGridView1.Columns["Codice"].DisplayIndex = 0;
            dataGridView1.Columns[0].DisplayIndex = 7;
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Visible = true;

        }

        //Gestione evento on-click per i pulasnti Aggiungi al carrello/Elimina
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.RowIndex != senderGrid.Rows.Count - 1)
            {
                if (senderGrid.Columns[e.ColumnIndex].Index == 0)
                {
                    if (cart.IsDisposed)
                        cart = new Carrello(client, utente);
                    var quantita = new Quantita(client, utente, cart, Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[senderGrid.Columns["Codice"].Name].Value.ToString()), 0);
                    quantita.Show();
                }
                else
                {
                    if (client.EliminaPrenotazione((int)senderGrid.Rows[e.RowIndex].Cells["ID_Prenotazione"].Value))
                    {
                        MessageBox.Show("Prenotazione rimossa con successo", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.PerformClick();
                    }
                    else
                        MessageBox.Show("Qualcosa è andato storto!!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        //Gestione evento on-click per l'immagine del Carrello
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (cart.IsDisposed)
                cart = new Carrello(client, utente);
            cart.WindowState = FormWindowState.Minimized;
            cart.Show();
            cart.WindowState = FormWindowState.Normal;
        }

        //Gestione evento on-click per il pulsante Mie Prenotazioni
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.ListaPrenotazioni(utente.getEmail());
            dataGridView1.Columns["ID_Prenotazione"].DisplayIndex = 0;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].DisplayIndex = 11;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Visible = true;
            int dgv_width = dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            this.Width = dgv_width + 140;

        }

        //Gestione evento on-click per il pulsante Logout
        private void LOGOUT_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(OpenLoginForm)); //you create a new thread
            MessageBox.Show("Utente è stato disconnesso con successo", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); //you close your current form (for example a menu)
            t.Start();  //you start the thread
        }

        //Funzione di appoggio per il pulsante Logout
        public static void OpenLoginForm()
        {
            var client = new ServiceReference1.Service1Client();
            Application.Run(new LogIn(client)); //run your new form
        }
    }
}
