using PedidosWeb.Core.Lineas;

namespace PedidosWeb.Core.Pedidos;

/// <summary>
/// Calcula subtotales y total de un pedido a partir de sus líneas (HU-02, incremento 1).
/// Montos en pesos chilenos (CLP), sin IVA: <c>decimal</c> sin decimales.
/// </summary>
public static class CalculadoraTotalPedido
{
    /// <summary>Subtotal de una línea: <c>Cantidad × PrecioUnitario</c>.</summary>
    /// <exception cref="ArgumentNullException">Si <paramref name="linea"/> es nula.</exception>
    public static decimal CalcularSubtotal(LineaPedido linea)
    {
        ArgumentNullException.ThrowIfNull(linea);
        return linea.Cantidad * linea.PrecioUnitario;
    }

    /// <summary>Total del pedido: suma de los subtotales de sus líneas. Sin líneas, el total es 0.</summary>
    /// <exception cref="ArgumentNullException">Si <paramref name="lineas"/> es nula.</exception>
    public static decimal CalcularTotal(IEnumerable<LineaPedido> lineas)
    {
        ArgumentNullException.ThrowIfNull(lineas);
        return lineas.Sum(CalcularSubtotal);
    }
}
