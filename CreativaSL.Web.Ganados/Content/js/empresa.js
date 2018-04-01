var Empresa = function () {
    var TblCuentasBancarias;
    "use strict"
    var SaveEmpresa = function () {

        $("#frmEditEmpresa").on("submit", function (e) {
            e.preventDefault();
            var data = $("form#frmEditEmpresa")[0];
            var fileData = new FormData(data);

            $.ajax({
                type: 'POST',
                data: fileData,
                url: '/Admin/CatEmpresa/SaveEmpresa/',
                contentType: false,
                processData: false,
                cache: false,
                success: function (response) {
                    if (response.Success)
                        Mensaje(response.Mensaje, "1");
                    else
                        Mensaje(response.Mensaje, "2");
                },
                failure: function (response) {
                    Mensaje(response.Mensaje, "2");
                },
                error: function (response) {
                    Mensaje(response.Mensaje, "2");
                }
            });
        });
       
    };
    var RunFileInput = function (LogoRFC, LogoEmpresa){
        $('#fileLogoEmpresa').fileinput({
            theme: 'fa',
            language: 'es',
            showRemove: false,
            showClose: false,
            showUpload: false,
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:image/png;base64,' + LogoEmpresa + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Logo de la empresa' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png"],
            required: true
        })
        $('#fileLogoRFC').fileinput({
            theme: 'fa',
            language: 'es',
            showRemove: false,
            showClose: false,
            showUpload: false,
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:image/png;base64,'+ LogoRFC + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del R.F.C.' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png"],
            required: true
        })
    };
    var LoadTableCuentasBancarias = function (IDEmpresa) {
        TblCuentasBancarias = $('#TblCuentasBancarias').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDEmpresa": IDEmpresa
                },
                "url": "/Admin/CatEmpresa/LoadTableCuentasBancarias/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                {
                    "data": null,
                    "render": function (data, type, full) {
                        return "<img style='width: 100px; height: 100px; max-width: 100 %; max-height: 100 %;' src='data: image/png; base64,"+ full["ImgBanco"] +"' />";
                    }
                },
                { "data": "NomBanco" },
                { "data": "Titular" },
                { "data": "NumTarjeta" },
                { "data": "NumCuenta" },
                { "data": "ClabeInter" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["IDDatosBan"] + "' title='Editar' class='btn btn-yellow tooltips btn-sm editCuentaBancaria' data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a title='Eliminar' data-id='" + full["IDDatosBan"] + "' data-hrefa='/Admin/CatEmpresa/DeleteCuentaBancaria/" + full["IDDatosBan"] + "' class='btn btn-danger tooltips btn-sm deleteCuentaBancaria' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a  data-id='" + full["IDDatosBan"] + "' class='editCuentaBancaria' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a class='deleteCuentaBancaria' role='menuitem' tabindex='-1'  data-id='" + full["IDDatosBan"] + "' data-hrefa='/Admin/CatEmpresa/DeleteCuentaBancaria/" + full["IDDatosBan"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>" +
                            "</ul>" +
                            "</div>" +
                            "</div>";
                    }
                }
            ],
            //Para agregar algún evento a la tabla se puede poner aquí
            "drawCallback": function (settings) {
                $(".editCuentaBancaria").on("click", function () {
                    var IDCuentaBancaria = $(this).data("id")
                    ModalCuentaBancaria(IDCuentaBancaria, IDEmpresa);
                });
                $(".deleteCuentaBancaria").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var row = $(this).attr('data-id');
                    var box = $("#mb-remove-row");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje(result.Mensaje, "1");
                                    TblCuentasBancarias.ajax.reload();
                                }
                                else
                                    Mensaje(result.Mensaje, "2");
                            },
                            error: function () {
                                Mensaje(result.Mensaje, "2");
                            }
                        });
                    });
                });
            }
        });
        
    }
    var LoadModal = function (IDEmpresa){
        $("#btnCrearCuentaBancaria").on("click", function () {
            ModalCuentaBancaria(0, IDEmpresa);
        });
    }
    //Funciones
    function ModalCuentaBancaria(IDCuentaBancaria, IDEmpresa) {
        $.ajax({
            url: '/Admin/CatEmpresa/ModalCuentaBancaria/',
            type: "POST",
            dataType: 'HTML',
            data: { IDCuentaBancaria: IDCuentaBancaria, IDCliente: IDEmpresa },
            success: function (data) {
                $('#ContenidoModalCuentasBancaria').html(data);
                $('#ModalCuentasBancaria').modal({ backdrop: 'static', keyboard: false });

                $("#AC_CuentaBancaria").on("click", function (e) {
                    var form = $("#frmModalCuentaBancaria")[0];

                    var fileData = new FormData(form);
                    $.ajax({
                        type: 'POST',
                        data: fileData,
                        url: '/Admin/CatEmpresa/InsertUpdateCuentaBancaria/',
                        contentType: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            $('#ModalCuentasBancaria').modal('hide');

                            if (response.Success) {
                                Mensaje(response.Mensaje, "1");
                                TblCuentasBancarias.ajax.reload();
                            }
                            else
                                Mensaje(response.Mensaje, "2");
                        },
                        failure: function (response) {
                            Mensaje(response.Mensaje, "2");
                        },
                        error: function (response) {
                            Mensaje(response.Mensaje, "2");
                        }
                    });
                });
            }
        });

    }

    return {
        init: function (LogoRFC, LogoEmpresa, IDEmpresa) {
            SaveEmpresa();
            RunFileInput(LogoRFC, LogoEmpresa);
            LoadTableCuentasBancarias(IDEmpresa);
            LoadModal(IDEmpresa);
        }
    };
}();