namespace Desafio_ianderson_cocco
{
    partial class frm_Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gbMoviemntarEstoque = new GroupBox();
            cblTipoMovimento = new CheckedListBox();
            label3 = new Label();
            txtEstoque = new TextBox();
            btnNovo = new Button();
            txtMemoriaRegistro = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txbQtd = new TextBox();
            cbProduto = new ComboBox();
            btnInserir = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            btnCalcularComissao = new Button();
            label4 = new Label();
            txtJuros = new TextBox();
            label5 = new Label();
            txtValorTomado = new TextBox();
            dpVencimento = new DateTimePicker();
            label6 = new Label();
            btnCalcular = new Button();
            gbMoviemntarEstoque.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // gbMoviemntarEstoque
            // 
            gbMoviemntarEstoque.Controls.Add(cblTipoMovimento);
            gbMoviemntarEstoque.Controls.Add(label3);
            gbMoviemntarEstoque.Controls.Add(txtEstoque);
            gbMoviemntarEstoque.Controls.Add(btnNovo);
            gbMoviemntarEstoque.Controls.Add(txtMemoriaRegistro);
            gbMoviemntarEstoque.Controls.Add(label2);
            gbMoviemntarEstoque.Controls.Add(label1);
            gbMoviemntarEstoque.Controls.Add(txbQtd);
            gbMoviemntarEstoque.Controls.Add(cbProduto);
            gbMoviemntarEstoque.Controls.Add(btnInserir);
            gbMoviemntarEstoque.Location = new Point(237, 12);
            gbMoviemntarEstoque.Name = "gbMoviemntarEstoque";
            gbMoviemntarEstoque.Size = new Size(507, 426);
            gbMoviemntarEstoque.TabIndex = 2;
            gbMoviemntarEstoque.TabStop = false;
            gbMoviemntarEstoque.Text = "2 - Movimentar estoque";
            // 
            // cblTipoMovimento
            // 
            cblTipoMovimento.FormattingEnabled = true;
            cblTipoMovimento.Items.AddRange(new object[] { "Entrada", "Saida" });
            cblTipoMovimento.Location = new Point(356, 51);
            cblTipoMovimento.Name = "cblTipoMovimento";
            cblTipoMovimento.Size = new Size(77, 40);
            cblTipoMovimento.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(279, 51);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 13;
            label3.Text = "Estoque";
            // 
            // txtEstoque
            // 
            txtEstoque.Location = new Point(279, 68);
            txtEstoque.Name = "txtEstoque";
            txtEstoque.ReadOnly = true;
            txtEstoque.Size = new Size(71, 23);
            txtEstoque.TabIndex = 12;
            // 
            // btnNovo
            // 
            btnNovo.Location = new Point(188, 301);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(85, 40);
            btnNovo.TabIndex = 9;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            // 
            // txtMemoriaRegistro
            // 
            txtMemoriaRegistro.Location = new Point(6, 96);
            txtMemoriaRegistro.Multiline = true;
            txtMemoriaRegistro.Name = "txtMemoriaRegistro";
            txtMemoriaRegistro.ReadOnly = true;
            txtMemoriaRegistro.Size = new Size(495, 199);
            txtMemoriaRegistro.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(202, 51);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 7;
            label2.Text = "Quantidade";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 51);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 6;
            label1.Text = "Item";
            // 
            // txbQtd
            // 
            txbQtd.Location = new Point(202, 68);
            txbQtd.Name = "txbQtd";
            txbQtd.Size = new Size(71, 23);
            txbQtd.TabIndex = 4;
            // 
            // cbProduto
            // 
            cbProduto.FormattingEnabled = true;
            cbProduto.Location = new Point(6, 67);
            cbProduto.Name = "cbProduto";
            cbProduto.Size = new Size(190, 23);
            cbProduto.TabIndex = 3;
            cbProduto.SelectedIndexChanged += cbProduto_SelectedIndexChanged;
            // 
            // btnInserir
            // 
            btnInserir.Location = new Point(439, 65);
            btnInserir.Name = "btnInserir";
            btnInserir.Size = new Size(62, 23);
            btnInserir.TabIndex = 2;
            btnInserir.Text = "Registrar";
            btnInserir.UseVisualStyleBackColor = true;
            btnInserir.Click += btnInserir_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCalcular);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(dpVencimento);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtValorTomado);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtJuros);
            groupBox1.Location = new Point(750, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(251, 135);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "3 - Calcular juros";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnCalcularComissao);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 100);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "1 - Calcular comissão";
            // 
            // btnCalcularComissao
            // 
            btnCalcularComissao.Location = new Point(24, 39);
            btnCalcularComissao.Name = "btnCalcularComissao";
            btnCalcularComissao.Size = new Size(153, 23);
            btnCalcularComissao.TabIndex = 1;
            btnCalcularComissao.Text = "1 - Calcular comissão";
            btnCalcularComissao.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 76);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 15;
            label4.Text = "Juros";
            label4.Click += label4_Click;
            // 
            // txtJuros
            // 
            txtJuros.Location = new Point(18, 96);
            txtJuros.Name = "txtJuros";
            txtJuros.ReadOnly = true;
            txtJuros.Size = new Size(71, 23);
            txtJuros.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 24);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 17;
            label5.Text = "Valor tomado";
            // 
            // txtValorTomado
            // 
            txtValorTomado.Location = new Point(18, 41);
            txtValorTomado.Name = "txtValorTomado";
            txtValorTomado.Size = new Size(78, 23);
            txtValorTomado.TabIndex = 16;
            // 
            // dpVencimento
            // 
            dpVencimento.Format = DateTimePickerFormat.Short;
            dpVencimento.Location = new Point(102, 42);
            dpVencimento.Name = "dpVencimento";
            dpVencimento.Size = new Size(103, 23);
            dpVencimento.TabIndex = 18;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(102, 24);
            label6.Name = "label6";
            label6.Size = new Size(97, 15);
            label6.TabIndex = 19;
            label6.Text = "Data Vencimento";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(95, 92);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(85, 28);
            btnCalcular.TabIndex = 17;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // frm_Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1027, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(gbMoviemntarEstoque);
            Name = "frm_Principal";
            Text = "Desafio Ianderson Cocco";
            gbMoviemntarEstoque.ResumeLayout(false);
            gbMoviemntarEstoque.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox gbMoviemntarEstoque;
        private Label label2;
        private Label label1;
        private TextBox txbQtd;
        private ComboBox cbProduto;
        private Button btnInserir;
        private Button btnNovo;
        private TextBox txtMemoriaRegistro;
        private Label label3;
        private TextBox txtEstoque;
        private CheckedListBox cblTipoMovimento;
        private GroupBox groupBox1;
        private Label label4;
        private TextBox txtJuros;
        private GroupBox groupBox2;
        private Button btnCalcularComissao;
        private Label label5;
        private TextBox txtValorTomado;
        private Button btnCalcular;
        private Label label6;
        private DateTimePicker dpVencimento;
    }
}
