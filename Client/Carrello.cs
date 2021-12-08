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
    public partial class Carrello : Form
    {
        ServiceReference1.Service1Client client;
        Server.Utente utente;
        //Creazione tabella carello per finalizzare acqusito partite selezionate
        public Carrello(ServiceReference1.Service1Client Client, Server.Utente u)
        {
            client = Client;
            utente = u;
            InitializeComponent();
            dataGridView1.Columns.Add("Codice", "Codice");
            dataGridView1.Columns.Add("Incontro", "Incontro");
            dataGridView1.Columns.Add("DataPartita", "Data partita");
            dataGridView1.Columns.Add("OraInizioPartita", "Ora Inizio Partita");
            dataGridView1.Columns.Add("Luogo", "Luogo");
            dataGridView1.Columns.Add("Citta", "Citta");
            dataGridView1.Columns.Add("N° Biglietti", "N° Biglietti");
            dataGridView1.Columns.Add("N° Posti", "N° Posti");
            dataGridView1.Columns.Add("Prezzo Biglietto in €", "Prezzo Biglietto in €");
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
        //Aggiunge prenotazione al carrello
        public void AddPrenotazione(int CodicePartita, decimal q)
        {
            dataGridView1.Rows.Add(client.CarrelloTabelle(CodicePartita, q).Rows[0].ItemArray);
        }
        public void ModificaPrenotazione(int row, decimal q)
        {
            dataGridView1.Rows[row].Cells["N° Biglietti"].Value = Decimal.ToInt32(q);
            dataGridView1.Rows[row].Cells["N° Posti"].Value = client.CarrelloTabelle((int)dataGridView1.Rows[row].Cells["Codice"].Value, q).Rows[0]["N° Posti"].ToString();
            dataGridView1.Rows[row].Cells["Prezzo Biglietto in €"].Value = client.CarrelloTabelle((int)dataGridView1.Rows[row].Cells["Codice"].Value, q).Rows[0]["Prezzo Biglietto in €"].ToString();

        }
        public void EliminaPrenotazione(int row)
        {
            dataGridView1.Rows.RemoveAt(row);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.RowIndex != senderGrid.Rows.Count - 1)
            {
                if (senderGrid.Columns[e.ColumnIndex].Index == 9)
                {
                    this.Hide();
                    var quantita = new Quantita(client, utente, this, Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["Codice"].Value), senderGrid.Rows[e.RowIndex].Index);
                    quantita.button2.Visible = true;
                    quantita.button1.Visible = false;
                    quantita.Show();
                }
                else
                    EliminaPrenotazione(senderGrid.Rows[e.RowIndex].Index);
            }
        }
        private void Carrello_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && dataGridView1.Rows.Count > 1)
            {
                DialogResult dialogResult = MessageBox.Show("Chiusura della finestra comporta in perdita degli articoli presenti nel Carrello", "Attenzione!!", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK) return;
                else if (dialogResult == DialogResult.Cancel) e.Cancel = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            int totale = 0;
            foreach ( DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index < dataGridView1.Rows.Count - 1)
                    totale += Convert.ToInt32(row.Cells[8].Value.ToString());

            }
            textBox1.Text = totale.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 1)
            {
                DataTable dt = new DataTable();
                //Adding the Columns.
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    dt.Columns.Add(column.HeaderText, typeof(string));
                }

                //Adding the Rows.
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        else
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = string.Empty;
                    }
                }
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
                dt.TableName = "Prenotazioni";
                if (client.RegistraPrenotazione(utente, dt))
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Acquisto andato a buon fine", "Success", MessageBoxButtons.OK);
                    this.Close();
                }
            }
            else
                MessageBox.Show("CARRELLO VUOTO!! PRIMA AGGIUNGI QUALCOSA", "Error", MessageBoxButtons.OK);

        }
    }
}
