var CompraTransaccion = function () {
    "use strict"
    //datatables
    var tblDocumentoPorCobrarDetalles;
    //otros
    var id_padre = $("#IDCompra").val();
    var opcion = 1;

    /*INICIA COBROS*/
    var LoadTableDocumentoPorCobrarDetalles = function () {

        //tblDocumentoPorCobrarDetalles = $('#tbl_documentosPorCobrarDetalles').DataTable({
        //    "language": {
        //        "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
        //    },
        //    responsive: true,
        //    "ajax": {
        //        "data": {
        //            "IDCompra": IDCompra
        //        },
        //        "url": "/Admin/Compra/TableJsonDocumentosDetalles/",
        //        "type": "POST",
        //        "datatype": "json",
        //        "dataSrc": ''
        //    },
        //    "columns": [
        //        { "data": "tipo" },
        //        { "data": "producto" },
        //        { "data": "cantidad" },
        //        { "data": "precioUnitario" },
        //        { "data": "subtotal" },
        //        {
        //            "data": null,
        //            "render": function (data, type, full) {

        //                return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
        //                    "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
        //                    "<a data-hrefa='/Admin/Flete/DEL_Documento/' title='Eliminar' data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-danger tooltips btn-sm deleteDocumento' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
        //                    "</div>" +
        //                    "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
        //                    "<div class='btn-group'>" +
        //                    "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
        //                    "<i class='fa fa-cog'></i> <span class='caret'></span>" +
        //                    "</a>" +
        //                    "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
        //                    "<li>" +
        //                    "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='editDocumento' role='menuitem' tabindex='-1'>" +
        //                    "<i class='fa fa-edit'></i> Editar" +
        //                    "</a>" +
        //                    "</li>" +
        //                    "<li>" +
        //                    "<a data-hrefa='/Admin/Flete/DEL_Documento/' class='deleteDocumento' role='menuitem' tabindex='-1' data-id='" + full["id_detalleDoctoCobrar"] + "'>" +
        //                    "<i class='fa fa-trash-o'></i> Eliminar" +
        //                    "</a>" +
        //                    "</li>" +
        //                    "</ul>" +
        //                    "</div>" +
        //                    "</div>";
        //            }
        //        }
        //    ],
        //    "drawCallback": function (settings) {
        //        $(".editDocumento").on("click", function () {
        //            var IDDocumento = $(this).data("id");

        //            ModalDocumento(IDFlete, IDDocumento);
        //        });
        //        $(".deleteDocumento").on("click", function () {
        //            var url = $(this).attr('data-hrefa');
        //            var row = $(this).attr('data-id');
        //            var box = $("#mb-deleteDocumento");
        //            box.addClass("open");
        //            box.find(".mb-control-yes").on("click", function () {
        //                box.removeClass("open");
        //                $.ajax({
        //                    url: url,
        //                    data: { IDDocumento: row },
        //                    type: 'POST',
        //                    dataType: 'json',
        //                    success: function (result) {
        //                        if (result.Success) {
        //                            box.find(".mb-control-yes").prop('onclick', null).off('click');
        //                            Mensaje("Documento eliminado con éxito.", "1");
        //                            $("#ModalDocumento").modal('hide');
        //                            tableDocumentos.ajax.reload();
        //                        }
        //                        else
        //                            Mensaje(result.Mensaje, "2");
        //                    },
        //                    error: function (result) {
        //                        Mensaje(result.Mensaje, "2");
        //                    }
        //                });
        //            });
        //        });
        //    }
        //});

        $("#btnAddCobroDetallesPagos").on("click", function () {
            ModalDocCobrarDetallePago(opcion, id_padre);
        });

    };
    function ModalDocCobrarDetallePago(opcion, id_padre) {
        $("body").css("cursor", "progress");
        $.ajax({
            url: '/Admin/DocumentoXCobrar/ModalRegistrarComprobantePago/',
            type: "POST",
            data: { opcion: opcion, id: id_padre },
            success: function (data) {
                $("body").css("cursor", "default");
                $('#ContenidoModal').html(data);
                $('#Modal').modal({ backdrop: 'static', keyboard: false });

                //LoadValidation_AC_ModalCobro();
                RunEventsComprobantePago();
            }
        });
    }
    var RunEventsComprobantePago = function () {
        //var Imagen = document.getElementById("MostrarImagen").value;
        //var ExtensionImagen = document.getElementById("ExtensionImagenBase64").value;
        //$('#ImagenPost').fileinput({
        //    theme: 'fa',
        //    language: 'es',
        //    showUpload: false,
        //    uploadUrl: "#",
        //    autoReplace: true,
        //    overwriteInitial: true,
        //    showUploadedThumbs: false,
        //    maxFileCount: 1,
        //    initialPreview: [
        //        '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:image/png;base64,' + Imagen + '" />'
        //    ],
        //    initialPreviewConfig: [
        //        { caption: 'Imagen del documento' }
        //    ],
        //    initialPreviewShowDelete: false,
        //    showRemove: true,
        //    showClose: true,
        //    layoutTemplates: { actionDelete: '' },
        //    allowedFileExtensions: ["png", 'jpg', 'bmp', 'jpeg'],
        //    required: true
        //})
        //$('#ImagenPost').on('fileclear', function (event) {
        //    document.getElementById("MostrarImagen").value = "";
        //});


        $('#divBancarizado').hide(0);
        $("#Id_formaPago").on("change", function () {
            var bancarizado = $(this).find(":selected").data("bancarizado");
            if (bancarizado == 1) {
                $('#divBancarizado').show(1000);
            }
            else{
                $('#divBancarizado').hide(1000);
            }
        });
    }
    var LoadValidation_AC_Documento = function () {
        var form1 = $('#frm_AC_Documentos');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_FleteDocumentos"),
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
                IDTipoDocumento: {
                    required: true,
                    min: 1
                }
            },
            messages: {
                IDTipoDocumento: {
                    required: "-Seleccione un tipo de documento.",
                    min: "-Seleccione un tipo de documento."
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
                AC_Documento();
            }
        });
    };
    function AC_Documento() {
        var form = $("#frm_AC_Documentos")[0];
        var formData = new FormData(form);

        var arrayKeys = new Array();
        var arrayValues = new Array();

        for (var key of formData.keys()) {
            arrayKeys.push("Documento." + key);
            console.log(key);
        }
        for (var value of formData.values()) {
            arrayValues.push(value);
            console.log(value);
        }
        for (var i = 0; i < arrayKeys.length; i++) {
            formData.append(key[i], value[i]);
        }

        $("body").css("cursor", "progress");
        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/AC_Documento/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                $("body").css("cursor", "default");
                if (response.Success) {
                    Mensaje("Datos guardados con éxito.", "1");
                    $("#ModalDocumento").modal('hide');
                    tableDocumentos.ajax.reload();
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    /*TERMINA DOCUMENTOS*/

    return {
        init: function () {
            LoadTableDocumentoPorCobrarDetalles();
        }
    };
}();