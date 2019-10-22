var CostoExtra_AC = function () {
    "use strict";
    var idVenta = $("#IdVenta").val();

    var Setting = function () {
        SettingMsk();
        SettingSubtotal();
    };

    function SettingSubtotal() {
        $('.eventSubtotal').on('keyup', function (e) {
            console.log("event");
            var subtotal = 1;
            $(".eventSubtotal").each(function () {
                subtotal *= MoneyToNumber($(this).val());
            });

            $("#Subtotal").val(subtotal.toFixed(2));
            $(".mskMoney").maskMoney('mask');
        });
    }

    function SettingMsk() {
        $(".mskNumber").maskMoney(
            {
                allowZero: true,
                precision: 2
            }
        );
        $(".mskMoney").maskMoney(
            {
                allowZero: true,
                precision: 2,
                prefix: '$ '
            }
        );

        $(".mskNumber").maskMoney('mask');
        $(".mskMoney").maskMoney('mask');
    }

    var Validations = function () {
        var form1 = $('#frmCostoExtra');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        form1.validate({ // initialize the plugin
            errorElement: "dd", // contain the error msg in a span tag
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
                }
            },
            ignore: "",
            rules: {
                NombreProducto: {
                    required: true
                },
                NombreUnidadMedida: {
                    required: true
                },
                Cantidad: {
                    required: true,
                    numeroConComas: true
                },
                PrecioUnitario: {
                    required: true,
                    StringMoneyGreaterOrEqualToZero: true
                },
                Subtotal: {
                    required: true,
                    StringMoneyGreaterOrEqualToZero: true
                }
            },
            messages: {
                NombreProducto: {
                    required: "-El campo: Nombre del producto/servicio, es requerido"
                },
                NombreUnidadMedida: {
                    required: "-El campo: Unidad de medida, es requerido"
                },
                Cantidad: {
                    required: "-El campo: Cantidad, es requerido",
                    numeroConComas: "-El campo: Cantidad, debe ser igual o mayor que 0."
                },
                PrecioUnitario: {
                    required: "-El campo: Precio unitario, es requerido",
                    StringMoneyGreaterOrEqualToZero: "-El campo: Precio unitario, debe ser igual o mayor que 0."
                },
                Subtotal: {
                    required: "-El campo: Subtotal, es requerido",
                    StringMoneyGreaterOrEqualToZero: "-El campo: Subtotal, debe ser igual o mayor que 0."
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
                SendForm();
            }
        });
    };

    function SendForm() {
        var cantidad = $("#Cantidad").val();
        var precioUnitario = $("#PrecioUnitario").val();
        var subtotal = $("#Subtotal").val();

        var form = $('form#frmCostoExtra')[0];
        var formData = new FormData(form);

        formData.set('Cantidad', StringToNumber(cantidad));
        formData.set('PrecioUnitario', MoneyToNumber(precioUnitario));
        formData.set('subtotal', MoneyToNumber(subtotal));

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Venta/CostoExtra_AC/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                window.location.href = '/Admin/Venta/CostosExtra?idVenta=' + idVenta;
            },
            error: function (request, status, error) {
                window.location.href = '/Admin/Venta/CostosExtra?idVenta=' + idVenta;
            }
        });
    }

    function StringToNumber(value) {
        var newValue = String(value).replace(/,/g, '');

        return IsNumber(newValue);
    }

    function MoneyToNumber(value) {
        var newValue = String(value).replace('$ ', '');

        return StringToNumber(newValue);
    }

    function IsNumber(newValue) {
        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }

    return {
        init: function (option) {
            Setting();
            Validations();
        }
    };
}();