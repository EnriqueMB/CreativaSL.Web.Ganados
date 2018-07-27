var Combustible = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-ec');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $.validator.addMethod("validarImagen", function () {
            console.log(document.getElementById("ImgTicket").value);
            if (document.getElementById("ImgTicket").value === '') {
                if ((document.getElementById("ImgTicket").value === ''))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }, 'Debe seleccionar una imagen.');
        $('#form-ec').validate({
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
                NoTicket: { required: true },
                IDSucursal: { required: true },
                IDProveedor: { required: true },
                Litros: {min:0.1, required: true },
                KMInicial: { required: true, min: 0 },
                Total: { min:1, required: true },
                ImgTicket: { validarImagen: true, formatoPNG: true },
                IDVehiculo: { required: true },
                IDTipoCombustible: { CMBINT: true }
            },
            messages: {
                NoTicket: { required: "Ingrese el número del ticket" },
                IDSucursal: { required: "Seleccione una sucursal." },
                IDProveedor: { required: "Seleccione un proveedor" },
                Litros: {min:"Ingrese litros mayor que 0 ", required: "Ingrese la cantidad en litros" },
                KMInicial: { required: "Ingrese el Kilometraje inicial.", min:"El KMInicial no puede ser un valor negativo." },
                Total: { min: "El total debe ser mayor a 0", required: "Ingrese el importe total del ticket" },
                ImgTicket: { validarImagen:"Ingrese una imagen correcta", formatoPNG: "El formato de la imagen debe ser png" },
                IDVehiculo: { required: "Seleccione un vehículo" },
                IDTipoCombustible: { CMBINT: "Seleccione un tipo de combustible" }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error').find('.form-control-feedback').removeClass('glyphicon-ok').addClass('glyphicon-remove');
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
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success').find('.form-control-feedback').removeClass('glyphicon-remove').addClass('glyphicon-ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
                //this.submit();
            }
        });
    };

    var runElements = function () {
        $('#Fecha').datepicker({
            format: 'dd/mm/yyyy'
        });        
        //$('.bootstrap-select-searchbox').append("<span class='glyphicon form-control-feedback'></span>");
        //$('.bootstrap-select-searchbox').focusout(function () {
        //    console.log($(this).closest('.form-group').find('select'));
        //    console.log($('#IDSucursal'));
        //    $(this).closest('.form-group').find('select').trigger("focusout");
        //})
    };

    var runCombos = function () {

        $('#IDSucursal').on('change', function (event) {
            $("#IDVehiculo option").remove();
            $("#IDProveedor option").remove();
            var IdSucursal = $(this).val();
            getCatVehiculos(IdSucursal);
            getCatProveedores(IdSucursal);
            //$(this).trigger("focusout");
        });
        function getCatVehiculos(IdSucursal) {
            $.ajax({
                url: "/Admin/EntregaCombustible/ObtenerComboVehiculos",
                data: { IDSucursal: IdSucursal },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "2");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDVehiculo").append('<option value="' + result[i].IDVehiculo + '">' + result[i].Modelo + '</option>');
                    }
                    $('#IDVehiculo.select').selectpicker('refresh');
                }
            });
        }

        function getCatProveedores(IdSucursal) {
            $.ajax({
                url: "/Admin/EntregaCombustible/ObtenerComboProveedores",
                data: { IDSucursal: IdSucursal },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "2");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDProveedor").append('<option value="' + result[i].IDProveedor + '">' + result[i].NombreRazonSocial + '</option>');
                    }
                    $('#IDProveedor.select').selectpicker('refresh');
                }
            });
        }

    };

    var runFileInput = function () {
        
        //console.log(band);
        //if (!band)
        //{
        //console.log("IF");
        //    $('#UrlImagen64').fileinput({
        //        theme: 'fa',
        //        language: 'es',
        //        showUpload: false,
        //        uploadUrl: "#",
        //        showUploadedThumbs: false,
        //        overwriteInitial: false,
        //        maxFileCount: 1,
        //        allowedFileExtensions: ["png"],
        //        required: true
        //    })
        //}
        //else
        //{
        //console.log("else");
        //    $('#UrlImagen64').fileinput({
        //        theme: 'fa',
        //        language: 'es',
        //        showRemove: false,
        //        showClose: false,
        //        showUpload: false,
        //        uploadUrl: "#",
        //        autoReplace: true,
        //        overwriteInitial: true,
        //        showUploadedThumbs: false,
        //        maxFileCount: 1,
        //        initialPreview: [
        //            '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:image/png;base64,' + ImgTicket + '" />'
        //        ],
        //        initialPreviewConfig: [
        //            { caption: 'Ticket de combustible' }
        //        ],
        //        initialPreviewShowDelete: false,
        //        showRemove: false,
        //        showClose: false,
        //        layoutTemplates: { actionDelete: '' },
        //        allowedFileExtensions: ["png"],
        //        required: true
        //    })
        //}
        
    };

    return {
        //main function to initiate template pages
        init: function (band, img64 ) {
            runValidator1();
            runElements();
            runCombos();
            runFileInput();
        }
    };
}();