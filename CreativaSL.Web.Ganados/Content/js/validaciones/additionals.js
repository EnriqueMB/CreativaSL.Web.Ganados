$.validator.addMethod("descripcion", function (value, element) {
    return this.optional(element) || /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\,\w\s\.\-\#]*$/i.test(value);
}, "invalid description");

$.validator.addMethod("direccion", function (value, element) {
    return this.optional(element) || /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\w\s\.\-\#]*$/i.test(value);
}, "invalid address");

$.validator.addMethod("ife", function (value, element) {
    return this.optional(element) || /^.*(?=.{13})[+-]?\d+(\.\d+)?$/i.test(value);
}, "invalid dni");

$.validator.addMethod("nombre", function (value, element) {
    return this.optional(element) || /^([a-zA-ZñÑáéíóúÁÉÍÓÚüÜ]{2,50}[\s]*)+$/i.test(value);
}, "invalid name");

$.validator.addMethod("rfc", function (value, element) {
    return this.optional(element) || /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[A-Z|\d]{3})$/i.test(value);
}, "invalid rfc");

$.validator.addMethod("tarjetaCredito", function (value, element) {
    var numTarjeta = value.replace(/-/g, "");
    return this.optional(element) || /^([0-9]){16,16}$/i.test(numTarjeta);
}, "invalid credit card");

$.validator.addMethod("decimal", function (value, element) {
    return this.optional(element) || $.isNumeric(value);
}, "Invalid number");

$.validator.addMethod("telefono", function (value, element) {
    return this.optional(element) || /^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$/i.test(value);
}, "invalid phone number");

$.validator.addMethod("texto", function (value, element) {
    return this.optional(element) || /^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\#\.\;\:\[\]\*\?\¿\¡\!\+\_\s]*$/i.test(value);
}, "invalid text");
$.validator.addMethod("placa", function (value, element) {
    return this.optional(element) || /^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\-\s]*$/i.test(value);
}, "invalid text");
$.validator.addMethod("tarjetaCirculacion", function (value, element) {
    return this.optional(element) || /^[A-Za-záéíóúñÁÉÍÓÚÑ]*$/i.test(value);
}, "invalid text");
$.validator.addMethod("cuenta", function (value, element) {
    return this.optional(element) || /^[0-9]{5,10}$/i.test(value);
}, "invalid bank account");
$.validator.addMethod("numeros", function (value, element) {
    return this.optional(element) || /^\d*\.?\d*$/i.test(value);
}, "invalid entry");
$.validator.addMethod("clabe", function (value, element) {
    return this.optional(element) || /^[0-9]{18}$/i.test(value);
}, "invalid bank clabe ");

$.validator.addMethod("CMBINT", function (value, element) {
    if ((value === '0') || (value === '-1')) {
     
        return false;
    }
    else {
        return true;
    }
}, 'Seleccione un valor del combo.');

$.validator.addMethod("imagenExtesion", function (value, element, params) {
    //Checamos que tenga un archivo el input file
    if (element.value.length == 0) {
        return false;
    }
    //Si hay obtenemos la extensión
    var arrayString = element.value.split(".");
    var longitud = arrayString.length;
    var extension = arrayString[longitud - 1];

    if (extension.localeCompare("png") == 0 || extension.localeCompare("jpg") == 0 || extension.localeCompare("jpeg") == 0 || extension.localeCompare("bmp") == 0) {
        return true;
    }
    else {
        return false;
    }
}, 'Solo archivos con formato PNG, JPG, JPEG y BMP.');

$.validator.addMethod("ImagenRequerida", function (value, element, params) {
    //Checamos que tenga un archivo el input file
    if (element.value.length === 0) {
        var imagenServidor = document.getElementById(params[0]).value;

        if (imagenServidor === "0") {
            
            return false;
        }
        else {
            
            return true;
        }
    }
    else {
        //Si hay obtenemos la extensión
        var arrayString = element.value.split(".");
        var longitud = arrayString.length;
        var extension = arrayString[longitud - 1];

        console.log(extension);

        if (extension.localeCompare("png") === 0 || extension.localeCompare("jpg") === 0 || extension.localeCompare("jpeg") === 0 || extension.localeCompare("bmp") === 0 || extension.localeCompare("heic") === 0) {
            return true;
        }
        else {
            return false;
        }
    }
}, 'Solo archivos con formato PNG, JPG, JPEG, BMP y HEIC.');

