var CompraEvento = function () {
    "use strict";
    var Id_compra = $("#Id_compra").val();
    var Id_eventoCompra = $("#Id_eventoCompra").val();
    var tblGanadoConEvento, tblGanadoCargado;
    var validation_summary = $("#validation_summary");
    var nNodes;
    var NUM_ARETE = 0, GENERO = 1, PESO = 2, ANTIGUO_COSTO_POR_KILO = 3, NUEVO_COSTO_POR_KILO = 4, TOTAL = 5;
    var NUM_ELEMENTOS_FILA = 6;
    
    /*INICIA EVENTO*/
    var initFuncionesEvento = function () {
        var Imagen = document.getElementById("ImagenBase64").value;

        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
            //,defaultTime: hora
        });
        $('#HttpImagen').fileinput({
            theme: 'fa',
            language: 'es',
            maxFileCount: 1,
            overwriteInitial: true,
            showUploadedThumbs: false,
            initialPreview: [
                '<img class="file-preview-image"  style=" width: auto !important; height: auto; max-width: 100%; max-height: 100%;" src="' +
                Imagen +
                '" />'
            ],
            initialPreviewConfig: [
               { caption: 'Imagen' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            showUpload: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ['png', 'jpg', 'gif', 'jpeg', 'heic'],
            previewFileIcon: '<i class="fa fa-file"></i>',
            preferIconicPreview: true, // this will force thumbnails to display icons for following file extensions
            previewFileIconSettings: { // configure your icon file extensions
                'heic': '<i class="fa fa-file-text text-primary"></i>'
            },
            previewFileExtSettings: { // configure the logic for determining icon file extensions
                'heic': function (ext) {
                    return ext.match(/(heic)$/i);
                }
            }
        });
        $('#HttpImagen').on('fileclear', function (event) {
            document.getElementById("ImagenMostrar").value = "";
        });

        tblGanadoCargado = $('#tblGanadoCargado').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDCompra": Id_compra
                },
                "url": "/Admin/Compra/DatatableGanadoDisponibleXIDCompra/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_ganado" },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoPagado" },
                { "data": "precioKilo" },
                { "data": "subtotal" }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [3],
                    render: function (data, type, row) {
                        var value = data + " kg.";
                        return value;
                    }
                },
                {
                    "targets": [4],
                    render: $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "targets": [5],
                    render: $.fn.dataTable.render.number(',', '.', 2, '$')
                }
            ]
        });

        tblGanadoConEvento = $('#tblGanadoConEvento').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "searching": false,
            "ordering": false,
            "autoWidth": false,
            columnDefs: [
                { "type": "html-input", "targets": [1, 2, 3, 4, 5] }
            ],
            "drawCallback": function (settings) {
                $(".kg").maskMoney(
                    {
                        allowZero: true,
                        precision: 0,
                        suffix: ' kg'
                    }
                );
                $(".money").maskMoney(
                    {
                        allowZero: true,
                        precision: 2,
                        prefix: '$ '
                    }
                );
            }

        });

        $(document).on('submit', 'form#frm_evento', function (e) {
            e.preventDefault();

            var Id_tipoEvento = $("#Id_tipoEvento");
            var HoraDeteccion = $("#HoraDeteccion");
            var Lugar = $("#Lugar");
            var FechaDeteccion = $("#FechaDeteccion");
            var tblGanado = $("#tblGanadoConEvento");
            var Id_TipoDeDeduccion = $("#Id_TipoDeDeduccion");

            var expRegularFecha = /^([0-2][0-9]|3[0-1])(\/)(0[1-9]|1[0-2])\2(\d{4})$/i;
            var expRegularHora = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/i;
            var expRegularMonto = /^0[.][0-9]{1,2}$|[1-9]([0-9]+)?([.][0-9]{1,2})?$/i;

            var opcion_Id_tipoEvento = Id_tipoEvento.val();
            var value_HoraDeteccion = HoraDeteccion.val();
            var value_Lugar = Lugar.val();
            var value_FechaDeteccion = FechaDeteccion.val();
            
            var opcion_Id_TipoDeDeduccion = Id_TipoDeDeduccion.val();
            var flag_errorTipoEvento = 0;
            var flag_errorLugar = 0;
            var flag_errorFecha = 0;
            var flag_errorHora = 0;
            var flag_ganado = 1;
            var flag_errorGanado = 0;

            opcion_Id_tipoEvento = String(opcion_Id_tipoEvento);
            value_HoraDeteccion = String(value_HoraDeteccion);
            value_Lugar = String(value_Lugar);
            value_FechaDeteccion = String(value_FechaDeteccion);
            opcion_Id_TipoDeDeduccion = String(opcion_Id_TipoDeDeduccion);

            if (opcion_Id_tipoEvento.localeCompare("0") === 0) {
                Id_tipoEvento.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddTipoEvento").show(0);
                flag_errorTipoEvento = 1;
            } else {
                Id_tipoEvento.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddTipoEvento").hide(0);
                flag_errorTipoEvento = 0;
            }

            if (value_Lugar === '' || value_Lugar.length === 0) {
                Lugar.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddLugar").show(0);
                flag_errorLugar = 1;
            } else {
                Lugar.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddLugar").hide(0);
                flag_errorLugar = 0;
            }

            var validarExpreFecha = expRegularFecha.test(value_FechaDeteccion);
            if (value_FechaDeteccion === '' || value_FechaDeteccion.length === 0 || validarExpreFecha === false) {
                FechaDeteccion.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddFecha").show(0);
                flag_errorFecha = 1;
            } else {
                FechaDeteccion.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddFecha").hide(0);
                flag_errorFecha = 0;
            }

            var validarExpreHora = expRegularHora.test(value_HoraDeteccion);
            if (value_HoraDeteccion === '' || value_HoraDeteccion.length === 0 || validarExpreHora === false) {
                HoraDeteccion.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddHora").show(0);
                flag_errorHora = 1;
            } else {
                HoraDeteccion.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddHora").hide(0);
                flag_errorHora = 0;
            }

            if (typeof nNodes === "undefined" || nNodes.length === 0)
            {
                $("#tblGanadoConEvento").removeClass('successTableCSL').addClass("errorTableCSL");
                $("#ddTablaGanado").show(0);
                flag_errorGanado = 1;
            }
            else
            {
                $("#tblGanadoConEvento").removeClass('errorTableCSL').addClass("successTableCSL");
                $("#ddTablaGanado").hide(0);
                flag_errorGanado = 0;
            }

            if(flag_errorGanado === 0)
            {
                for (var i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA)
                {
                    var parse_nuevoCosto = Number.parseFloat(GetMoneySinSimbolo(nNodes[i + NUEVO_COSTO_POR_KILO].value).toString().replace(/,/g, "")).toFixed(2);
                    
                    if (isNaN(parse_nuevoCosto))
                    {
                        nNodes[i + NUEVO_COSTO_POR_KILO].classList.remove('okCSLGanado');
                        nNodes[i + NUEVO_COSTO_POR_KILO].classList.add('errorCSLGanado');
                        flag_ganado = 0;
                    }
                    else
                    {
                        var validarExpreMonto = expRegularMonto.test(parse_nuevoCosto);
                        //validando con la expresión regular positivo, con solo 2 digitos 
                        if (validarExpreMonto === false) {
                            nNodes[i + NUEVO_COSTO_POR_KILO].classList.remove('okCSLGanado');
                            nNodes[i + NUEVO_COSTO_POR_KILO].classList.add('errorCSLGanado');
                            flag_ganado = 0;
                        }
                        else {
                            nNodes[i + NUEVO_COSTO_POR_KILO].classList.remove('errorCSLGanado');
                            nNodes[i + NUEVO_COSTO_POR_KILO].classList.add('okCSLGanado');
                        }
                    }
                }

                if (flag_ganado === 1)
                {
                    $("#ddGanado").hide(0);
                }
                else
                {
                    $("#ddGanado").show(0);
                }
            }            

            if (flag_errorTipoEvento === 0 && flag_errorLugar === 0 && flag_errorFecha === 0 && flag_errorHora === 0 && flag_ganado === 1 && flag_errorGanado === 0) {
                //todo bien, obtenemos los id de los ganados y el nuevo precio 
                var ListaIDGanadosDelEvento = new Array();
                var ListaNuevoPrecioGanado = new Array();
                var cantidad = 0;

                for (i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA) {
                    //var nuevo_costo = nNodes[i + NUEVO_COSTO_POR_KILO].value;
                    parse_nuevoCosto = Number.parseFloat(GetMoneySinSimbolo(nNodes[i + NUEVO_COSTO_POR_KILO].value).toString().replace(/,/g, "")).toFixed(2);

                    var id_ganado = nNodes[i + NUM_ARETE].dataset.id;
                    var item = id_ganado + "|" + parse_nuevoCosto;

                    ListaIDGanadosDelEvento.unshift(item);
                    cantidad = cantidad + 1;
                }

                var form = $('form#frm_evento')[0];
                var formData = new FormData(form);

                formData.append('ListaIDGanadosDelEvento', ListaIDGanadosDelEvento);
                formData.append('Cantidad', cantidad);

                $.ajax({
                    type: 'POST',
                    data: formData,
                    url: '/Admin/Compra/EventoCompra/',
                    contentType: false,
                    processData: false,
                    cache: false,
                    success: function (response) {
                        window.location.href = '/Admin/Compra/RecepcionCompra?IDCompra=' + Id_compra;
                    },
                    error: function (request, status, error) {
                        window.location.href = '/Admin/Compra/RecepcionCompra?IDCompra=' + Id_compra;
                    }
                });
            }
        });

        $('#tblGanadoConEvento tbody').on('change', '.inputCSL', function (e) {
            var $td = $(this).parent();
            
            $td.find('input').attr('value', this.value);

            //obtenemos el valor de toda la fila
            var $tr = $(this).closest("tr");
            var fila = $tr.find('.cslElegido');
            calculosGanado(fila);
        });

        /*seleccionar filas*/
        $('#tblGanadoCargado tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblGanadoConEvento tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });

        $('#enviar').click(function () {
            var rows = tblGanadoCargado.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];
                var subtotal = Number.parseFloat(d.subtotal.toString().replace(/,/g, "")).toFixed(2);

                AgergarFilas
                (

                    d.id_ganado, d.numArete, d.genero,
                    Number.parseFloat(d.pesoPagado).toFixed(0),
                    Number.parseFloat(d.precioKilo).toFixed(2),
                    subtotal
                );

            }
            tblGanadoCargado.rows('.selected').remove().draw(false);
        });

        $('#regresar').click(function () {
            var row = tblGanadoConEvento.rows('.selected').nodes().to$().find('.cslElegido');
            //obtenemos el valor de toda la fila
            for (var x = 0; x < row.length; x += 6)
            {
                var subtotal = Number.parseFloat(row[x + 5].dataset.total).toFixed(2);

                tblGanadoCargado.row.add({
                    "id_ganado": row[x].dataset.id,
                    "numArete": row[x].value,
                    "genero": row[x + 1].value,
                    "pesoPagado": GetKilosSinSimbolo(row[x + 2].value),
                    "precioKilo": GetMoneySinSimbolo(row[x + 3].value),
                    "subtotal": subtotal
                }).draw();
            }

            tblGanadoConEvento.rows('.selected').remove().draw(false);
        });
    }

    function AgergarFilas( id_fila, numArete, genero, peso, costoxkilo, total) {
        //columna, numArete
        var html_arete = '<input id="arete_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslElegido" type="text" value="' + numArete + '" data-toggle="tooltip" data-placement="top" title="Número de arete." readonly="readonly">';
        //columna, genero
        var html_genero = '<input id="genero_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslElegido" type="text" value="' + genero + '" data-toggle="tooltip" data-placement="top" title="Género del ganado." readonly="readonly">';
        //columna, peso
        var html_peso = '<input id="peso_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslElegido kg" type="text" value="' + peso + '" data-toggle="tooltip" data-placement="top" title="Peso pagado por kilo." readonly="readonly">';
        //columna, precio por kilo anterior
        var html_precioXkiloAnterior = '<input id="precioXkiloAnterior_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslElegido money" type="text"  value="' + costoxkilo + '" data-toggle="tooltip" data-placement="top" title="Antiguo costo por kilo." readonly="readonly">';
        //columna, precio por kilo nuevo
        var html_precioXkiloNuevo = '<input id="precioXkiloNuevo_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido money" type="text"  value="' + costoxkilo + '" data-toggle="tooltip" data-placement="top" title="Nuevo costo por kilo.">';
        //columna, total
        var html_total = '<input id="total_' + id_fila + '" data-id="' + id_fila + '" data-total = "' + total + '" class="form-control cslElegido money" type="text" value="' + total + '" data-toggle="tooltip" data-placement="top" title="Total del ganado." readonly="readonly">';

        tblGanadoConEvento.row.add([
            html_arete,
            html_genero,
            html_peso,
            html_precioXkiloAnterior,
            html_precioXkiloNuevo,
            html_total
        ]).draw(false);

        nNodes = tblGanadoConEvento.rows().nodes().to$().find('.cslElegido');

        $(".kg").maskMoney('mask');
        $(".money").maskMoney('mask');
    }

    function calculosGanado(fila) {
        var nuevo_costo = Number.parseFloat(GetMoneySinSimbolo(fila[NUEVO_COSTO_POR_KILO].value)).toFixed(2);
        var peso_ganado = Number.parseFloat(GetKilosSinSimbolo(fila[PESO].value)).toFixed(0);

        nuevo_costo = Number.parseFloat(nuevo_costo);
        var nuevo_total;

        if (isNaN(nuevo_costo))
        {
            nuevo_total = 0;
        }
        else
        {
            nuevo_total = (nuevo_costo * peso_ganado).toFixed(2);
        }

        fila[TOTAL].value = nuevo_total;
        $(".kg").maskMoney('mask');
        $(".money").maskMoney('mask');

    }

    function GetKilosSinSimbolo(value) {
        var newValue = value.split(" ", 1);

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }

    function GetMoneySinSimbolo(value) {
        var newValue = value.split(" ", 2);
        newValue = newValue[1].toString().replace(/,/g, '');

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }


    /*TERMINA EVENTO*/

    return {
        init: function () {
            initFuncionesEvento();

            validation_summary.append("<dd id='ddTipoEvento' style='color: #ff004d !important; '>Por favor, seleccione un tipo de evento.</dd>");
            validation_summary.append("<dd id='ddLugar' style='color: #ff004d !important; '>Por favor, escriba un lugar del evento.</dd>");
            validation_summary.append("<dd id='ddFecha' style='color: #ff004d !important; '>Por favor, seleccione una fecha del evento.</dd>");
            validation_summary.append("<dd id='ddHora' style='color: #ff004d !important; '>Por favor, seleccione una hora del evento.</dd>");
            validation_summary.append("<dd id='ddGanado' style='color: #ff004d !important; '>Por favor, escriba el nuevo precio del ganado.</dd>");
            validation_summary.append("<dd id='ddTablaGanado' style='color: #ff004d !important; '>Por favor, seleccione un ganado para el evento.</dd>");

            $("#ddTipoEvento").hide(0);
            $("#ddLugar").hide(0);
            $("#ddFecha").hide(0);
            $("#ddHora").hide(0);
            $("#ddGanado").hide(0);
            $("#ddTablaGanado").hide(0);

        }
    };
}();