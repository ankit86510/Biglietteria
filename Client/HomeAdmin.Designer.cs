
namespace Client
{
    partial class HomeAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeAdmin));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ClientiCensiti = new System.Windows.Forms.Button();
            this.Storico = new System.Windows.Forms.Button();
            this.InsNuovoEvento = new System.Windows.Forms.Button();
            this.VissModElimPartita = new System.Windows.Forms.Button();
            this.Logout = new System.Windows.Forms.Button();
            this.customInstaller1 = new MySql.Data.MySqlClient.CustomInstaller();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1133, 371);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ClientiCensiti
            // 
            this.ClientiCensiti.Location = new System.Drawing.Point(67, 33);
            this.ClientiCensiti.Name = "ClientiCensiti";
            this.ClientiCensiti.Size = new System.Drawing.Size(89, 23);
            this.ClientiCensiti.TabIndex = 1;
            this.ClientiCensiti.Text = "Lista Clienti";
            this.ClientiCensiti.UseVisualStyleBackColor = true;
            this.ClientiCensiti.Click += new System.EventHandler(this.ClientiCensiti_Click);
            // 
            // Storico
            // 
            this.Storico.Location = new System.Drawing.Point(235, 33);
            this.Storico.Name = "Storico";
            this.Storico.Size = new System.Drawing.Size(109, 23);
            this.Storico.TabIndex = 2;
            this.Storico.Text = "Storico biglietti";
            this.Storico.UseVisualStyleBackColor = true;
            this.Storico.Click += new System.EventHandler(this.Storico_Click);
            // 
            // InsNuovoEvento
            // 
            this.InsNuovoEvento.Location = new System.Drawing.Point(436, 33);
            this.InsNuovoEvento.Name = "InsNuovoEvento";
            this.InsNuovoEvento.Size = new System.Drawing.Size(141, 23);
            this.InsNuovoEvento.TabIndex = 3;
            this.InsNuovoEvento.Text = "Inserisci nuovi eventi";
            this.InsNuovoEvento.UseVisualStyleBackColor = true;
            this.InsNuovoEvento.Click += new System.EventHandler(this.button3_Click);
            // 
            // VissModElimPartita
            // 
            this.VissModElimPartita.Location = new System.Drawing.Point(666, 33);
            this.VissModElimPartita.Name = "VissModElimPartita";
            this.VissModElimPartita.Size = new System.Drawing.Size(179, 23);
            this.VissModElimPartita.TabIndex = 4;
            this.VissModElimPartita.Text = "Visualizza/Modifica/Elimina Partita";
            this.VissModElimPartita.UseVisualStyleBackColor = true;
            this.VissModElimPartita.Click += new System.EventHandler(this.button4_Click);
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(926, 58);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(156, 23);
            this.Logout.TabIndex = 5;
            this.Logout.Text = "LOGOUT";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1018, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Admin Account";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Client.Properties.Resources.admin;
            this.pictureBox1.Location = new System.Drawing.Point(944, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // HomeAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 481);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.VissModElimPartita);
            this.Controls.Add(this.InsNuovoEvento);
            this.Controls.Add(this.Storico);
            this.Controls.Add(this.ClientiCensiti);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "HomeAdmin";
            this.Text = "HomeAdmin";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ClientiCensiti;
        private System.Windows.Forms.Button Storico;
        private System.Windows.Forms.Button InsNuovoEvento;
        public System.Windows.Forms.Button VissModElimPartita;
        public System.Windows.Forms.Button Logout;
        private MySql.Data.MySqlClient.CustomInstaller customInstaller1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}