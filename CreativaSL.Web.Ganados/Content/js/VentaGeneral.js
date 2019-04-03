var VentaGeneral = function () {
    "use strict";
    var tbl1;
    var nNodes;
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
            var id_tipoProducto = $("#TipoProducto").val();

            if ($("#Producto option:selected"))
                var producto = $("#Producto option:selected").text();

            var readonlyCantidad = "";
            var precioUnitario = "0.00";
            var cantidad = "1.00";
            var cClass = "";

            if (id_tipoProducto === "1") { //producto almacen
                precioUnitario = $("#Producto").children(":selected").attr("data-preciounidad");
                precioUnitario = Number.parseFloat(precioUnitario).toFixed(2);
                cClass = "cCantidad";
            }
            else if (id_tipoProducto === "2") { //vehiculo
                readonlyCantidad = "readonly = 'readonly'";
                cClass = "";
            }

            var inputCantidad = "<input type='text' class=' iCantidad " + cClass + "' value='" + cantidad + "' " + readonlyCantidad + ">";
            var inputPrecioUnitario = "<input type='text'  class='cMoney' value='" + precioUnitario + "'>";
            var inputSubtotal = "<input type='text' class='' value='$ " + (Number.parseFloat(cantidad) * Number.parseFloat(precioUnitario)).toFixed(2) + "' readonly='readonly'>";

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

        });


        $('#tbl1 tbody').on('keyup', 'tr', function () {
            var data = tbl1.row(this).node();
            var cells = $(data).find("td");
            var cantidad = Number.parseFloat($(cells[1]).find("input").val());
            var precioUnitario = GetMoneySinSimbolo($(cells[2]).find("input").val());
            var subtotal = "$ " + (cantidad * precioUnitario).toFixed(2);

            $(cells[3]).find("input").val(subtotal);
        });

        $(document).on('submit', 'form#frm_ventaGeneral', function (e) {
            e.preventDefault();
            nNodes = tbl1.rows().data();
            console.log("submit");
            console.log(nNodes);
            console.log("---------------------");


         
            //if (flag_errorGanado === 0) {
            //    for (var i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA) {
            //        var parse_nuevoCosto = Number.parseFloat(GetMoneySinSimbolo(nNodes[i + NUEVO_COSTO_POR_KILO].value).toString().replace(/,/g, "")).toFixed(2);

            //        if (isNaN(parse_nuevoCosto)) {
            //            nNodes[i + NUEVO_COSTO_POR_KILO].classList.remove('okCSLGanado');
            //            nNodes[i + NUEVO_COSTO_POR_KILO].classList.add('errorCSLGanado');
            //            flag_ganado = 0;
            //        }
            //        else {
            //            var validarExpreMonto = expRegularMonto.test(parse_nuevoCosto);
            //            //validando con la expresión regular positivo, con solo 2 digitos 
            //            if (validarExpreMonto === false) {
            //                nNodes[i + NUEVO_COSTO_POR_KILO].classList.remove('okCSLGanado');
            //                nNodes[i + NUEVO_COSTO_POR_KILO].classList.add('errorCSLGanado');
            //                flag_ganado = 0;
            //            }
            //            else {
            //                nNodes[i + NUEVO_COSTO_POR_KILO].classList.remove('errorCSLGanado');
            //                nNodes[i + NUEVO_COSTO_POR_KILO].classList.add('okCSLGanado');
            //            }
            //        }
            //    }

            //    if (flag_ganado === 1) {
            //        $("#ddGanado").hide(0);
            //    }
            //    else {
            //        $("#ddGanado").show(0);
            //    }
            //}

            //if (flag_errorTipoEvento === 0 && flag_errorLugar === 0 && flag_errorFecha === 0 && flag_errorHora === 0 && flag_ganado === 1 && flag_errorGanado === 0) {
            //    //todo bien, obtenemos los id de los ganados y el nuevo precio 
            //    var ListaIDGanadosDelEvento = new Array();
            //    var ListaNuevoPrecioGanado = new Array();
            //    var cantidad = 0;

            //    for (i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA) {
            //        //var nuevo_costo = nNodes[i + NUEVO_COSTO_POR_KILO].value;
            //        parse_nuevoCosto = Number.parseFloat(GetMoneySinSimbolo(nNodes[i + NUEVO_COSTO_POR_KILO].value).toString().replace(/,/g, "")).toFixed(2);

            //        var id_ganado = nNodes[i + NUM_ARETE].dataset.id;
            //        var item = id_ganado + "|" + parse_nuevoCosto;

            //        ListaIDGanadosDelEvento.unshift(item);
            //        cantidad = cantidad + 1;
            //    }

            //    var form = $('form#frm_evento')[0];
            //    var formData = new FormData(form);

            //    formData.append('ListaIDGanadosDelEvento', ListaIDGanadosDelEvento);
            //    formData.append('Cantidad', cantidad);

            //    $.ajax({
            //        type: 'POST',
            //        data: formData,
            //        url: '/Admin/Compra/EventoCompra/',
            //        contentType: false,
            //        processData: false,
            //        cache: false,
            //        success: function (response) {
            //            window.location.href = '/Admin/Compra/RecepcionCompra?IDCompra=' + Id_compra;
            //        },
            //        error: function (request, status, error) {
            //            window.location.href = '/Admin/Compra/RecepcionCompra?IDCompra=' + Id_compra;
            //        }
            //    });
            //}
        });

    };

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

    return {
        //main function to initiate template pages
        init: function () {
            eventos();
        }
    };
}();

