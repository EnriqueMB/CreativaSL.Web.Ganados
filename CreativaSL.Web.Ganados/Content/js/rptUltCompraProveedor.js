var rptProximaCompra = function () {
    "use strict";
    // Funcion para validar registrar
   
 
    var rptProxiCompra = function () {
        $('#calendar').fullCalendar({
            lang: 'es',
            height: 600,
            firstDay: 1,
            handleWindowResize: true,
            timezone: 'America/Mexico city',
            eventOverlap: false,
            displayEventTime: true,
            events: function (start, end, timezone, callback) {
                $.ajax({
                    url: '/Admin/RptUltCompraProveedor/ProximaCompra',
                    dataType: 'json',
                    method: 'post',
                    data: {
                        start: '' + start.format('YYYY-MM-DD') + '',
                        end: '' + end.format('YYYY-MM-DD') + ''
                    },
                    success: function (doc) {
                        var events = [];
                        $(doc).each(function (item, value) {
                            events.push({
                                id: value.id,
                                title: value.NombreProveedor,
                                start: value.start,
                                description: "<p> <strong>Folio Compra : </strong>" + value.FolioCompra + "</p>" + "<p><strong>Ganado Comprado Hembra: </strong>" + value.GanadosPactadoHembras + "</p>" +
                   " <p><strong> Ganado Comprado Macho: </strong>" + value.GanadosPactadoMachos + " </p>",
                            });
                        });
                        callback(events);
                    },
                    error: function (xhr, status, error) {
                        alert(xhr.responseText);
                    }
                });
            },
            eventClick: function (event, jsEvent, view) {

                $('#modalTitle').html(event.title);
                $('#modalBody').html(event.description);
                //$('#eventUrl').attr('href', event.url);
                $('#fullCalModal').modal();
            },
        });
    };

    return {
        //main function to initiate template pages
        init: function () {
            rptProxiCompra();
        }
    };
}();