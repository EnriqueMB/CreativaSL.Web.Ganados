var VentaGeneral = function () {
    "use strict";
    var tbl1;
    var nNodes;
    var ITEMS_FILA = 6;
    // Funcion para validar registrar
    var eventos = function () {

        tbl1 = $('#tbl1').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "searching": false,
            "ordering": false,
            "autoWidth": false,
            "columnDefs": [
                {
                    "targets": [0, 1],
                    "visible": false,
                    "searchable": false
                }
            ],
            "drawCallback": function (settings) {
                $(".cMoney").maskMoney(
                    {
                        allowZero: true,
                        precision: 2,
                        prefix: '$ '
                    }
                );
                $(".cCantidad").maskMoney(
                    {
                        allowZero: true,
                        precision: 2
                    }
                );
            }
        });

        $('#div1').hide(0);
        $('#div2').hide(0);
        $('#div3').hide(0);
        $('#div4').hide(0);

        $("#TipoProducto").on("change", function () {
            var opcion = $(this).val();
            if (opcion === "1") {
                //los tipo 1, equivale a producto que hay en almacen por lo que hay que obtener los almacenes activos
                GetAlmacenes();
                $('#div2').hide(0);
                $('#div3').hide(0);
                $('#div1').show(1000);

            }
            else if (opcion === "2") {
                GetVehiculosXIDEmpresa();
                $('#div1').hide(0);
                $('#div3').hide(0);
                $('#div2').show(1000);
            }
            else if (opcion === "0") {
                $('#div1').hide(0);
                $('#div2').hide(0);
                $('#div3').hide(0);
                Ocultar_Mostrar_Boton();
            }

        });

        $("#Almacenes").on("change", function () {
            var opcion = $(this).val();
            GetProductos_Almacen(opcion);
            $('#div2').show(1000);
        });

        $("#Producto").on("change", function () {
            var opcion = $(this).val();
            var opcionTipoProducto = $("#TipoProducto").val();

            if (opcion !== '0') {
                if (opcionTipoProducto === "1") {
                    var existencia = $(this).children(":selected").attr("data-existencia");
                    var unidadmedida = $(this).children(":selected").attr("data-unidadmedida");
                    $("#cantidadExistencia").val(existencia + " " + unidadmedida);

                    $('#div3').show(1000);
                }
                else {
                    $('#div3').hide(0);
                }
            }
            else {
                $('#div3').hide(0);
            }

            Ocultar_Mostrar_Boton();
        });

        $("#btnAddProducto").on("click", function () {
            var id_producto = $("#Producto").val();

            if (ValidarProducto(id_producto)) {
                var id_tipoProducto = $("#TipoProducto").val();
                var producto = "";
                var cantidadExistencia = "";
                var almacen = "";
                var id_unidadProducto = "";

                if ($("#Producto option:selected")) {
                    producto = $("#Producto option:selected").text();
                    cantidadExistencia = $("#Producto").children(":selected").attr("data-existencia");
                }

                var readonlyCantidad = "";
                var precioUnitario = "0.00";
                var cantidad = "1.00";
                var cClass = "";

                if (id_tipoProducto === "1") { //producto almacen
                    precioUnitario = $("#Producto").children(":selected").attr("data-preciounidad");
                    precioUnitario = Number.parseFloat(precioUnitario).toFixed(2);
                    cClass = "cCantidad";
                    almacen = $("#Almacenes").val();
                    id_unidadProducto = $("#Producto").children(":selected").attr("data-id_unidadproducto");
                }
                else if (id_tipoProducto === "2") { //vehiculo
                    readonlyCantidad = "readonly = 'readonly'";
                    cClass = "";
                    cantidadExistencia = 1;
                }

                var inputCantidad = "<input type='text' class=' iCantidad " + cClass + "' value='" + cantidad + "' " + readonlyCantidad + " data-cantidadexistencia='" + cantidadExistencia + "' data-almacen='" + almacen + "' data-id_unidadproducto='" + id_unidadProducto + "' data-toggle='tooltip' data-placement='top' title='Cantidad máxima permitida: " + cantidadExistencia + "'>";
                var inputPrecioUnitario = "<input type='text'  class='cMoney' value='" + precioUnitario + "'>";
                var inputSubtotal = "<input type='text' class='' value='$ " + format(Number.parseFloat(cantidad) * Number.parseFloat(precioUnitario), 2) + "' readonly='readonly'>";

                var eliminar = '<div class="visible-md visible-lg hidden-sm hidden-xs">' +
                    '<a title="Eliminar" class="btn btn-danger tooltips btn-sm delete" data-toggle="tooltip" data-placement="top" data-original-title="Eliminar"><i class="fa fa-trash-o"></i></a>' +
                    '</div>' +
                    '<div class="visible-xs visible-sm hidden-md hidden-lg">' +
                    '<div class="btn-group">' +
                    '<a class="btn btn-danger dropdown-toggle btn-sm" data-toggle="dropdown" href="#"' +
                    '<i class="fa fa-cog"></i> <span class="caret"></span>' +
                    '</a>' +
                    '<ul role="menu" class="dropdown-menu pull-right dropdown-dark">' +
                    '<li>' +
                    '<a class="delete" role="menuitem" tabindex="-1">' +
                    '<i class="fa fa-trash-o"></i> Eliminar' +
                    '</a>' +
                    '</li>' +
                    '</ul>' +
                    '</div>' +
                    '</div>';

                AddProducto(id_producto, id_tipoProducto, producto, inputCantidad, inputPrecioUnitario, inputSubtotal, eliminar);

                ActualizarSimbolos();

                var total = ObtenerTotalVenta();
                $("#tfooter1").html("Total: $" + total);

            }
            else {
                alert("Este producto ya esta agregado a la venta.");
            }
        });

        $('#tbl1 tbody').on('keyup', 'tr', function () {
            var data = tbl1.row(this).node();
            var cells = $(data).find("td");
            var cantidad = GetNumber($(cells[1]).find("input").val());
            var cantidadExistencia = $(cells[1]).find("input").attr("data-cantidadexistencia");

            if (cantidad <= cantidadExistencia) {
                var precioUnitario = GetMoneySinSimbolo($(cells[2]).find("input").val());
                var subtotal = "$ " + format(cantidad * precioUnitario, 2);
                $(cells[3]).find("input").val(subtotal);
                $(cells[1]).find("input").addClass("successCID");
                $(cells[1]).find("input").removeClass("errorCID");
            }
            else {
                $(cells[1]).find("input").addClass("errorCID");
                $(cells[1]).find("input").removeClass("successCID");
            }

            var total = ObtenerTotalVenta();
            $("#tfooter1").html("Total: $" + total);

        });

        $("#tbl1 tbody").on("click", ".delete", function (e) {

            var tr = $(this).parents('tr');
            var box = $("#mb-delete-row");
            box.addClass("open");
            box.find(".mb-control-yes").on("click", function () {
                box.removeClass("open");
                tbl1.row(tr).remove().draw(false);
                var total = ObtenerTotalVenta();
                $("#tfooter1").html("Total: $" + total);
            });
        });

        $(document).on('submit', 'form#frm_ventaGeneral', function (e) {
            e.preventDefault();
            var tieneErrorTbl = true;
            var tieneErrorSucursal = true;
            var tieneErrorCliente = true;
            nNodes = tbl1.rows().data();

            //validamos sucursal
            var sucursal = $("#Id_sucursal").val();

            if (sucursal === '') {
                tieneErrorSucursal = true;
                $("#Id_sucursal").closest('.input-group ').removeClass('has-success').addClass("has-error");
                $("#ddSucursal").show(0);
            }
            else {
                tieneErrorSucursal = false;
                $("#Id_sucursal").closest('.input-group ').removeClass('has-error').addClass("has-success");
                $("#ddSucursal").hide(0);
            }

            //validamos cliente
            var cliente = $("#Id_cliente").val();

            if (cliente === '') {
                tieneErrorCliente = true;
                $("#Id_cliente").closest('.input-group ').removeClass('has-success').addClass("has-error");
                $("#ddCliente").show(0);
            }
            else {
                tieneErrorCliente = false;
                $("#Id_cliente").closest('.input-group ').removeClass('has-error').addClass("has-success");
                $("#ddCliente").hide(0);
            }

            //validamos tabla de productos
            if (nNodes.length > 0) {
                tieneErrorTbl = false;
                $("#tbl1").addClass("successCID");
                $("#tbl1").removeClass("errorCID");
                $("#ddTabla").hide(0);
            }
            else {
                tieneErrorTbl = true;
                $("#tbl1").addClass("errorCID");
                $("#tbl1").removeClass("successCID");
                $("#ddTabla").show(0);
            }

            if (!tieneErrorSucursal && !tieneErrorCliente && !tieneErrorTbl) {

                //obtenemos los productos 
                var productos = [];
                var rows = tbl1.rows().data();
                for (var i = 0; i < rows.length; i++) {
                    var cells = rows[i];

                    var rowActualDT = tbl1.row(i).node();
                    var cellsActualDT = $(rowActualDT).find("td");

                    var id = 0;
                    var fk_id = 0;
                    var id_producto = cells[0];
                    var id_tipoProducto = cells[1];
                    var id_documentoPorCobrar = '';
                    var cantidad = $(cellsActualDT[1]).find("input").val();
                    var precioUnitario = GetMoneySinSimbolo($(cellsActualDT[2]).find("input").val());
                    var almacen = $(cellsActualDT[1]).find("input").attr("data-almacen");
                    var id_unidadProducto = $(cellsActualDT[1]).find("input").attr("data-id_unidadproducto");

                    var producto =
                    {
                        Id: id,
                        Fk_id: fk_id,
                        Id_producto: id_producto,
                        Id_tipoProducto: id_tipoProducto,
                        Id_documentoPorCobrar: id_documentoPorCobrar,
                        Cantidad: cantidad,
                        PrecioUnitario: precioUnitario,
                        Id_almacen: almacen,
                        Id_unidadProducto: id_unidadProducto
                    };

                    productos.push(producto);
                }

                var Datos = {
                    id_sucursal: sucursal,
                    id_cliente: cliente,
                    listaProducto: productos
                };

                $.ajax({
                    dataType: 'json',
                    type: 'POST',
                    url: '/Admin/VentaGeneral/Create/',
                    data: Datos,
                    success: function (response) {
                        window.location.href = '/Admin/VentaGeneral/Index';
                    },
                    error: function (request, status, error) {
                        window.location.href = '/Admin/VentaGeneral/Index';
                    }
                });
            }
        });

    };

    function ObtenerTotalVenta() {
        var rows = tbl1.rows().nodes();
        var total = 0;

        for (var i = 0; i < rows.length; i++) {
            var cells = $(rows[i]).find("td");
            var subtotal = GetMoneySinSimbolo($(cells[3]).find("input").val());
            total += subtotal;
        }

        return format(total, 2);
    }

    function ValidarProducto(id_producto) {
        var rows = tbl1.rows().data();
        var unico = true;

        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            var id_productoRow = row[0];

            if (id_producto === id_productoRow) {
                unico = false;
                break;
            }
        }

        return unico;
    }

    function format(nStr, decimales) {
        nStr += '';
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = 0;
        if (x.length > 1) {
            var aux = Number.parseFloat('.' + x[1]).toFixed(decimales).split('.');
            x2 = "." + aux[1];
        }
        else {
            x2 = ".";
            for (var i = 0; i < decimales; i++) {
                x2 += "0"; 
            }
        }

        //var x2 = x.length > 1 ? Number.parseFloat('.' + x[1]).toFixed(decimales) : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }

        return x1 + x2;
    } 

    function AddProducto(id_producto, id_tipoProducto, producto, cantidad, precioUnitario, subtotal, eliminar) {

        tbl1.row.add([
            id_producto,
            id_tipoProducto,
            producto,
            cantidad,
            precioUnitario,
            subtotal,
            eliminar
        ]).draw(false);
    }

    function ActualizarSimbolos() {
        $(".cCantidad").maskMoney('mask');
        $(".cMoney").maskMoney('mask');
    }

    function GetMoneySinSimbolo(value) {
        var newValue = value.split(" ", 2);
        newValue = newValue[1];
        newValue = newValue.toString().replace(/,/g, "");

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return Number.parseFloat(newValue);
        }
    }

    function GetNumber(value) {
        value = value.toString().replace(/,/g, "");

        if (Number.isNaN(value)) {
            return 0;
        }
        else {
            return Number.parseFloat(value);
        }
    }

    function GetAlmacenes() {
        $.ajax({
            url: '/Admin/DocumentoXCobrar/GetAlmacenes/',
            type: "POST",
            dataType: 'json',
            data: {},
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "2");
            },
            success: function (result) {
                $("#Almacenes option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Almacenes").append('<option value="' + result[i].IDAlmacen + '">' + result[i].Descripcion + '</option>');
                }
                $('#Almacenes.select').selectpicker('refresh');

            }
        });
    }

    function GetProductos_Almacen(almacen) {
        $.ajax({
            url: '/Admin/DocumentoXCobrar/GetProductosAlmacen/',
            type: "POST",
            dataType: 'json',
            data: { almacen: almacen },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "2");
            },
            success: function (result) {
                $("#Producto option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Producto").append('<option value="' + result[i].IDProductoAlmacen + '" data-existencia="' + result[i].Existencia + '" data-preciounidad="' + result[i].PrecioUnidad + '" data-id_unidadproducto="' + result[i].Id_unidadProducto + '" data-unidadmedida="' + result[i].UnidadMedida + '" >' + result[i].Nombre + '</option>');
                }
                $('#Producto.select').selectpicker('refresh');

                Ocultar_Mostrar_Boton();
            }
        });
    }

    function GetVehiculosXIDEmpresa(IDEmpresa, IDSucursal) {
        $.ajax({
            url: '/Admin/VentaGeneral/ComboVehiculos/',
            type: "POST",
            dataType: 'json',
            data: { },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "2");
            },
            success: function (result) {
                $('#Producto').empty();
                var optgroup = result[0].Modelo;
                var option = '<optgroup label="' + result[0].Modelo + '"><option value="' + result[0].IDVehiculo + '">' + result[0].nombreMarca + '</option>';

                for (var i = 1; i < result.length; i++) {
                    if (optgroup == result[i].Modelo) {
                        option += '<option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                    }
                    else {
                        //Cerramos el grupo
                        option += '</optgroup>';
                        //Anexamos al select
                        $("#Producto").append(option);
                        //Creamos un group nuevo
                        option = '<optgroup label="' + result[i].Modelo + '"><option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                        optgroup = result[i].Modelo;
                    }
                }
                //Anexamos el último valor
                option += '</optgroup>';
                $("#Producto").append(option);
                $('#Producto.select').selectpicker('refresh');

                Ocultar_Mostrar_Boton();
            }
        });
    }

    function Ocultar_Mostrar_Boton() {

        var selectedTipoProducto = document.getElementById("TipoProducto");
        var opcionTipoProducto = selectedTipoProducto.options[selectedTipoProducto.selectedIndex].value;

        var selectedProducto = document.getElementById("Producto");
        var opcionProducto = selectedProducto.options[selectedProducto.selectedIndex].value;
        
        if (opcionTipoProducto === "1") {
            var selectedAlmacen = document.getElementById("Almacenes");
            var opcionAlmacen = selectedAlmacen.options[selectedAlmacen.selectedIndex].value;

            if (opcionAlmacen !== "" && (opcionProducto !== "" || opcionProducto === "0")) {
                $('#div4').show(1000);
            }
            else {
                $('#div4').hide(0);
            }
        }
        else if (opcionTipoProducto === "2") {

            if (opcionProducto !== "") {
                $('#div4').show(1000);
            }
            else {
                $('#div4').hide(0);
            }
        }
        else if (opcionTipoProducto === "0") {
            $('#div4').hide(0);
        }
    }

    function InicializarMensajesError() {
        $("#validation_summary").append("<dd id='ddSucursal' style='color: #ff004d !important; '>Por favor, seleccione una sucursal.</dd>");
        $("#validation_summary").append("<dd id='ddCliente' style='color: #ff004d !important; '>Por favor, seleccione un cliente.</dd>");
        $("#validation_summary").append("<dd id='ddTabla' style='color: #ff004d !important; '>Por favor, agregue un producto a la venta.</dd>");

        $("#ddSucursal").hide(0);
        $("#ddCliente").hide(0);
        $("#ddTabla").hide(0);
    }

    return {
        //main function to initiate template pages
        init: function () {
            InicializarMensajesError();
            eventos();
        }
    };
}();