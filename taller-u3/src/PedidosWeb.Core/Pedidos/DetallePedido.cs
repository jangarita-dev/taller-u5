namespace PedidosWeb.Core.Pedidos;

/// <summary>
/// Detalle de un pedido para el vendedor: datos del pedido, su cliente, sus líneas y su total (HU-02).
/// </summary>
/// <param name="Id">Número correlativo del pedido.</param>
/// <param name="Folio">Folio con formato PED-{SUCURSAL}-{NUMERO}.</param>
/// <param name="Fecha">Fecha en que se emitió el pedido.</param>
/// <param name="Estado">Estado del pedido como texto, tal como lo guarda el legado.</param>
/// <param name="Sucursal">Código de la sucursal que emitió el pedido.</param>
/// <param name="ClienteId">Código del cliente, tomado del pedido.</param>
/// <param name="ClienteNombre">Nombre del cliente, tomado del pedido.</param>
/// <param name="Lineas">Líneas del pedido, ordenadas por número.</param>
/// <param name="Total">Suma de los subtotales de las líneas, en CLP sin IVA.</param>
public sealed record DetallePedido(
    int Id,
    string Folio,
    DateOnly Fecha,
    string Estado,
    string Sucursal,
    string ClienteId,
    string ClienteNombre,
    IReadOnlyList<LineaDetallePedido> Lineas,
    decimal Total);
