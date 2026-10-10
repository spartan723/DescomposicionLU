using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DescomposicionLU.WinForms.Forms;
using DescomposicionLU.WinForms.Models;
using DescomposicionLU.WinForms.Services;

namespace DescomposicionLU.WinForms;

public partial class Form1 : Form
{
    private int currentDimension = 3;
    private LuResult? lastResult = null;
    private Fraction[,]? lastMatrixA = null;

    public Form1()
    {
        InitializeComponent();
        InitializeExamples();
        InitializeMatrixGrids(currentDimension);
        LoadExample(0); // Cargar ejemplo clásico inicial
    }

    private void InitializeExamples()
    {
        cmbExamples.Items.Clear();
        cmbExamples.Items.Add("Cargar Ejemplo: 1. Clásico 3x3 (Enteros)");
        cmbExamples.Items.Add("Cargar Ejemplo: 2. Con Fracciones y Decimales (1/3, -0.5)");
        cmbExamples.Items.Add("Cargar Ejemplo: 3. Sistema 4x4 (Racionales)");
        cmbExamples.Items.Add("Cargar Ejemplo: 4. Pivote Nulo (Requiere pivoteo parcial)");
        cmbExamples.SelectedIndex = 0;
    }

    private void InitializeMatrixGrids(int n)
    {
        currentDimension = n;
        numN.Value = n;

        SetupGrid(dgvA, n, isEditable: true);
        SetupGrid(dgvL, n, isEditable: false);
        SetupGrid(dgvU, n, isEditable: false);
        SetupGrid(dgvVerification, n, isEditable: false);

        lastResult = null;
        lastMatrixA = null;
        txtSteps.Clear();
        txtConsole.Clear();
        lblVerificationStatus.Text = "Ejecute el cálculo para verificar el producto de matrices L · U.";
        lblVerificationStatus.ForeColor = Color.FromArgb(71, 85, 105);
        statusLabel.Text = $"Cuadrícula lista para matriz de orden {n}x{n}.";
    }

