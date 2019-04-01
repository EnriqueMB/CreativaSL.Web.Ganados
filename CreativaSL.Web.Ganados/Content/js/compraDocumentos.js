var CompraDocumentos = function () {
    "use strict";
    // Funcion para validar registrar
    var tblDocumentos;
    var IDCompra = $("#Id_servicio").val();

    var runValidator1 = function () {
        var form1 = $('#frm_documento');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({
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
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                "PrecioUnitarioDocumentacion": { numeroConComas: true },
                Id_conceptoSalidaDeduccion: { min: 1 }
            },
            messages: {
                Id_conceptoSalidaDeduccion: { min: "Por favor, seleccione un concepto de salida de deducción." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.form-control-feedback').removeClass('glyphicon-ok').addClass('glyphicon-remove');
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
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.form-control-feedback').removeClass('glyphicon-remove').addClass('glyphicon-ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                AC_CostoDocumentos();
            }
        });
    };

    var runElements = function () {
        $('#PrecioUnitarioDocumentacion').on({
            "focus": function (event) {
                $(event.target).select();
            },
            "keyup": function (event) {
                $(event.target).val(function (index, v) {
                    var number = cpf(v);
                    return number;
                });
            }
        });
        $("#btnAddDocumento").on("click", function () {
            var IDCompra = $("#Id_servicio").val();
            window.location.href = '/Admin/Compra/DocumentoCompra?IDCompra=' + IDCompra + '&IDDocumento=0';
        });

        tblDocumentos = $('#tblDocumentos').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDCompra": IDCompra
                },
                "url": "/Admin/Compra/TableJsonDocumentos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "descripcion" },
                { "data": "clave" },
                {
                    "data": null,
                    "render": function (data, type, full) {
                        var imagen64 = full["imagen"];
                        var img = "";
                        if (imagen64 != null) {

                            var extension = "";

                            var position = imagen64.indexOf("iVBOR");
                            //imagen png
                            if (position != -1)
                                extension = "image/png";

                            position = imagen64.indexOf("/9j/4");
                            if (position != -1)
                                extension = "image/jpeg";
                            //bmp de 256 colores
                            position = imagen64.indexOf("Qk3");
                            if (position == 0)
                                extension = "image/bmp";

                            //bmp de monocromatico colores
                            position = imagen64.indexOf("Qk2");
                            if (position == 0)
                                extension = "image/bmp";

                            //bmp de 16 colores
                            position = imagen64.indexOf("Qk1");
                            if (position == 0)
                                extension = "image/bmp";

                            img = "<img class='file-preview-image' style='width: 150px; height: 150px;' src='data:" + extension + ";base64," + full["imagen"] + "' />";
                        }
                        else {
                            img = "<img class='file-preview-image' style='width: 150px; height: 150px;' src='/Content/img/GrupoOcampo.png' />";
                        }
                        return img;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_documentoCompraDetalle"] + "' class='btn btn-yellow tooltips btn-sm editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Compra/DEL_Documento/' title='Eliminar' data-id='" + full["id_documentoCompraDetalle"] + "' class='btn btn-danger tooltips btn-sm deleteDocumento' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_documentoCompraDetalle"] + "' class='editDocumento' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Compra/DEL_Documento/' class='deleteDocumento' role='menuitem' tabindex='-1' data-id='" + full["id_documentoCompraDetalle"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>" +
                            "</ul>" +
                            "</div>" +
                            "</div>";
                    }
                }
            ],
            "drawCallback": function (settings) {
                $(".editDocumento").on("click", function () {
                    var IDDocumento = $(this).data("id");
                    window.location.href = '/Admin/Compra/DocumentoCompra?IDCompra=' + IDCompra + '&IDDocumento=' + IDDocumento;
                });

                $(".deleteDocumento").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var row = $(this).attr('data-id');
                    var box = $("#mb-deleteDocumento");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { IDDocumento: row, Id_servicio: IDCompra },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje(result.Mensaje, "1");
                                    tblDocumentos.ajax.reload();
                                }
                                else
                                    location.reload();
                            },
                            error: function (result) {
                                location.reload();
                            }
                        });
                    });
                });
            }
        });

    }

    function cpf(v) {
        v = v.replace(/([^0-9\.]+)/g, '');
        v = v.replace(/^[\.]/, '');
        v = v.replace(/[\.][\.]/g, '');
        v = v.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
        v = v.replace(/\.(\d{1,2})\./g, '.$1');
        v = v.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
        v = v.split('').reverse().join('').replace(/^[\,]/, '');
        return v;
    }
    function AC_CostoDocumentos() {
        var form = $("#frm_documento")[0];
        var formData = new FormData(form);

        var costo = $("#PrecioUnitarioDocumentacion").val();
        costo = Number.parseFloat(costo.replace(/,/g, ''));
        formData.set("PrecioUnitarioDocumentacion", costo);

        $("body").css("cursor", "progress");
        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Compra/AC_CostoDocumentos/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                $("body").css("cursor", "default");
                if (response.Success) {
                    Mensaje("Datos guardados con éxito.", "1");
                    $("#Id_documentoPorPagar").val(response.Mensaje);
                }
                else {
                    window.location.href = '/Admin/Compra/Index/';
                }
            }
        });
    }

    return {
        init: function () {
            runValidator1();
            runElements();
        }
    };
}();

$(".addToList").click(function () {
    $.each($(".gallery .gallery-item input[type='checkbox']:checked"), function (i, l) {
        // console.log($(this).attr("name"));
        var getRef = $(this).attr("name");
        var countElementsInList = $(".listaFierroContent .lista .listaItem[refitem='" + getRef + "']").length;
        if (countElementsInList == 0) {
            var getDescription = $(".gallery .gallery-item[refitem='" + getRef + "'] .meta strong").text();
            $(".listaFierroContent .lista").append(`
                    <div refitem='`+ getRef + `' class="listaItem">
                    <div class="description"><input readonly name="`+ getRef + `" value="` + getDescription + `" /></div>
                    <div class="buttonErase" eraseto='`+ getRef + `'>
                    <a class="btn btn-danger tooltips btn-sm eliminar" data-original-title="Eliminar" title="Eliminar de esta lista"><i class="fa fa-trash-o"></i></a>
                    </div>
                    </div>
                `);
            $(".gallery .gallery-item[refitem='" + getRef + "']").addClass("dontShow");
        }
        countElementsInList = $(".listaFierroContent .lista .listaItem").length;
        console.log(countElementsInList);
        if (countElementsInList > 0) {
            $(".listaFierroContent form button[type='submit']").addClass("showButton");
            $(".listaFierroContent .lista .listaMessage").css("display", "none");
        }
        else {
            $(".listaFierroContent form button[type='submit']").removeClass("showButton");
        }


    });
});

$(document).on("click", ".buttonErase", function () {
    var getRef = $(this).attr("eraseto");
    $(".gallery .gallery-item[refitem='" + getRef + "']").removeClass('dontShow');
    $(".gallery .gallery-item[refitem='" + getRef + "'] .icheckbox_minimal-grey").removeClass('checked');
    $(".gallery .gallery-item[refitem='" + getRef + "'] input[type='checkbox']").prop('checked', false);
    $(".listaFierroContent .lista .listaItem[refitem='" + getRef + "']").remove();

    var countElementsInList = $(".listaFierroContent .lista .listaItem").length;
    console.log(countElementsInList);
    if (countElementsInList == 0) {
        $(".listaFierroContent form button[type='submit']").removeClass("showButton");
        $(".listaFierroContent .lista .listaMessage").css("display", "block");
    }
});