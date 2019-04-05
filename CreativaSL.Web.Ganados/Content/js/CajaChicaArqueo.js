var Arqueo = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-dg');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $.validator.addMethod("cRequired", $.validator.methods.required,
            "Debe ingresar un valor en cantidad");
        $.validator.addClassRules({
            txtvalues: {
                cRequired: true,
            }
        });

        $('#form-dg').validate({
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
                }
            },
            ignore: "",
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
                form.submit();
                //this.submit();
            }
        });

        //$(".txtvalues").rules("add", {
        //    required: true,
        //    messages: {
        //        required: "Debe ingresar un valor en cantidad"
        //    }
        //});
    };

    var eventos = function () {
        $('.txtvalues').keyup(function () {
            calcularArqueo();
        });
    };

    function calcularArqueo() {
        var table = document.getElementById("tbl1");
        var total = 0;
        for (var i = 1; i < table.rows.length; i++) { //el 1 es para evitar los titulos

            var row = "";
            var denominacion = table.rows[i].cells[2].innerHTML; 
            var cantidad = table.rows[i].cells[3].firstChild.value;

            var subtotal = denominacion * cantidad;

            table.rows[i].cells[4].innerHTML = subtotal;

            total += subtotal;
        }
        document.getElementById("Total").value = total;
    }

    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            eventos();
        }
    };
}();