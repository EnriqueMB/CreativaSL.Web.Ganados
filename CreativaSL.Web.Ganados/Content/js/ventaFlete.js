var VentaFlete = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();
   
    /*INICIA FLETE*/
    var InitMap = function (option) {
        
            var directionsDisplay = new google.maps.DirectionsRenderer;
            var directionsService = new google.maps.DirectionsService;
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 10,
                center: { lat: 17.6063149, lng: -93.204288 }
            });
            directionsDisplay.setMap(map);

            var onChangeHandler = function () {
                CalculateAndDisplayRoute(directionsService, directionsDisplay);
            };
            document.getElementById("Flete_Trayecto_ListaLugarOrigen").addEventListener('change', onChangeHandler);
            document.getElementById("Flete_Trayecto_ListaLugarDestino").addEventListener('change', onChangeHandler);

            CalculateAndDisplayRoute(directionsService, directionsDisplay);
        
    };
    var Eventos = function (){
        $("#Id_cliente").on("change", function () {
            var Id_cliente = $(this).val();
            GetListadoLugaresCliente(Id_cliente);
        });
        $("#Flete_Id_empresa").on("change", function () {
            var Id_empresa = $(this).val();
            GetChoferesXIDEmpresa(Id_empresa);
            GetVehiculosXIDEmpresa(Id_empresa);
            GetLugaresXIDEmpresa(Id_empresa);
        });
    }
    function CalculateAndDisplayRoute(directionsService, directionsDisplay) {
        var selectIndexInicio = document.getElementById('Flete_Trayecto_ListaLugarOrigen').selectedIndex;
        var optionInicio = document.getElementById('Flete_Trayecto_ListaLugarOrigen').options.item(selectIndexInicio);
        var latitudInicial = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial = optionInicio.dataset.longitud.replace(",", ".");

        var selectIndexFinal = document.getElementById('Flete_Trayecto_ListaLugarDestino').selectedIndex;
        var optionFinal = document.getElementById('Flete_Trayecto_ListaLugarDestino').options.item(selectIndexFinal);
        var latitudFinal = optionFinal.dataset.latitud.replace(",", ".");
        var longFinal = optionFinal.dataset.longitud.replace(",", ".");

        var inicio = new google.maps.LatLng(latitudInicial, longInicial);
        var final = new google.maps.LatLng(latitudFinal, longFinal);

        if ((latitudInicial != 0 && longInicial != 0) && (latitudFinal != 0 && longFinal != 0)) {
            directionsService.route({
                origin: inicio,
                destination: final,
                travelMode: 'DRIVING'
            }, function (response, status) {
                if (status === 'OK') {
                    directionsDisplay.setDirections(response);
                } else {
                    window.alert('No se pudo cargar la ubicación, verifique sus coordenas en el catálogo de Lugares, estatus: ' + status);
                }
            });
        }
        else {

            if ((optionInicio.text != "-- Seleccione --") && (optionFinal.text != "-- Seleccione --"))
                window.alert('No se pudo cargar la ubicación, verifique sus coordenas en el catálogo de Lugares');
        }
    }
    function GetListadoLugaresCliente(Id_cliente) {
        $.ajax({
            url: '/Admin/Venta/GetListadoLugaresCliente/',
            type: "POST",
            dataType: 'json',
            data: { Id_cliente: Id_cliente },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Flete_Trayecto_ListaLugarDestino option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Flete_Trayecto_ListaLugarDestino").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }
    function GetChoferesXIDEmpresa(Id_empresa) {
        $.ajax({
            url: '/Admin/Venta/GetChoferesXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: Id_empresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Flete_id_chofer option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Flete_id_chofer").append('<option value="' + result[i].IDChofer + '">' + result[i].Nombre + '</option>');
                }
                $('#Flete_id_chofer.select').selectpicker('refresh');


                
            }
        });
    }
    function GetVehiculosXIDEmpresa(Id_empresa) {
        $.ajax({
            url: '/Admin/Venta/GetVehiculosXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: Id_empresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $('#Flete_id_vehiculo').empty();
                var optgroup = result[0].Modelo;
                var option = '<optgroup label="' + result[0].Modelo + '"><option value="' + result[0].IDVehiculo + '">' + result[0].nombreMarca + '</option>';

                for (var i = 1; i < result.length; i++) {
                    if (optgroup == result[i].Modelo) {
                        option += '<option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                    }
                    else {
                        //Cerramos el grupo
                        option += '</optgroup>';
                        //Anexamos al select
                        $("#Flete_id_vehiculo").append(option);
                        //Creamos un group nuevo
                        option = '<optgroup label="' + result[i].Modelo + '"><option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                        optgroup = result[i].Modelo;
                    }
                }
                //Anexamos el último valor
                option += '</optgroup>';
                $("#Flete_id_vehiculo").append(option);
                $('#Flete_id_vehiculo.select').selectpicker('refresh');
            }
        });
    }
    function GetLugaresXIDEmpresa(Id_empresa) {
        $.ajax({
            url: '/Admin/Venta/GetListadoLugaresEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: Id_empresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $("#Flete_Trayecto_ListaLugarOrigen option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Flete_Trayecto_ListaLugarOrigen").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }


    var initFuncionesGanado = function () {
        //Seleccionar filas 
    
        $(document).on('submit', 'form#frm_venta', function (e) {
            e.preventDefault();

            //datos de las tablas
            var ganado = tblGanadoJaula.rows().data();
            var objGanado, cantidad = 0;
            var listaGanado = new Array();

            if (ganado.length == 0) {
                
                $("#tblGanadoJaula").addClass("errorTableCSL");
                $("#validation_summary").append("");
                $("#validation_summary").append("<dd style='color: #ff004d !important; '>-Debe de seleccionar un ganado para su venta</dd>");
            }
            else {
                
                $("#validation_summary").append("");
                for (var i = 0 ; i < ganado.length ; i++) {
                    listaGanado.unshift(ganado[i].id_ganado);
                    cantidad = i + 1;
                }
            }
        });
        
         
    };
    
    /*TERMINA FLETE*/





       return {
        init: function () {
            InitMap();
            Eventos();
        }
    };
}();