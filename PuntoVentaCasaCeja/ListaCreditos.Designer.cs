﻿namespace PuntoVentaCasaCeja
{
    partial class ListaCreditos
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.clientinfo = new System.Windows.Forms.TableLayoutPanel();
            this.tablaCreditos = new System.Windows.Forms.DataGridView();
            this.BSelCliente = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.clientinfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaCreditos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BSelCliente);
            this.groupBox1.Controls.Add(this.exitButton);
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1460, 821);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LISTADO DE CREDITOS";
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.exitButton.Location = new System.Drawing.Point(1187, -4);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(259, 57);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "SALIR (Esc)";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.clientinfo, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(9, 60);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1443, 753);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 657);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1435, 72);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // clientinfo
            // 
            this.clientinfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientinfo.ColumnCount = 1;
            this.clientinfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.clientinfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.clientinfo.Controls.Add(this.tablaCreditos, 0, 0);
            this.clientinfo.Location = new System.Drawing.Point(4, 4);
            this.clientinfo.Margin = new System.Windows.Forms.Padding(4);
            this.clientinfo.Name = "clientinfo";
            this.clientinfo.RowCount = 1;
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66616F));
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66616F));
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66616F));
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66616F));
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66767F));
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.clientinfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66767F));
            this.clientinfo.Size = new System.Drawing.Size(1435, 645);
            this.clientinfo.TabIndex = 0;
            // 
            // tablaCreditos
            // 
            this.tablaCreditos.AllowUserToAddRows = false;
            this.tablaCreditos.AllowUserToDeleteRows = false;
            this.tablaCreditos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaCreditos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tablaCreditos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tablaCreditos.DefaultCellStyle = dataGridViewCellStyle2;
            this.tablaCreditos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaCreditos.Location = new System.Drawing.Point(3, 2);
            this.tablaCreditos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tablaCreditos.Name = "tablaCreditos";
            this.tablaCreditos.ReadOnly = true;
            this.tablaCreditos.RowHeadersVisible = false;
            this.tablaCreditos.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.tablaCreditos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.tablaCreditos.RowTemplate.Height = 50;
            this.tablaCreditos.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaCreditos.Size = new System.Drawing.Size(1429, 641);
            this.tablaCreditos.TabIndex = 0;
            this.tablaCreditos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaCreditos_CellContentClick);
            this.tablaCreditos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaCreditos_CellDoubleClick);
            this.tablaCreditos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TablaCreditos_KeyDown);
            // 
            // BSelCliente
            // 
            this.BSelCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BSelCliente.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.BSelCliente.Location = new System.Drawing.Point(794, -3);
            this.BSelCliente.Margin = new System.Windows.Forms.Padding(4);
            this.BSelCliente.Name = "BSelCliente";
            this.BSelCliente.Size = new System.Drawing.Size(385, 57);
            this.BSelCliente.TabIndex = 8;
            this.BSelCliente.Text = "SEL. CLIENTE (ENTER)";
            this.BSelCliente.UseVisualStyleBackColor = true;
            this.BSelCliente.Click += new System.EventHandler(this.BSelCliente_Click);
            // 
            // ListaCreditos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 850);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListaCreditos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Creditos";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.clientinfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaCreditos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel clientinfo;
        private System.Windows.Forms.DataGridView tablaCreditos;
        private System.Windows.Forms.Button BSelCliente;
    }
}