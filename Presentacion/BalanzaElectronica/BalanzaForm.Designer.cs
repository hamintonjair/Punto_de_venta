
namespace Punto_de_venta.Presentacion.BalanzaElectronica
{
    partial class BalanzaForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalanzaForm));
            this.PanelContenedorTODO = new System.Windows.Forms.Panel();
            this.lblestado = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtresultado = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnProbar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel38 = new System.Windows.Forms.Panel();
            this.cbListarPuertos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.puertos = new System.IO.Ports.SerialPort(this.components);
            this.PanelContenedorTODO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelContenedorTODO
            // 
            this.PanelContenedorTODO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelContenedorTODO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(178)))), ((int)(((byte)(20)))));
            this.PanelContenedorTODO.Controls.Add(this.lblestado);
            this.PanelContenedorTODO.Controls.Add(this.button2);
            this.PanelContenedorTODO.Controls.Add(this.pictureBox4);
            this.PanelContenedorTODO.Controls.Add(this.pictureBox3);
            this.PanelContenedorTODO.Controls.Add(this.button1);
            this.PanelContenedorTODO.Controls.Add(this.label6);
            this.PanelContenedorTODO.Controls.Add(this.panel1);
            this.PanelContenedorTODO.Controls.Add(this.txtresultado);
            this.PanelContenedorTODO.Controls.Add(this.label5);
            this.PanelContenedorTODO.Controls.Add(this.btnProbar);
            this.PanelContenedorTODO.Controls.Add(this.label4);
            this.PanelContenedorTODO.Controls.Add(this.pictureBox2);
            this.PanelContenedorTODO.Controls.Add(this.panel38);
            this.PanelContenedorTODO.Controls.Add(this.cbListarPuertos);
            this.PanelContenedorTODO.Controls.Add(this.label2);
            this.PanelContenedorTODO.Controls.Add(this.label1);
            this.PanelContenedorTODO.Controls.Add(this.pictureBox1);
            this.PanelContenedorTODO.Controls.Add(this.label25);
            this.PanelContenedorTODO.ForeColor = System.Drawing.Color.White;
            this.PanelContenedorTODO.Location = new System.Drawing.Point(138, 80);
            this.PanelContenedorTODO.Name = "PanelContenedorTODO";
            this.PanelContenedorTODO.Size = new System.Drawing.Size(680, 447);
            this.PanelContenedorTODO.TabIndex = 642;
            // 
            // lblestado
            // 
            this.lblestado.AutoSize = true;
            this.lblestado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblestado.Location = new System.Drawing.Point(388, 257);
            this.lblestado.Name = "lblestado";
            this.lblestado.Size = new System.Drawing.Size(23, 25);
            this.lblestado.TabIndex = 620;
            this.lblestado.Text = "?";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::Punto_de_venta.Properties.Resources.negro;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(444, 371);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 31);
            this.button2.TabIndex = 621;
            this.button2.Text = "Enviar";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(607, 364);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 38);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 619;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(607, 241);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 38);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 618;
            this.pictureBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::Punto_de_venta.Properties.Resources.Rojo;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(249, 355);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 47);
            this.button1.TabIndex = 617;
            this.button1.Text = "Guardar";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(235, 304);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(315, 48);
            this.label6.TabIndex = 616;
            this.label6.Text = "Paso 3: Si ya reconoce el peso, entonces de click a \"Guardar\"";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(106)))), ((int)(((byte)(93)))));
            this.panel1.Location = new System.Drawing.Point(239, 285);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 1);
            this.panel1.TabIndex = 615;
            // 
            // txtresultado
            // 
            this.txtresultado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtresultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtresultado.ForeColor = System.Drawing.Color.White;
            this.txtresultado.Location = new System.Drawing.Point(444, 213);
            this.txtresultado.Multiline = true;
            this.txtresultado.Name = "txtresultado";
            this.txtresultado.Size = new System.Drawing.Size(116, 25);
            this.txtresultado.TabIndex = 614;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(389, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 613;
            this.label5.Text = "Peso:";
            // 
            // btnProbar
            // 
            this.btnProbar.BackColor = System.Drawing.Color.Transparent;
            this.btnProbar.BackgroundImage = global::Punto_de_venta.Properties.Resources.azul1;
            this.btnProbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProbar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProbar.FlatAppearance.BorderSize = 0;
            this.btnProbar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnProbar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnProbar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProbar.ForeColor = System.Drawing.Color.White;
            this.btnProbar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProbar.Location = new System.Drawing.Point(249, 203);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(110, 48);
            this.btnProbar.TabIndex = 612;
            this.btnProbar.Text = "Probar";
            this.btnProbar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProbar.UseVisualStyleBackColor = false;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(235, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(346, 20);
            this.label4.TabIndex = 548;
            this.label4.Text = "Paso 2: Seleccione un puerto y de click a probar";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(607, 110);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 546;
            this.pictureBox2.TabStop = false;
            // 
            // panel38
            // 
            this.panel38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(106)))), ((int)(((byte)(93)))));
            this.panel38.Location = new System.Drawing.Point(239, 154);
            this.panel38.Name = "panel38";
            this.panel38.Size = new System.Drawing.Size(418, 1);
            this.panel38.TabIndex = 545;
            // 
            // cbListarPuertos
            // 
            this.cbListarPuertos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbListarPuertos.FormattingEnabled = true;
            this.cbListarPuertos.Location = new System.Drawing.Point(393, 117);
            this.cbListarPuertos.Name = "cbListarPuertos";
            this.cbListarPuertos.Size = new System.Drawing.Size(121, 21);
            this.cbListarPuertos.TabIndex = 544;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(327, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 543;
            this.label2.Text = "Puerto:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(235, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(391, 20);
            this.label1.TabIndex = 542;
            this.label1.Text = "Paso 1: Eleija un puerto, pruebe hasta que se conecte";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 307);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 541;
            this.pictureBox1.TabStop = false;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(178)))), ((int)(((byte)(20)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(680, 50);
            this.label25.TabIndex = 540;
            this.label25.Text = "Configurar Balanza Electrónica";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // puertos
            // 
            this.puertos.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.puertos_DataReceived);
            // 
            // BalanzaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(106)))), ((int)(((byte)(93)))));
            this.ClientSize = new System.Drawing.Size(1029, 587);
            this.Controls.Add(this.PanelContenedorTODO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BalanzaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.BalanzaForm_Load);
            this.PanelContenedorTODO.ResumeLayout(false);
            this.PanelContenedorTODO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelContenedorTODO;
        internal System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbListarPuertos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Panel panel38;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtresultado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnProbar;
        private System.IO.Ports.SerialPort puertos;
        private System.Windows.Forms.Label lblestado;
        private System.Windows.Forms.Button button2;
    }
}