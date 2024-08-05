namespace PuntoVentaCasaCeja
{
    partial class Usuarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tablaUsuarios = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.crear = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl = new System.Windows.Forms.Label();
            this.txtbuscar = new System.Windows.Forms.TextBox();
            this.salir = new System.Windows.Forms.Button();
            this.Bmodificar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaUsuarios)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tablaUsuarios, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 14);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1211, 652);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // tablaUsuarios
            // 
            this.tablaUsuarios.AllowUserToAddRows = false;
            this.tablaUsuarios.AllowUserToDeleteRows = false;
            this.tablaUsuarios.AllowUserToOrderColumns = true;
            this.tablaUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablaUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tablaUsuarios.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tablaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaUsuarios.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tablaUsuarios.DefaultCellStyle = dataGridViewCellStyle4;
            this.tablaUsuarios.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tablaUsuarios.Location = new System.Drawing.Point(4, 115);
            this.tablaUsuarios.Margin = new System.Windows.Forms.Padding(4);
            this.tablaUsuarios.MultiSelect = false;
            this.tablaUsuarios.Name = "tablaUsuarios";
            this.tablaUsuarios.RowHeadersVisible = false;
            this.tablaUsuarios.RowHeadersWidth = 51;
            this.tablaUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaUsuarios.Size = new System.Drawing.Size(1203, 533);
            this.tablaUsuarios.StandardTab = true;
            this.tablaUsuarios.TabIndex = 0;
            this.tablaUsuarios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tablaUsuarios_KeyDown);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.69758F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.11694F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.18548F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel2.Controls.Add(this.crear, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.salir, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.Bmodificar, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1203, 103);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // crear
            // 
            this.crear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crear.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.crear.Location = new System.Drawing.Point(497, 4);
            this.crear.Margin = new System.Windows.Forms.Padding(4);
            this.crear.Name = "crear";
            this.crear.Size = new System.Drawing.Size(261, 84);
            this.crear.TabIndex = 2;
            this.crear.Text = "NUEVO USUARIO (F2)";
            this.crear.UseVisualStyleBackColor = true;
            this.crear.Click += new System.EventHandler(this.crear_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lbl, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtbuscar, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(485, 100);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl.Location = new System.Drawing.Point(4, 0);
            this.lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(169, 37);
            this.lbl.TabIndex = 6;
            this.lbl.Text = "BUSCAR (F1)";
            // 
            // txtbuscar
            // 
            this.txtbuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtbuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtbuscar.Location = new System.Drawing.Point(4, 41);
            this.txtbuscar.Margin = new System.Windows.Forms.Padding(4);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Size = new System.Drawing.Size(477, 42);
            this.txtbuscar.TabIndex = 1;
            this.txtbuscar.TextChanged += new System.EventHandler(this.txtbuscar_TextChanged);
            // 
            // salir
            // 
            this.salir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.salir.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            this.salir.Location = new System.Drawing.Point(996, 4);
            this.salir.Margin = new System.Windows.Forms.Padding(4);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(203, 84);
            this.salir.TabIndex = 3;
            this.salir.Text = "SALIR (Esc)";
            this.salir.UseVisualStyleBackColor = true;
            this.salir.Click += new System.EventHandler(this.salir_Click);
            // 
            // Bmodificar
            // 
            this.Bmodificar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Bmodificar.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.Bmodificar.Location = new System.Drawing.Point(766, 4);
            this.Bmodificar.Margin = new System.Windows.Forms.Padding(4);
            this.Bmodificar.Name = "Bmodificar";
            this.Bmodificar.Size = new System.Drawing.Size(222, 84);
            this.Bmodificar.TabIndex = 14;
            this.Bmodificar.Text = "MODIFICAR (F3)";
            this.Bmodificar.UseVisualStyleBackColor = true;
            this.Bmodificar.Click += new System.EventHandler(this.Bmodificar_Click);
            // 
            // Usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 681);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Usuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrar Usuarios";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaUsuarios)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView tablaUsuarios;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button crear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.TextBox txtbuscar;
        private System.Windows.Forms.Button salir;
        private System.Windows.Forms.Button Bmodificar;
    }
}