using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class NuovaPartita : Form
    {
        public ServiceReference1.Service1Client client;
        DataGridViewRow row;
        HomeAdmin ha;

        //Costruttori
        //Istanzia oggetto nuova partita caricando lista degli stadi, delle squadre, time e date picker
        public NuovaPartita(HomeAdmin a, ServiceReference1.Service1Client Client)
        {
            client = Client;
            ha = a;
            InitializeComponent();
            stadioPicker.DataSource = client.RicercaStadio();
            stadioPicker.DisplayMember = "Nome";
            stadioPicker.ValueMember = "Nome";

        }
        public NuovaPartita(HomeAdmin a, ServiceReference1.Service1Client Client, DataGridViewRow r)
        {
            ha = a;
            client = Client;
            row = r;
            InitializeComponent();
            this.Text = "Modifica Partita";
            label1.Text = "Modifica partita esistente: ";
            stadioPicker.DataSource = client.RicercaStadio();
            stadioPicker.DisplayMember = "Nome";
            stadioPicker.ValueMember = "Nome";
            dateTimePicker1.Value = (DateTime)r.Cells["DataPartita"].Value;
            dateTimePicker2.Value = DateTime.ParseExact(Convert.ToString(r.Cells["OraInizioPartita"].Value), "HH:mm:ss", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            string incontro = (string)r.Cells["Incontro"].Value;
            string[] squadre = incontro.Split('-');
            listBox1.SelectedItem  = squadre[0];
            listBox2.SelectedItem = squadre[1];
            stadioPicker.SelectedIndex = stadioPicker.FindString((string)r.Cells["Luogo"].Value);

        }

        //Gestione evento on-click per il pulsante Salva
        // Salva l'incontro inserito se non ci sono errori di concomitanza, se l'evento è già presente, se lo stadio è già occupato in quella data
        private void Salva_Click(object sender, EventArgs e)
        {
            if (listBox1.Text == listBox2.Text)
            {
                MessageBox.Show("L'incontro non può essere tra due squadre uguali", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var incontro = listBox1.Text + '-' + listBox2.Text;
            if (dateTimePicker1.Text != string.Empty && dateTimePicker2.Text != string.Empty && stadioPicker.Text != string.Empty)
            {
                try
                {
                    dateTimePicker1.CustomFormat = "yyyy-MM-dd";
                    if (row == null)
                    {
                        if (client.InsNuovaPartita(dateTimePicker1.Value, dateTimePicker2.Value, incontro, stadioPicker.Text))
                        {
                            MessageBox.Show("Nuovo evento inserito correttamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ha.VissModElimPartita.PerformClick();
                            this.Close();
                            return;
                        }
                    }
                    else
                    {
                        if (client.ModificaPartita((int)row.Cells["Codice"].Value, dateTimePicker1.Value, dateTimePicker2.Value, incontro, stadioPicker.Text))
                        {
                            MessageBox.Show("Modifica avvenuta correttamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ha.VissModElimPartita.PerformClick();
                            this.Close();
                            return;
                        }
                    }
                    MessageBox.Show("Problemi di concomitanza nell'inserimento della partita" + "\r\n" +
                    "Possibili errori potrebbero essere i seguenti:" + "\r\n" +
                    "1)Inserimento di una partita già esistente" + "\r\n" +
                    "2)Le squadre selezionate hanno già in programma una partita nella data selezionata" + "\r\n" +
                    "3)La data selezionata ha già in programma una partita in quello stadio" + "\r\n" +
                    "(uno stadio può avere in programma solo una partita nell'arco della giornata)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Qualcosa è andato storto!!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Per favore riempi tutti i campi richiesti", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