    private void SetupGrid(DataGridView dgv, int n, bool isEditable)
    {
        dgv.Columns.Clear();
        dgv.Rows.Clear();

        for (int c = 0; c < n; c++)
        {
            var col = new DataGridViewTextBoxColumn
            {
                HeaderText = $"C{c + 1}",
                Name = $"col{c}",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = Math.Max(58, 240 / n)
            };
            dgv.Columns.Add(col);
        }

        for (int r = 0; r < n; r++)
        {
            int rowIndex = dgv.Rows.Add();
            dgv.Rows[rowIndex].HeaderCell.Value = $"F{r + 1}";
            for (int c = 0; c < n; c++)
            {
                dgv.Rows[rowIndex].Cells[c].Value = isEditable ? "0" : "";
            }
        }

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void BtnApplyN_Click(object? sender, EventArgs e)
    {
        int n = (int)numN.Value;
        InitializeMatrixGrids(n);
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        for (int r = 0; r < currentDimension; r++)
        {
            for (int c = 0; c < currentDimension; c++)
            {
                dgvA.Rows[r].Cells[c].Value = "0";
                dgvL.Rows[r].Cells[c].Value = "";
                dgvU.Rows[r].Cells[c].Value = "";
                dgvVerification.Rows[r].Cells[c].Value = "";
            }
        }
        lastResult = null;
        lastMatrixA = null;
        txtSteps.Clear();
        txtConsole.Clear();
        lblVerificationStatus.Text = "Matriz reiniciada a ceros.";
        lblVerificationStatus.ForeColor = Color.FromArgb(71, 85, 105);
        statusLabel.Text = "Matriz limpiada.";
    }

    private void CmbExamples_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbExamples.SelectedIndex >= 0)
        {
            LoadExample(cmbExamples.SelectedIndex);
        }
    }

    private void LoadExample(int index)
    {
        switch (index)
        {
            case 0: // Clásico 3x3
                SetMatrixValues(new string[,]
                {
                    { "2", "-1", "-2" },
                    { "-4", "6", "3" },
                    { "-4", "-2", "8" }
                });
                break;

            case 1: // Fracciones y decimales (idéntico al ejemplo de la descripción del script Python: '2 1/3 -0.5')
                SetMatrixValues(new string[,]
                {
                    { "2", "1/3", "-0.5" },
                    { "1", "-2", "4" },
                    { "-1/2", "0", "3" }
                });
                break;

            case 2: // 4x4
                SetMatrixValues(new string[,]
                {
                    { "1", "2", "3", "4" },
                    { "2", "5", "1", "10" },
                    { "3", "1", "-1", "2" },
                    { "4", "10", "2", "8" }
                });
                break;

            case 3: // Pivote Nulo
                SetMatrixValues(new string[,]
                {
                    { "0", "1", "2" },
                    { "1", "2", "3" },
                    { "2", "3", "4" }
                });
                break;
        }
    }

    private void SetMatrixValues(string[,] values)
    {
        int n = values.GetLength(0);
        InitializeMatrixGrids(n);

        for (int r = 0; r < n; r++)
        {
            for (int c = 0; c < n; c++)
            {
                dgvA.Rows[r].Cells[c].Value = values[r, c];
            }
        }

        statusLabel.Text = $"Ejemplo cargado correctamente ({n}x{n}).";
    }

    private void BtnTextImport_Click(object? sender, EventArgs e)
    {
        using var dialog = new TextInputForm();
        if (dialog.ShowDialog(this) == DialogResult.OK && dialog.ParsedMatrix != null)
        {
            int n = dialog.DetectedDimension;
            InitializeMatrixGrids(n);

            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    dgvA.Rows[r].Cells[c].Value = dialog.ParsedMatrix[r, c].ToString();
                }
            }

            statusLabel.Text = $"Matriz de {n}x{n} importada con éxito desde texto.";
        }
    }

    private DisplayFormat GetCurrentDisplayFormat()
    {
        if (rbDecimals.Checked) return DisplayFormat.Decimals;
        if (rbBoth.Checked) return DisplayFormat.Both;
        return DisplayFormat.Fractions;
    }

    private int GetDecimals() => (int)numDecimals.Value;

    private void FormatOption_Changed(object? sender, EventArgs e)
    {
        if (lastResult != null && lastMatrixA != null)
        {
            RenderResults(lastResult, lastMatrixA);
        }
    }

    private void BtnCalculate_Click(object? sender, EventArgs e)
    {
        int n = currentDimension;
        Fraction[,] A = new Fraction[n, n];

        // 1. Leer y validar cada celda de la matriz A
        for (int r = 0; r < n; r++)
        {
            for (int c = 0; c < n; c++)
            {
                object? cellVal = dgvA.Rows[r].Cells[c].Value;
                string strVal = cellVal?.ToString()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(strVal))
                {
                    MessageBox.Show(
                        $"La celda en la posición Fila {r + 1}, Columna {c + 1} está vacía. Por favor ingrese un número, decimal o fracción.",
                        "Celda Vacía",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    dgvA.CurrentCell = dgvA.Rows[r].Cells[c];
                    return;
                }

                try
                {
                    A[r, c] = Fraction.Parse(strVal);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error en la celda ({r + 1}, {c + 1}): '{strVal}'.\n{ex.Message}\n\nFormatos válidos: '4', '-10', '0.25', '-1.5', '1/3', '-5/2'.",
                        "Error de Formato",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    dgvA.CurrentCell = dgvA.Rows[r].Cells[c];
                    return;
                }
            }
        }

        // 2. Ejecutar la descomposición LU
        var sw = Stopwatch.StartNew();
        try
        {
            LuResult result = LuDecomposer.Decompose(A);
            sw.Stop();

            lastResult = result;
            lastMatrixA = A;

            RenderResults(result, A);

            statusLabel.Text = $"Descomposición LU completada con éxito en {sw.ElapsedMilliseconds} ms. A = L · U verificado.";
        }
        catch (DivideByZeroException dbzEx)
        {
            sw.Stop();
            lastResult = null;
            lastMatrixA = A;

            // Limpiar matrices L y U
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    dgvL.Rows[r].Cells[c].Value = "";
                    dgvU.Rows[r].Cells[c].Value = "";
                    dgvVerification.Rows[r].Cells[c].Value = "";
                }
            }

            lblVerificationStatus.Text = "✗ NO SE PUDO FACTORIZAR: " + dbzEx.Message;
            lblVerificationStatus.ForeColor = Color.FromArgb(220, 38, 38);

            txtSteps.Text = $"[ERROR CRÍTICO]\n{dbzEx.Message}\n\n" +
                            "El método de Doolittle estándar no permite continuar cuando un elemento pivote U[i,i] es cero.\n" +
                            "Para factorizar esta matriz, se requiere el algoritmo con pivoteo parcial (PA = LU) mediante intercambio de filas.";

            txtConsole.Text = LuDecomposer.FormatMatrixText(A, "Matriz Original A (Exacta)", DisplayFormat.Fractions, GetDecimals()) +
                              $"\nError de descomposición: {dbzEx.Message}\n";

            MessageBox.Show(
                dbzEx.Message,
                "Pivote Nulo Detectado (ZeroDivisionError)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            statusLabel.Text = "Error: Pivote nulo encontrado. Requiere pivoteo parcial.";
        }
        catch (Exception ex)
        {
            sw.Stop();
            MessageBox.Show(
                $"Ocurrió un error inesperado al descomponer la matriz:\n{ex.Message}",
                "Error en Descomposición",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            statusLabel.Text = $"Error: {ex.Message}";
        }
    }

    private void RenderResults(LuResult result, Fraction[,] A)
    {
        int n = currentDimension;
        DisplayFormat format = GetCurrentDisplayFormat();
        int decimals = GetDecimals();

        // 1. Llenar Matriz L y Matriz U con estilos
        for (int r = 0; r < n; r++)
        {
            for (int c = 0; c < n; c++)
            {
                // Matriz L
                var cellL = dgvL.Rows[r].Cells[c];
                cellL.Value = result.L[r, c].ToDisplayString(format, decimals);

                if (r == c)
                {
                    cellL.Style.BackColor = Color.FromArgb(236, 253, 245); // Verde muy suave para diagonal 1s
                    cellL.Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (r < c)
                {
                    cellL.Style.BackColor = Color.FromArgb(248, 250, 252); // Ceros de L
                    cellL.Style.ForeColor = Color.FromArgb(148, 163, 184);
                }
                else
                {
                    cellL.Style.BackColor = Color.White;
                    cellL.Style.ForeColor = Color.FromArgb(15, 23, 42);
                }

                // Matriz U
                var cellU = dgvU.Rows[r].Cells[c];
                cellU.Value = result.U[r, c].ToDisplayString(format, decimals);

                if (r == c)
                {
                    cellU.Style.BackColor = Color.FromArgb(239, 246, 255); // Azul suave para pivotes
                    cellU.Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (r > c)
                {
                    cellU.Style.BackColor = Color.FromArgb(248, 250, 252); // Ceros de U
                    cellU.Style.ForeColor = Color.FromArgb(148, 163, 184);
                }
                else
                {
                    cellU.Style.BackColor = Color.White;
                    cellU.Style.ForeColor = Color.FromArgb(15, 23, 42);
                }

                // Matriz Verificación L * U
                var cellVerif = dgvVerification.Rows[r].Cells[c];
                cellVerif.Value = result.ProductLU[r, c].ToDisplayString(format, decimals);
                cellVerif.Style.BackColor = (result.ProductLU[r, c] == A[r, c]) 
                    ? Color.FromArgb(240, 253, 244) 
                    : Color.FromArgb(254, 242, 242);
            }
        }

        // 2. Banner de Verificación
        if (result.VerificationPassed)
        {
            lblVerificationStatus.Text = "✓ COMPROBACIÓN EXITOSA: El producto matricial L · U coincide exactamente con la Matriz Original A en todos sus elementos.";
            lblVerificationStatus.ForeColor = Color.FromArgb(22, 101, 52);
        }
        else
        {
            lblVerificationStatus.Text = "✗ ADVERTENCIA: Se detectó una inconsistencia en el producto L · U.";
            lblVerificationStatus.ForeColor = Color.FromArgb(220, 38, 38);
        }

        // 3. Tab Paso a Paso
        var sbSteps = new StringBuilder();
        foreach (string step in result.Steps)
        {
            sbSteps.AppendLine(step);
        }
        txtSteps.Text = sbSteps.ToString();

        // 4. Tab Salida Consola (Idéntica al script Python)
        var sbConsole = new StringBuilder();
        sbConsole.AppendLine(LuDecomposer.FormatMatrixText(A, "Matriz Original A (Exacta)", DisplayFormat.Fractions, decimals));

        if (format == DisplayFormat.Fractions || format == DisplayFormat.Both)
        {
            sbConsole.AppendLine(LuDecomposer.FormatMatrixText(result.L, "Matriz L (Fracciones / Enteros)", DisplayFormat.Fractions, decimals));
            sbConsole.AppendLine(LuDecomposer.FormatMatrixText(result.U, "Matriz U (Fracciones / Enteros)", DisplayFormat.Fractions, decimals));
        }

        if (format == DisplayFormat.Decimals || format == DisplayFormat.Both)
        {
            sbConsole.AppendLine(LuDecomposer.FormatMatrixText(result.L, $"Matriz L (Decimales - {decimals} cifras)", DisplayFormat.Decimals, decimals));
            sbConsole.AppendLine(LuDecomposer.FormatMatrixText(result.U, $"Matriz U (Decimales - {decimals} cifras)", DisplayFormat.Decimals, decimals));
        }

        sbConsole.AppendLine(LuDecomposer.FormatMatrixText(result.ProductLU, "Verificación Producto L * U", format, decimals));
        sbConsole.AppendLine(result.VerificationPassed 
            ? "Resultado: L * U = A (Verificación Matemática Exacta ✓)" 
            : "Resultado: Error en verificación");

        txtConsole.Text = sbConsole.ToString();
    }

    private void BtnCopyConsole_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtConsole.Text))
        {
            MessageBox.Show("No hay datos de salida para copiar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        Clipboard.SetText(txtConsole.Text);
        MessageBox.Show("¡Texto copiado al portapapeles exitosamente!", "Copiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
