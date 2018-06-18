var Reporte = function () {
    "use strict";

    var runDatePicker = function () {
        $('#FechaInicio').datepicker({
            format: 'dd/mm/yyyy',
            //startDate: '-0d',
            autoclose: true
        });
        $('#FechaFin').datepicker({
            format: 'dd/mm/yyyy',
            //startDate: '0d',
            autoclose: true
        });
        //$('#FechaFin').datepicker('setDaysOfWeekDisabled', [1,6]);
    };

    var runBTN = function () {
        $("#BTN1").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var ban = 0;
            var ban1 = 0;
            if (fecha === "") {
                document.getElementById("result").innerHTML = "La fecha inicio " + fecha + " es incorrecta";
                $('#1').addClass('has-error');
            }
            else {
                document.getElementById("result").innerHTML = "La fecha inicio " + fecha + " es correcta";
                ban = 1;
                $('#1').removeClass('has-error');
            }
            if (fecha2 === "") {
                document.getElementById("result2").innerHTML = "La fecha fin " + fecha2 + " es incorrecta";
                $('#2').addClass('has-error');
            }
            else {
                document.getElementById("result2").innerHTML = "La fecha fin " + fecha2 + " es correcta";
                ban1 = 1;
                $('#2').removeClass('has-error');
            }
            var Inicio = fecha;
            var Fin = fecha2;
            if (ban == 1 && ban1 == 1) {
                window.location.href = "/RptProveedorMermaAlta/Reporte2?id=" + Inicio + "&id2=" + Fin;
            }
        });
        $("#BTN2").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var ban = 0;
            var ban1 = 0;
            if (fecha === "") {
                document.getElementById("result").innerHTML = "La fecha inicio " + fecha + " es incorrecta";
                $('#1').addClass('has-error');
            }
            else {
                document.getElementById("result").innerHTML = "La fecha inicio " + fecha + " es correcta";
                ban = 1;
                $('#1').removeClass('has-error');
            }
            if (fecha2 === "") {
                document.getElementById("result2").innerHTML = "La fecha fin " + fecha2 + " es incorrecta";
                $('#2').addClass('has-error');
            }
            else {
                document.getElementById("result2").innerHTML = "La fecha fin " + fecha2 + " es correcta";
                ban1 = 1;
                $('#2').removeClass('has-error');
            }
            var Inicio = fecha;
            var Fin = fecha2;
            if (ban == 1 && ban1 == 1) {
                window.location.href = "/RptProveedorMermaAlta/Reporte2?id=" + Inicio + "&id2=" + Fin;
            }
        });
    };

    return {
        //main function to initiate template pages
        init: function () {
            runDatePicker();
            runBTN();
        }
    };
}();