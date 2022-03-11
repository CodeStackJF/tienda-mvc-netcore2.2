// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on('submit', '#SetDetalle', function(e){
    e.preventDefault();
    if(!$('#SetEncabezado').valid())
    {
        return false;
    }
    var IdCompra = 0;
    $.ajax({
        type: 'POST',
        url: '/Compras/SetEncabezado/',
        data: $('#SetEncabezado').serialize(),
        success: function(data)
        {
            if(data.estado){
                $('#SetDetalle #Item2_IdCompra').val(data.resultado);
                $('#SetEncabezado #Item1_IdCompra').val(data.resultado);
                $.ajax({
                    type: 'POST',
                    url: '/Compras/SetDetalle/',
                    data: $('#SetDetalle').serialize(),
                    success: function(data)
                    {
                        var tbl = $('#tbl-detalle-compra tbody');
                        var detalle = data.resultado;
                        tbl.append('<tr><td>'+detalle.producto.marca + ' - ' + detalle.producto.modelo+'</td>'+
                                        '<td>'+detalle.producto.descripcion+'</td>'+
                                        '<td>'+detalle.cantidad+'</td>'+
                                        '<td>$'+detalle.precio.toFixed(2)+'</td>'+
                                        '<td>$'+detalle.iva.toFixed(2)+'</td>'+
                                        '<td>$'+detalle.total.toFixed(2)+'</td>'+
                                        '</tr>');
                    }
                });
            }
            else
            {
                alert(data.mensaje);
            }
        }
    });
})