$.validator.addMethod("formatoPNG", function (value, element, params) {
    //Bandera que me indica si hay o no imagen en el servidor
    var imgBD = element.dataset.imgbd;
    //Hay imagen en el servidor?
    if (imgBD == "1") {
        return true;
    }
    else {
        //Hay un elemento en el file input?
        if (element.value.length == 0 || element === undefined) {
            return false;
        }
        else {
            return true;
        }
    }

}, 'Por favor, debe seleccionar una imagen.');
$.validator.addMethod("validarImgEdit", function (value, element, params) {
    //Bandera que me indica si hay o no imagen en el servidor
    var imgBD = element.dataset.imgbd;
    //Hay imagen en el servidor?
    if (imgBD) {
        return true;
    }
    else {
        //Hay un elemento en el file input?
        if (element.value.length == 0 || element === undefined) {
            return false;
        }
        else {
            return true;
        }
    }

}, 'Debe seleccionar una imagen.');

$.validator.addMethod("validarImgEdit2", function (value, element, params) {
    //Bandera que me indica si hay o no imagen en el servidor
    var bandera = document.getElementById(params[0]).value;
    //Hay imagen en el servidor?
    if (bandera == "True") {
        return true;
    }
    else {
        //No Hay un elemento en el file input
        if (element.value.length == 0 || element === undefined) {
            return false;
        }
        else {
            return true;
        }
    }

}, 'Debe seleccionar una imagen.');

$.validator.addMethod("validarImagen", function () {

    if (document.getElementById("ImgINEE").value === '') {
        if ((document.getElementById("ImgINEE").value === ''))
            return false;
        else
            return true;
    }
    else
        return true;
}, 'Debe seleccionar una imagen.');

$.validator.addMethod("fecha", function (value, element) {
    return this.optional(element) || /^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})$/i.test(value);
}, "Formato de la fecha inválido debe ser: dd/mm/yyyy ");

$.validator.addMethod("group", function (value, element, params) {
    var inputGroup = document.getElementById(params[0]).value;
    var inputGroup2 = document.getElementById(params[1]).value;
    var expReg = new RegExp("^[1-9]+[0-9]+([.][0-9]+)?$|^[0]+([.][0-9]+)$");
    var error = false;

    if (expReg.test(inputGroup)) {
        error = true;
        //console.log("valido 1: " + error);
        return error;
    }
    else {
        error = false;
        //console.log("valido 1: " + error);
    }

    if (expReg.test(inputGroup2)) {
        error = true;
        //console.log("valido 2: " + error);
    }
    else {
        error = false;
        //console.log("valido 2:" + error);
    }

    return error;

}, function (params, element) {
    //Error personalizado
    return params[2];
});
//SAT
$.validator.addMethod("TasaCuotaSAT", function (value, element) {
    return this.optional(element) || /^([0-9][.])+([0-9]{1,6})$/i.test(value);
}, "La Tasa o Cuota debe ser un número entero, seguido de hasta 6 decimales.");

$.validator.addMethod("BaseSAT", function (value, element) {
    return this.optional(element) || /^[1-9]([0-9]+)?([.][0-9]{1,6})?$/i.test(value);
}, "La Base debe ser mayor que cero, hasta 6 decimales.");

$.validator.addMethod("validarTblEventoGanadoVenta", function () {

    if (document.getElementById("ImgINEE").value === '') {
        if ((document.getElementById("ImgINEE").value === ''))
            return false;
        else
            return true;
    }
    else
        return true;
}, 'Debe seleccionar una imagen.');

$.validator.addMethod("numeroConComas", function (value, element, params) {
    //Checamos que tenga un dato el input
    if (value === '') {
        return false;
    }
    //Si hay le quitamos la coma, en caso que sean miles
    var valorInput = element.value;
    var numero = Number.parseFloat(valorInput.replace(/,/g, ''));

    if (Number.isNaN(numero)) {
        return false;
    }
    else {
        if (numero >= 0) {
            return true;
        }
        else {
            return false;
        }
    }

}, 'Por favor, escriba un número mayor a 0.');

$.validator.addMethod("horas24", function (value, element) {
    return this.optional(element) || /^([0-5]?[0-9])(:[0-5][0-9])$/i.test(value);
}, "Por favor, seleccione una hora válida (hh:mm).");


$.validator.addMethod("LimitSize", function (value, element, param) {

    var archivoInput = document.getElementById(param[0]);
    var archivos = archivoInput.files;
    var size = Number.parseFloat(param[1]) * 1024 * 1024;
    var error = "";

    if (archivos.length > 0) {
        if (archivos[0].size > size) {
            error = "El tamaño del archivo no puede superar los: " + param[1] + " MB.";
            $.validator.messages['LimitSize'] = error;
            return false;
        }
    }
    else {
        error = "Por favor, ingrese un archivo.";
        $.validator.messages['LimitSize'] = error;
        return false;
    }
    return true;
});

jQuery.validator.addMethod("notEqual", function (value, element, param) {
    return this.optional(element) || value != param;
}, "Please specify a different (non-default) value");


