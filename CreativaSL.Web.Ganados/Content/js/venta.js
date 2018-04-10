var Venta = function () {
    var cGanadoM = 0, cGanadoH = 0, kGanadoM = 0, kGanadoH = 0;
    var kgMachos = document.getElementById("kgMachos");
    var kgHembras = document.getElementById("kgHembras");
    var kgTotal = document.getElementById("kgTotal");
    var cMachos = document.getElementById("cMachos");
    var cHembras = document.getElementById("cHembras");
    var cTotal = document.getElementById("cTotal");
    var tblCorral, tblJaula;
    var iGanadoSeleccionado = document.getElementById("iGanadoSeleccionado");

    var initDataTables = function () {
        tblCorral = $('#corral').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true
        });
        tblJaula = $('#jaula').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true
        });
    };
    var initFuncGanado = function () {
        //Seleccionar filas 
        $('#corral tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#jaula tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });

        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            cGanadoM = 0, cGanadoH = 0, kGanadoM = 0, kGanadoH = 0;
            var rows = tblCorral.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblJaula.row.add([
                    d[0], //Dueño
                    d[1], //Genero
                    d[2]  //Peso
                ]).draw();

                if (d[1] == "MACHO") {
                    cGanadoM += 1;
                    kGanadoM += parseInt(d[2]);
                }
                else {
                    cGanadoH += 1;
                    kGanadoH += parseInt(d[2]);
                }
            }
            sumarGenero(cGanadoM, cGanadoH);
            sumarPesoGanado(kGanadoM, kGanadoH);
            tblCorral.row('.selected').remove().draw(false);
        });
        $('#regresar').click(function () {
            cGanadoM = 0, cGanadoH = 0, kGanadoM = 0, kGanadoH = 0;

            var rows = tblJaula.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblCorral.row.add([
                    d[0], //Dueño
                    d[1], //Genero
                    d[2]  //Peso
                ]).draw();
                if (d[1] == "MACHO") {
                    cGanadoM += 1;
                    kGanadoM += parseInt(d[2]);
                }
                else {
                    cGanadoH += 1;
                    kGanadoH += parseInt(d[2]);
                }
            }
            restarGenero(cGanadoM, cGanadoH);
            restarPesoGanado(kGanadoM, kGanadoH);
            tblJaula.row('.selected').remove().draw(false);
        });
    };
 
   
    var initModal = function () {
        //ModalEvento
        $('#btnAddEvento').on('click', function () {
            ModalEvento(0);
        });

    };
    function LoadItemsModal() {
        $("#fechaEvento").datepicker({
            format: 'dd/mm/yyyy',
            language: 'es'
        });

        $('#timeEvento').timepicker();

        $('#descontar').change(function () {
            if ($('#descontar').prop('checked')) {
                $('.esconder').show(1000);
            }
            else{
                $('.esconder').hide(1000);
            }
        });

    }
    //Funciones para los modales
    function ModalEvento() {
        $.ajax({
            url: 'ModalEvento',
            type: "POST",
            data: {},
            success: function (data) {
                $('#ContenidoModalEvento').html(data);
                $('#ModalEvento').modal({ backdrop: 'static', keyboard: false });

                LoadItemsModal();


            }
        });
    }
    //Funciones para suma y resta de ganado - pesos
    function sumarPesoGanado(kMachos, kHembras) {
        kgMachos.value = parseInt(kgMachos.value) + parseInt(kMachos);
        kgHembras.value = parseInt(kgHembras.value) + parseInt(kHembras);
        kgTotal.value = parseInt(kgMachos.value) + parseInt(kgHembras.value);
    }
    function restarPesoGanado(kMachos, kHembras) {
        kgMachos.value = parseInt(kgMachos.value) - parseInt(kMachos);
        kgHembras.value = parseInt(kgHembras.value) - parseInt(kHembras);
        kgTotal.value = parseInt(kgMachos.value) + parseInt(kgHembras.value);
    }
    function sumarGenero(cantMachos, cantHembras) {
        cMachos.value = parseInt(cMachos.value) + parseInt(cantMachos);
        cHembras.value = parseInt(cHembras.value) + parseInt(cantHembras);
        cTotal.value = parseInt(cMachos.value) + parseInt(cHembras.value);
        iGanadoSeleccionado.value = cTotal.value;
    }
    function restarGenero(cantMachos, cantHembras) {
        cMachos.value = parseInt(cMachos.value) - parseInt(cantMachos);
        cHembras.value = parseInt(cHembras.value) - parseInt(cantHembras);
        cTotal.value = parseInt(cMachos.value) + parseInt(cHembras.value);
        iGanadoSeleccionado.value = cTotal.value;
    }

    return {
        init: function () {
            initDataTables();
            initFuncGanado();
            initModal();
        }
    };
}();