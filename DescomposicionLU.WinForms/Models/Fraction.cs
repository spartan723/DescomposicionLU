using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace DescomposicionLU.WinForms.Models;

/// <summary>
/// Representa un número racional exacto (fracción) con numerador y denominador
/// utilizando BigInteger para evitar desbordamientos aritméticos.
/// </summary>
public readonly struct Fraction : IEquatable<Fraction>, IComparable<Fraction>
{
    public BigInteger Numerator { get; }
    public BigInteger Denominator { get; }

    public static readonly Fraction Zero = new(BigInteger.Zero, BigInteger.One);
    public static readonly Fraction One = new(BigInteger.One, BigInteger.One);

    public Fraction(BigInteger numerator, BigInteger denominator)
    {
        if (denominator == BigInteger.Zero)
            throw new DivideByZeroException("El denominador no puede ser cero.");

        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }

        if (numerator == BigInteger.Zero)
        {
            Numerator = BigInteger.Zero;
            Denominator = BigInteger.One;
        }
        else
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(BigInteger.Abs(numerator), denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }
    }

    public Fraction(long numerator, long denominator = 1) 
        : this(new BigInteger(numerator), new BigInteger(denominator))
    {
    }

    public bool IsZero => Numerator == BigInteger.Zero;
    public bool IsOne => Numerator == BigInteger.One && Denominator == BigInteger.One;
    public bool IsInteger => Denominator == BigInteger.One;

    /// <summary>
    /// Convierte una cadena con entero ('4'), decimal ('0.25', '0,25') o fracción ('1/3', '-5/2')
    /// a un objeto Fraction exacto, idéntico a Python fractions.Fraction(val_str).
    /// </summary>
    public static Fraction Parse(string valStr)
    {
        if (string.IsNullOrWhiteSpace(valStr))
            throw new FormatException("La cadena de entrada no puede estar vacía.");

        string s = valStr.Trim().Replace(" ", "");

        // 1. Caso Fracción: "a/b"
        if (s.Contains('/'))
        {
            string[] parts = s.Split('/');
            if (parts.Length != 2)
                throw new FormatException($"Formato de fracción inválido: '{valStr}'.");

            if (!BigInteger.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger num))
                throw new FormatException($"Numerador inválido: '{parts[0]}'.");

            if (!BigInteger.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger den))
                throw new FormatException($"Denominador inválido: '{parts[1]}'.");

            if (den == BigInteger.Zero)
                throw new DivideByZeroException($"División por cero en la fracción: '{valStr}'.");

            return new Fraction(num, den);
        }

        // Normalizar coma a punto decimal
        s = s.Replace(',', '.');

        // 2. Caso Decimal: "0.25", "-1.5", ".5", "3."
        if (s.Contains('.'))
        {
            // Permitir notación científica simple si existe: ej. 1.5e-2
            if (s.Contains('e') || s.Contains('E'))
            {
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double dVal))
                {
                    return FromDouble(dVal);
                }
                throw new FormatException($"Valor con exponente inválido: '{valStr}'.");
            }

            int dotIndex = s.IndexOf('.');
            string intPart = s.Substring(0, dotIndex);
            string decPart = s.Substring(dotIndex + 1);

            bool isNegative = false;
            if (intPart.StartsWith("-"))
            {
                isNegative = true;
                intPart = intPart.Substring(1);
            }
            else if (intPart.StartsWith("+"))
            {
                intPart = intPart.Substring(1);
            }

            if (string.IsNullOrEmpty(intPart)) intPart = "0";
            if (string.IsNullOrEmpty(decPart)) decPart = "0";

            if (!BigInteger.TryParse(intPart, NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger intNum))
                throw new FormatException($"Parte entera inválida en '{valStr}'.");

            // Quitar ceros a la derecha de decPart
            string decTrimmed = decPart.TrimEnd('0');
            if (string.IsNullOrEmpty(decTrimmed))
            {
                BigInteger res = isNegative ? -intNum : intNum;
                return new Fraction(res, BigInteger.One);
            }

            int scale = decTrimmed.Length;
            BigInteger powerOf10 = BigInteger.Pow(10, scale);
            if (!BigInteger.TryParse(decTrimmed, NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger decNum))
                throw new FormatException($"Parte decimal inválida en '{valStr}'.");

            BigInteger fullNumerator = (intNum * powerOf10) + decNum;
            if (isNegative) fullNumerator = -fullNumerator;

            return new Fraction(fullNumerator, powerOf10);
        }

        // 3. Caso Entero
        if (BigInteger.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger intVal))
        {
            return new Fraction(intVal, BigInteger.One);
        }

        throw new FormatException($"El valor '{valStr}' no es un entero, decimal o fracción válida.");
    }

    public static bool TryParse(string valStr, out Fraction result)
    {
        try
        {
            result = Parse(valStr);
            return true;
        }
        catch
        {
            result = Zero;
            return false;
        }
    }

    public static Fraction FromDouble(double value, int maxDenominator = 1000000)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentException("No se puede convertir NaN o Infinito a Fracción.");

        // Aproximación por fracciones continuas
        double sign = value < 0 ? -1 : 1;
        value = Math.Abs(value);

        long m00 = 1, m01 = 0, m10 = 0, m11 = 1;
        double x = value;

        while (m10 * (long)x + m11 <= maxDenominator)
        {
            long a = (long)x;
            long t;

            t = m00 * a + m01; m01 = m00; m00 = t;
            t = m10 * a + m11; m11 = m10; m10 = t;

            if (Math.Abs(x - a) < 1e-12)
                break;

            x = 1.0 / (x - a);
        }

        return new Fraction((long)(sign * m00), m10);
    }

    // Operadores aritméticos
    public static Fraction operator +(Fraction a, Fraction b)
        => new((a.Numerator * b.Denominator) + (b.Numerator * a.Denominator), a.Denominator * b.Denominator);

    public static Fraction operator -(Fraction a, Fraction b)
        => new((a.Numerator * b.Denominator) - (b.Numerator * a.Denominator), a.Denominator * b.Denominator);

    public static Fraction operator *(Fraction a, Fraction b)
        => new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.Numerator == BigInteger.Zero)
            throw new DivideByZeroException("División por una fracción con valor cero.");

        return new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
    }

    public static Fraction operator -(Fraction a) => new(-a.Numerator, a.Denominator);
    public static Fraction operator +(Fraction a) => a;

    public static bool operator ==(Fraction a, Fraction b) => a.Equals(b);
    public static bool operator !=(Fraction a, Fraction b) => !a.Equals(b);
    public static bool operator <(Fraction a, Fraction b) => a.CompareTo(b) < 0;
    public static bool operator <=(Fraction a, Fraction b) => a.CompareTo(b) <= 0;
    public static bool operator >(Fraction a, Fraction b) => a.CompareTo(b) > 0;
    public static bool operator >=(Fraction a, Fraction b) => a.CompareTo(b) >= 0;

    public static implicit operator Fraction(int value) => new(value, 1);
    public static implicit operator Fraction(long value) => new(value, 1);

    public double ToDouble() => (double)Numerator / (double)Denominator;

    public string ToDecimalString(int decimals = 4)
    {
        double val = ToDouble();
        string format = "0." + new string('#', Math.Max(0, decimals));
        if (decimals == 0) format = "0";
        // Si no tiene decimales suficientes, mostrar número exacto o con padding
        string res = val.ToString($"F{Math.Max(0, decimals)}", CultureInfo.InvariantCulture);
        // Si termina con .0000 innecesario cuando decimals > 0 y es entero, podemos conservarlo
        return res;
    }

    public override string ToString()
    {
        if (Numerator == BigInteger.Zero)
            return "0";
        if (Denominator == BigInteger.One)
            return Numerator.ToString();

        return $"{Numerator}/{Denominator}";
    }

    public string ToDisplayString(DisplayFormat format, int decimals = 4)
    {
        return format switch
        {
            DisplayFormat.Fractions => ToString(),
            DisplayFormat.Decimals => ToDecimalString(decimals),
            DisplayFormat.Both => IsInteger 
                ? ToString() 
                : $"{ToString()} ({ToDecimalString(decimals)})",
            _ => ToString()
        };
    }

    public bool Equals(Fraction other) => Numerator == other.Numerator && Denominator == other.Denominator;

    public override bool Equals(object? obj) => obj is Fraction f && Equals(f);

    public override int GetHashCode() => HashCode.Combine(Numerator, Denominator);

    public int CompareTo(Fraction other)
    {
        BigInteger diff = (Numerator * other.Denominator) - (other.Numerator * Denominator);
        return diff.Sign;
    }
}

public enum DisplayFormat
{
    Fractions = 1,
    Decimals = 2,
    Both = 3
}
