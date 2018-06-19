var CompraGanado = function () {
    "use strict"
    var tblGanado;
    var numeroFila = 1;
    var IDCompra = $("#IDCompra").val();
    var SALUDABLE = 1;
    var nNodes, listaPrecioPesoProveedor, tolerancia, listaCorrales;
    var guardarIDs = new Array();
    var IMAGEN = 0, MENSAJE = 1, ARETE = 2, GENERO = 3, PESO = 4, REPESO = 5, MERMA = 6, PESOPAGAR = 7, COSTOPORKILO = 8, CORRAL = 9, TOTAL = 10, BTN_ELIMINAR = 11, BTN_ELIMINAR_MIN = 12;


    var LoadTableGanado = function () {
        tblGanado = $('#tblGanado').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            "searching": false,
            "ordering": false,
            //"orderFixed": {
            //    "pre": [3, 'asc'],
            //    "post": [0, 'asc']
            //},
            columnDefs: [
                { "type": "html-input", "targets": [1, 2, 3, 4, 5, 6, 7, 8, 9, 10] }
            ],
        });

        //para que realice la busqueda por el valor de cada elemento
        $.fn.dataTableExt.ofnSearch['html-input'] = function (value) {
            return $(value).val();
        };
        
        $.ajax({
            url: '/Admin/Compra/TableJsonGanadoCompra/',
            type: "POST",
            data: { IDCompra: IDCompra },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    AgergarFilas(
                        data[i].id_ganado, true, "Registrado", data[i].numArete, data[i].genero,
                        data[i].pesoInicial, data[i].pesoFinal, data[i].merma, data[i].pesoPagado, data[i].precioKilo,
                        data[i].id_corral, data[i].subtotal, data[i].id_detalleDocumentoPorPagar);
                }
            }
        });
    }
    function AgergarFilas(
        id_fila,    guardado,   mensaje,    numArete,   genero,
        peso, repeso, merma, pesopagar, costoxkilo,
        id_corral, total, iddetalledocumento) {
        //1 columna, imagen y aviso
        var html_imagen = '';
        if (guardado)
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido"  src="/Content/img/tabla/ok.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        else
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido" src="/Content/img/tabla/cancel.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        //2 columna, arete
        var html_arete = '<input id="arete_' + id_fila + '" data-id="' + id_fila + '"  data-iddetalledocumento = "' + iddetalledocumento +'" class="form-control inputCSL cslElegido" type="text" maxlength="15" value="' + numArete + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el número de arete.">';
        //3 columna, genero
        var html_macho = '<option value="MACHO">Macho</option>';
        var html_hembra = '<option value="HEMBRA">Hembra</option>';

        if (genero == "Macho" || genero == "MACHO") {
            html_macho = '<option value="Macho" selected>Macho</option>';
        }
        else if (genero == "Hembra" || genero == "HEMBRA") {
            html_hembra = '<option value="Hembra" selected>Hembra</option>';
        }
        var html_genero = '<select id="genero_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Por favor, seleccione el género del animal." >' +
            html_macho +
            html_hembra +
            '</select> ';

        //4 columna, peso
        var html_peso = '<input id="peso_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + peso + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el peso inicial del ganado.">';
        //5 columna, repeso
        var html_repeso = '<input id="repeso_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + repeso + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el repeso del ganado.">';
        //6 columna, merma
        var html_merma = '<input id="merma_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + merma + '" data-toggle="tooltip" data-placement="top" title="Merma generada del ganado." readonly="readonly">';
        //7 columna, pesoPagar
        var html_pesoPagar = '<input id="pesopagar_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslTotal cslElegido" type="number" value="' + pesopagar + '" data-toggle="tooltip" data-placement="top" title="Peso a pagar." readonly="readonly">';
        //8 columna, costoXkilo
        var html_costoxkilo = '<input id="costoxkilo_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslTotal cslElegido" type="number" value="' + costoxkilo + '" data-toggle="tooltip" data-placement="top" title="Costo por kilo." readonly="readonly">';
        //9 columna, corral
        var html_corral = '<select id="corral_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Corral asignado." >';
        var opciones_corrales = '';
        id_corral = parseInt(id_corral);

        for (var item in listaCorrales) {
            var id_corral_server = parseInt(listaCorrales[item].Id_corral);
            //console.log("server : " + id_corral_server + "  id_corral: " + id_corral);
            if (id_corral_server == id_corral) {
                
                opciones_corrales += '<option value="' + listaCorrales[item].Descripcion + '" data-id="' + listaCorrales[item].Id_corral +'" selected>' + listaCorrales[item].Descripcion + '</option>';
            }
            else {
                opciones_corrales += '<option value="' + listaCorrales[item].Descripcion + '" data-id="' + listaCorrales[item].Id_corral +'">' + listaCorrales[item].Descripcion + '</option>';
            }
        }
        html_corral += opciones_corrales;
        html_corral += '</select> ';

        //11 columna, total
        var html_total = '<input id="total_' + id_fila + '"class="form-control cslElegido inputCSL" type="number" min="1" value="' + total + '"  data-toggle="tooltip" data-placement="top" title="Total de la compra" readonly="readonly">';

        tblGanado.row.add([
            html_imagen,
            html_arete,
            html_genero,
            html_peso,
            html_repeso,
            html_merma,
            html_pesoPagar,
            html_costoxkilo,
            html_corral,
            html_total,
            ' <div class="visible-md visible-lg hidden-sm hidden-xs">' +
            '<a id="a_' + id_fila + '" data-hrefa="/Admin/Compra/DEL_Ganado/" title="Eliminar" data-id="' + id_fila + '" data-iddetalledocumento = "' + iddetalledocumento +'"  class="btn btn-danger tooltips btn-sm deleteGanado cslElegido" data-toggle="tooltip" data-placement="top" data-original-title="Eliminar"><i class="fa fa-trash-o"></i></a>' +
            '</div>' +
            '<div class="visible-xs visible-sm hidden-md hidden-lg">' +
            '<div class="btn-group">' +
            '<a class="btn btn-danger dropdown-toggle btn-sm" data-toggle="dropdown" href="#"' +
            '<i class="fa fa-cog"></i> <span class="caret"></span>' +
            '</a>' +
            '<ul role="menu" class="dropdown-menu pull-right dropdown-dark">' +
            '<li>' +
            '<a id="aMin_' + id_fila + '" data-hrefa="/Admin/Compra/DEL_Ganado/" data-id="' + id_fila + '" data-iddetalledocumento = "' + iddetalledocumento +'"  class="deleteGanado cslElegido" role="menuitem" tabindex="-1" data-id="">' +
            '<i class="fa fa-trash-o"></i> Eliminar' +
            '</a>' +
            '</li>' +
            '</ul>' +
            '</div>' +
            '</div>'
        ]).draw(false);
        nNodes = tblGanado.rows().nodes().to$().find('.cslElegido');
    }

    var RunEventoGanado = function () {
        $("#tblGanado tbody").on("click", ".deleteGanado", function (e) {
            
            var tr = $(this).parents('tr');
            var url = $(this).attr('data-hrefa');
            var id_ganado = $(this).attr('data-id');
            var Id_detalleDocumentoPorCobrar = $(this).attr('data-iddetalledocumento');
            var box = $("#mb-delete-ganado");
            box.addClass("open");
            box.find(".mb-control-yes").on("click", function () {
                box.removeClass("open");
                if (id_ganado.length == 36) {
                    $.ajax({
                        url: url,
                        data: { IDCompra: IDCompra, IDGanado: id_ganado, Id_detalleDocumentoPorCobrar: Id_detalleDocumentoPorCobrar },
                        type: 'POST',
                        dataType: 'json',
                        success: function (result) {
                            if (result.Success) {
                                box.find(".mb-control-yes").prop('onclick', null).off('click');
                                $("#Modal").modal('hide');
                                Mensaje("Ganado eliminado correctamente", "1");
                                var obj = JSON.parse(result.Mensaje);
                                ActualizarGenerales(obj.CantidadMachos, obj.CantidadHembras, obj.CantidadTotal, obj.MermaMachos, obj.MermaHembras, obj.MermaTotal, obj.KilosMachos, obj.KilosHembras, obj.KilosTotal, obj.MontoTotalGanado)
                                tblGanado.row(tr).remove().draw(false);
                            }
                            else
                                Mensaje(result.Mensaje, "2");
                        }
                    });
                }
                else {
                    tblGanado.row(tr).remove().draw(false);
                }
               
            });
        });
        $('#btnVerListaPrecios').on('click', function () {
            $("body").css("cursor", "progress");
            var id_tipo_proveedor = document.getElementById("Proveedor_IDTipoProveedor").value;
            
            $.ajax({
                url: '/Admin/Compra/ModalListaPrecios/',
                type: "POST",
                data: { IDTipoProveedor: id_tipo_proveedor},
                success: function (data) {
                    $("body").css("cursor", "default");
                    $('#ContenidoModal').html(data);
                    $('#Modal').modal({ backdrop: 'static', keyboard: false });
                }
            });
        });
        $('#btnAddRowGanado').on('click', function () {
            var numero = document.getElementById("inputGanado").value;
            if (!/^([0-9])*$/.test(numero)) {
                Mensaje("Por favor, ingrese un número.", 2);
            }
            else if (numero.length == 0) {
                Mensaje("Por favor, ingrese un número.", 2);
            }
            else {
                for (var i = 0; i < numero; i++) {
                    AgergarFilas(numeroFila, false, "Sin registrar", 0, "macho", 0, 0, 0, 0, 0, 0, 0, 1, numeroFila);
                    numeroFila++;
                }
            }
            //después de inserter muestra las nuevas filas
            tblGanado.order([0, 'desc']).draw(false);
        });
        $('#btnSaveRowGanado').on('click', function () {
            var flag_error = false, flag_ok = false, flag = true;
            var conteo = 1, NUM_ELEMENTOS_FILA = 13, mensaje_error_general;

            guardarIDs = eliminarDuplicadosArray(guardarIDs);
            console.log(nNodes);

            for (var index = 0; index < guardarIDs.length; index++) {
                var id_guardado = guardarIDs[index];
                //comparo en los nodos y avanzo en grupo de la cantidad de filas
                for (var i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA) {
                    //obtengo el id de la fila
                    var id_fila = nNodes[i + 2].dataset.id;
                    //son iguales, valido 
                    if (id_guardado == id_fila) {
                        /*INICIA VALIDACION*/
                        //arete
                        if (nNodes[i + ARETE].value.length == 0 || nNodes[i + ARETE].value == '' || nNodes[i + ARETE].value == null || nNodes[i + ARETE].value == 0) {
                            nNodes[i + ARETE].classList.remove('okCSLGanado');
                            nNodes[i + ARETE].classList.add('errorCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + ARETE].classList.remove('errorCSLGanado');
                            nNodes[i + ARETE].classList.add('okCSLGanado');
                        }
                        //peso
                        if (nNodes[i + PESO].value.length == 0 || nNodes[i + PESO].value == '' || nNodes[i + PESO].value == null || nNodes[i + PESO].value <= 0 || isNaN(nNodes[i + PESO].value)) {
                            nNodes[i + PESO].classList.add('errorCSLGanado');
                            nNodes[i + PESO].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + PESO].classList.add('okCSLGanado');
                            nNodes[i + PESO].classList.remove('errorCSLGanado');
                        }
                        //repeso
                        if (nNodes[i + REPESO].value.length == 0 || nNodes[i + REPESO].value == '' || nNodes[i + REPESO].value == null || nNodes[i + REPESO].value < 0 || isNaN(nNodes[i + REPESO].value)) {
                            nNodes[i + REPESO].classList.add('errorCSLGanado');
                            nNodes[i + REPESO].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + REPESO].classList.add('okCSLGanado');
                            nNodes[i + REPESO].classList.remove('errorCSLGanado');
                        }
                        //pesoAPagar
                        if (nNodes[i + PESOPAGAR].value.length == 0 || nNodes[i + PESOPAGAR].value == '' || nNodes[i + PESOPAGAR].value == null || nNodes[i + PESOPAGAR].value <= 0 || isNaN(nNodes[i + PESOPAGAR].value)) {
                            nNodes[i + PESOPAGAR].classList.add('errorCSLGanado');
                            nNodes[i + PESOPAGAR].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + PESOPAGAR].classList.add('okCSLGanado');
                            nNodes[i + PESOPAGAR].classList.remove('errorCSLGanado');
                        }
                        //costoXkilo
                        if (nNodes[i + COSTOPORKILO].value.length == 0 || nNodes[i + COSTOPORKILO].value == '' || nNodes[i + COSTOPORKILO].value == null || nNodes[i + COSTOPORKILO].value <= 0 || isNaN(nNodes[i + COSTOPORKILO].value)) {
                            nNodes[i + COSTOPORKILO].classList.add('errorCSLGanado');
                            nNodes[i + COSTOPORKILO].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + COSTOPORKILO].classList.add('okCSLGanado');
                            nNodes[i + COSTOPORKILO].classList.remove('errorCSLGanado');
                        }

                        if (flag) {
                            nNodes[i].src = "/Content/img/tabla/loading.gif";
                            nNodes[i + MENSAJE].innerText = "Guardando";
                            var id_ganado = nNodes[i + ARETE].dataset.id;
                            var id_detalleDocumento = nNodes[i + ARETE].dataset.iddetalledocumento;
                            var numArete = nNodes[i + ARETE].value;
                            var id_genero = nNodes[i + GENERO].value;
                            var peso = nNodes[i + PESO].value;
                            var repeso = nNodes[i + REPESO].value;
                            var merma = nNodes[i + MERMA].value;
                            var peso_pagar = nNodes[i + PESOPAGAR].value;
                            var costo_kilo = nNodes[i + COSTOPORKILO].value;
                            var id_corral = nNodes[i + CORRAL].selectedOptions[0].dataset.id;

                            $.ajax({
                                url: '/Admin/Compra/AC_Ganado/',
                                type: "POST",
                                data: {
                                    IDCompra: IDCompra, IDGanado: id_ganado, numArete: numArete, id_genero: id_genero,
                                    peso: peso, repeso: repeso, merma: merma, peso_pagar: peso_pagar,
                                    costo_kilo: costo_kilo, id_corral: id_corral, Id_detalleDocumentoPorCobrar: id_detalleDocumento,
                                    indiceActual: i
                                },
                                success: function (response) {
                                    var obj = JSON.parse(response.Mensaje);
                                    var indice = parseInt(obj.indiceActual);

                                    if (response.Success) {
                                        flag_ok = true;
                                        //imagen
                                        nNodes[indice].src = "/Content/img/tabla/ok.png";
                                        nNodes[indice].id = "img_" + obj.id_ganado;
                                        //label
                                        nNodes[indice + MENSAJE].id = "lbl_" + obj.id_ganado;
                                        nNodes[indice + MENSAJE].innerText = "Registrado";
                                        //numArete 
                                        nNodes[indice + ARETE].id = "arete_" + obj.id_ganado;
                                        nNodes[indice + ARETE].dataset.id = obj.id_ganado;
                                        nNodes[indice + ARETE].dataset.iddetalledocumento = obj.id_detalleDoctoCobrar;
                                        //genero
                                        nNodes[indice + GENERO].id = "genero_" + obj.id_ganado;
                                        //peso
                                        nNodes[indice + PESO].id = "peso_" + obj.id_ganado;
                                        //repeso
                                        nNodes[indice + REPESO].id = "repeso_" + obj.id_ganado;
                                        //merma_
                                        nNodes[indice + MERMA].id = "merma_" + obj.id_ganado;
                                        //pesopagar_
                                        nNodes[indice + PESOPAGAR].id = "pesopagar_" + obj.id_ganado;
                                        //costoxkilo_
                                        nNodes[indice + COSTOPORKILO].id = "costoxkilo_" + obj.id_ganado;
                                        //corral_
                                        nNodes[indice + CORRAL].id = "corral_" + obj.id_ganado;
                                        //total_
                                        nNodes[indice + TOTAL].id = "total_" + obj.id_ganado;

                                        //a (btn eliminar)
                                        nNodes[indice + BTN_ELIMINAR].id = "a_" + obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR].dataset.id = obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR].dataset.iddetalledocumento = obj.id_detalleDoctoCobrar
                                        //amin (btn eliminar min)
                                        nNodes[indice + BTN_ELIMINAR_MIN].id = "aMin_" + obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR_MIN].dataset.id = obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR_MIN].dataset.iddetalledocumento = obj.id_detalleDoctoCobrar

                                        ActualizarGenerales(obj.CantidadMachos, obj.CantidadHembras, obj.CantidadTotal, obj.MermaMachos, obj.MermaHembras, obj.MermaTotal, obj.KilosMachos, obj.KilosHembras, obj.KilosTotal, obj.MontoTotalGanado)
                                    }
                                    else {
                                        nNodes[indice].src = "/Content/img/tabla/cancel.png";
                                        nNodes[indice + MENSAJE].innerText = obj.Mensaje;
                                    }
                                }
                            });

                        }

                    }
                }
            }
        });
        $('#tblGanado tbody').on('change', '.inputCSL', function (e) {
            var $td = $(this).parent();
            
            $td.find('input').attr('value', this.value);

            //obtenemos el valor de toda la fila
            var $tr = $(this).closest("tr");
            var fila = $tr.find('.cslElegido');
            calculosGanado(fila);

            var idx = fila[2].dataset.id;
            tblGanado.cell($td).invalidate('dom').draw(false);
            guardarIDs.push(idx);
        });
        $('#tblGanado tbody').on('change', '.cslTotal', function (e) {
            var $td = $(this).parent();
            $td.find('input').attr('value', this.value);

            //obtenemos el valor de toda la fila
            var $tr = $(this).closest("tr");
            var fila = $tr.find('.cslElegido');

            fila[11].value = TotalPagar(fila[8].value, fila[9].value);

            var idx = fila[2].dataset.id;
            tblGanado.cell($td).invalidate('dom').draw(false);
            guardarIDs.push(idx);
        });
        $("#tblGanado tbody").on('change', '.selectCSL', function (e) {
            var $td = $(this).parent();
            var value = this.value;

            $td.find('option').each(function (i, o) {
                if ($(o).val() == value) {
                    $(o).attr('selected', "selected");
                }
                else {
                    $(o).removeAttr('selected');
                }
            })

            //obtenemos el valor de toda la fila
            var $tr = $(this).closest("tr");
            var fila = $tr.find('.cslElegido');

            //solo se le asigna el calculo al select de genero
            var id = this.id;
            var genero_ganado = "genero_";
            var buscar = id.indexOf(genero_ganado);

            if (buscar != -1) {
                calculosGanado(fila);
            }

            //actualizo datatables y guardo el nodo actualizado
            var idx = fila[2].dataset.id;
            tblGanado.cell($td).invalidate('dom').draw(false);
            guardarIDs.push(idx);
        });

        function eliminarDuplicadosArray(arr) {
            var i,
                len = arr.length,
                out = [],
                obj = {};

            for (i = 0; i < len; i++) {
                obj[arr[i]] = 0;
            }
            for (i in obj) {
                out.push(i);
            }
            return out;
        }
    }

    function ActualizarGenerales(CantidadMachos, CantidadHembras, CantidadTotal, MermaMachos, MermaHembras, MermaTotal, KilosMachos, KilosHembras, KilosTotal, MontoTotalGanado) {
        var cantidadMachos = CantidadMachos;
        var cantidadHembras = CantidadHembras;
        var cantidadTotal = CantidadTotal
        var mermaMachos = parseFloat(MermaMachos);
        var mermaHembras = parseFloat(MermaHembras);
        var mermaTotal = parseFloat(MermaTotal);
        var kilosMachos = parseFloat(KilosMachos);
        var kilosHembras = parseFloat(KilosHembras);
        var kilosTotal = parseFloat(KilosTotal);
        var montoTotalGanado = parseFloat(MontoTotalGanado);

        //actualizamos los generales
        $("#GanadosCompradoMachos").val(cantidadMachos);
        $("#GanadosCompradoHembras").val(cantidadHembras);
        $("#GanadosCompradoTotal").val(cantidadTotal);
        $("#MermaMachos").val(mermaMachos.toFixed(2));
        $("#MermaHembras").val(mermaHembras.toFixed(2));
        $("#MermaTotal").val(mermaTotal.toFixed(2));
        $("#KilosMachos").val(kilosMachos.toFixed(2));
        $("#KilosHembras").val(kilosHembras.toFixed(2));
        $("#KilosTotal").val(kilosTotal.toFixed(2));
        $("#MontoTotalGanado").val(montoTotalGanado.toFixed(2));
    }

    //Nuevas funciones para el calculo del peso
    function calculosGanado(fila) {
        var mermaObtenida, pesoPagar, precioXkilo, total, pesoFinal;
        var genero = fila[GENERO].value;
        var peso = fila[PESO].value;
        var repeso = fila[REPESO].value;

        //NO hay repeso
        if (repeso <= 0) {
            repeso = peso;
        }
        //merma
        mermaObtenida = MermaGenerada(peso, repeso); //bien
        //peso_a_pagar
        pesoPagar = PesoSugerido(peso, mermaObtenida);
        //precio por kilo
        precioXkilo = PrecioSugerido(pesoPagar, genero);
        //total de la compra
        total = TotalPagar(pesoPagar, precioXkilo);
        corral
        var corral = CorralSugerido(pesoPagar, genero);
        //console.log("corral: " + corral);
        //quitamos lo seleccionado del select 
        $("#" + fila[CORRAL].id + " option").removeAttr('selected');
        //agregamos el seleccionado
        $("#" + fila[CORRAL].id + " option[value='" + corral + "']").attr('selected', 'selected');

        fila[MERMA].value = mermaObtenida;
        fila[PESOPAGAR].value = pesoPagar;
        fila[COSTOPORKILO].value = precioXkilo;
        fila[TOTAL].value = total;
    }
    function MermaGenerada(pesoInicial, pesoFinal) {
        var mermaGenerada = (((pesoFinal * 100) / pesoInicial) - 100) * (-1);
        mermaGenerada = Math.abs(mermaGenerada.toFixed(2));

        if (isNaN(mermaGenerada))
            mermaGenerada = 0;

        return mermaGenerada;
    }
    function PesoSugerido(peso, MermaObtenida) {
        if (MermaObtenida > tolerancia) {

            var pesoPagar = peso - (peso * (tolerancia / 100));

            return Math.trunc(pesoPagar);
        }
        else {
            return Math.trunc(peso);
        }
    }
    function PrecioSugerido(peso, genero) {
        var genero = (genero == "Macho" || genero == "MACHO") ? true : false;
        for (var item in listaPrecioPesoProveedor) {
            if (listaPrecioPesoProveedor[item].EsMacho == genero) {
                if ((listaPrecioPesoProveedor[item].PesoMinimo <= peso) && (peso <= listaPrecioPesoProveedor[item].PesoMaximo)) {
                    return listaPrecioPesoProveedor[item].Precio;
                }
            }
        }
        return 0;
    }
    function TotalPagar(peso, precioxkilo){
        var total = peso * precioxkilo;
        return total.toFixed(2);
    }
    //despues de 3 o 4 veces deja de funcionar el select D:
    function CorralSugerido(peso, genero) {
        for (var item in listaCorrales) {
            if (listaCorrales[item].Genero.trim().localeCompare(genero) == 0) {
                if ((listaCorrales[item].Rango_inferior <= peso) && (peso <= listaCorrales[item].Rango_superior)) {
                    return listaCorrales[item].Descripcion;
                }
            }
        }
        return "Sin corral";
    }
    
    return {
        init: function (lista, toleranciaP, listacorral) {
            listaPrecioPesoProveedor = lista;
            tolerancia = toleranciaP;
            listaCorrales = listacorral;
            LoadTableGanado();
            RunEventoGanado();
        }
    };
}();