using System;
using System.Drawing;
using System.Windows.Forms;
using DescomposicionLU.WinForms.Models;

namespace DescomposicionLU.WinForms.Forms;

public class TextInputForm : Form
{
    private TextBox txtInput = null!;
    private Button btnAccept = null!;
    private Button btnCancel = null!;
    private Label lblInstructions = null!;

    public Fraction[,]? ParsedMatrix { get; private set; }
    public int DetectedDimension { get; private set; }

    public TextInputForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Ingresar Matriz por Texto";
        Size = new Size(580, 420);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

        lblInstructions = new Label
        {
            Text = "Ingrese o pegue cada fila de la matriz en una línea separada.\n" +
                   "Separe los elementos de cada fila por espacios o tabulaciones.\n" +
                   "Valores admitidos: Enteros ('4'), Decimales ('0.25', '-1.5') o Fracciones ('1/3', '-5/2').",
            Location = new Point(20, 15),
            Size = new Size(525, 60),
            ForeColor = Color.FromArgb(51, 65, 85)
        };

        txtInput = new TextBox
        {
            Multiline = true,
            ScrollBars = ScrollBars.Both,
            Location = new Point(20, 85),
            Size = new Size(525, 220),
            Font = new Font("Consolas", 10.5f, FontStyle.Regular),
            Text = "2 1/3 -0.5\r\n1 -2 4\r\n-1/2 0 3"
        };

        btnAccept = new Button
        {
            Text = "Cargar Matriz",
            Location = new Point(310, 325),
            Size = new Size(115, 36),
            BackColor = Color.FromArgb(37, 99, 235),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnAccept.FlatAppearance.BorderSize = 0;
        btnAccept.Click += BtnAccept_Click;

        btnCancel = new Button
        {
            Text = "Cancelar",
            Location = new Point(435, 325),
            Size = new Size(110, 36),
            BackColor = Color.FromArgb(241, 245, 249),
            ForeColor = Color.FromArgb(51, 65, 85),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        Controls.Add(lblInstructions);
        Controls.Add(txtInput);
        Controls.Add(btnAccept);
        Controls.Add(btnCancel);

        AcceptButton = btnAccept;
        CancelButton = btnCancel;
    }

    private void BtnAccept_Click(object? sender, EventArgs e)
    {
        string text = txtInput.Text.Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            MessageBox.Show("Por favor ingrese al menos una fila de valores.", "Entrada Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        int rows = lines.Length;

        if (rows < 2)
        {
            MessageBox.Show("La matriz debe tener al menos orden 2x2.", "Dimensión Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Fraction[,] matrix = new Fraction[rows, rows];

        for (int r = 0; r < rows; r++)
        {
            string[] items = lines[r].Trim().Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length != rows)
            {
                MessageBox.Show(
                    $"Error en la Fila {r + 1}: Debe contener exactamente {rows} elementos para ser una matriz cuadrada {rows}x{rows}.\n" +
                    $"Se detectaron {items.Length} elementos.",
                    "Error de Formato de Matriz",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            for (int c = 0; c < rows; c++)
            {
                try
                {
                    matrix[r, c] = Fraction.Parse(items[c]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error al interpretar el valor '{items[c]}' en la posición ({r + 1}, {c + 1}):\n{ex.Message}",
                        "Error de Parseo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
        }

        ParsedMatrix = matrix;
        DetectedDimension = rows;
        DialogResult = DialogResult.OK;
        Close();
    }
}
