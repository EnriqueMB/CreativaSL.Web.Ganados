var CompraEventoDetalles = function () {
    "use strict"
    var Id_compra = $("#Id_compra").val();
    var Id_eventoCompra = $("#Id_eventoCompra").val();
    var tblGanadoConEvento;
    
        
    /*INICIA EVENTO*/
    var initFuncionesEvento = function () {
        var Imagen = document.getElementById("ImagenBase64").value;

        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
            //,defaultTime: hora
        });
        $('#HttpImagen').fileinput({
            theme: 'fa',
            language: 'es',
            maxFileCount: 1,
            overwriteInitial: true,
            showUploadedThumbs: false,
            allowedFileExtensions: ['png', 'jpg', 'jpeg', 'bmp', 'heic'],
            initialPreview: [
                '<img class="file-preview-image"  style=" width: auto !important; height: auto; max-width: 100%; max-height: 100%;" src="' + Imagen + '" />'
            ],
            initialPreviewConfig: [
               { caption: 'Imagen' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            showUpload: false,
            layoutTemplates: { actionDelete: '' },
            previewFileIcon: '<i class="fa fa-file"></i>',
            preferIconicPreview: true, // this will force thumbnails to display icons for following file extensions
            previewFileIconSettings: { // configure your icon file extensions
                'heic': '<i class="fa fa-file-text text-primary"></i>'
            },
            previewFileExtSettings: { // configure the logic for determining icon file extensions
                'heic': function (ext) {
                    return ext.match(/(heic)$/i);
                }
            }
        });
        $('#HttpImagen').on('fileclear', function (event) {
            document.getElementById("ImagenMostrar").value = "";
        });

        tblGanadoConEvento = $('#tblGanadoConEvento').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDCompra": Id_compra, "Id_eventoCompra": Id_eventoCompra
                },
                "url": "/Admin/Compra/DatatableGanadoEventoXIDCompra/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                {   "data": "numArete" },
                {   "data": "genero" },
                {   "data": null,
                    "render": function (data, type, full) 
                    {
                        return full["pesoPagado"] + " kg.";
                    }
                },
                {
                    "data": "precioAnterior",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "precioKilo",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                }
            ]
        });
    }

    /*TERMINA EVENTO*/

    return {
        init: function () {
            initFuncionesEvento();
        }
    };
}();