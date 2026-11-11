namespace PedidosWeb.Core.Folios;

/// <summary>
/// Genera el folio de un pedido con el mismo formato del sistema legado.
/// Las reglas completas y los ejemplos están en el README, sección «La función del taller».
/// </summary>
public static class FolioFormatter
{
    /// <summary>
    /// Devuelve el folio con el formato PED-{SUCURSAL}-{NUMERO}, por ejemplo "PED-STGO-000123".
    /// </summary>
    /// <param name="numero">Número correlativo del pedido, entre 1 y 999999.</param>
    /// <param name="sucursal">Código de la sucursal: de 2 a 4 letras de la A a la Z.</param>
    /// <exception cref="ArgumentOutOfRangeException">Si el número está fuera de 1..999999.</exception>
    /// <exception cref="ArgumentException">Si la sucursal no tiene de 2 a 4 letras de la A a la Z.</exception>
    public static string FormatearFolio(int numero, string sucursal)
    {
        if (numero is < 1 or > 999999)
            throw new ArgumentOutOfRangeException(nameof(numero), numero,
                "El número debe estar entre 1 y 999999.");

        if (sucursal is null || sucursal.Length is < 2 or > 4 || !sucursal.All(c => c is >= 'A' and <= 'Z'))
            throw new ArgumentException(
                "La sucursal debe tener de 2 a 4 letras de la A a la Z.", nameof(sucursal));

        return $"PED-{sucursal}-{numero:D6}";
    }
}
