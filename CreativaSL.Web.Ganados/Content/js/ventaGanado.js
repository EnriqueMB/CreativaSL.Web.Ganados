var CompraAgendar = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();

    /*INICIA GANADO*/
    var initDataTables = function () {
    
    var tblGanadoCorral = $('#tblGanadoCorral').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_venta": Id_venta
                },
                "url": "/Admin/Venta/DatatableGanadoActual/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_ganado" },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoPagado" },
                { "data": "estado" }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
       

        tblJaula = $('#jaula').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true
        });
    };
    /*TERMINA GANADO*/





       return {
        init: function () {
            initDataTables();
        }
    };
}();