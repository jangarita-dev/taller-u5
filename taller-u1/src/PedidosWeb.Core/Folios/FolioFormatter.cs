namespace PedidosWeb.Core.Folios;

/// <summary>
/// Genera el folio de un pedido con el mismo formato del sistema legado.
/// Las reglas completas y los ejemplos están en el README, sección «La función del taller».
/// </summary>
public static class FolioFormatter
{
    private const int NumeroMinimo = 1;
    private const int NumeroMaximo = 999999;
    private const int LargoMinimoSucursal = 2;
    private const int LargoMaximoSucursal = 4;

    /// <summary>
    /// Devuelve el folio con el formato PED-{SUCURSAL}-{NUMERO}, por ejemplo "PED-STGO-000123".
    /// </summary>
    /// <param name="numero">Número correlativo del pedido, entre 1 y 999999.</param>
    /// <param name="sucursal">Código de la sucursal: de 2 a 4 letras de la A a la Z.</param>
    /// <exception cref="ArgumentOutOfRangeException">Si <paramref name="numero"/> está fuera de rango (R1).</exception>
    /// <exception cref="ArgumentException">Si <paramref name="sucursal"/> es nula, vacía o no cumple R3.</exception>
    public static string FormatearFolio(int numero, string sucursal)
    {
        // R5: el número se valida antes que la sucursal.
        // R1: el número debe estar entre 1 y 999999, ambos incluidos.
        if (numero < NumeroMinimo || numero > NumeroMaximo)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numero), numero, $"El número debe estar entre {NumeroMinimo} y {NumeroMaximo}.");
        }

        var codigo = NormalizarSucursal(sucursal);

        // R4: el número se completa con ceros a la izquierda hasta 6 dígitos.
        return $"PED-{codigo}-{numero:D6}";
    }

    private static string NormalizarSucursal(string sucursal)
    {
        // R3: nula o vacía es inválida.
        ArgumentNullException.ThrowIfNull(sucursal);

        // R2: se quitan los espacios de los extremos y se pasa a mayúsculas.
        // ToUpperInvariant evita que el resultado dependa de la cultura del equipo.
        var codigo = sucursal.Trim().ToUpperInvariant();

        // R3: entre 2 y 4 letras ASCII de la A a la Z (sin Ñ, tildes, números ni espacios internos).
        if (codigo.Length < LargoMinimoSucursal
            || codigo.Length > LargoMaximoSucursal
            || !codigo.All(c => c is >= 'A' and <= 'Z'))
        {
            throw new ArgumentException(
                $"La sucursal debe tener entre {LargoMinimoSucursal} y {LargoMaximoSucursal} letras de la A a la Z.",
                nameof(sucursal));
        }

        return codigo;
    }
}
