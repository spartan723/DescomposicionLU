namespace DescomposicionLU.WinForms;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        pnlHeader = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        pnlControls = new Panel();
        grpMatrixConfig = new GroupBox();
        lblDim = new Label();
        numN = new NumericUpDown();
        btnApplyN = new Button();
        btnClear = new Button();
        btnTextImport = new Button();
        cmbExamples = new ComboBox();
        grpDisplayFormat = new GroupBox();
        rbFractions = new RadioButton();
        rbDecimals = new RadioButton();
        rbBoth = new RadioButton();
        lblDecimals = new Label();
        numDecimals = new NumericUpDown();
        btnCalculate = new Button();
        tabControlMain = new TabControl();
        tabMatrices = new TabPage();
        tblMatricesLayout = new TableLayoutPanel();
        pnlMatrixA = new Panel();
        dgvA = new DataGridView();
        lblMatrixA = new Label();
        pnlMatrixL = new Panel();
        dgvL = new DataGridView();
        lblMatrixL = new Label();
        pnlMatrixU = new Panel();
        dgvU = new DataGridView();
        lblMatrixU = new Label();
        tabSteps = new TabPage();
        txtSteps = new RichTextBox();
        tabVerification = new TabPage();
        dgvVerification = new DataGridView();
        pnlVerificationHeader = new Panel();
        lblVerificationStatus = new Label();
        tabConsole = new TabPage();
        txtConsole = new TextBox();
        pnlConsoleBottom = new Panel();
        btnCopyConsole = new Button();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        pnlHeader.SuspendLayout();
        pnlControls.SuspendLayout();
        grpMatrixConfig.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numN).BeginInit();
        grpDisplayFormat.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numDecimals).BeginInit();
        tabControlMain.SuspendLayout();
        tabMatrices.SuspendLayout();
        tblMatricesLayout.SuspendLayout();
        pnlMatrixA.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvA).BeginInit();
        pnlMatrixL.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvL).BeginInit();
        pnlMatrixU.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvU).BeginInit();
        tabSteps.SuspendLayout();
        tabVerification.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvVerification).BeginInit();
        pnlVerificationHeader.SuspendLayout();
        tabConsole.SuspendLayout();
        pnlConsoleBottom.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
        pnlHeader.Controls.Add(lblTitle);
        pnlHeader.Controls.Add(lblSubtitle);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1100, 70);
        pnlHeader.TabIndex = 2;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(18, 10);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(385, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Descomposición LU (Método de Doolittle)";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F);
        lblSubtitle.ForeColor = Color.FromArgb(203, 213, 225);
        lblSubtitle.Location = new Point(20, 38);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(561, 17);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Aritmética fraccionaria exacta con enteros, decimales y fracciones (1/3, -5/2, 0.25, 4) • A = L · U";
        // 
        // pnlControls
        // 
        pnlControls.BackColor = Color.FromArgb(248, 250, 252);
        pnlControls.Controls.Add(grpMatrixConfig);
        pnlControls.Controls.Add(grpDisplayFormat);
        pnlControls.Controls.Add(btnCalculate);
        pnlControls.Dock = DockStyle.Top;
        pnlControls.Location = new Point(0, 70);
        pnlControls.Name = "pnlControls";
        pnlControls.Padding = new Padding(12, 6, 12, 6);
        pnlControls.Size = new Size(1100, 115);
        pnlControls.TabIndex = 1;
        // 
        // grpMatrixConfig
        // 
        grpMatrixConfig.Controls.Add(lblDim);
        grpMatrixConfig.Controls.Add(numN);
        grpMatrixConfig.Controls.Add(btnApplyN);
        grpMatrixConfig.Controls.Add(btnClear);
        grpMatrixConfig.Controls.Add(btnTextImport);
        grpMatrixConfig.Controls.Add(cmbExamples);
        grpMatrixConfig.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpMatrixConfig.ForeColor = Color.FromArgb(30, 41, 59);
        grpMatrixConfig.Location = new Point(12, 6);
        grpMatrixConfig.Name = "grpMatrixConfig";
        grpMatrixConfig.Size = new Size(540, 98);
        grpMatrixConfig.TabIndex = 0;
        grpMatrixConfig.TabStop = false;
        grpMatrixConfig.Text = "1. Configuración de la Matriz A";
        // 
        // lblDim
        // 
        lblDim.AutoSize = true;
        lblDim.Font = new Font("Segoe UI", 9F);
        lblDim.Location = new Point(12, 26);
        lblDim.Name = "lblDim";
        lblDim.Size = new Size(61, 15);
        lblDim.TabIndex = 0;
        lblDim.Text = "Orden (n):";
        // 
        // numN
        // 
        numN.Font = new Font("Segoe UI", 9F);
        numN.Location = new Point(80, 24);
        numN.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        numN.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
        numN.Name = "numN";
        numN.Size = new Size(50, 23);
        numN.TabIndex = 1;
        numN.Value = new decimal(new int[] { 3, 0, 0, 0 });
        // 
        // btnApplyN
        // 
        btnApplyN.Font = new Font("Segoe UI", 9F);
        btnApplyN.Location = new Point(136, 22);
        btnApplyN.Name = "btnApplyN";
        btnApplyN.Size = new Size(62, 27);
        btnApplyN.TabIndex = 2;
        btnApplyN.Text = "Crear";
        btnApplyN.Click += BtnApplyN_Click;
        // 
        // btnClear
        // 
        btnClear.Font = new Font("Segoe UI", 9F);
        btnClear.Location = new Point(204, 22);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(66, 27);
        btnClear.TabIndex = 3;
        btnClear.Text = "Limpiar";
        btnClear.Click += BtnClear_Click;
        // 
        // btnTextImport
        // 
        btnTextImport.Font = new Font("Segoe UI", 9F);
        btnTextImport.Location = new Point(276, 22);
        btnTextImport.Name = "btnTextImport";
        btnTextImport.Size = new Size(95, 27);
        btnTextImport.TabIndex = 4;
        btnTextImport.Text = "Pegar Texto...";
        btnTextImport.Click += BtnTextImport_Click;
        // 
        // cmbExamples
        // 
        cmbExamples.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbExamples.Font = new Font("Segoe UI", 9F);
        cmbExamples.Location = new Point(12, 60);
        cmbExamples.Name = "cmbExamples";
        cmbExamples.Size = new Size(359, 23);
        cmbExamples.TabIndex = 5;
        cmbExamples.SelectedIndexChanged += CmbExamples_SelectedIndexChanged;
        // 
        // grpDisplayFormat
        // 
        grpDisplayFormat.Controls.Add(rbFractions);
        grpDisplayFormat.Controls.Add(rbDecimals);
        grpDisplayFormat.Controls.Add(rbBoth);
        grpDisplayFormat.Controls.Add(lblDecimals);
        grpDisplayFormat.Controls.Add(numDecimals);
        grpDisplayFormat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpDisplayFormat.ForeColor = Color.FromArgb(30, 41, 59);
        grpDisplayFormat.Location = new Point(562, 6);
        grpDisplayFormat.Name = "grpDisplayFormat";
        grpDisplayFormat.Size = new Size(330, 98);
        grpDisplayFormat.TabIndex = 1;
        grpDisplayFormat.TabStop = false;
        grpDisplayFormat.Text = "2. Formato de Salida";
        // 
        // rbFractions
        // 
        rbFractions.AutoSize = true;
        rbFractions.Checked = true;
        rbFractions.Font = new Font("Segoe UI", 8.5F);
        rbFractions.Location = new Point(12, 22);
        rbFractions.Name = "rbFractions";
        rbFractions.Size = new Size(134, 19);
        rbFractions.TabIndex = 0;
        rbFractions.TabStop = true;
        rbFractions.Text = "1. Fracciones exactas";
        rbFractions.CheckedChanged += FormatOption_Changed;
        // 
        // rbDecimals
        // 
        rbDecimals.AutoSize = true;
        rbDecimals.Font = new Font("Segoe UI", 8.5F);
        rbDecimals.Location = new Point(12, 46);
        rbDecimals.Name = "rbDecimals";
        rbDecimals.Size = new Size(91, 19);
        rbDecimals.TabIndex = 1;
        rbDecimals.Text = "2. Decimales";
        rbDecimals.CheckedChanged += FormatOption_Changed;
        // 
        // rbBoth
        // 
        rbBoth.AutoSize = true;
        rbBoth.Font = new Font("Segoe UI", 8.5F);
        rbBoth.Location = new Point(12, 70);
        rbBoth.Name = "rbBoth";
        rbBoth.Size = new Size(126, 19);
        rbBoth.TabIndex = 2;
        rbBoth.Text = "3. Ambos formatos";
        rbBoth.CheckedChanged += FormatOption_Changed;
        // 
        // lblDecimals
        // 
        lblDecimals.AutoSize = true;
        lblDecimals.Font = new Font("Segoe UI", 8.5F);
        lblDecimals.Location = new Point(200, 47);
        lblDecimals.Name = "lblDecimals";
        lblDecimals.Size = new Size(64, 15);
        lblDecimals.TabIndex = 3;
        lblDecimals.Text = "Decimales:";
        // 
        // numDecimals
        // 
        numDecimals.Font = new Font("Segoe UI", 9F);
        numDecimals.Location = new Point(268, 45);
        numDecimals.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
        numDecimals.Name = "numDecimals";
        numDecimals.Size = new Size(48, 23);
        numDecimals.TabIndex = 4;
        numDecimals.Value = new decimal(new int[] { 4, 0, 0, 0 });
        numDecimals.ValueChanged += FormatOption_Changed;
        // 
        // btnCalculate
        // 
        btnCalculate.BackColor = Color.FromArgb(37, 99, 235);
        btnCalculate.Cursor = Cursors.Hand;
        btnCalculate.FlatAppearance.BorderSize = 0;
        btnCalculate.FlatStyle = FlatStyle.Flat;
        btnCalculate.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        btnCalculate.ForeColor = Color.White;
        btnCalculate.Location = new Point(902, 13);
        btnCalculate.Name = "btnCalculate";
        btnCalculate.Size = new Size(165, 89);
        btnCalculate.TabIndex = 2;
        btnCalculate.Text = "▶ CALCULAR\r\nDESCOMPOSICIÓN";
        btnCalculate.UseVisualStyleBackColor = false;
        btnCalculate.Click += BtnCalculate_Click;
        // 
        // tabControlMain
        // 
        tabControlMain.Controls.Add(tabMatrices);
        tabControlMain.Controls.Add(tabSteps);
        tabControlMain.Controls.Add(tabVerification);
        tabControlMain.Controls.Add(tabConsole);
        tabControlMain.Dock = DockStyle.Fill;
        tabControlMain.Font = new Font("Segoe UI", 9.5F);
        tabControlMain.Location = new Point(0, 185);
        tabControlMain.Name = "tabControlMain";
        tabControlMain.SelectedIndex = 0;
        tabControlMain.Size = new Size(1100, 513);
        tabControlMain.TabIndex = 0;
        // 
        // tabMatrices
        // 
        tabMatrices.Controls.Add(tblMatricesLayout);
        tabMatrices.Location = new Point(4, 26);
        tabMatrices.Name = "tabMatrices";
        tabMatrices.Size = new Size(1092, 483);
        tabMatrices.TabIndex = 0;
        tabMatrices.Text = "  Matrices (A, L, U)  ";
        // 
        // tblMatricesLayout
        // 
        tblMatricesLayout.ColumnCount = 3;
        tblMatricesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tblMatricesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tblMatricesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        tblMatricesLayout.Controls.Add(pnlMatrixA, 0, 0);
        tblMatricesLayout.Controls.Add(pnlMatrixL, 1, 0);
        tblMatricesLayout.Controls.Add(pnlMatrixU, 2, 0);
        tblMatricesLayout.Dock = DockStyle.Fill;
        tblMatricesLayout.Location = new Point(0, 0);
        tblMatricesLayout.Name = "tblMatricesLayout";
        tblMatricesLayout.Padding = new Padding(6);
        tblMatricesLayout.RowCount = 1;
        tblMatricesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tblMatricesLayout.Size = new Size(1092, 483);
        tblMatricesLayout.TabIndex = 0;
        // 
        // pnlMatrixA
        // 
        pnlMatrixA.Controls.Add(dgvA);
        pnlMatrixA.Controls.Add(lblMatrixA);
        pnlMatrixA.Dock = DockStyle.Fill;
        pnlMatrixA.Location = new Point(9, 9);
        pnlMatrixA.Name = "pnlMatrixA";
        pnlMatrixA.Padding = new Padding(4);
        pnlMatrixA.Size = new Size(353, 465);
        pnlMatrixA.TabIndex = 0;
        // 
        // dgvA
        // 
        dgvA.Dock = DockStyle.Fill;
        dgvA.Location = new Point(4, 32);
        dgvA.Name = "dgvA";
        dgvA.Size = new Size(345, 429);
        dgvA.TabIndex = 0;
        // 
        // lblMatrixA
        // 
        lblMatrixA.Dock = DockStyle.Top;
        lblMatrixA.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMatrixA.ForeColor = Color.FromArgb(30, 41, 59);
        lblMatrixA.Location = new Point(4, 4);
        lblMatrixA.Name = "lblMatrixA";
        lblMatrixA.Size = new Size(345, 28);
        lblMatrixA.TabIndex = 1;
        lblMatrixA.Text = "Matriz Original A (Editable)";
        // 
        // pnlMatrixL
        // 
        pnlMatrixL.Controls.Add(dgvL);
        pnlMatrixL.Controls.Add(lblMatrixL);
        pnlMatrixL.Dock = DockStyle.Fill;
        pnlMatrixL.Location = new Point(368, 9);
        pnlMatrixL.Name = "pnlMatrixL";
        pnlMatrixL.Padding = new Padding(4);
        pnlMatrixL.Size = new Size(353, 465);
        pnlMatrixL.TabIndex = 1;
        // 
        // dgvL
        // 
        dgvL.Dock = DockStyle.Fill;
        dgvL.Location = new Point(4, 32);
        dgvL.Name = "dgvL";
        dgvL.Size = new Size(345, 429);
        dgvL.TabIndex = 0;
        // 
        // lblMatrixL
        // 
        lblMatrixL.Dock = DockStyle.Top;
        lblMatrixL.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMatrixL.ForeColor = Color.FromArgb(16, 110, 80);
        lblMatrixL.Location = new Point(4, 4);
        lblMatrixL.Name = "lblMatrixL";
        lblMatrixL.Size = new Size(345, 28);
        lblMatrixL.TabIndex = 1;
        lblMatrixL.Text = "Matriz L (Triangular Inferior - 1 en diagonal)";
        // 
        // pnlMatrixU
        // 
        pnlMatrixU.Controls.Add(dgvU);
        pnlMatrixU.Controls.Add(lblMatrixU);
        pnlMatrixU.Dock = DockStyle.Fill;
        pnlMatrixU.Location = new Point(727, 9);
        pnlMatrixU.Name = "pnlMatrixU";
        pnlMatrixU.Padding = new Padding(4);
        pnlMatrixU.Size = new Size(356, 465);
        pnlMatrixU.TabIndex = 2;
        // 
        // dgvU
        // 
        dgvU.Dock = DockStyle.Fill;
        dgvU.Location = new Point(4, 32);
        dgvU.Name = "dgvU";
        dgvU.Size = new Size(348, 429);
        dgvU.TabIndex = 0;
        // 
        // lblMatrixU
        // 
        lblMatrixU.Dock = DockStyle.Top;
        lblMatrixU.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMatrixU.ForeColor = Color.FromArgb(37, 99, 235);
        lblMatrixU.Location = new Point(4, 4);
        lblMatrixU.Name = "lblMatrixU";
        lblMatrixU.Size = new Size(348, 28);
        lblMatrixU.TabIndex = 1;
        lblMatrixU.Text = "Matriz U (Triangular Superior - Pivotes)";
        // 
        // tabSteps
        // 
        tabSteps.Controls.Add(txtSteps);
        tabSteps.Location = new Point(4, 26);
        tabSteps.Name = "tabSteps";
        tabSteps.Size = new Size(192, 70);
        tabSteps.TabIndex = 1;
        tabSteps.Text = "  Paso a Paso Detallado  ";
        // 
        // txtSteps
        // 
        txtSteps.BackColor = Color.FromArgb(254, 254, 255);
        txtSteps.BorderStyle = BorderStyle.None;
        txtSteps.Dock = DockStyle.Fill;
        txtSteps.Font = new Font("Consolas", 10F);
        txtSteps.Location = new Point(0, 0);
        txtSteps.Name = "txtSteps";
        txtSteps.ReadOnly = true;
        txtSteps.Size = new Size(192, 70);
        txtSteps.TabIndex = 0;
        txtSteps.Text = "";
        // 
        // tabVerification
        // 
        tabVerification.Controls.Add(dgvVerification);
        tabVerification.Controls.Add(pnlVerificationHeader);
        tabVerification.Location = new Point(4, 26);
        tabVerification.Name = "tabVerification";
        tabVerification.Size = new Size(192, 70);
        tabVerification.TabIndex = 2;
        tabVerification.Text = "  Verificación (L · U = A)  ";
        // 
        // dgvVerification
        // 
        dgvVerification.Dock = DockStyle.Fill;
        dgvVerification.Location = new Point(0, 50);
        dgvVerification.Name = "dgvVerification";
        dgvVerification.Size = new Size(192, 20);
        dgvVerification.TabIndex = 0;
        // 
        // pnlVerificationHeader
        // 
        pnlVerificationHeader.BackColor = Color.FromArgb(241, 245, 249);
        pnlVerificationHeader.Controls.Add(lblVerificationStatus);
        pnlVerificationHeader.Dock = DockStyle.Top;
        pnlVerificationHeader.Location = new Point(0, 0);
        pnlVerificationHeader.Name = "pnlVerificationHeader";
        pnlVerificationHeader.Padding = new Padding(12, 10, 12, 10);
        pnlVerificationHeader.Size = new Size(192, 50);
        pnlVerificationHeader.TabIndex = 1;
        // 
        // lblVerificationStatus
        // 
        lblVerificationStatus.Dock = DockStyle.Fill;
        lblVerificationStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblVerificationStatus.Location = new Point(12, 10);
        lblVerificationStatus.Name = "lblVerificationStatus";
        lblVerificationStatus.Size = new Size(168, 30);
        lblVerificationStatus.TabIndex = 0;
        lblVerificationStatus.Text = "Ejecute el cálculo para verificar el producto de matrices L · U.";
        // 
        // tabConsole
        // 
        tabConsole.Controls.Add(txtConsole);
        tabConsole.Controls.Add(pnlConsoleBottom);
        tabConsole.Location = new Point(4, 26);
        tabConsole.Name = "tabConsole";
        tabConsole.Size = new Size(1092, 483);
        tabConsole.TabIndex = 3;
        tabConsole.Text = "  Salida Formato Texto (Consola)  ";
        // 
        // txtConsole
        // 
        txtConsole.BackColor = Color.FromArgb(15, 23, 42);
        txtConsole.BorderStyle = BorderStyle.None;
        txtConsole.Dock = DockStyle.Fill;
        txtConsole.Font = new Font("Consolas", 10.5F);
        txtConsole.ForeColor = Color.FromArgb(241, 245, 249);
        txtConsole.Location = new Point(0, 0);
        txtConsole.Multiline = true;
        txtConsole.Name = "txtConsole";
        txtConsole.ReadOnly = true;
        txtConsole.ScrollBars = ScrollBars.Both;
        txtConsole.Size = new Size(1092, 437);
        txtConsole.TabIndex = 0;
        // 
        // pnlConsoleBottom
        // 
        pnlConsoleBottom.BackColor = Color.FromArgb(241, 245, 249);
        pnlConsoleBottom.Controls.Add(btnCopyConsole);
        pnlConsoleBottom.Dock = DockStyle.Bottom;
        pnlConsoleBottom.Location = new Point(0, 437);
        pnlConsoleBottom.Name = "pnlConsoleBottom";
        pnlConsoleBottom.Padding = new Padding(8);
        pnlConsoleBottom.Size = new Size(1092, 46);
        pnlConsoleBottom.TabIndex = 1;
        // 
        // btnCopyConsole
        // 
        btnCopyConsole.BackColor = Color.FromArgb(71, 85, 105);
        btnCopyConsole.Cursor = Cursors.Hand;
        btnCopyConsole.Dock = DockStyle.Left;
        btnCopyConsole.FlatAppearance.BorderSize = 0;
        btnCopyConsole.FlatStyle = FlatStyle.Flat;
        btnCopyConsole.ForeColor = Color.White;
        btnCopyConsole.Location = new Point(8, 8);
        btnCopyConsole.Name = "btnCopyConsole";
        btnCopyConsole.Size = new Size(230, 30);
        btnCopyConsole.TabIndex = 0;
        btnCopyConsole.Text = "📋 Copiar Texto al Portapapeles";
        btnCopyConsole.UseVisualStyleBackColor = false;
        btnCopyConsole.Click += BtnCopyConsole_Click;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 698);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1100, 22);
        statusStrip.TabIndex = 3;
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(388, 17);
        statusLabel.Text = "Listo. Ingrese los valores de la matriz A o cargue un ejemplo predefinido.";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 720);
        Controls.Add(tabControlMain);
        Controls.Add(pnlControls);
        Controls.Add(pnlHeader);
        Controls.Add(statusStrip);
        MinimumSize = new Size(960, 620);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Descomposición LU (Método de Doolittle) - Aritmética Exacta con Fracciones";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlControls.ResumeLayout(false);
        grpMatrixConfig.ResumeLayout(false);
        grpMatrixConfig.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numN).EndInit();
        grpDisplayFormat.ResumeLayout(false);
        grpDisplayFormat.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numDecimals).EndInit();
        tabControlMain.ResumeLayout(false);
        tabMatrices.ResumeLayout(false);
        tblMatricesLayout.ResumeLayout(false);
        pnlMatrixA.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvA).EndInit();
        pnlMatrixL.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvL).EndInit();
        pnlMatrixU.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvU).EndInit();
        tabSteps.ResumeLayout(false);
        tabVerification.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvVerification).EndInit();
        pnlVerificationHeader.ResumeLayout(false);
        tabConsole.ResumeLayout(false);
        tabConsole.PerformLayout();
        pnlConsoleBottom.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private void ConfigureDataGridView(DataGridView dgv, bool isReadOnly)
    {
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.AllowUserToResizeRows = false;
        dgv.RowHeadersWidth = 46;
        dgv.ReadOnly = isReadOnly;
        dgv.BackgroundColor = Color.White;
        dgv.BorderStyle = BorderStyle.FixedSingle;
        dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgv.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F);
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
        dgv.EnableHeadersVisualStyles = true;
    }

    #endregion

    private Panel pnlHeader;
    private Label lblTitle;
    private Label lblSubtitle;
    private Panel pnlControls;
    private GroupBox grpMatrixConfig;
    private Label lblDim;
    private NumericUpDown numN;
    private Button btnApplyN;
    private Button btnClear;
    private ComboBox cmbExamples;
    private Button btnTextImport;
    private GroupBox grpDisplayFormat;
    private RadioButton rbFractions;
    private RadioButton rbDecimals;
    private RadioButton rbBoth;
    private Label lblDecimals;
    private NumericUpDown numDecimals;
    private Button btnCalculate;
    private TabControl tabControlMain;
    private TabPage tabMatrices;
    private TableLayoutPanel tblMatricesLayout;
    private Panel pnlMatrixA;
    private Label lblMatrixA;
    private DataGridView dgvA;
    private Panel pnlMatrixL;
    private Label lblMatrixL;
    private DataGridView dgvL;
    private Panel pnlMatrixU;
    private Label lblMatrixU;
    private DataGridView dgvU;
    private TabPage tabSteps;
    private RichTextBox txtSteps;
    private TabPage tabVerification;
    private Panel pnlVerificationHeader;
    private Label lblVerificationStatus;
    private DataGridView dgvVerification;
    private TabPage tabConsole;
    private TextBox txtConsole;
    private Panel pnlConsoleBottom;
    private Button btnCopyConsole;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusLabel;
}
