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
    public partial class Quantita : Form
    {
        ServiceReference1.Service1Client client;
        Server.Utente utente;
        Carrello cart;
        int value;
        public Quantita(ServiceReference1.Service1Client Client, Server.Utente u, Carrello c,int s)
        {
            client = Client;
            utente = u;
            cart = c;
            value = s;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                if (cart.dataGridView1.Rows.Count > 1)
                {
                    foreach (DataGridViewRow row in cart.dataGridView1.Rows)
                    {
                        if (row.Index < cart.dataGridView1.Rows.Count - 1)
                            if (Convert.ToInt32(row.Cells[0].Value.ToString()) == value)
                            {
                                MessageBox.Show("E' già presente una prenotazione di questa partita nel carrello. Se vuoi aggiungere altri biglieti per questa partita, modifica quella già presente nel carrello! ", "Attenzione", MessageBoxButtons.OK);
                                this.Close();
                                return;
                            }
                    }
                }

                cart.AddPrenotazione(value, numericUpDown1.Value);
                this.Close();
                cart.WindowState = FormWindowState.Minimized;
                cart.Show();
                cart.WindowState = FormWindowState.Normal;
            }
            else
                MessageBox.Show("Il valore inserito deve essere maggiore ad 0", "Error", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                cart.ModificaPrenotazione(value, numericUpDown1.Value);
                this.Close();
                cart.WindowState = FormWindowState.Minimized;
                cart.Show();
                cart.WindowState = FormWindowState.Normal;
            }
            else
                MessageBox.Show("Il valore inserito deve essere maggiore ad 0", "Error", MessageBoxButtons.OK);
        }
    }
}
