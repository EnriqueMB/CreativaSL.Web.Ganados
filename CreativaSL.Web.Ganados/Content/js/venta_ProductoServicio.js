var Venta_ProductoServicio = function () {
    "use strict"
                   
    /*INICIA IMPUESTO*/
    var LoadValidationFleteImpuesto = function () {
        var form1 = $('#frm_AC_FleteImpuesto');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_FleteImpuesto"),
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
                "TipoImpuesto.Clave": { min: 1 },
                Base: { required: true,  BaseSAT: true },
                "Impuesto.Clave": { min: 1 },
                "TipoFactor.Clave": { min: 1 },
                "TasaCuota": { required: true, TasaCuotaSAT: true },
                Importe: {  required: true   }
            },
            messages: {
                "TipoImpuesto.Clave": { min: "Por favor, seleccione un tipo de impuesto." },
                Base: { required: "Por favor, escriba una cantidad base." },
                "Impuesto.Clave": { min: "Por favor, seleccione un impuesto." },
                "TipoFactor.Clave": { min: "Por favor, seleccione un Tipo o Factor." },
                "TasaCuota": { required: "Por favor, escriba una tasa o cuota." },
                Importe: { required: "Por favor, escriba un importe." }
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
    var RunEventsFleteimpuesto = function () {
        var option = $("#TipoFactor_Clave").val();

        TipoFactor(option);

        $("#TipoFactor_Clave").on("change", function () {
            var option = $(this).val();
            TipoFactor(option);
        });
        $("#Base").on("keyup keydown", function (e) {
            ObtenerImporte();
        });
        $("#TasaCuota").on("keyup keydown", function (e) {
            ObtenerImporte();
        });
    }
    function TipoFactor(option) {
        var TasaCuota = $("#TasaCuota");
        var Importe = $("#Importe");

        if (option == 3) {
            TasaCuota.attr('readonly', true);
            TasaCuota.rules("remove", "required TasaCuotaSAT");
            TasaCuota.closest(".controlError").removeClass("has-success has-error");
            $("#validation_summary_AC_FleteImpuesto").find("dd[for='TasaCuota']").addClass('help-block valid').text('');
            TasaCuota.val("0");
            Importe.val("0");
        }
        else {
            TasaCuota.attr('readonly', false);
            TasaCuota.rules("add", { required: true, TasaCuotaSAT: true });
        }
    }
    function ObtenerImporte() {
        var Base = $('#Base').val();
        var TasaCuota = $('#TasaCuota').val();
        var Importe = $("#Importe");

        if (isNaN(Base) || isNaN(TasaCuota)) {
            Importe.val("0");
        }
        else {
            var total = Base * TasaCuota;
            Importe.val(total.toFixed(2));
        }
    }
    function AC_FleteImpuesto() {
        var form = $("#frm_AC_FleteImpuesto")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Venta/AC_Impuesto/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje("Datos guardados con éxito.", "1");
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    /*TERMINA IMPUESTO*/
        return {
        init: function () {
            LoadValidationFleteImpuesto();
            RunEventsFleteimpuesto();
        }
    };
}();