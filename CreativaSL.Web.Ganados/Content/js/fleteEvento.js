﻿var FleteEvento = function () {
    "use strict";
    var Id_flete = $("#Id_flete").val();
    var Id_eventoFlete = $("#Id_eventoFlete").val();
    var opcionDeduccion = $("#AplicaDeduccion").val();
    var opcionGanado = $("#AplicaGanado").val();
    var tblGanadoCargado, tblGanadoConEvento;
    var MontoDeduccion = $("#MontoDeduccion");
    var validation_summary = $("#validation_summary");

    /*INICIA EVENTO*/
    var initFuncionesEvento = function () {
        var Imagen = document.getElementById("ImagenMostrar").value;
        var ExtensionImagen = document.getElementById("ExtensionImagenBase64").value;

        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
        });
        $('#HttpImagen').fileinput({
            theme: 'fa',
            language: 'es',
            maxFileCount: 1,
            overwriteInitial: true,
            showUploadedThumbs: false,
            initialPreview: [
                '<img class="file-preview-image"  style=" width: auto !important; height: auto; max-width: 100%; max-height: 100%;" src="data:' + ExtensionImagen + ' ;base64,' + Imagen + '" />'
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
        $('#AplicaDeduccion').on('change', function (event) {
            var opcion = $(this).val();
            ToggleDivDeduccion(opcion);
        });
        $('#AplicaGanado').on('change', function (event) {
            var opcion = $(this).val();
            ToggleDivGanado(opcion);
        });
        
        tblGanadoCargado = $('#tblGanadoCargado').DataTable({
            "language": {
                "url": "../../Content/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_flete": Id_flete, "Id_eventoFlete": Id_eventoFlete
                },
                "url": "/Admin/Flete/DatatableGanadoEnFlete/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_producto" },
                { "data": "id_ganado" },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoAproximado" },
                { "data": "propio" }
            ],
            "columnDefs": [
                {
                    "targets": [0, 1],
                    "visible": false,
                    "searchable": false
                }
            ]

        });
        tblGanadoConEvento = $('#tblGanadoConEvento').DataTable({
            "language": {
                "url": "../../Content/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_flete": Id_flete, "Id_eventoFlete": Id_eventoFlete
                },
                "url": "/Admin/Flete/DatatableGanadoConEvento/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_producto" },
                { "data": "id_ganado" },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoAproximado" },
                { "data": "propio" },
            ],
            "columnDefs": [
                {
                    "targets": [0, 1],
                    "visible": false,
                    "searchable": false
                }
            ]

        });

        /*seleccionar filas*/
        $('#tblGanadoCargado tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblGanadoConEvento tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        /*Pasar y regresar filas en las tablas*/
        $('#enviar').click(function () {
            var rows = tblGanadoCargado.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblGanadoConEvento.row.add({
                    "id_producto": d.id_producto,
                    "id_ganado": d.id_ganado,               
                    "numArete": d.numArete,                 
                    "genero": d.genero,                     
                    "pesoAproximado": d.pesoAproximado,     
                    "propio": d.propio
                }).draw();
            }
            tblGanadoCargado.row('.selected').remove().draw(false);
        });
        $('#regresar').click(function () {
            var rows = tblGanadoConEvento.rows('.selected').data();
            var rowsGanadoCargado = tblGanadoCargado.rows().data();
            var flag_mismoGanado = false;

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                for (var x = 0 ; x < rowsGanadoCargado.length ; x++) {
                    var ganadoCargado = rowsGanadoCargado[x];

                    if (ganadoCargado.id_ganado == d.id_ganado) {
                        flag_mismoGanado = true;
                        break;
                    }
                }
                if(flag_mismoGanado === false){
                    tblGanadoCargado.row.add({
                        "id_producto": d.id_producto,
                        "id_ganado": d.id_ganado,
                        "numArete": d.numArete,
                        "genero": d.genero,
                        "pesoAproximado": d.pesoAproximado,
                        "propio": d.propio
                    }).draw();
                }
                else {
                    flag_mismoGanado = false;
                }

            }
            tblGanadoConEvento.row('.selected').remove().draw(false);
        });
        $(document).on('submit', 'form#frm_evento', function (e) {
            e.preventDefault();

            var Id_tipoEvento = $("#Id_tipoEvento");
            var Id_conceptoDocumento = $("#Id_conceptoDocumento");
            var HoraDeteccion = $("#HoraDeteccion");
            var Lugar = $("#Lugar");
            var FechaDeteccion = $("#FechaDeteccion");
            var MontoDeduccion = $("#MontoDeduccion");
            var tblGanado = $("#tblGanadoConEvento");
            var Id_TipoDeDeduccion = $("#Id_TipoDeDeduccion");
            var ganado = tblGanadoConEvento.rows().data();

            var expRegularFecha = /^([0-2][0-9]|3[0-1])(\/)(0[1-9]|1[0-2])\2(\d{4})$/i;
            var expRegularHora = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/i;
            var expRegularMonto = /^([0-9]+)?([.][0-9]{1,2})?$/i;

            var opcion_Id_tipoEvento = Id_tipoEvento.val();
            var opcion_Id_conceptoDocumento = Id_conceptoDocumento.val();
            var value_HoraDeteccion = HoraDeteccion.val();
            var value_Lugar = Lugar.val();
            var value_FechaDeteccion = FechaDeteccion.val();
            var value_MontoDeduccion = MontoDeduccion.val();
            var opcion_Id_TipoDeDeduccion = Id_TipoDeDeduccion.val();
            var flag_errorTipoEvento = 0;
            var flag_errorConceptoDocumento = 0;
            var flag_errorLugar = 0;
            var flag_errorFecha = 0;
            var flag_errorHora = 0;
            var flag_errorDeduccion = 0;
            var flag_errorGanado = 0;
            var flag_errorTipoDeduccion = 0;

            opcion_Id_tipoEvento = String(opcion_Id_tipoEvento);
            value_HoraDeteccion = String(value_HoraDeteccion);
            value_Lugar = String(value_Lugar);
            value_FechaDeteccion = String(value_FechaDeteccion);
            opcion_Id_TipoDeDeduccion = String(opcion_Id_TipoDeDeduccion);
            opcion_Id_conceptoDocumento = String(opcion_Id_conceptoDocumento);

            if (opcion_Id_tipoEvento.localeCompare("0") == 0) {
                Id_tipoEvento.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddTipoEvento").show(0);
                flag_errorTipoEvento = 1;
            } else {
                Id_tipoEvento.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddTipoEvento").hide(0);
                flag_errorTipoEvento = 0;
            }

            if (opcion_Id_conceptoDocumento.localeCompare("0") == 0) {
                Id_conceptoDocumento.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddConceptoDocumento").show(0);
                flag_errorConceptoDocumento = 1;
            } else {
                Id_conceptoDocumento.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddConceptoDocumento").hide(0);
                flag_errorConceptoDocumento = 0;
            }

            if (value_Lugar === '' || value_Lugar.length == 0) {
                Lugar.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddLugar").show(0);
                flag_errorLugar = 1;
            } else {
                Lugar.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddLugar").hide(0);
                flag_errorLugar = 0;
            }

            var validarExpreFecha = expRegularFecha.test(value_FechaDeteccion);
            if (value_FechaDeteccion === '' || value_FechaDeteccion.length == 0 || validarExpreFecha === false) {
                FechaDeteccion.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddFecha").show(0);
                flag_errorFecha = 1;
            } else {
                FechaDeteccion.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddFecha").hide(0);
                flag_errorFecha = 0;
            }

            var validarExpreHora = expRegularHora.test(value_HoraDeteccion);
            if (value_HoraDeteccion === '' || value_HoraDeteccion.length == 0 || validarExpreHora === false) {
                HoraDeteccion.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddHora").show(0);
                flag_errorHora = 1;
            } else {
                HoraDeteccion.closest('.controlError').removeClass('has-error').addClass("has-success");
                $("#ddHora").hide(0);
                flag_errorHora = 0;
            }

            //validando monto
            if (isNaN(value_MontoDeduccion)) {
                MontoDeduccion.closest('.controlError').removeClass('has-success').addClass("has-error");
                $("#ddMonto").show(0);
                flag_errorDeduccion = 1;
            }
            else {
                var validarExpreMonto = expRegularMonto.test(value_MontoDeduccion);

                //validando con la expresión regular positivo, con solo 2 digitos 
                if (validarExpreMonto === false) {
                    MontoDeduccion.closest('.controlError').removeClass('has-success').addClass("has-error");
                    $("#ddMonto").show(0);
                    flag_errorDeduccion = 1;
                }
                else {
                    MontoDeduccion.closest('.controlError').removeClass('has-error').addClass("has-success");
                    $("#ddMonto").hide(0);
                    flag_errorDeduccion = 0;
                }

                if (opcion_Id_TipoDeDeduccion.localeCompare("0") == 0) {
                    Id_TipoDeDeduccion.closest('.controlError').removeClass('has-success').addClass("has-error");
                    $("#ddTipoDeduccion").show(0);
                    flag_errorTipoDeduccion = 1
                } else {
                    Id_TipoDeDeduccion.closest('.controlError').removeClass('has-error').addClass("has-success");
                    $("#ddTipoDeduccion").hide(0);
                    flag_errorTipoDeduccion = 0;
                }
            }

            if (ganado.length == 0) {
                tblGanado.removeClass('successTableCSL').addClass("errorTableCSL");
                $("#ddGanado").show(0);
                flag_errorGanado = 1;
            }
            else {
                tblGanado.removeClass('errorTableCSL').addClass("successTableCSL");
                $("#ddGanado").hide(0);
                flag_errorGanado = 0;
            }

            if (flag_errorTipoEvento == 0 &&
                flag_errorLugar == 0 &&
                flag_errorFecha == 0 &&
                flag_errorHora == 0 &&
                flag_errorDeduccion == 0 &&
                flag_errorGanado == 0 &&
                flag_errorTipoDeduccion == 0 &&
                flag_errorConceptoDocumento == 0) {
                var ganado = tblGanadoConEvento.rows().data();
                var objGanado, cantidad = 0;
                var ListaIDGanadosDelEvento = new Array();

                for (var i = 0 ; i < ganado.length ; i++) {
                    ListaIDGanadosDelEvento.unshift(ganado[i].id_producto);
                    cantidad = i + 1;
                }

                var form = $('form#frm_evento')[0];
                var formData = new FormData(form);

                formData.append('ListaIDGanadosDelEvento', ListaIDGanadosDelEvento);
                formData.append('Cantidad', cantidad);

                $.ajax({
                    type: 'POST',
                    data: formData,
                    url: '/Admin/Flete/AC_FleteEvento/',
                    contentType: false,
                    processData: false,
                    cache: false,
                    success: function (response) {
                        window.location.href = '/Admin/Flete/AC_FleteRecepcion?IDFlete=' + Id_flete;
                    },
                    error: function (request, status, error) {
                        window.location.href = '/Admin/Flete/AC_FleteRecepcion?IDFlete=' + Id_flete;
                    }
                });
            }
        });
    }


    /*TERMINA EVENTO*/

    return {
        init: function () {
            initFuncionesEvento();

            validation_summary.append("<dd id='ddTipoEvento' style='color: #ff004d !important; '>Por favor, seleccione un tipo de evento.</dd>");
            validation_summary.append("<dd id='ddLugar' style='color: #ff004d !important; '>Por favor, escriba un lugar del evento.</dd>");
            validation_summary.append("<dd id='ddFecha' style='color: #ff004d !important; '>Por favor, seleccione una fecha del evento.</dd>");
            validation_summary.append("<dd id='ddHora' style='color: #ff004d !important; '>Por favor, seleccione una hora del evento.</dd>");
            validation_summary.append("<dd id='ddMonto' style='color: #ff004d !important; '>Por favor, escriba un monto para la deducción.</dd>");
            validation_summary.append("<dd id='ddGanado' style='color: #ff004d !important; '>Por favor, seleccione un ganado para aplicarle el evento.</dd>");
            validation_summary.append("<dd id='ddTipoDeduccion' style='color: #ff004d !important; '>Por favor, seleccione un tipo de deducción.</dd>");
            validation_summary.append("<dd id='ddConceptoDocumento' style='color: #ff004d !important; '>Por favor, seleccione un concepto.</dd>");

            $("#ddTipoEvento").hide(0);
            $("#ddLugar").hide(0);
            $("#ddFecha").hide(0);
            $("#ddHora").hide(0);
            $("#ddMonto").hide(0);
            $("#ddGanado").hide(0);
            $("#ddTipoDeduccion").hide(0);
            $("#ddConceptoDocumento").hide(0);
        }
    };
}();