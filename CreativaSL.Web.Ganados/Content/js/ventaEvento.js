var VentaEvento = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();
    var Id_eventoVenta = $("#Id_eventoVenta").val();
    var opcionDeduccion = $("#AplicaDeduccion").val();
    var opcionGanado = $("#AplicaGanado").val();
    var tblGanadoCargado, tblGanadoConEvento;
    var MontoDeduccion = $("#MontoDeduccion");

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
            allowedFileExtensions: ['png'],
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
            layoutTemplates: { actionDelete: '' }
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
        
        ToggleDivDeduccion(opcionDeduccion);
        ToggleDivGanado(opcionGanado);

        tblGanadoCargado = $('#tblGanadoCargado').DataTable({
            "language": {
                "url": "../../Content/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_venta": Id_venta
                },
                "url": "/Admin/Venta/DatatableGanadoVendidoVivo/",
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
                    "Id_venta": Id_venta, "Id_eventoVenta": Id_eventoVenta
                },
                "url": "/Admin/Venta/DatatableGanadoConEvento/",
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
                    "id_ganado": d.id_ganado,               
                    "numArete": d.numArete,                 
                    "genero": d.genero,                     
                    "pesoPagado": d.pesoPagado,     
                    "precioKilo": d.precioKilo,
                    "subtotal": d.subtotal
                }).draw();
            }
            tblGanadoCargado.row('.selected').remove().draw(false);
        });
        $('#regresar').click(function () {
            var rows = tblGanadoConEvento.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblGanadoCargado.row.add({
                    "id_ganado": d.id_ganado,               
                    "numArete": d.numArete,                 
                    "genero": d.genero,                     
                    "pesoPagado": d.pesoPagado,
                    "precioKilo": d.precioKilo,
                    "subtotal": d.subtotal
                }).draw();
            }
            tblGanadoConEvento.row('.selected').remove().draw(false);
        });
    }
    var initValidaciones = function () {
        var form1 = $('#frm_evento');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({
            errorElement: "dd",
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
                Id_tipoEvento: { min: 1 },
                Lugar: { required: true },
                FechaDeteccion: { required: true },
                HoraDeteccion: { required: true },
                MontoDeduccion: { required: true, min: 1 }
            },
            messages: {
                Id_tipoEvento: { min: "Por favor, seleccione un tipo de evento." },
                Lugar: { required: "Por favor, escriba el lugar del evento" },
                FechaDeteccion: { required: "Por favor, seleccione la fecha de detección del evento." },
                HoraDeteccion: { required: "Por favor, seleccione la hora de detección del evento." },
                MontoDeduccion: { required: "Por favor, escriba el monto de la deducción.", min: "Por favor, escriba un número en el monto de deducción." }
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
                AC_Evento();
            }
        });
    };
    function AC_Evento() {

    }
    function ToggleDivDeduccion(opcion) {
        if (opcion.localeCompare("true") == 0) {
            AgregarValidacionesDeduccion();
            $('#divDeducciones').show(1000);
        }
        else if(opcion.localeCompare("false") == 0) {
            QuitarValidacionesDeduccion();
            $('#divDeducciones').hide(1000);
        }
    }
    function ToggleDivGanado(opcion) {
        if (opcion.localeCompare("true") == 0) {
            //AgregarValidacionesGanado();
            $('#divGanado').show(1000);
        }
        else if (opcion.localeCompare("false") == 0) {
            //QuitarValidacionesGanado();
            $('#divGanado').hide(1000);
        }
    }
    function AgregarValidacionesDeduccion() {
        MontoDeduccion.rules("add", { min: 1 });
    }
    function QuitarValidacionesDeduccion() {
        MontoDeduccion.rules("remove", "min");
        MontoDeduccion.closest(".form-group").removeClass("has-success has-error");
        MontoDeduccion.find("dd[for='MontoDeduccion']").addClass('help-block valid').text('');
    }
    function AgregarValidacionesGanado() {
        //datos de las tablas
        var ganado = tblGanadoJaula.rows().data();
        var objGanado, cantidad = 0;
        var listaGanado = new Array();

        if (ganado.length == 0) {
            $("#tblGanadoJaula").addClass("errorTableCSL");
            $("#validation_summary").append("");
            $("#validation_summary").append("<dd style='color: #ff004d !important; '>-Debe de seleccionar un ganado para su venta</dd>");
        }
        else {

            $("#validation_summary").append("");

            for (var i = 0 ; i < ganado.length ; i++) {
                listaGanado.unshift(ganado[i].id_ganado);
                cantidad = i + 1;
            }

            var formData = new FormData();
            //datos de las tablas
            formData.append('IDVenta', Id_venta);
            formData.append('ListaIDGanadosParaVender', listaGanado);

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

        MontoDeduccion.rules("add", { min: 1 });
    }

    /*TERMINA EVENTO*/

    return {
        init: function () {
            initValidaciones();
            initFuncionesEvento();
        }
    };
}();