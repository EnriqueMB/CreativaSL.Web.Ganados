var VentaGanado = function () {
    "use strict";
    var Id_venta = $("#Id_venta").val();
    var Id_sucursal = $("#Id_sucursal").val();
    var ListaDePrecios;
    var TipoDeVenta = Number.parseInt($("#TipoVenta").val());

    var tblGanadoCorral, tblGanadoJaula;
    var ref_cabezas_machos = $("#CbzMachos");
    var ref_cabezas_hembras = $("#CbzHembras");
    var ref_cabezas_total = $("#CbzTotal");
    var ref_kgMachos = $("#KgMachos");
    var ref_kgHembras = $("#KgHembras");
    var ref_kgTotal = $("#KgTotal");
    var ref_costoMachos = $("#CostoMachos");
    var ref_costoHembras = $("#CostoHembras");
    var ref_costoTotal = $("#CostoTotal");
    var ref_montoTotalGanado = $("#MontoTotalGanado");

    var cabezas_machos = parseInt($("#CbzMachos").val());
    var cabezas_hembras = parseInt($("#CbzHembras").val());
    var cabezas_total = parseInt($("#CbzTotal").val());
    var kgMachos = parseFloat($("#KgMachos").val());
    var kgHembras = parseFloat($("#KgHembras").val());
    var kgTotal = parseFloat($("#KgTotal").val());
    var costoMachos = Number.parseFloat($("#CostoMachos").val().replace("$", '').replace(/,/g, ""));
    var costoHembras = Number.parseFloat($("#CostoHembras").val().replace("$", '').replace(/,/g, ""));
    var costoTotal = Number.parseFloat($("#CostoTotal").val().replace("$", '').replace(/,/g, ""));
    var montoTotalGanado = Number.parseFloat($("#MontoTotalGanado").val().replace("$", '').replace(/,/g, ""));
    var genero;

    /*INICIA GANADO*/
    var initDataTables = function () {
        tblGanadoCorral = $('#tblGanadoCorral').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_sucursal": Id_sucursal
                },
                "url": "/Admin/Venta/DatatableGanadoActual/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_ganado" },
                { "data": "id_detalle" },
                { "data": "numArete" },
                { "data": "corral" },
                { "data": "genero" },
                {
                    "data": "merma",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '', ' %')
                },
                {
                    "data": "pesoInicial",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                },
                {
                    "data": "precioKilo",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
            ],
            "columnDefs": [
                {
                    "targets": [0, 1, 5, 7],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
        tblGanadoJaula = $('#tblGanadoJaula').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    Id_venta: Id_venta
                },
                "url": "/Admin/Venta/DatatableGanadoParaVenta/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_ganado" },
                { "data": "id_detalle" },
                { "data": "numArete" },
                { "data": "corral" },
                { "data": "genero" },
                {
                    "data": "merma",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '', ' %')
                },
                {
                    "data": "pesoInicial",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                },
                {
                    "data": "precioKilo",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        if (TipoDeVenta === 1) { //venta directa
                            return "$ 0.00";
                        }
                        else if (TipoDeVenta === 2) //venta por rango de precios
                        {
                            if (row["pesoInicial"] === null || row["genero"] === null) {
                                return "$ 0.00";
                            }
                            else {
                                var pesoInicial = Number.parseFloat(row["pesoInicial"]).toFixed(2);
                                var genero = row["genero"].trim();

                                var nuevoPrecio = PrecioSugerido(pesoInicial, genero);
                                return "$ " + Number.parseFloat(nuevoPrecio).toFixed(2);
                            }
                        }
                    }
                },
                {
                    "data": "me",
                    "render": function (data, type, row) {
                        return '<input name= "me" class="kg2" type="text" style="color:black" data-peso="'+ row["pesoInicial"] +'" data-precio="'+ row["precioKilo"] +'" data-id="'+ row["id_ganado"] +'" value="' + data + ' kg">';

                    }
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                }
            ],
            "columnDefs": [
                {
                    "targets": [0, 1, 5, 7],
                    "visible": false,
                    "searchable": false
                }
            ],
            rowId: function (a) {
                return a.id_ganado;
            },
            "drawCallback": function (settings) {
                $(".kg2").maskMoney({ allowZero: true, suffix: ' kg', precision: 0 });

                $('.kg2').keyup(function () {
                    var me = $(this).val().split(" ", 1).toString().replace(/,/g, "");

                    if (Number.isNaN(me))
                        me = 0;
                    else {
                       
                        var peso = this.dataset.peso;
                        var precio = this.dataset.precio;
                        var id = this.dataset.id;

                        if (Number.isNaN(peso) || Number.isNaN(precio)) {
                            peso = 0;
                            precio = 0;
                        }
                        var row = tblGanadoJaula.row('#' + id).nodes().to$().find("td");
                        var antiguoPrecio = Number.parseFloat(GetKilosSinSimboloSinEspacios(row[6].innerHTML));
                       

                        me = Number.parseFloat(me);
                        var nuevoPrecioPorKilo = Number.parseFloat(GetMoneySinSimbolo(row[4].innerHTML));

                        var nuevoPeso = Number.parseFloat(peso);
                        nuevoPeso = nuevoPeso + me;
                        var nuevoSubtotal = nuevoPeso * nuevoPrecioPorKilo;
                        row[6].innerHTML = "$" + Number.parseFloat(nuevoSubtotal).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');

                        var total = Number.parseFloat(GetMoneySinSimbolo($("#MontoTotalGanado").val()));
                        total = total - antiguoPrecio;
                        total = total + nuevoSubtotal;

                        $("#MontoTotalGanado").val(total.toFixed(2));
                        $(".money").maskMoney('mask');
                        CalculoMerma();
                        
                    }
                });
            }
        });

        

    };

    function CalculoMerma() {
        //merma
        var rows = tblGanadoJaula.rows().nodes().to$();
        var mermaGeneralMachos = 0;
        var mermaGeneralHembras = 0;

        for (var i = 0; i < rows.length; i++) {
            var cells = $(rows[i]).find("td");

            for (var x = 0; x < cells.length; x += 7) {
                var genero = cells[x + 2].innerHTML;
                genero = genero.trim();
                var mermaExtraUsuario = Number.parseFloat(GetKilosSinSimbolo($(cells[x + 5]).find("input").val()));

                if (genero.localeCompare("MACHO") == 0) {
                    mermaGeneralMachos += mermaExtraUsuario;
                }
                else if (genero.localeCompare("HEMBRA") == 0) {
                    mermaGeneralHembras += mermaExtraUsuario;
                }
            }
        }

        $("#MermaExtraMachos").val(mermaGeneralMachos);
        $("#MermaExtraHembras").val(mermaGeneralHembras);
        $("#ME").val((mermaGeneralHembras + mermaGeneralMachos));

        $(".kgMerma").maskMoney('mask');
    }

    function PrecioSugerido(peso, genero) {
        genero = (genero == "Macho" || genero == "MACHO") ? true : false;
        for (var item in ListaDePrecios) {
            if (ListaDePrecios[item].EsMacho == genero) {
                if (ListaDePrecios[item].PesoMinimo <= peso && peso <= ListaDePrecios[item].PesoMaximo) {
                    return ListaDePrecios[item].Precio.toFixed(2);
                }
            }
        }
        return 0;
    }

    var initFuncionesGanado = function () {

        $(".kg").maskMoney(
            {
                allowZero: true,
                precision: 0,
                suffix: ' kg'
            }
        );
        $(".kgMerma").maskMoney(
            {
                allowZero: true,
                precision: 0,
                suffix: ' kg'
            }
        );
        $(".merma").maskMoney(
            {
                allowZero: true,
                precision: 2,
                suffix: ' %'
            }
        );
        $(".money").maskMoney(
            {
                allowZero: true,
                precision: 2,
                prefix: '$ '
            }
        );

        $(".kg").maskMoney('mask');
        $(".kgMerma").maskMoney('mask');
        $(".merma").maskMoney('mask');
        $(".money").maskMoney('mask');

        //Seleccionar filas 
        $('#tblGanadoCorral tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblGanadoJaula tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#seleccionarCorral').click(function () {
            var nodos = tblGanadoCorral.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });
        $('#seleccionarJaula').click(function () {
            var nodos = tblGanadoJaula.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $('#guardar').on('click', function () {
            //datos de las tablas

            var error = Validar();

            if (error == 0) {
                //var tblGanado = tblGanadoJaula.rows().data();
                var tblGanado = tblGanadoJaula.rows().nodes().to$().find("td");
                var monto = GetMoneySinSimbolo($("#MontoTotalGanado").val());

                var ganados = [];

                //console.log(tblGanado);
                for (var i = 0; i < tblGanado.length; i += 7) {
                    var id_ganado = $(tblGanado[i + 5]).find("input[name='me']").attr("data-id");
                    var me = Number.parseFloat(GetKilosSinSimbolo($(tblGanado[i + 5]).find("input[name='me']").val()));

                    var ganado =
                    {
                        Id_ganado: id_ganado,
                        MermaExtra: me
                    };
                    ganados.push(ganado);
                }

                var Datos = {
                    ListaGanadosParaVender: ganados,
                    IDVenta: Id_venta,
                    montoTotal: monto
                };

                $.ajax({
                    dataType: 'json', 
                    type: 'POST',
                    url: '/Admin/Venta/VentaGanado/',
                    data: Datos,
                    success: function (response) {
                        window.location.href = '/Admin/Venta/Index';
                    },
                    error: function (request, status, error) {
                        window.location.href = '/Admin/Venta/Index';
                    }
                });
            }
        });

        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            var rows = tblGanadoCorral.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];
                var pp = Number.parseFloat(d.pesoInicial).toFixed(2);
                var ge = d.genero.trim();

                var precioSugerido = PrecioSugerido(pp, ge);
                var subtotal = TipoDeVenta === 1 ? d.subtotal : precioSugerido * d.pesoInicial;

                //lo agrego a la tabla jaula para su envio
                tblGanadoJaula.row.add({
                    "id_ganado": d.id_ganado,
                    "id_detalle": d.id_detalle,
                    "numArete": d.numArete,
                    "corral": d.corral,
                    "genero": d.genero,
                    "merma": d.merma,
                    "pesoInicial": d.pesoInicial,
                    "precioKilo": d.precioKilo,
                    [8]: precioSugerido,
                    "subtotal": subtotal, 
                    "me": 0
                }).draw();

                //agrego los datos a los inputs
                genero = d.genero;
                genero = genero.trim();

                if (genero.localeCompare("MACHO") === 0) {
                    cabezas_machos += 1;
                    kgMachos += d.pesoInicial;
                    costoMachos += d.subtotal;
                }
                else if (genero.localeCompare("HEMBRA") === 0) {
                    cabezas_hembras += 1;
                    kgHembras += d.pesoInicial;
                    costoHembras += d.subtotal;
                }

                cabezas_total = cabezas_machos + cabezas_hembras;
                kgTotal = kgMachos + kgHembras;
                costoTotal = costoMachos + costoHembras;

                if (TipoDeVenta === 2) {
                    montoTotalGanado += subtotal;
                }

                ActualizarInputs();
            }
            tblGanadoCorral.rows('.selected').remove().draw(false);
        });

        $('#regresar').click(function () {
            var rows = tblGanadoJaula.rows('.selected').data();
            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                var arrayRow = Object.values(d);
                var subtotal = TipoDeVenta === 1 ? d.subtotal : d.pesoInicial * d.precioKilo;
                var pesoInicial = TipoDeVenta === 1 ? d.pesoInicial : arrayRow.length == 11 ? arrayRow[7] : arrayRow[5];

                tblGanadoCorral.row.add({
                    "id_ganado": d.id_ganado,
                    "id_detalle": d.id_detalle,
                    "numArete": d.numArete,
                    "corral": d.corral,
                    "genero": d.genero,
                    "merma": d.merma,
                    "pesoInicial": pesoInicial,
                    "precioKilo": d.precioKilo,
                    "subtotal": subtotal
                }).draw();

                //quito los datos a los inputs
                genero = d.genero;
                genero = genero.trim();
                if (genero.localeCompare("MACHO") === 0) {
                    cabezas_machos -= 1;
                    kgMachos -= pesoInicial;
                    costoMachos -= subtotal;
                }
                else if (genero.localeCompare("HEMBRA") === 0) {
                    cabezas_hembras -= 1;
                    kgHembras -= pesoInicial;
                    costoHembras -= subtotal;
                }
                cabezas_total = cabezas_machos + cabezas_hembras;
                kgTotal = kgMachos + kgHembras;
                costoTotal = costoMachos + costoHembras;

                if (TipoDeVenta === 2) {
                    var pesoInicial2 = d.pesoInicial;
                    var mermaExtra = Number.parseFloat(d.me);
                    pesoInicial2 = pesoInicial2 + mermaExtra;

                    var precioXKilo2 = Number.parseFloat(PrecioSugerido(pesoInicial2, d.genero.trim())).toFixed(2);

                    var nuevoMontoTotalGanado = pesoInicial2 * precioXKilo2;

                    montoTotalGanado -= nuevoMontoTotalGanado;
                }
                ActualizarInputs();

            }
            tblGanadoJaula.rows('.selected').remove().draw(false);
        });

        $('#btnVerListaPrecios').on('click', function () {
            $("body").css("cursor", "progress");
            var Id_venta = document.getElementById("Id_venta").value;

            $.ajax({
                url: '/Admin/Venta/ModalListaPrecios/',
                type: "POST",
                data: { Id_venta: Id_venta },
                success: function (data) {
                    $("body").css("cursor", "default");
                    $('#ContenidoModal').html(data);
                    $('#Modal').modal({ backdrop: 'static', keyboard: false });
                }
            });
        });
    };

    function GetKilosSinSimbolo(value) {
        var newValue = value.split(" ", 1);
        newValue = newValue.toString().replace(/,/g, "");

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }

    function GetKilosSinSimboloSinEspacios(value) {
        var newValue = value.split("$", 2);
        newValue = newValue[1];
        newValue = newValue.toString().replace(/,/g, "");

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }


    function GetMoneySinSimbolo(value) {
        var newValue = value.split(" ", 2);
        newValue = newValue[1];
        newValue = newValue.toString().replace(/,/g, "");

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }

    function cpf(v) {
        v = v.replace(/([^0-9]+)/g, '');
        v = v.replace(/^[\.]/, '');
        v = v.replace(/[\.][\.]/g, '');
        //v = v.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
        //v = v.replace(/\.(\d{1,2})\./g, '.$1');
        v = v.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
        v = v.split('').reverse().join('').replace(/^[\,]/, '');
        return v;
    }
    function Validar() {
        var error = 0;
        var ganado = tblGanadoJaula.rows().data();

        if (ganado.length == 0) {
            //console.log("SIN GANADO");
            $("#tblGanadoJaula").addClass("errorTableCSL");
            $("#tblGanadoJaula").removeClass("okCSLGanado");
            $("#validation_summary").find("dd[for='Ganado']").addClass('help-block valid').text('-Debe de seleccionar un ganado para su venta');
            error = 1;
        }
        else {
            //console.log("YA TIENE GANADO");
            $("#tblGanadoJaula").addClass("okCSLGanado");
            $("#tblGanadoJaula").removeClass("errorTableCSL");
            $("#validation_summary").find("dd[for='Ganado']").addClass('help-block valid').text('');
        }

        var me = $("#ME").val();

        if (me === '') {
            $("#txtME").addClass("has-error");
            $("#txtME").removeClass("has-success");
            $("#validation_summary").find("dd[for='ME']").addClass('help-block valid').text('-M.E. debe ser un número positivo mayor o igual a 0.');
            error = 1;
        }
        //Si hay le quitamos la coma, en caso que sean miles
        var numero = GetKilosSinSimbolo(me);

        if (Number.isNaN(numero)) {
            $("#txtME").addClass("has-error");
            $("#txtME").removeClass("has-success");
            $("#validation_summary").find("dd[for='ME']").addClass('help-block valid').text('-M.E. debe ser un número positivo mayor o igual a 0.');
            error = 1;
        }
        else {
            if (numero >= 0) {
                $("#txtME").addClass("has-success");
                $("#txtME").removeClass("has-error");
                $("#validation_summary").find("dd[for='ME']").addClass('help-block valid').text('');
            }
            else {
                $("#txtME").addClass("has-error");
                $("#txtME").removeClass("has-success");
                $("#validation_summary").find("dd[for='ME']").addClass('help-block valid').text('-M.E. debe ser un número positivo mayor o igual a 0.');
                error = 1;
            }
        }

        var montototal = $("#MontoTotalGanado").val();

        if (montototal === '') {
            $("#txtMontoTotal").addClass("has-error");
            $("#txtMontoTotal").removeClass("has-success");
            $("#validation_summary").find("dd[for='MontoTotal']").addClass('help-block valid').text('-Monto total debe ser un número positivo mayor o igual a 0.');
            error = 1;
        }
        //Si hay le quitamos la coma, en caso que sean miles
        var numeroMontoTotal = GetMoneySinSimbolo(montototal);

        if (Number.isNaN(numeroMontoTotal)) {
            $("#txtMontoTotal").addClass("has-error");
            $("#txtMontoTotal").removeClass("has-success");
            $("#validation_summary").find("dd[for='MontoTotal']").addClass('help-block valid').text('-Monto total debe ser un número positivo mayor o igual a 0.');
            error = 1;
        }
        else {
            if (numeroMontoTotal >= 0) {
                $("#txtMontoTotal").addClass("has-success");
                $("#txtMontoTotal").removeClass("has-error");
                $("#validation_summary").find("dd[for='MontoTotal']").addClass('help-block valid').text('');
            }
            else {
                $("#txtMontoTotal").addClass("has-error");
                $("#txtMontoTotal").removeClass("has-success");
                $("#validation_summary").find("dd[for='MontoTotal']").addClass('help-block valid').text('-Monto total debe ser un número positivo mayor o igual a 0.');
                error = 1;
            }
        }

        return error;
    }
    function ActualizarInputs() {
        ref_cabezas_machos.val(cabezas_machos);
        ref_cabezas_hembras.val(cabezas_hembras);
        ref_cabezas_total.val(cabezas_total);

        ref_kgMachos.val(kgMachos);
        ref_kgHembras.val(kgHembras);
        ref_kgTotal.val(kgTotal);

        ref_costoMachos.val(costoMachos.toFixed(2));
        ref_costoHembras.val(costoHembras.toFixed(2));
        ref_costoTotal.val(costoTotal.toFixed(2));

        ref_montoTotalGanado.val(montoTotalGanado.toFixed(2));

        $(".kg").maskMoney('mask');
        $(".kgMerma").maskMoney('mask');
        $(".merma").maskMoney('mask');
        $(".money").maskMoney('mask');
    }
    /*TERMINA GANADO*/

   


    return {
        init: function (listaDePrecios) {
            ListaDePrecios = listaDePrecios;
            initDataTables();
            initFuncionesGanado();
        }
    };
}();