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
    public partial class NuovaPartita : Form
    {
        public ServiceReference1.Service1Client client;

        public NuovaPartita(ServiceReference1.Service1Client Client)
        {
            client = Client;
            InitializeComponent();
            stadioPicker.DataSource = client.RicercaStadio();
            stadioPicker.DisplayMember = "Nome";
            stadioPicker.ValueMember = "Nome";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Salva_Click(object sender, EventArgs e)
        {
            Console.WriteLine(dateTimePicker1.Text);
            if (dateTimePicker1.Text != string.Empty && dateTimePicker2.Text != string.Empty && incontro.Text != string.Empty && stadioPicker.Text != string.Empty)
            {
                try
                {
                    dateTimePicker1.CustomFormat = "yyyy-MM-dd";
                    if (client.InsNuovaPartita(dateTimePicker1.Value, dateTimePicker2.Value, incontro.Text, stadioPicker.Text))
                    {
                        MessageBox.Show("Nuovo evento inserito correttamente", "Success", MessageBoxButtons.OK);
                    }
                    else
                        MessageBox.Show("Utente con la Email già presente nel database! Inserisci una email diversa ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception)
                {
                    MessageBox.Show("Qualcosa è andato storto!!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Per favore riempi tutti i campi richiesti", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stadioPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
