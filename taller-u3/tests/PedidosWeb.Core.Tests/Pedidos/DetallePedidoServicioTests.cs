using PedidosWeb.Core.Lineas;
using PedidosWeb.Core.Pedidos;

namespace PedidosWeb.Core.Tests.Pedidos;

public class DetallePedidoServicioTests
{
    private readonly PedidoRepositorioEnMemoria _pedidos = new();
    private readonly DetallePedidoServicio _servicio;

    public DetallePedidoServicioTests()
    {
        _servicio = new DetallePedidoServicio(_pedidos, new LineaPedidoRepositorioEnMemoria());
    }

    // CA-04
    [Fact]
    public void ObtenerDetalle_Pedido8_DevuelveDatosYTresLineasEnOrden()
    {
        var detalle = _servicio.ObtenerDetalle(8);

        Assert.NotNull(detalle);
        Assert.Equal("PED-STGO-000008", detalle.Folio);
        Assert.Equal("C-001", detalle.ClienteId);
        Assert.Equal("Ferretería El Clavo", detalle.ClienteNombre);
        Assert.Equal("Entregado", detalle.Estado);
        Assert.Equal(new[] { 1, 2, 3 }, detalle.Lineas.Select(l => l.Numero));
    }

    // CA-05
    [Fact]
    public void ObtenerDetalle_Los45Pedidos_TotalCalculadoIgualAlGuardado()
    {
        var diferencias = _pedidos.ObtenerTodos()
            .Where(p => _servicio.ObtenerDetalle(p.Id)!.Total != p.Total)
            .Select(p => p.Id)
            .ToList();

        Assert.Equal(45, _pedidos.ObtenerTodos().Count);
        Assert.Empty(diferencias);
    }

    // CA-06
    [Fact]
    public void ObtenerDetalle_IdInexistente_DevuelveNull()
    {
        var detalle = _servicio.ObtenerDetalle(999);

        Assert.Null(detalle);
    }
}
