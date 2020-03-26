var Jaula = function () {
    "use strict";
    var tblVentas, tblJaula, tDetalleGanado;
    var listaventas,listidsventas;
    var UrlVentas = "/Admin/Jaulas/ObtenerVentas";
    var UrlIdsVentas = "/Admin/Jaulas/ObtenerIdsVentas";
    var ref_cabezas_machos = $("#CbzMachos");
    var ref_cabezas_hembras = $("#CbzHembras");
    var ref_cabezas_total = $("#CbzTotal");
    var ref_kgMachos = $("#KgMachos");
    var ref_kgHembras = $("#KgHembras");
    var ref_kgTotal = $("#KgTotal");
    var ref_ImpMachos = $("#ImpMachos");
    var ref_ImpHembras = $("#ImpHembras");
    var ref_ImpTotal = $("#ImpTotal");
    var ref_Percepcion = $("#Percepcion");
    var ref_CostoExtra = $("#CostoExtra");
    var ref_Deduccion = $("#Deduccion");
    var ref_Total = $("#Total");
    var ref_Pago = $("#Pago");
    var ref_Pendiente = $("#Pendiente");
    var ref_IdVentas = $("#IDsVentas");
    var ref_HTraslado = $("#HTraslado");
    var ref_FechaSalida = $("#FechaSalida");
    var ref_Hsalida = $("#HSalida");
    var ref_Fechallegada = $("#Fechallegada");
    var ref_HLlegada = $("#HLlegada");

    var ref_marca = $("#marca");
    var ref_color = $("#color");
    var ref_modelo = $("#modelo");
    var ref_placa = $("#placa");
    var ref_remolque = $("#remolque");
    var IDJaula = $("#IDJaula").val();
    var CbzMachos = parseInt($("#CbzMachos").val());
    var CbzHembras = parseInt($("#CbzHembras").val());
    var CbzTotal = parseInt($("#CbzTotal").val());
    var kgMachos = parseFloat($("#KgMachos").val());
    var kgHembras = parseFloat($("#KgHembras").val());
    var kgTotal = parseFloat($("#KgTotal").val());
    var ImpMachos = Number.parseFloat($("#ImpMachos").val().replace("$", '').replace(/,/g, ""));
    var ImpHembras = Number.parseFloat($("#ImpHembras").val().replace("$", '').replace(/,/g, ""));
    var ImpTotal = Number.parseFloat($("#ImpTotal").val().replace("$", '').replace(/,/g, ""));
    var Percepcion = Number.parseFloat($("#Percepcion").val().replace("$", '').replace(/,/g, ""));
    var CostoExtra = Number.parseFloat($("#CostoExtra").val().replace("$", '').replace(/,/g, ""));
    var Deduccion = Number.parseFloat($("#Deduccion").val().replace("$", '').replace(/,/g, ""));
    var Total = Number.parseFloat($("#Total").val().replace("$", '').replace(/,/g, ""));
    var Pago = Number.parseFloat($("#Pago").val().replace("$", '').replace(/,/g, ""));
    var Pendiente = Number.parseFloat($("#Pendiente").val().replace("$", '').replace(/,/g, ""));
    var Hinicio, HFinal, Finicio, Ffinal
    var cargarIDsVentasUPD = function () {
        if (IDJaula != "" && IDJaula != null) {
            $.ajax({
                url: UrlIdsVentas,
                type: "POST",
                data: {IdJaula: IDJaula},
                dataType: "json",
                async:false,
                success: function (result) {
                    listidsventas = result;
                    console.log(listidsventas);
                    if (listidsventas.length != 0) {
                        var nodos = tblVentas.rows({ page: 'all', search: 'applied' }).nodes();
                        var rows = tblVentas.rows().data();
                        for (var i = 0; i < nodos.length; i++) {
                            var row = rows[i];
                            for (var z = 0; z < listidsventas.length; z++) {                               
                                if (row.Id_venta === listidsventas[z])
                                $(nodos[i]).toggleClass("selected");
                            }                                                                                        
                        }
                        Enviar();
                    }
                },
                error: function () {
                    Mensaje(
                        "Error al tratar de obtener las ventas, intentelo más tarde o contacte con soporte técnico",
                        "2");
                }
            });
            
            
        } else {
            console.log("nada");
        }
    }
    var CalcularTraslado = function () {
        //ref_Fechallegada.val(Date.now());
        //ref_FechaSalida.val(Date.now());
        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
        });
        Finicio = formatfecha(ref_FechaSalida.val());
        Ffinal = formatfecha(ref_Fechallegada.val());
        ref_Hsalida.on("change", function () {
            Hinicio = ref_Hsalida.val();            

            console.log(Hinicio);
            calcular(Finicio, Hinicio, Ffinal, HFinal);
            Validar();
        });
        
        
        ref_HLlegada.on("change", function () {
            HFinal = ref_HLlegada.val();
            console.log(HFinal);
            calcular(Finicio, Hinicio, Ffinal, HFinal);
            Validar();
            
        });                
        ref_FechaSalida.on("change", function () {
            Finicio = formatfecha(ref_FechaSalida.val());
            console.log(Finicio);
            calcular(Finicio, Hinicio, Ffinal, HFinal);
        });

        ref_Fechallegada.on("change", function () {
            Ffinal = formatfecha(ref_Fechallegada.val());
            console.log(Ffinal);
            calcular(Finicio, Hinicio, Ffinal, HFinal);
            
        });
                
        
    };
    function formatfecha(fecha) {
        var arr = fecha.split("/");
        return arr[2]+"/"+arr[1]+"/"+arr[0]
    };
    function calcular(FI, HI, FF, HF) {
        var srt1 = FI + " " + HI
        var FDateI = new Date(srt1);
        var srt2 = FF + " " + HF;
        var FDateF = new Date(srt2);
        var Fdiferencia = Math.abs(FDateF - FDateI);
        console.log(FDateI);
        console.log(Fdiferencia);
        if (!isNaN(Fdiferencia)) {
            ref_HTraslado.val(timeConversion(Fdiferencia));
        } else {
            ref_HTraslado.val("00:00:00");
        }
        
    };
    function timeConversion(millisec) {

       var seconds = (millisec / 1000).toFixed(1);

        var minutes = (millisec / (1000 * 60)).toFixed(1);

        var hours = (millisec / (1000 * 60 * 60)).toFixed(1);

        var days = (millisec / (1000 * 60 * 60 * 24)).toFixed(1);

       if (minutes < 60) {
            return minutes + " Min";
        } else if (hours < 24) {
            return hours + " Hrs";
        } else {
            return days + " Dias"
        }
       // return  hours+ "Hrs";
       
    }
    var cargarVehiculo = function () {
        var IdVehiculo = $("#cmbVehiculo").val();
        if (IdVehiculo.length != 0) {
            GetVehiculo(IdVehiculo);
        }
        $("#cmbVehiculo").on("change", function () {
            IdVehiculo = $("#cmbVehiculo").val();
            GetVehiculo(IdVehiculo);
        });
        
    };
    function GetVehiculo(IDVehiculo) {
        $.ajax({
            url: "/Admin/Jaulas/ObtenerVehiculo/",
            data: { IDVehiculo: IDVehiculo },
            async: false,
            dataType: "json",
            type: "POST",
            error: function () {
                Mensaje("Ocurrió un error al cargar datos del vehiculo", "1");
            },
            success: function (result) {
                ref_marca.val( result.nombreMarca);
                ref_color.val(result.Color);
                ref_placa.val(result.Placas);
                ref_modelo.val(result.Modelo);
                ref_remolque.val(result.PlacaJaula);
            }
        });
    }
    var tblVentasData = function () {
        $.ajax({
            url: UrlVentas,
            type: "GET",
            dataType: "json",
            success: function (result) {
                tblVentas.clear().draw();
                tblJaula.clear().draw();
                tDetalleGanado.clear().draw();
                listaventas = result;
                for (var i = 0; i < result.length; i++) {
                    tblVentas.row.add({
                        "Id_venta": result[i].Id_venta,
                        "Folio": result[i].Folio,
                        "Sucursal": result[i].Sucursal,
                        "CantTotal": result[i].CantTotal,
                        "PesoTotal": result[i].PesoTotal
                    }).draw();
                }
                cargarIDsVentasUPD();
            },
            error: function () {
                Mensaje(
                    "Error al tratar de obtener las ventas, intentelo más tarde o contacte con soporte técnico",
                    "2");
            }
        });
    };
    var initDataTables = function () {
        tDetalleGanado = $('#tDetalleGanado').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "Id_venta" },
                { "data": "Folio" },
                { "data": "Sucursal" },
                { "data": "CbzMacho" },
                {
                    "data": "KgMacho",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                },
                {
                    "data": "ImporteMacho",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                { "data": "CbzHembra" },
                {
                    "data": "KgHembra",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                },
                {
                    "data": "ImporteHembra",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "Percepcion",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "CostoExtra",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "Deduccion",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "Total",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
        tblVentas = $('#tblVentas').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "Id_venta" },
                { "data": "Folio" },
                { "data": "Sucursal" },
                { "data": "CantTotal" },
                { "data": "PesoTotal" }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

        tblJaula = $('#tblJaula').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "Id_venta" },
                { "data": "Folio" },
                { "data": "Sucursal" },
                { "data": "CantTotal" },
                { "data": "PesoTotal" }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    };
    function post(path, params, method = 'post') {

        // The rest of this code assumes you are not using a library.
        // It can be made less wordy if you use one.
        var form = document.createElement('form');
        form.method = method;
        form.action = path;

        for (const key in params) {
            if (params.hasOwnProperty(key)) {
                var hiddenField = document.createElement('input');
                hiddenField.type = 'hidden';
                hiddenField.name = key;
                hiddenField.value = params[key];

                form.appendChild(hiddenField);
            }
        }

        document.body.appendChild(form);
        form.submit();
    }
    var runValidator1 = function () {
        var form1 = $('#form-dg');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-dg').validate({
            errorElement: "span", // contain the error msg in a span tag
            errorClass: 'help-block color',
            errorLabelContainer: $("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") == "dd" || element.attr("name") == "mm" || element.attr("name") == "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") == "text") {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                IDVehiculo: { required: true },
                IDChofer: { required: true },
                Observaciones: { required: true },

            },
            messages: {
                IDVehiculo: { required: "Seleccione un vehiculo" },
                IDChofer: { required: "Seleccione el chofer" },
                Observaciones: { required: "Ingrese una observacion" },



            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.form-group').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
                //this.submit();
            }
        });
    };
    var initFuncionesJaula = function () {
        $('#tblVentas tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblJaula tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
            
            var nodos = tDetalleGanado.rows({ page: 'all', search: 'applied' }).nodes();
            $(nodos[tblJaula.row(this).index()]).toggleClass('selected');
        });

        $('#seleccionarTodaLaTblVentas').click(function () {
            var nodos = tblVentas.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $('#seleccionarTodaLaTblJaula').click(function () {
            var nodos = tblJaula.rows({ page: 'all', search: 'applied' }).nodes();
            var nodJau = tDetalleGanado.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
                $(nodJau[i]).toggleClass("selected");
            }
        });

        $('#GuardarJaula').on('click', function () {
            //datos de las tablas

            var error = Validar();
            if (!error) {

                var rows = tblJaula.rows().data();

                var ventas = [];

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];

                    var Id_venta = row.Id_venta;
                    console.log(Id_venta);
                    ventas.push(Id_venta);
                }
                ref_IdVentas.val(ventas);
                console.log("Exito paps");
               // post(UrlReporteCliente, { ventas: ventas, id_jaula: id_jaula });
            }
        });

        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            Enviar();
        });
       
        $('#regresar').click(function () {
            var rows = tblJaula.rows('.selected').data();
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];

                tblVentas.row.add({
                    "Id_venta": row.Id_venta,
                    "Folio": row.Folio,
                    "Sucursal": row.Sucursal,
                    "CantTotal": row.CantTotal,
                    "PesoTotal": row.PesoTotal
                }).draw();
                for (var z = 0; z < listaventas.length; z++) {
                    if (listaventas[z].Id_venta === row.Id_venta) {
                        CbzMachos -= listaventas[z].CbzMacho;
                        CbzHembras -= listaventas[z].CbzHembra;
                        CbzTotal -= listaventas[z].CantTotal;
                        kgHembras -= listaventas[z].KgHembra;
                        kgMachos -= listaventas[z].KgMacho;
                        kgTotal -= listaventas[z].PesoTotal;
                        ImpMachos -= listaventas[z].ImporteMacho;
                        ImpHembras -= listaventas[z].ImporteHembra;
                        ImpTotal -= listaventas[z].ImporteTotal;
                        Percepcion -= listaventas[z].Percepcion;
                        console.log(listaventas[z].CostoExtra);
                        CostoExtra -= listaventas[z].CostoExtra;
                        Deduccion -= listaventas[z].Deduccion;
                        Total -= listaventas[z].Total;
                        Pago -= listaventas[z].Pago;
                        Pendiente -= listaventas[z].Pendiente;
                        ActualizarInputs();
                        break;
                    }
                }
               
            }
            tblJaula.rows('.selected').remove().draw();
            tDetalleGanado.rows('.selected').remove().draw();
           
            
        });
                
    };
    function ActualizarInputs() {
        ref_cabezas_machos.val(CbzMachos);
        ref_cabezas_hembras.val(CbzHembras);
        ref_cabezas_total.val(CbzTotal);

        ref_kgMachos.val(kgMachos);
        ref_kgHembras.val(kgHembras);
        ref_kgTotal.val(kgTotal.toFixed(2));


        //ref_montoTotalGanado.val(montoTotalGanado.toFixed(2));

        ref_ImpMachos.val(ImpMachos.toFixed(2));
        ref_ImpHembras.val(ImpHembras.toFixed(2));
        ref_ImpTotal.val(ImpTotal.toFixed(2));
        ref_Percepcion.val(Percepcion);
        ref_CostoExtra.val(CostoExtra);
        ref_Deduccion.val(Deduccion);
        ref_Total.val(Total);
        ref_Pago.val(Pago);
        ref_Pendiente.val(Pendiente);

    }
    function Enviar() {
        var rows = tblVentas.rows('.selected').data();

        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];

            //lo agrego a la tabla jaula para su envio
            tblJaula.row.add({
                "Id_venta": row.Id_venta,
                "Folio": row.Folio,
                "Sucursal": row.Sucursal,
                "CantTotal": row.CantTotal,
                "PesoTotal": row.PesoTotal
            }).draw();
            var z = 0;
            for (var z = 0; z < listaventas.length; z++) {
                if (listaventas[z].Id_venta === row.Id_venta) {
                    tDetalleGanado.row.add({
                        "Id_venta": listaventas[z].Id_venta,
                        "Folio": listaventas[z].Folio,
                        "Sucursal": listaventas[z].Sucursal,
                        "CbzMacho": listaventas[z].CbzMacho,
                        "KgMacho": listaventas[z].KgMacho,
                        "ImporteMacho": listaventas[z].ImporteMacho,
                        "CbzHembra": listaventas[z].CbzHembra,
                        "KgHembra": listaventas[z].KgHembra,
                        "ImporteHembra": listaventas[z].ImporteHembra,
                        "Percepcion": listaventas[z].Percepcion,
                        "CostoExtra": listaventas[z].CostoExtra,
                        "Deduccion": listaventas[z].Deduccion,
                        "Total": listaventas[z].Total

                    }).draw();
                    CbzMachos += listaventas[z].CbzMacho;
                    CbzHembras += listaventas[z].CbzHembra;
                    CbzTotal += listaventas[z].CantTotal;
                    kgHembras += listaventas[z].KgHembra;
                    kgMachos += listaventas[z].KgMacho;
                    kgTotal += listaventas[z].PesoTotal;
                    ImpMachos += listaventas[z].ImporteMacho;
                    ImpHembras += listaventas[z].ImporteHembra;
                    ImpTotal += listaventas[z].ImporteTotal;
                    Percepcion += listaventas[z].Percepcion;
                    CostoExtra += listaventas[z].CostoExtra;
                    Deduccion += listaventas[z].Deduccion;
                    Total += listaventas[z].Total;
                    Pago += listaventas[z].Pago;
                    Pendiente += listaventas[z].Pendiente;
                    ActualizarInputs();
                    break;
                }
            }
        }
        tblVentas.rows('.selected').remove().draw();
    }
    function Validar() {
        var error = false;
        var expRegularHora = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/i;
        var Jaula = tblJaula.rows().data();
        var valHinicio = String(Hinicio);
        var valHfinal = String(HFinal);
        var flagJaula = false, flagHI = false, flagHF = false;

        if (Jaula.length === 0) {
            $("#tblJaula").addClass("errorCID");
            $("#tblJaula").removeClass("successCID");
            $("#validation_summary").find("dd[for='ErrorJaula']").addClass('help-block valid').text('Debe de seleccionar por lo menos una venta.');
            flagJaula = true;
        }
        else {
            $("#tblJaula").addClass("successCID");
            $("#tblJaula").removeClass("errorCID");
            $("#validation_summary").find("dd[for='ErrorJaula']").addClass('help-block valid').text('');
            flagJaula = false;
        }

        var validarExpreHora = expRegularHora.test(valHinicio);
        if (valHinicio === '' || valHinicio.length == 0 || validarExpreHora === false) {
            $("#HSalida").addClass("errorCID");
            $("#HSalida").removeClass("successCID");
            $("#validation_summary").find("dd[for='ErrorHSalida']").addClass('help-block valid').text('Debe seleccionar una hora de salida.');
            flagHI = true;
        } else {
            $("#HSalida").addClass("successCID");
            $("#HSalida").removeClass("errorCID");
            $("#validation_summary").find("dd[for='ErrorHSalida']").addClass('help-block valid').text('');
            flagHI = false;
        }

        var validarExpreHora = expRegularHora.test(valHfinal);
        if (valHfinal === '' || valHfinal.length == 0 || validarExpreHora === false) {
            $("#HLlegada").addClass("errorCID");
            $("#HLlegada").removeClass("successCID");
            $("#validation_summary").find("dd[for='ErrorHLlegada']").addClass('help-block valid').text('Debe seleccionar una hora de llegada.');
            flagHF = true;
        } else {
            $("#HLlegada").addClass("successCID");
            $("#HLlegada").removeClass("errorCID");
            $("#validation_summary").find("dd[for='ErrorHLlegada']").addClass('help-block valid').text('');
            flagHF = false;
            
        }
        console.log("Jaula " + flagJaula);
        console.log("HI " + flagHI);
        console.log("HF " + flagHF);
        if (flagJaula && flagHI && flagHF) {
            error = true;
            
        }
        console.log("este es el resultado de error" + error);
        return error;
    }
    return {
        init: function () {                       
            CalcularTraslado();            
            cargarVehiculo();
            initDataTables();
            tblVentasData();            
            initFuncionesJaula();
            runValidator1();           
        }
    };
}();