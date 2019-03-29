var CajaChica = function () {
    "use strict";
    var uiDatatable = function () {

        var table = $('.datatable2').DataTable({
            "processing": true,
            "serverSide": true,
            "language": {
                "url": "/../Content/json/Spanish.json"
            },
            "ajax": {
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/Areas/Admin/WebServices/DatatableServ.asmx/CajaChicaIndex",
                data: function (d) {
                    return JSON.stringify({ parameters: d });
                }
            }
        });
    };
    return {
        //main function to initiate template pages
        init: function () {
            uiDatatable();
        }
    };
}();