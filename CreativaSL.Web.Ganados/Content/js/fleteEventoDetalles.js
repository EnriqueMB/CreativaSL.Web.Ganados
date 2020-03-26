var FleteEvento = function () {
    "use strict"
    var Id_flete = $("#Id_flete").val();
    var Id_eventoFlete = $("#Id_eventoFlete").val();
    var tblGanadoCargado, tblGanadoConEvento;

    /*INICIA EVENTO*/
    var initFuncionesEvento = function () {
        var Imagen = document.getElementById("ImagenBase64").value;

        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
        });
        $('#HttpImagen').fileinput({
            theme: 'fa',
            language: 'es',
            maxFileCount: 1,
            overwriteInitial: true,
            showUploadedThumbs: false,
            allowedFileExtensions: ['png','jpg','jpeg','bmp', 'heic'],
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
            layoutTemplates: { actionDelete: '' }
        });
        $('#HttpImagen').on('fileclear', function (event) {
            document.getElementById("ImagenMostrar").value = "";
        });
       
        
        tblGanadoConEvento = $('#tblGanadoConEvento').DataTable({
            "language": {
                "url": "../../Content/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_flete": Id_flete, "Id_eventoFlete": Id_eventoFlete
                },
                "url": "/Admin/Flete/DatatableGanadoConEvento/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_producto" },
                { "data": "id_ganado" },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoAproximado" },
                { "data": "propio" },
            ],
            "columnDefs": [
                {
                    "targets": [0, 1],
                    "visible": false,
                    "searchable": false
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