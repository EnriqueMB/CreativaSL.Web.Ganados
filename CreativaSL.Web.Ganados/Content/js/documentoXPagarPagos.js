var DocumentoXPagar = function () {
    "use strict"
    var Id_documentoPorPagar;
    var tipoServicio = document.getElementById("TipoServicio").value;
    var bancarizadoForm = document.getElementById("Bancarizado");
    var cuentaBeneficiante = $("#Id_cuentaBancariaBeneficiante");
    var cuentaOrdenante = $("#Id_cuentaBancariaOrdenante");
    var imagen = $("#HttpImagen");
    var folioINE = $("#FolioIFE");
    var numeroAutorizacion = $("#NumeroAutorizacion");

    var RunEventsComprobantePago = function () {
        var Imagen = document.getElementById("ImagenMostrar").value;
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
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:' + ExtensionImagen + ';base64,' + Imagen + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del recibo' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png", "jpg", "png", "jpeg", ]
        })
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
                cuentaOrdenante.rules("add", { required: true });
                imagen.rules("add", { ImagenRequerida: true });
                folioINE.rules("add", { required: true });
                numeroAutorizacion.rules("add", { required: true });
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
        cuentaOrdenante.rules("remove", "required");
        imagen.rules("remove", "ImagenRequerida");
        folioINE.rules("remove", "required");
        numeroAutorizacion.rules("remove", "required");

        cuentaBeneficiante.closest(".controlError").removeClass("has-success has-error");
        cuentaOrdenante.closest(".controlError").removeClass("has-success has-error");
        imagen.closest(".controlError").removeClass("has-success has-error");
        folioINE.closest(".controlError").removeClass("has-success has-error");
        numeroAutorizacion.closest(".controlError").removeClass("has-success has-error");

        $("#validation_summary").find("dd[for='FolioIFE']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='NumeroAutorizacion']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='HttpImagen']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='Id_cuentaBancariaOrdenante']").addClass('help-block valid').text('');
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
    //function NombreEmpresa() {
    //    var empresa = $('#Id_documentoPorPagar').find(":selected");
    //    $("#NombreEmpresa").val(empresa[0].dataset.nombre);
    //}

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
                //bancarizado
                "FolioIFE": {
                    required: true
                },
                "NumeroAutorizacion": {
                    required: true
                },
                "HttpImagen": {
                    ImagenRequerida: true
                },
                "Id_cuentaBancariaOrdenante": {
                    required: true
                },
                "Id_cuentaBancariaBeneficiante": {
                    required: true
                }
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
                //bancarizado
                FolioIFE: {
                    required: "Ingrese el folio del INE"
                },
                NumeroAutorizacion: {
                    required: "Ingrese el numero de autorización"
                },
                HttpImagen: {
                    required: "Ingrese una imagen"
                },
                Id_cuentaBancariaOrdenante: {
                    required: "Seleccione una cuenta de banco de la empresa"
                },
                Id_cuentaBancariaBeneficiante: {
                    required: "Seleccione una cuenta de banco del proveedor / cliente"
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
               // AC_Comprobante();
            }
        });
    };
    function AC_Comprobante() {
        var form = $("#form_Comprobante")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/DocumentoXPagar/PagosEdit/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    //Mensaje("Datos guardados con éxito.", "1");
                    var href = $("#regresar").attr('href');
                    window.location.href = href;
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    return {
        init: function () {
            LoadValidation_AC_Documento();
            RunEventsComprobantePago();
            //NombreEmpresa();

        }
    };
}();
