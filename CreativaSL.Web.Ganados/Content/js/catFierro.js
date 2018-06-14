var Fierro = function () {
    "use strict"
    var Inicializar = function () {
        // init wPaint
        var a = $('#wPaint').wPaint({
            menuOffsetLeft: -35,
            menuOffsetTop: -50,
            bg: '#FFFFFF',
            saveImg: saveImg,
            loadImgBg: null,
            loadImgFg: null,
            path: '/Content/js/plugins/wPaint/'
        });


        var images = [
      '/test/uploads/wPaint.png',
        ];

        function saveImg(image) {
            var _this = this;

            $.ajax({
                type: 'POST',
                url: '/test/upload.php',
                data: { image: image },
                success: function (resp) {

                    // internal function for displaying status messages in the canvas
                    _this._displayStatus('Image saved successfully');

                    // doesn't have to be json, can be anything
                    // returned from server after upload as long
                    // as it contains the path to the image url
                    // or a base64 encoded png, either will work
                    resp = $.parseJSON(resp);

                    // update images array / object or whatever
                    // is being used to keep track of the images
                    // can store path or base64 here (but path is better since it's much smaller)
                    images.push(resp.img);

                    // do something with the image
                    $('#wPaint-img').attr('src', image);
                }
            });
        }
    }

    var Validaciones = function () {
        var form1 = $('#frm_fierro');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
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
                NombreFierro: {
                    required: true
                }
            },
            messages: {
                NombreFierro: {
                    required: "Ingrese un nombre del fierro."
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.controlError').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                AC_FIERRO();
            }
        });
    }
   
    function AC_FIERRO() {
        var form = $("#frm_fierro")[0];
        var formData = new FormData(form);

        var canvas = document.getElementById('wPaint-canvas');
        var dataURL = canvas.toDataURL();
        console.log(dataURL);

        formData.append("ImgFierro", dataURL);


        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/CatFierro/Create/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje("Registro guardado con éxito.", "1");
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }


    return {
        init: function () {
            Inicializar();
            Validaciones();
        }
    };
}();