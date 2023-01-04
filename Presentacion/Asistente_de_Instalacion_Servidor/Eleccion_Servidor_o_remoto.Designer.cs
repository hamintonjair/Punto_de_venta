
namespace Punto_de_venta.Presentacion.Asistente_de_Instalacion_Servidor
{
    partial class Eleccion_Servidor_o_remoto
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Eleccion_Servidor_o_remoto));
            this.Panel8 = new System.Windows.Forms.Panel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.lblEstado_de_conexion = new System.Windows.Forms.Label();
            this.datalistado = new System.Windows.Forms.DataGridView();
            this.Eliminar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PictureBox7 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button2 = new System.Windows.Forms.Button();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Panel8.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datalistado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).BeginInit();
            this.Panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(106)))), ((int)(((byte)(93)))));
            this.Panel8.Controls.Add(this.Panel3);
            this.Panel8.Controls.Add(this.PictureBox7);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Panel8.Location = new System.Drawing.Point(0, 0);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(1211, 86);
            this.Panel8.TabIndex = 590;
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.lblEstado_de_conexion);
            this.Panel3.Controls.Add(this.datalistado);
            this.Panel3.Location = new System.Drawing.Point(347, 7);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(30, 12);
            this.Panel3.TabIndex = 606;
            // 
            // lblEstado_de_conexion
            // 
            this.lblEstado_de_conexion.AutoSize = true;
            this.lblEstado_de_conexion.BackColor = System.Drawing.Color.White;
            this.lblEstado_de_conexion.Location = new System.Drawing.Point(69, 34);
            this.lblEstado_de_conexion.Name = "lblEstado_de_conexion";
            this.lblEstado_de_conexion.Size = new System.Drawing.Size(83, 13);
            this.lblEstado_de_conexion.TabIndex = 605;
            this.lblEstado_de_conexion.Text = "lblRutaInstancia";
            // 
            // datalistado
            // 
            this.datalistado.AllowUserToAddRows = false;
            this.datalistado.AllowUserToDeleteRows = false;
            this.datalistado.AllowUserToOrderColumns = true;
            this.datalistado.BackgroundColor = System.Drawing.Color.White;
            this.datalistado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.datalistado.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.datalistado.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.datalistado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datalistado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Eliminar});
            this.datalistado.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datalistado.DefaultCellStyle = dataGridViewCellStyle2;
            this.datalistado.EnableHeadersVisualStyles = false;
            this.datalistado.GridColor = System.Drawing.Color.LightGray;
            this.datalistado.Location = new System.Drawing.Point(13, 13);
            this.datalistado.Name = "datalistado";
            this.datalistado.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datalistado.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.datalistado.RowHeadersVisible = false;
            this.datalistado.RowHeadersWidth = 5;
            this.datalistado.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.datalistado.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightGray;
            this.datalistado.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.datalistado.RowTemplate.Height = 60;
            this.datalistado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datalistado.Size = new System.Drawing.Size(50, 21);
            this.datalistado.TabIndex = 606;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Visible = false;
            // 
            // PictureBox7
            // 
            this.PictureBox7.Image = global::Punto_de_venta.Properties.Resources.logoJOJAMA1;
            this.PictureBox7.Location = new System.Drawing.Point(0, 0);
            this.PictureBox7.Name = "PictureBox7";
            this.PictureBox7.Size = new System.Drawing.Size(96, 86);
            this.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox7.TabIndex = 604;
            this.PictureBox7.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(1027, 242);
            this.Label1.TabIndex = 605;
            this.Label1.Text = "Instalar Sisconta";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button2
            // 
            this.Button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button2.BackColor = System.Drawing.Color.Transparent;
            this.Button2.BackgroundImage = global::Punto_de_venta.Properties.Resources.verde;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button2.FlatAppearance.BorderSize = 0;
            this.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.Button2.ForeColor = System.Drawing.Color.Black;
            this.Button2.Location = new System.Drawing.Point(380, 253);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(247, 84);
            this.Button2.TabIndex = 608;
            this.Button2.Text = "Iniciar";
            this.Button2.UseVisualStyleBackColor = false;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Panel4
            // 
            this.Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(106)))), ((int)(((byte)(93)))));
            this.Panel4.Controls.Add(this.Button2);
            this.Panel4.Controls.Add(this.Label1);
            this.Panel4.Location = new System.Drawing.Point(91, 118);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(1027, 464);
            this.Panel4.TabIndex = 613;
            // 
            // Eleccion_Servidor_o_remoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(178)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1211, 641);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.Panel8);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Eleccion_Servidor_o_remoto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Eleccion_Servidor_o_remoto_Load);
            this.Panel8.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datalistado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox7)).EndInit();
            this.Panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel8;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Label lblEstado_de_conexion;
        internal System.Windows.Forms.DataGridView datalistado;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn Eliminar;
        internal System.Windows.Forms.PictureBox PictureBox7;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Panel Panel4;
    }
}