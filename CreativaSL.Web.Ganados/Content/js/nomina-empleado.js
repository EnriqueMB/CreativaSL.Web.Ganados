var NominaEmpleado = function () {
    "use strict";
    // Funcion para validar registrar

    var runCombos = function () {

        $('#IDSucursal').on('change', function (event) {
            $("#IDTabla  tr").remove();
            getDatostablaEmpleado($("#IDSucursal").val());
        });
        // $("#IDPuesto").trigger('change');
        //$("#IDPuesto").change(function () {
        //    $("#IDCategoriaPuesto option").remove();
        //    getDatosRegimen($("#IDPuesto").val());
        //});
        function getDatostablaEmpleado(IDSucursal) {
            $.ajax({
                url: "/Admin/Nomina/DatostablaEmpleado",
                data: { IDS: IDSucursal },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "1");
                },
                success: function (result) {
                    var Numero = 0;
                    for (var i = 0; i < result.length; i++) {
                        Numero = i;
                        $("#IDTabla").append('<tr><td><input checked="checked" id="ListaEmpleados_' + Numero + '.AbrirCaja" name="ListaEmpleados[' + Numero + '].AbrirCaja" type="checkbox" value="true" /><input name="ListaEmpleados[' + Numero + '].AbrirCaja" type="hidden" value="false" /> </td>' +
                            ' <td>' + result[i].CodigoUsuario + '</td>'+
                            '<td>' + result[i].NombreEmpleado + '</td>' +
                            '<td>' + result[i].Puesto + '</td>' +
                            ' <td>' + result[i].CategoriaPuesto + '</td>'+
                            '<td>' + result[i].Sueldo + '</td>'+
                            '<td>' + result[i].Percepciones + '</td>'+
                            '<td>' + result[i].Deducciones + '</td>'+
                            '<td style="display:none"><input id="ListaEmpleados_' + Numero + '.IDEmpleado" name="ListaEmpleados[' + Numero + '].IDEmpleado" type="text" value="' + result[i].IDEmpleado + '" /></td></tr>');
                    }
                    //$('#IDCategoriaPuesto.select').selectpicker('refresh');
                }
            });
        }
    };

    return {
        //main function to initiate template pages
        init: function () {
            runCombos();
        }
    };
}();