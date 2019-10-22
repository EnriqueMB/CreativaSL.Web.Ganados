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
                var rptAnalisisMermaAlta = "/Admin/reportes/RptMermaAlta?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptAnalisisMermaAlta, '_blank');
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
                var rptCompraGanado = "/Admin/reportes/RptProveedorVendioMas?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptCompraGanado, '_blank');
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
                var rptSalidaGastos = "/Admin/reportes/RptSalidas?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1 + "#zoom=125";
                window.open(rptSalidaGastos, '_blank');
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
                var rptAnalisisVenta = "/Admin/reportes/RptGanadosVendidos?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptAnalisisVenta, '_blank');
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
                var rptAnalisisPerdida = "/Admin/reportes/RptGanadosMtoVenta?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptAnalisisPerdida, '_blank');
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
                var rptUtilidades = "/Admin/reportes/RptSocios?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptUtilidades, '_blank');
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
                 var rptfletes = "/Admin/reportes/RptFletes?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                 window.open(rptfletes, '_blank');
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
                  var ReporteEntrada = "/Admin/reportes/RptEntradasV2?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                  window.open(ReporteEntrada, '_blank');

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
                var rptAnalisisMantenimiento = "/Admin/reportes/RptMttVehiculo?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptAnalisisMantenimiento, '_blank');
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
                var rptAnalisisRendimiento = "/Admin/reportes/RptRendimientoVehiculo?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptAnalisisRendimiento, '_blank');
            }
        });
        $("#BTN17").click(function () {
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
                var rptAnalisisAlmacen = "/Admin/reportes/RptAlmacen?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptAnalisisAlmacen, '_blank');
            }
        });
        $("#BTN18").click(function () {
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
                var rptEntradasAlmacen = "/Admin/reportes/RptEntradaAlmacen?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptEntradasAlmacen, '_blank');
            }
        });
        $("#BTN19").click(function () {
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
                var rptSalidasAlmacen = "/Admin/reportes/RptSalidaAlmacen?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptSalidasAlmacen, '_blank');
            }
        });
        $("#BTN20").click(function () {
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
                var url = "/Admin/reportes/RptVentaMerma?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(url, '_blank');
            }
        });
        //--------------------------------------------inicio----------------------------------------------
        $("#BTN21").click(function () {
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
                var UrlCombustible = "/Admin/reportes/RptCombustibles?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(UrlCombustible, '_blank');
            }
        });
        //--------------------------------------------fin-------------------------------------------------

        //--------------------------------------------inicio--------------rptproveedores------------------
        $("#BTN22").click(function () {
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
                var rptproveedor = "/Admin/reportes/RptPagosProveedor?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptproveedor, '_blank');
            }
        });
        //--------------------------------------------fin-------------------------------------------------
        //--------------------------------------------inicio--------------rptproveedores------------------
        $("#BTN23").click(function () {
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
                var rptproveedor = "/Admin/reportes/RptUtilidadVentaCompra?id=PDF&id2=" + Inicio + "&id3=" + Fin + "&id4=" + IDSucural1;
                window.open(rptproveedor, '_blank');
            }
        });
        //--------------------------------------------fin-------------------------------------------------
    };

    return {
        //main function to initiate template pages
        init: function () {
            runDatePicker();
            runBTN();
        }
    };
}();