var Servicios = function () {
    "use strict";
    var uiDatatable = function () {
        if ($(".datatable2").length > 0) {
            $(".datatable2").dataTable({
                "order": [],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                },
                responsive: true
            });
            $(".datatable2").on('page.dt', function () {
                onresize(100);
            });
        }
    };//END Datatable
    return {
        //main function to initiate template pages
        init: function () {
            uiDatatable();
        }
    };
}();