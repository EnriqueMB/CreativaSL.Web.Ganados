﻿var FleteRecepcion = function () {
    "use strict"
    var IDFlete = document.getElementById("id_flete").value;
    var tblEventos, tblGanadoCargado, tblGanadoSufrioEvento, IDEventoGeneral, IDEvento;

    /*TABLAS*/
    var LoadTableTiposEventos = function () {
        tblEventos = $('#tblEventos').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDFlete": IDFlete
                },
                "url": "/Admin/Flete/TableJsonFleteEventos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "descripcion" },
                { "data": "cantidad" },
                { "data": "lugar" },
                {
                    "data": "fechaDeteccion",
                    "render": function (data, type, row) {
                        if (data === null)
                            fecha = "Sin dato";
                        else {
                            var dateSplit = data.split('-');
                            var dia = dateSplit[2];
                            dia = dia.split('T');
                            dia = dia[0];
                            var mes = dateSplit[1];
                            var año = dateSplit[0];
                            var fecha = dia + '-' + mes + '-' + año;
                        }

                        return fecha;
                    }
                },
                { "data": "horaDeteccion" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_evento"] + "' class='btn btn-yellow tooltips btn-sm edit' title='Editar'  data-placement='top' data-original-title='Editar'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Flete/DEL_Evento/' title='Eliminar' data-id='" + full["id_evento"] + "' class='btn btn-danger tooltips btn-sm delete' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_evento"] + "' class='edit' data-id='" + full["id_evento"] + "'  role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Flete/DEL_Evento/' class='delete' role='menuitem' tabindex='-1' data-id='" + full["id_evento"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>" +
                            "</ul>" +
                            "</div>" +
                            "</div>";
                    }
                }
            ],
            "drawCallback": function (settings) {
                $(".edit").on("click", function () {
                    var id_evento = $(this).data("id");
                    window.location.href = '/Admin/Flete/AC_FleteEvento?IDFlete=' + IDFlete + '&Id_eventoFlete=' + id_evento;
                });
                $(".delete").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var id = $(this).attr('data-id');
                    var box = $("#mb-delete-evento");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { IDEvento: id, IDFlete: IDFlete },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    $("#ModalImpuesto").modal('hide');
                                    Mensaje(result.Mensaje, "1");
                                    tblEventos.ajax.reload();
                                }
                                else
                                    Mensaje(result.Mensaje, "2");
                            },
                            error: function (result) {
                                Mensaje(result.Mensaje, "2");
                            }
                        });
                    });
                });
            }
        });
    }   
    function LoadTableGanadoCargado() {
        tblGanadoCargado = $('#tblGanadoCargado').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDFlete": IDFlete, "IDEvento": IDEvento
                },
                "url": "/Admin/Flete/TableJsonProductoGanadoCargadoLibreEvento/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_producto" },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoAproximado" }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    }
    function LoadTableGanadoAccidentado() {
        tblGanadoSufrioEvento = $('#tblGanadoSufrioEvento').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDFlete": IDFlete, "IDEvento": IDEvento
                },
                "url": "/Admin/Flete/TableJsonProductoGanadoCargadoConEvento/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_producto" },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoAproximado" }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    }

    /*EVENTOS*/
    var RunEventsEvento = function () {
        $("#btnAddEvento").on("click", function () {
            window.location.href = '/Admin/Flete/AC_FleteEvento?IDFlete=' + IDFlete + '&Id_eventoFlete=0';
        });
        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
        });
    }
    function RunEventsEventoID() {
        var Imagen = document.getElementById("ImagenMostrar").value;
        var ExtensionImagen = document.getElementById("ExtensionImagenBase64").value;
        IDEvento = document.getElementById("IDEvento").value;

        $("#FechaDeteccion").datepicker({
            format: 'dd/mm/yyyy',
            language: 'es',
            autoclose: true
        });
        $('#HoraDetecccion').timepicker({
            minuteStep: 1,
            showMeridian: false
        });
        $('#HttpImagen').fileinput({
            theme: 'fa',
            language: 'es',
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                '<img class="file-preview-image"  style=" width: auto !important; height: auto; max-width: 100%; max-height: 100%;" src="data:' + ExtensionImagen + ' ;base64,' + Imagen + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del evento' }
            ],
            initialPreviewShowDelete: false,
            showRemove: true,
            showClose: true,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png", 'jpg', 'bmp', 'jpeg'],
            required: true
        })
        $('#HttpImagen').on('fileclear', function (event) {
            document.getElementById("ImagenMostrar").value = "";
        });

        /*seleccionar filas*/
        $('#tblGanadoCargado tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblGanadoSufrioEvento tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        /*Pasar y regresar filas en las tablas*/
        $('#enviar').click(function () {
            var rows = tblGanadoCargado.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblGanadoSufrioEvento.row.add({
                    "id_producto": d.id_producto,           //id
                    "numArete": d.numArete,                 //numArete
                    "genero": d.genero,                     //genero
                    "pesoAproximado": d.pesoAproximado      //peso
                }).draw();
            }
            tblGanadoCargado.row('.selected').remove().draw(false);
        });
        $('#regresar').click(function () {
            var rows = tblGanadoSufrioEvento.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblGanadoCargado.row.add({
                    "id_producto": d.id_producto,           //id
                    "numArete": d.numArete,                 //numArete
                    "genero": d.genero,                     //genero
                    "pesoAproximado": d.pesoAproximado      //peso
                }).draw();
            }
            tblGanadoSufrioEvento.row('.selected').remove().draw(false);
        });
    }

    /*MODALES*/
    function ModalEvento(IDEvento) {
        $("body").css("cursor", "progress");
        $.ajax({
            url: '/Admin/Flete/ModalEvento/',
            type: "POST",
            data: { IDFlete: IDFlete, IDEvento: IDEvento },
            success: function (data) {
                $("body").css("cursor", "default");
                $('#ContenidoModalEvento').html(data);
                $('#ModalEvento').modal({ backdrop: 'static', keyboard: false });
                RunEventsEventoID();
                LoadTableGanadoCargado();
                LoadTableGanadoAccidentado();
                LoadValidation_AC_Evento();
                
            }
        });
    }

    /*VALIDACIONES*/
    function LoadValidation_AC_Evento() {
        var form1 = $('#frmEvento');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $(".validation_summary_evento"),
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
                IDTipoEvento: {
                    required: true,
                    min: 1
                },
                Lugar: {
                    required: true
                },
                FechaDeteccion: {
                    required: true
                },
                HoraDetecccion: {
                    required: true
                }
            },
            messages: {
                IDTipoEvento: {
                    required: "-Seleccione un tipo de evento.",
                    min: "-Seleccione un tipo de evento."
                },
                Lugar: {
                    required: "-Seleccione un lugar."
                },
                FechaDeteccion: {
                    required: "-Seleccione una fecha."
                },
                HoraDetecccion: {
                    required: "-Seleccione una hora."
                }
            },
            invalidHandler: function (event, validator) {
                successHandler1.hide();
                errorHandler1.show();
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
            },
            unhighlight: function (element) {
                $(element).closest('.controlError').removeClass('has-error');
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                AC_Evento();
            }
        });
    };
    var LoadValidation_AC_RecepcionDestino = function () {
        var form1 = $('#Frm_AC_RecepcionDestino');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_RecepciónDestino"),
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
                "RecepcionDestino.fechaLlegada": {
                    required: true
                },
                "RecepcionDestino.HoraLlegada": {
                    required: true
                },
                "RecepcionDestino.recibidoPor": {
                    required: true
                },
                "RecepcionDestino.HoraDescarga": {
                    required: true
                },
                "RecepcionDestino.kiloTotalRecibido": {
                    min: 0
                },
                "RecepcionDestino.GanadosTotal": {
                    min: 1
                }
            },
            messages: {
                "RecepcionDestino.fechaLlegada": {
                    required: "Por favor, seleccione una fecha de llegada."
                },
                "RecepcionDestino.HoraLlegada": {
                    required: "Por favor, seleccione una hora de llegada."
                },
                "RecepcionDestino.recibidoPor": {
                    required: "Por favor, escriba el nombre de la persona que recibio la mercancia."
                },
                "RecepcionDestino.HoraDescarga": {
                    required: "Por favor, seleccione una hora de descarga."
                },
                "RecepcionDestino.kiloTotalRecibido": {
                    min: "Por favor, escriba la cantidad de kilos que recibio el lugar destino."
                },
                "RecepcionDestino.GanadosTotal": {
                    min: "Por favor, escriba la cantidad de ganados que recibio el lugar destino."
                }
            },
            invalidHandler: function (event, validator) {
                successHandler1.hide();
                errorHandler1.show();
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
            },
            unhighlight: function (element) {
                $(element).closest('.controlError').removeClass('has-error');
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                AC_RecepcionDestino();
            }
        });
    };
    var LoadValidation_AC_RecepcionOrigen = function () {
        var form1 = $('#Frm_AC_RecepcionOrigen');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_RecepciónOrigen"),
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
                "RecepcionOrigen.FechaLlegada": {
                    required: true
                },
                "RecepcionOrigen.HoraLlegada": {
                    required: true
                },
                "RecepcionOrigen.KilometrajeFinal": {
                    required: true,
                    min: 1
                }
            },
            messages: {
                "RecepcionOrigen.FechaLlegada": {
                    required: "Por favor, seleccione una fecha de llegada."
                },
                "RecepcionOrigen.HoraLlegada": {
                    required: "Por favor, seleccione una hora de llegada."
                },
                "RecepcionOrigen.KilometrajeFinal": {
                    required: "Por favor, escriba el kilometraje final.",
                    min: "Por favor, escriba el kilometraje final."
                }
            },
            invalidHandler: function (event, validator) {
                successHandler1.hide();
                errorHandler1.show();
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
            },
            unhighlight: function (element) {
                $(element).closest('.controlError').removeClass('has-error');
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                AC_RecepcionOrigen();
            }
        });
    };

    /*AC*/
    function AC_Evento() {
        //datos del formulario
        var form = $("#frmEvento")[0];
        var formData = new FormData(form);
        //datos de las tablas
        var ganadoAccidentado = tblGanadoSufrioEvento.rows().data();
        var objGanado, cantidad = 0;
        var listaGanado = new Array();

        for (var i = 0 ; i < ganadoAccidentado.length ; i++)
        {
            listaGanado.unshift(ganadoAccidentado[i].id_producto);
            cantidad = i + 1;
        }
        formData.append('ListaProductosEvento', listaGanado);
        formData.append('Cantidad', cantidad);

        formData.set('HoraDetecccion', ObtenerFecha(formData.get('HoraDetecccion')));

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/AC_Evento/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
                $("#ModalEvento").modal('hide');
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje(response.Mensaje, "1");
                    $("#ModalEvento").modal('hide');
                    tblEventos.ajax.reload();
                }
                else {
                    Mensaje(response.Mensaje, "2");
                    $("#ModalEvento").modal('hide');
                }
            }
        });
    }
    function AC_RecepcionDestino() {
        //datos del formulario
        var form = $("#Frm_AC_RecepcionDestino")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/AC_RecepcionDestino/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                var json = JSON.parse(response.Mensaje);
                if (response.Success) {
                    Mensaje(json.Mensaje, "1");
                }
                else {
                    Mensaje(json.Mensaje, "2");
                }
            }
        });
    }
    function AC_RecepcionOrigen() {
        //datos del formulario
        var form = $("#Frm_AC_RecepcionOrigen")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/AC_RecepcionOrigen/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                 var json = JSON.parse(response.Mensaje);
                if (response.Success) {
                    Mensaje(json.Mensaje, "1");
                }
                else {
                    Mensaje(json.Mensaje, "2");
                }
            }
        });
    }

    /*OTRAS FUNCIONES*/
    function ObtenerFecha(fechaFormulario) {
        var arrayTime = fechaFormulario.split(':');
        var arrayAMPM = arrayTime[1].split(' ');
        var intHora = parseInt(arrayTime[0]);
        var hora = (arrayAMPM[1] == 'PM') ? intHora + 12 : intHora;
        var minuto = arrayAMPM[0];
        var nuevaHora = hora + ':' + minuto + ':00';

        return nuevaHora;
    }


    return {
        init: function (ID_Flete) {
            LoadTableTiposEventos();
            RunEventsEvento();
            LoadValidation_AC_RecepcionDestino();
            LoadValidation_AC_RecepcionOrigen();
        }
    };
}();