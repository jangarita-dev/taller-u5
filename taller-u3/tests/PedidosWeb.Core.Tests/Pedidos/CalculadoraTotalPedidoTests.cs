using PedidosWeb.Core.Lineas;
using PedidosWeb.Core.Pedidos;

namespace PedidosWeb.Core.Tests.Pedidos;

public class CalculadoraTotalPedidoTests
{
    // CA-01
    [Fact]
    public void CalcularTotal_LineasDelPedido8_Devuelve120100()
    {
        var lineas = new LineaPedidoRepositorioEnMemoria().ObtenerPorPedido(8);

        var total = CalculadoraTotalPedido.CalcularTotal(lineas);

        Assert.Equal(120100m, total);
    }

    // CA-02
    [Fact]
    public void CalcularSubtotal_12Por2950_Devuelve35400()
    {
        var linea = new LineaPedido(8, 2, "Mantequilla 250 g", 12, 2950m);

        var subtotal = CalculadoraTotalPedido.CalcularSubtotal(linea);

        Assert.Equal(35400m, subtotal);
    }

    // CA-03
    [Fact]
    public void CalcularTotal_ListaVacia_DevuelveCero()
    {
        var total = CalculadoraTotalPedido.CalcularTotal(Array.Empty<LineaPedido>());

        Assert.Equal(0m, total);
    }
}
