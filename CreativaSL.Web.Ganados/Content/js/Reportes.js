var Reporte = function () {
    "use strict";

    var runDatePicker = function () {
        $('#FechaInicio').datepicker({
            format: 'dd/mm/yyyy',
            //startDate: '-0d',
            autoclose: true,language: 'es'
        });
        $('#FechaFin').datepicker({
            format: 'dd/mm/yyyy',
            //startDate: '0d',
            autoclose: true, language: 'es'
        });
        //$('#FechaFin').datepicker('setDaysOfWeekDisabled', [1,6]);
    };

    var runBTN = function () {
        $("#BTN1").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptMermaAlta?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
            }
        });
        $("#BTN2").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptProveedorVendioMas?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
            }
        });
        $("#BTN3").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptSalidas?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1 + "#zoom=125";
            }
        });
        $("#BTN4").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptGanadosVendidos?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
            }
        });
        $("#BTN5").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptGanadosMtoVenta?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
            }
        });
        $("#BTN6").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptSocios?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
            }
        });
        $("#BTN7").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptGanadosMtoCompra?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
            }
         });
        $("#BTN8").click(function () {
             var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                 window.location.href = "/Admin/reportes/RptJaulasXVenta?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
             }
         });
        $("#BTN9").click(function () {
             var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                 window.location.href = "/Admin/reportes/RptEntrada?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
             } RptJaulasXVenta
         });
        $("#BTN10").click(function () {
             var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                 window.location.href = "/Admin/reportes/RptCorrales?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
             } RptJaulasXVenta
         });
        $("#BTN11").click(function () {
             var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                 window.location.href = "/Admin/reportes/RptFletes?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
             }
         });
        $("#BTN12").click(function () {
             var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                 window.location.href = "/Admin/reportes/RptCuentaEstadoProveedor?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
             }
          });
        $("#BTN13").click(function () {
              var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                  window.location.href = "/Admin/reportes/RptCuentaEstadoProveedorActualizado?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
              }
          });
        $("#BTN14").click(function () {
              var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                  window.location.href = "/Admin/reportes/RptEntradasV2?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
              }
        });
        $("#BTN15").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptMttVehiculo?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
            }
        });
        $("#BTN16").click(function () {
            var fecha = document.getElementById("fec1").value;
            var fecha2 = document.getElementById("fec2").value;
            var IDSucural1 = document.getElementById("IDSucursalActual").value;
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
                window.location.href = "/Admin/reportes/RptRendimientoVehiculo?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
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