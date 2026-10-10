using System;
using System.Collections.Generic;
using System.Text;
using DescomposicionLU.WinForms.Models;

namespace DescomposicionLU.WinForms.Services;

public class LuResult
{
    public required Fraction[,] L { get; init; }
    public required Fraction[,] U { get; init; }
    public required Fraction[,] A { get; init; }
    public required Fraction[,] ProductLU { get; init; }
    public required bool VerificationPassed { get; init; }
    public List<string> Steps { get; init; } = new();
}

public static class LuDecomposer
{
    /// <summary>
    /// Calcula la descomposición LU por el Método de Doolittle con aritmética fraccionaria exacta:
    /// A = L * U, donde L tiene 1s en la diagonal principal.
    /// </summary>
    public static LuResult Decompose(Fraction[,] A)
    {
        int n = A.GetLength(0);
        if (n != A.GetLength(1))
            throw new ArgumentException("La matriz debe ser cuadrada (n x n).");

        Fraction[,] L = new Fraction[n, n];
        Fraction[,] U = new Fraction[n, n];

        // Inicializar L con diagonal de 1 y U con 0
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                L[i, j] = (i == j) ? Fraction.One : Fraction.Zero;
                U[i, j] = Fraction.Zero;
            }
        }

        List<string> steps = new();
        steps.Add($"=== INICIO DE DESCOMPOSICIÓN LU (MÉTODO DE DOOLITTLE) ===");
        steps.Add($"Orden de la matriz: {n}x{n}\n");

        for (int i = 0; i < n; i++)
        {
            steps.Add($"--- ETAPA {i + 1} ---");
            steps.Add($"1. Cálculo de la Fila {i + 1} de la Matriz U (para k = {i + 1} hasta {n}):");

            // 1. Calcular fila i de U
            for (int k = i; k < n; k++)
            {
                Fraction suma = Fraction.Zero;
                StringBuilder sumaStr = new();

                for (int s = 0; s < i; s++)
                {
                    Fraction prod = L[i, s] * U[s, k];
                    suma += prod;
                    if (s > 0) sumaStr.Append(" + ");
                    sumaStr.Append($"({L[i, s]} * {U[s, k]})");
                }

                U[i, k] = A[i, k] - suma;

                if (i > 0)
                {
                    steps.Add($"   U[{i + 1},{k + 1}] = A[{i + 1},{k + 1}] - [{sumaStr}] = {A[i, k]} - ({suma}) = {U[i, k]}");
                }
                else
                {
                    steps.Add($"   U[{i + 1},{k + 1}] = A[{i + 1},{k + 1}] = {U[i, k]}");
                }
            }

            // Validar existencia de pivote nulo
            if (U[i, i].IsZero)
            {
                string msgError = $"Pivote nulo en la posición ({i + 1}, {i + 1}). " +
                                  "Esta matriz requiere pivoteo parcial (PA = LU) o no admite factorización LU de Doolittle.";
                steps.Add($"\n[ERROR CRÍTICO] {msgError}");
                throw new DivideByZeroException(msgError);
            }

            // 2. Calcular columna i de L (para k = i + 1 hasta n - 1)
            if (i < n - 1)
            {
                steps.Add($"\n2. Cálculo de la Columna {i + 1} de la Matriz L (para k = {i + 2} hasta {n}):");
                for (int k = i + 1; k < n; k++)
                {
                    Fraction suma = Fraction.Zero;
                    StringBuilder sumaStr = new();

                    for (int s = 0; s < i; s++)
                    {
                        Fraction prod = L[k, s] * U[s, i];
                        suma += prod;
                        if (s > 0) sumaStr.Append(" + ");
                        sumaStr.Append($"({L[k, s]} * {U[s, i]})");
                    }

                    Fraction numerador = A[k, i] - suma;
                    L[k, i] = numerador / U[i, i];

                    if (i > 0)
                    {
                        steps.Add($"   L[{k + 1},{i + 1}] = (A[{k + 1},{i + 1}] - [{sumaStr}]) / U[{i + 1},{i + 1}] = ({A[k, i]} - {suma}) / {U[i, i]} = {numerador} / {U[i, i]} = {L[k, i]}");
                    }
                    else
                    {
                        steps.Add($"   L[{k + 1},{i + 1}] = A[{k + 1},{i + 1}] / U[{i + 1},{i + 1}] = {A[k, i]} / {U[i, i]} = {L[k, i]}");
                    }
                }
            }

            steps.Add("");
        }

        // Verificación: Calcular producto L * U
        Fraction[,] product = Multiply(L, U);
        bool verified = true;
        for (int r = 0; r < n; r++)
        {
            for (int c = 0; c < n; c++)
            {
                if (product[r, c] != A[r, c])
                {
                    verified = false;
                    break;
                }
            }
        }

        steps.Add($"=== VERIFICACIÓN MATEMÁTICA L * U ===");
        steps.Add(verified 
            ? "✓ COMPROBACIÓN EXITOSA: El producto L * U coincide exactamente con la matriz original A." 
            : "✗ ADVERTENCIA: Se detectó discrepancia en el producto L * U.");

        return new LuResult
        {
            L = L,
            U = U,
            A = A,
            ProductLU = product,
            VerificationPassed = verified,
            Steps = steps
        };
    }

    public static Fraction[,] Multiply(Fraction[,] X, Fraction[,] Y)
    {
        int n = X.GetLength(0);
        Fraction[,] result = new Fraction[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Fraction sum = Fraction.Zero;
                for (int k = 0; k < n; k++)
                {
                    sum += X[i, k] * Y[k, j];
                }
                result[i, j] = sum;
            }
        }

        return result;
    }

    /// <summary>
    /// Genera la representación en texto formateado de la matriz idéntico a print_matrix del script en Python.
    /// </summary>
    public static string FormatMatrixText(Fraction[,] M, string label, DisplayFormat format, int decimals = 4)
    {
        int n = M.GetLength(0);
        string[,] cells = new string[n, n];
        int[] colWidths = new int[n];

        for (int c = 0; c < n; c++)
        {
            int maxW = 0;
            for (int r = 0; r < n; r++)
            {
                string cellStr = M[r, c].ToDisplayString(format, decimals);
                cells[r, c] = cellStr;
                if (cellStr.Length > maxW)
                    maxW = cellStr.Length;
            }
            colWidths[c] = Math.Max(maxW, 3);
        }

        StringBuilder sb = new();
        sb.AppendLine($"--- {label} ---");

        for (int r = 0; r < n; r++)
        {
            sb.Append("[ ");
            for (int c = 0; c < n; c++)
            {
                sb.Append(cells[r, c].PadLeft(colWidths[c]));
                if (c < n - 1)
                    sb.Append("  ");
            }
            sb.AppendLine(" ]");
        }

        return sb.ToString();
    }
}
