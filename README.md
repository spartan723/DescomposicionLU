# Descomposición LU (Método de Doolittle) con Aritmética Exacta de Fracciones

Solución en **C# Windows Forms (.NET 8 / .NET 10)** con formato moderno de solución `.slnx` y clásico `.sln`.

Este proyecto replica y amplía la funcionalidad del script de Python:
- Entrada de matrices cuadradas $n \times n$ (de orden 2 hasta 10).
- Admite entradas exactas de números enteros (`4`), decimales (`0.25`, `-1.5`) y fracciones (`1/3`, `-5/2`).
- Aritmética fraccionaria exacta con `BigInteger` para evitar errores de redondeo o desbordamiento.
- Descomposición $A = L \cdot U$ por el **Método de Doolittle** ($L$ con 1s en la diagonal principal).
- Detección automática de pivote nulo con advertencia de que la matriz requiere pivoteo parcial ($PA = LU$).
- Visualización en **Fracciones exactas**, **Decimales** (con precisión configurable) o **Ambos formatos**.
- Comprobación matemática automática: $L \cdot U = A$.
- Registro detallado paso a paso con fórmulas y sustituciones.
- Salida idéntica a la consola del script de Python con botón de copiado rápido al portapapeles.
- Importación rápida mediante pegado de texto.

---

## 📁 Estructura del Proyecto

```
DescomposicionLU/
├── DescomposicionLU.slnx            <- Archivo de Solución moderno de Visual Studio
├── DescomposicionLU.sln             <- Archivo de Solución clásico de compatibilidad
├── README.md                        <- Documentación
└── DescomposicionLU.WinForms/
    ├── DescomposicionLU.WinForms.csproj
    ├── Program.cs                   <- Punto de entrada y auto-tests
    ├── Form1.cs                     <- Formulario principal con lógica visual
    ├── Form1.Designer.cs            <- Diseño de interfaz de usuario
    ├── Models/
    │   └── Fraction.cs              <- Estructura de fracción exacta y parseo
    ├── Services/
    │   └── LuDecomposer.cs          <- Algoritmo de Doolittle, verificación y formateo
    └── Forms/
        └── TextInputForm.cs         <- Ventana modal para ingresar matrices por texto
```

---

## 🚀 Cómo Ejecutar la Aplicación

### Opción 1: Desde la Terminal (CLI)
Para compilar y ejecutar directamente la aplicación:
```powershell
cd C:\Users\ultra\.gemini\antigravity\scratch\DescomposicionLU
dotnet run --project DescomposicionLU.WinForms\DescomposicionLU.WinForms.csproj
```

Para ejecutar los auto-tests matemáticos en consola:
```powershell
dotnet run --project DescomposicionLU.WinForms\DescomposicionLU.WinForms.csproj -- --test
```

### Opción 2: Desde Visual Studio 2022
1. Abre **Visual Studio 2022** (versión 17.10 o superior para `.slnx`).
2. Abre el archivo:
   `C:\Users\ultra\.gemini\antigravity\scratch\DescomposicionLU\DescomposicionLU.slnx`
   *(Si usas una versión anterior, puedes abrir `DescomposicionLU.sln`)*.
3. Presiona **F5** o haz clic en **Iniciar / Run**.

---

## 📊 Ejemplo de Verificación

Para la matriz:
$$A = \begin{pmatrix} 2 & 1/3 & -0.5 \\ 1 & -2 & 4 \\ -1/2 & 0 & 3 \end{pmatrix}$$

Los resultados exactos calculados por la aplicación son:
- **Matriz $L$**:
  $$\begin{pmatrix} 1 & 0 & 0 \\ 1/2 & 1 & 0 \\ -1/4 & -1/26 & 1 \end{pmatrix}$$
- **Matriz $U$**:
  $$\begin{pmatrix} 2 & 1/3 & -1/2 \\ 0 & -13/6 & 17/4 \\ 0 & 0 & 79/26 \end{pmatrix}$$
- **Verificación**: $L \cdot U = A$ (100% Exacto).
