using PedidosWeb.Core.Lineas;

namespace PedidosWeb.Core.Pedidos;

/// <summary>
/// Arma el detalle de un pedido a partir de sus repositorios (HU-02, incremento 2).
/// </summary>
public sealed class DetallePedidoServicio
{
    private readonly IPedidoRepositorio _pedidos;
    private readonly ILineaPedidoRepositorio _lineas;

    public DetallePedidoServicio(IPedidoRepositorio pedidos, ILineaPedidoRepositorio lineas)
    {
        _pedidos = pedidos ?? throw new ArgumentNullException(nameof(pedidos));
        _lineas = lineas ?? throw new ArgumentNullException(nameof(lineas));
    }

    /// <summary>
    /// El detalle del pedido con ese id, o <c>null</c> si el pedido no existe.
    /// Un pedido sin líneas devuelve lista vacía y total 0.
    /// </summary>
    public DetallePedido? ObtenerDetalle(int id)
    {
        Pedido? pedido = _pedidos.ObtenerPorId(id);
        if (pedido is null)
            return null;

        IReadOnlyList<LineaPedido> lineas = _lineas.ObtenerPorPedido(id);

        List<LineaDetallePedido> lineasDetalle = lineas
            .Select(l => new LineaDetallePedido(
                l.Numero,
                l.Producto,
                l.Cantidad,
                l.PrecioUnitario,
                CalculadoraTotalPedido.CalcularSubtotal(l)))
            .ToList();

        return new DetallePedido(
            pedido.Id,
            pedido.Folio,
            pedido.Fecha,
            pedido.Estado,
            pedido.Sucursal,
            pedido.ClienteId,
            pedido.ClienteNombre,
            lineasDetalle,
            CalculadoraTotalPedido.CalcularTotal(lineas));
    }
}
