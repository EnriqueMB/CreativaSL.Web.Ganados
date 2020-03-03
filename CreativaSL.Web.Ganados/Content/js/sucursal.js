var Sucursal = function () {
    "use strict";
    var map, marker;
    var selectLugar = document.getElementById('IDLugar');

    var Validaciones = function () {
        var form1 = $('#frmSucursal');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $('#frmSucursal').validate({ // initialize the plugin
            //debug: true,
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
                NombreSucursalMatriz: {
                    minlength: 5,
                    maxlength: 300
                },
                NombreSucursal: {
                    required: true,
                    minlength: 5,
                    maxlength: 300
                },
                Direccion: {
                    required: true,
                    minlength: 5,
                    maxlength: 300
                },
                IDLugar: {
                    required:true
                }
                , NombreCorto: {
                    required: true,
                    maxlength: 4
                }
            },
            messages: {
                NombreSucursalMatriz: {
                    minlength: jQuery.validator.format("-El campo: Nombre Empresa,  debe tener un mínimo de caracteres de: {0}"),
                    maxlength: jQuery.validator.format("-El campo: Nombre Empresa,  debe tener un máximo de caracteres de: {0}")
                },
                NombreSucursal: {
                    required: "Por favor, escriba el nombre de la sucursal.",
                    minlength: jQuery.validator.format("-El campo: Nombre Sucursal,  debe tener un mínimo de caracteres de: {0}"),
                    maxlength: jQuery.validator.format("-El campo: Nombre Sucursal,  debe tener un máximo de caracteres de: {0}")
                },
                Direccion: {
                    required: "Por favor, escriba la dirección de la sucursal.",
                    minlength: jQuery.validator.format("-El campo: Dirección,  debe tener un mínimo de caracteres de: {0}"),
                    maxlength: jQuery.validator.format("-El campo: Dirección,  debe tener un máximo de caracteres de: {0}")
                },
                IDLugar: {
                    required: "Por favor, seleccione un lugar de la sucursal."
                }
                , NombreCorto: {
                    required: "Por favor, escriba un nombre corto para el foliador.",
                    maxlength: jQuery.validator.format("-El campo: Dirección,  debe tener un máximo de caracteres de: {0}")
                },
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
    }
    var InitMap = function (option) {
        map = new google.maps.Map(document.getElementById('map'), {
            zoom: 10,
            center: { lat: 17.6063149, lng: -93.204288 },
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        var onChangeHandler = function () {
            AddMarcador(map);
        };
        selectLugar.addEventListener('change', onChangeHandler);
        if (option == 2) {
            AddMarcador(map);
        }
    };
    function AddMarcador(map) {
        var selectIndexInicio = selectLugar.selectedIndex;
        var optionInicio = selectLugar.options.item(selectIndexInicio);
        var latitudInicial = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial = optionInicio.dataset.longitud.replace(",", ".");

        var inicio = new google.maps.LatLng(latitudInicial, longInicial);
        if (marker != null)
            marker.setMap(null);

        marker = new google.maps.Marker({
            position: inicio,
            map: map
        });
        map.setCenter(marker.getPosition());

    }
    return {
        init: function (option) {
            Validaciones();
            InitMap(option);
        }
    };
}();