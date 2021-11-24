
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ClientiCensiti = new System.Windows.Forms.Button();
            this.Storico = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 121);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1511, 457);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ClientiCensiti
            // 
            this.ClientiCensiti.Location = new System.Drawing.Point(64, 41);
            this.ClientiCensiti.Margin = new System.Windows.Forms.Padding(4);
            this.ClientiCensiti.Name = "ClientiCensiti";
            this.ClientiCensiti.Size = new System.Drawing.Size(100, 28);
            this.ClientiCensiti.TabIndex = 1;
            this.ClientiCensiti.Text = "Lista Clienti";
            this.ClientiCensiti.UseVisualStyleBackColor = true;
            this.ClientiCensiti.Click += new System.EventHandler(this.ClientiCensiti_Click);
            // 
            // Storico
            // 
            this.Storico.Location = new System.Drawing.Point(240, 41);
            this.Storico.Margin = new System.Windows.Forms.Padding(4);
            this.Storico.Name = "Storico";
            this.Storico.Size = new System.Drawing.Size(145, 28);
            this.Storico.TabIndex = 2;
            this.Storico.Text = "Storico biglietti";
            this.Storico.UseVisualStyleBackColor = true;
            this.Storico.Click += new System.EventHandler(this.Storico_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(461, 41);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(188, 28);
            this.button3.TabIndex = 3;
            this.button3.Text = "Inserisci nuovi eventi";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(747, 41);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(208, 28);
            this.button4.TabIndex = 4;
            this.button4.Text = "Elimina/Modifica Partita";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // HomeAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 592);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Storico);
            this.Controls.Add(this.ClientiCensiti);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "HomeAdmin";
            this.Text = "HomeAdmin";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ClientiCensiti;
        private System.Windows.Forms.Button Storico;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}