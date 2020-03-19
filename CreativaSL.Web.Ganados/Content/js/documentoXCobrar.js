var DocumentoXPagar = function () {
    "use strict";
    var Id_documentoPorPagar;
    var tipoServicio = document.getElementById("TipoServicio").value;
    var bancarizadoForm = document.getElementById("Bancarizado");
    var cuentaBeneficiante = $("#Id_cuentaBancariaBeneficiante");
    var cuentaOrdenante = $("#Id_cuentaBancariaOrdenante");
    var imagen = $("#HttpImagen");

    var RunEventsComprobantePago = function () {
        var ExtensionImagen = document.getElementById("ExtensionImagenBase64").value;
        var ImagenServidor = document.getElementById("ImagenBase64").value;

        if (ImagenServidor === null || ImagenServidor.length == 0 || ImagenServidor == '') {
            document.getElementById("HttpImagen").dataset.imgBD = "0";
        }
        else {
            document.getElementById("HttpImagen").dataset.imgbd = "1";
        }


        $('#HttpImagen').fileinput({
            theme: 'fa',
            language: 'es',
            showUpload: false,
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="' + ImagenServidor + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del recibo' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
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

        if (bancarizadoForm.value == "False") {
            $('#divBancarizado').hide(0);
            QuitarValidacionesBancarizadas();
        }

        $("#Id_formaPago").on("change", function () {
            var bancarizadoOpcion = $(this).find(":selected").data("bancarizado");

            if (bancarizadoOpcion == 1) {
                $('#divBancarizado').show(1000);
                bancarizadoForm.value = true;
                cuentaBeneficiante.rules("add", { required: true });
                imagen.rules("add", { ImagenRequerida: ["ImagenServer"] });
            }
            else {
                $('#divBancarizado').hide(1000);
                QuitarValidacionesBancarizadas();
                bancarizadoForm.value = false;
            }
        });

        $("#Id_documentoPorPagar").on("change", function () {
            Id_documentoPorPagar = $(this).val();
            CuentasBancarias();
        });
        $("#Id_cuentaBancariaBeneficiante").on("change", function () {
            var opcion = $('#Id_cuentaBancariaBeneficiante').find(":selected");

            $("#NombreBancoBeneficiante").val(opcion[0].dataset.banco);
            $("#NumCuentaBeneficiante").val(opcion[0].dataset.numcuenta);
            $("#NumClabeBeneficiante").val(opcion[0].dataset.clabe);
            $("#NumTarjetaBeneficiante").val(opcion[0].dataset.numtarjeta);
        });
        $("#Id_cuentaBancariaOrdenante").on("change", function () {
            var opcion = $('#Id_cuentaBancariaOrdenante').find(":selected");

            $("#NombreBancoOrdenante").val(opcion[0].dataset.banco);
            $("#NumCuentaOrdenante").val(opcion[0].dataset.numcuenta);
            $("#NumClabeOrdenante").val(opcion[0].dataset.clabe);
            $("#NumTarjetaOrdenante").val(opcion[0].dataset.numtarjeta);
        });
    }
    function QuitarValidacionesBancarizadas() {
        cuentaBeneficiante.rules("remove", "required");
        imagen.rules("remove", "ImagenRequerida");

        cuentaBeneficiante.closest(".controlError").removeClass("has-success has-error");
        imagen.closest(".controlError").removeClass("has-success has-error");

        $("#validation_summary").find("dd[for='HttpImagen']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='Id_cuentaBancariaBeneficiante']").addClass('help-block valid').text('');
    }
    function CuentasBancarias() {
        $.ajax({
            url: '/Admin/DocumentoXPagar/GetCuentasBancarias/',
            type: "POST",
            dataType: 'json',
            data: { Id_documentoPorPagar: Id_documentoPorPagar, TipoServicio: tipoServicio },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "2");
            },
            success: function (result) {

                $("#Id_cuentaBancariaBeneficiante option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Id_cuentaBancariaBeneficiante").append('<option value="' + result[i].IDDatosBancarios + '" data-titular="' + result[i].Titular + '" data-numcuenta="' + result[i].NumCuenta + '" data-numclabe="' + result[i].Clabe + '" data-numtarjeta="' + result[i].NumTarjeta + '" data-banco="' + result[i].Banco.Descripcion + '" data-idbanco="' + result[i].Banco.IDBanco + '">' + result[i].Titular + '</option>');
                }
                $('#Id_cuentaBancariaBeneficiante.select').selectpicker('refresh');
            }
        });
    }

    var LoadValidation_AC_Documento = function () {
        var form1 = $('#form_Comprobante');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
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
                Id_documentoPorCobrar: {
                    required: true
                },
                Id_formaPago: {
                    required: true,
                    min: 1
                },
                Monto: {
                    required: true,
                    min: 1
                },
                fecha: {
                    required: true
                },
                "HttpImagen": {
                    ImagenRequerida: ["ImagenServer"]
                },
                "Id_cuentaBancariaBeneficiante": {
                    required: true
                },
            },
            messages: {
                Id_documentoPorCobrar: {
                    required: "Seleccione a quien se le asignara el cobro"
                },
                Id_formaPago: {
                    required: "Seleccione la forma de pago",
                    min: "Seleccione la forma de pago"
                },
                Monto: {
                    required: "Ingrese el monto",
                    min: "Ingrese el monto"
                },
                fecha: {
                    required: "Seleccione la fecha"
                },
                Id_cuentaBancariaBeneficiante: {
                    required: "Seleccione una cuenta de banco de la empresa"
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
                form.submit();
            }
        });
    };

    return {
        init: function () {
            LoadValidation_AC_Documento();
            RunEventsComprobantePago();
        }
    };
}();
