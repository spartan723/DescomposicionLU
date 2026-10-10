using System;
using DescomposicionLU.WinForms.Models;
using DescomposicionLU.WinForms.Services;

namespace DescomposicionLU.WinForms;

static class Program
{
    [STAThread]
    static int Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--test")
        {
            return RunSelfTests();
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
        return 0;
    }

    private static int RunSelfTests()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("  EJECUTANDO TESTS DE VERIFICACIÓN LU   ");
        Console.WriteLine("========================================");

        try
        {
            // Test 1: Parseo de fracciones, enteros y decimales
            Console.WriteLine("[1] Probando parse_value...");
            var f1 = Fraction.Parse("4");
            var f2 = Fraction.Parse("0.25");
            var f3 = Fraction.Parse("1/3");
            var f4 = Fraction.Parse("-5/2");
            var f5 = Fraction.Parse("-0.5");

            if (f1 != new Fraction(4, 1)) throw new Exception("Error en parse('4')");
            if (f2 != new Fraction(1, 4)) throw new Exception("Error en parse('0.25')");
            if (f3 != new Fraction(1, 3)) throw new Exception("Error en parse('1/3')");
            if (f4 != new Fraction(-5, 2)) throw new Exception("Error en parse('-5/2')");
            if (f5 != new Fraction(-1, 2)) throw new Exception("Error en parse('-0.5')");
            Console.WriteLine("    ✓ Parseo correcto: '4'->4, '0.25'->1/4, '1/3'->1/3, '-5/2'->-5/2, '-0.5'->-1/2");

            // Test 2: Descomposición LU de la matriz de prueba
            Console.WriteLine("\n[2] Probando Descomposición LU (3x3 con fracciones)...");
            Fraction[,] A = new Fraction[3, 3]
            {
                { Fraction.Parse("2"), Fraction.Parse("1/3"), Fraction.Parse("-0.5") },
                { Fraction.Parse("1"), Fraction.Parse("-2"), Fraction.Parse("4") },
                { Fraction.Parse("-1/2"), Fraction.Parse("0"), Fraction.Parse("3") }
            };

            var res = LuDecomposer.Decompose(A);

            // Verificar L
            if (res.L[0, 0] != 1 || res.L[0, 1] != 0 || res.L[0, 2] != 0) throw new Exception("L fila 0 incorrecta");
            if (res.L[1, 0] != new Fraction(1, 2) || res.L[1, 1] != 1 || res.L[1, 2] != 0) throw new Exception("L fila 1 incorrecta");
            if (res.L[2, 0] != new Fraction(-1, 4) || res.L[2, 1] != new Fraction(-1, 26) || res.L[2, 2] != 1) throw new Exception("L fila 2 incorrecta");

            // Verificar U
            if (res.U[0, 0] != 2 || res.U[0, 1] != new Fraction(1, 3) || res.U[0, 2] != new Fraction(-1, 2)) throw new Exception("U fila 0 incorrecta");
            if (res.U[1, 0] != 0 || res.U[1, 1] != new Fraction(-13, 6) || res.U[1, 2] != new Fraction(17, 4)) throw new Exception("U fila 1 incorrecta");
            if (res.U[2, 0] != 0 || res.U[2, 1] != 0 || res.U[2, 2] != new Fraction(79, 26)) throw new Exception("U fila 2 incorrecta");

            // Verificar producto L * U == A
            if (!res.VerificationPassed) throw new Exception("Fallo en verificación L * U == A");
            Console.WriteLine("    ✓ Matriz L calculada con exactitud absoluta.");
            Console.WriteLine("    ✓ Matriz U calculada con exactitud absoluta.");
            Console.WriteLine("    ✓ Verificación L * U = A confirmada.");

            // Test 3: Formateo de texto
            Console.WriteLine("\n[3] Probando formateo de consola...");
            string textOut = LuDecomposer.FormatMatrixText(res.L, "Matriz L (Fracciones)", DisplayFormat.Fractions);
            Console.WriteLine(textOut);

            // Test 4: Manejo de pivote nulo
            Console.WriteLine("[4] Probando detección de pivote nulo...");
            Fraction[,] singular = new Fraction[3, 3]
            {
                { Fraction.Parse("0"), Fraction.Parse("1"), Fraction.Parse("2") },
                { Fraction.Parse("1"), Fraction.Parse("2"), Fraction.Parse("3") },
                { Fraction.Parse("2"), Fraction.Parse("3"), Fraction.Parse("4") }
            };

            bool caughtDbz = false;
            try
            {
                LuDecomposer.Decompose(singular);
            }
            catch (DivideByZeroException dbz)
            {
                caughtDbz = true;
                Console.WriteLine($"    ✓ Excepción capturada exitosamente: {dbz.Message}");
            }

            if (!caughtDbz) throw new Exception("No se detectó el pivote nulo.");

            // Test 5: Instanciación e inicialización de Form1
            Console.WriteLine("[5] Probando instanciación de Form1 y sus componentes...");
            using (var form = new Form1())
            {
                var handle = form.Handle;
                Console.WriteLine("    ✓ Form1 inicializado y controles cargados correctamente.");
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("  TODOS LOS TESTS PASARON EXITOSAMENTE  ");
            Console.WriteLine("========================================");
            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR EN TEST]: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }
}