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
    var costoMachos = Number.parseFloat($("#CostoMachos").val().replace("$", '')); 
    var costoHembras = Number.parseFloat($("#CostoHembras").val().replace("$", '')); 
    var costoTotal = Number.parseFloat($("#CostoTotal").val().replace("$", ''));

    var montoTotalGanado = Number.parseFloat($("#MontoTotalGanado").val().replace("$", ''));

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
                    "data": "pesoPagado",
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
                    "targets": [0, 1],
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
                    "data": "pesoPagado",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                },
                {
                    "data": "precioKilo",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        if (TipoDeVenta === 1) //venta directa
                            return "$ 0.00";
                        else if (TipoDeVenta === 2) //venta por rango de precios
                        {
                            if (row["pesoPagado"] === null || row["genero"] === null)
                                return "$ 0.00";
                            else
                                return "$ " + Number.parseFloat(PrecioSugerido(row["pesoPagado"], row["genero"])).toFixed(2);
                        }
                    }
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                }
            ],
            "columnDefs": [
                {
                    "targets": [0, 1],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    };

    function PrecioSugerido(peso, genero) {
        genero = (genero === "Macho" || genero === "MACHO") ? true : false;
        for (var item in ListaDePrecios) {
            if (ListaDePrecios[item].EsMacho === genero) {
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
                precision: 2,
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
            for (var i = 0 ; i < nodos.length ; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });
        $('#seleccionarJaula').click(function () {
            var nodos = tblGanadoJaula.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0 ; i < nodos.length ; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $(document).on('submit', 'form#frm_venta', function (e) {
            e.preventDefault();
            
            //datos de las tablas


            var error = Validar();
            
            if (error == 0) {
                var ganado = tblGanadoJaula.rows().data();
                var me = $("#ME").val();
                me = Number.parseFloat(me.replace(/,/g, ''));

                var objGanado, cantidad = 0;
                var listaGanado = new Array();

                for (var i = 0 ; i < ganado.length ; i++) {
                        listaGanado.unshift(ganado[i].id_ganado);
                        cantidad = i + 1;
                }
                var formData = new FormData();
                //datos de las tablas
                formData.append('IDVenta', Id_venta);
                formData.append('ListaIDGanadosParaVender', listaGanado);
                formData.append('ME', me);

                $.ajax({
                    type: 'POST',
                    data: formData,
                    url: '/Admin/Venta/VentaGanado/',
                    contentType: false,
                    processData: false,
                    cache: false,
                    success: function (response) {
                        window.location.href = '/Admin/Venta/Index';
                    },
                    error: function (request, status, error) {
                        window.location.href = '/Admin/Venta/Index';
                    }
                });
            } 
            
            //if (ganado.length == 0) {
            //    $("#tblGanadoJaula").addClass("errorTableCSL");
            //    $("#validation_summary").append("");
            //    $("#validation_summary").append("<dd style='color: #ff004d !important; '>-Debe de seleccionar un ganado para su venta</dd>");
            //}
            //else {
                
            //    $("#validation_summary").append("");

            //    for (var i = 0 ; i < ganado.length ; i++) {
            //        listaGanado.unshift(ganado[i].id_ganado);
            //        cantidad = i + 1;
            //    }

            //    var formData = new FormData();
            //    //datos de las tablas
            //    formData.append('IDVenta', Id_venta);
            //    formData.append('ListaIDGanadosParaVender', listaGanado);

            //    $.ajax({
            //        type: 'POST',
            //        data: formData,
            //        url: '/Admin/Venta/VentaGanado/',
            //        contentType: false,
            //        processData: false,
            //        cache: false,
            //        success: function (response) {
            //            window.location.href = '/Admin/Venta/Index';
            //        },
            //        error: function (request, status, error) {
            //            window.location.href = '/Admin/Venta/Index';
            //        }
            //    });
            //}
        });
        
        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            var rows = tblGanadoCorral.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                var precioSugerido = PrecioSugerido(d.pesoPagado, d.genero);
                var subtotal = TipoDeVenta === 1 ? d.subtotal : precioSugerido * d.pesoPagado;

                //lo agrego a la tabla jaula para su envio
                tblGanadoJaula.row.add({
                    "id_ganado": d.id_ganado,
                    "id_detalle": d.id_detalle,
                    "numArete": d.numArete,                 
                    "corral": d.corral,
                    "genero": d.genero,                     
                    "merma": d.merma,
                    "pesoPagado": d.pesoPagado,
                    "precioKilo": d.precioKilo,
                    [8]: precioSugerido,
                    "subtotal": subtotal
                }).draw();

                //agrego los datos a los inputs
                genero = d.genero;
                genero = genero.trim();

                if (genero.localeCompare("MACHO") === 0) {
                    cabezas_machos += 1;
                    kgMachos += d.pesoPagado;
                    costoMachos += d.subtotal;
                }
                else if (genero.localeCompare("HEMBRA") === 0) {
                    cabezas_hembras += 1;
                    kgHembras += d.pesoPagado;
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
                var subtotal = TipoDeVenta === 1 ? d.subtotal : d.pesoPagado * d.precioKilo;
                var pesoPagado = TipoDeVenta === 1 ? d.pesoPagado : arrayRow[7];

                //console.log(d);
                //console.log(arrayRow);

                tblGanadoCorral.row.add({
                    "id_ganado": d.id_ganado,
                    "id_detalle": d.id_detalle,
                    "numArete": d.numArete,
                    "corral": d.corral,
                    "genero": d.genero,
                    "merma": d.merma,
                    "pesoPagado": pesoPagado,
                    "precioKilo": d.precioKilo,
                    "subtotal": subtotal
                }).draw();

                //quito los datos a los inputs
                genero = d.genero;
                genero = genero.trim();
                if (genero.localeCompare("MACHO") === 0 ) {
                    cabezas_machos -= 1;
                    kgMachos -= pesoPagado;
                    costoMachos -= subtotal;
                }
                else if (genero.localeCompare("HEMBRA") === 0) {
                    cabezas_hembras -= 1;
                    kgHembras -= pesoPagado;
                    costoHembras -= subtotal;
                }
                cabezas_total = cabezas_machos + cabezas_hembras;
                kgTotal = kgMachos + kgHembras;
                costoTotal = costoMachos + costoHembras;

                if (TipoDeVenta === 2) {
                    var nuevoMontoTotalGanado = d.pesoPagado * Number.parseFloat(arrayRow[0]).toFixed(2);
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
        var numero = Number.parseFloat(me.replace(/,/g, ''));

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



        //if (Number.isNaN(me)) {
        //    //console.log("NO ES UN NUMERO");
        //    $("#txtME").addClass("has-error");
        //    $("#txtME").removeClass("has-success");
        //    $("#validation_summary").find("dd[for='ME']").addClass('help-block valid').text('-M.E. debe ser un número positivo mayor o igual a 0.');
        //    error = 1;
        //}
        //else {

        //    if (me >= 0) {
        //        //console.log("MAYOR O IGUAL A 0");
        //        $("#txtME").addClass("has-success");
        //        $("#txtME").removeClass("has-error");
        //        $("#validation_summary").find("dd[for='ME']").addClass('help-block valid').text('');
        //    }
        //    else {
        //        //console.log("MENOR A 0");
        //        $("#txtME").addClass("has-error");
        //        $("#txtME").removeClass("has-success");
        //        $("#validation_summary").find("dd[for='ME']").addClass('help-block valid').text('-M.E. debe ser un número positivo mayor o igual a 0.');
        //        error = 1;
        //    }
            
        //}
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