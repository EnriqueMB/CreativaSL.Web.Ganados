var CompraGanado = function () {
    "use strict"
    var tblGanado;
    var numeroFila = 1;
    var IDCompra = $("#IDCompra").val();
    var nNodes, listaPrecioPesoProveedor, tolerancia, listaEstadoGanado, listaCorrales;
    var guardarNodos = new Array();

    var LoadTableGanado = function () {
        tblGanado = $('#tblGanado').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            "orderFixed": {
                "pre": [3, 'asc'],
                "post": [0, 'asc']
            },
            columnDefs: [
                { "type": "html-input", "targets": [1, 2, 3, 4, 5, 6, 7, 8, 9] },
                { "width": "5%", "targets": [0] }
            ]
        });
        //para que realice la busqueda por el valor de cada elemento
        $.fn.dataTableExt.ofnSearch['html-input'] = function (value) {
            return $(value).val();
        };
        //obtengo los valores de la tabla
        //$.ajax({
        //    url: '/Admin/Compra/TableJsonGanadoCompra/',
        //    type: "POST",
        //    data: { IDFlete: IDFlete },
        //    success: function (data) {
        //        for (var i = 0; i < data.length; i++) {
        //            AgergarFilas(true, data[i].numArete, data[i].genero, data[i].pesoAproximado, "Registrado", data[i].id_producto);
        //        }
        //        RunEventoGanado();
        //    }
        //});
    }
    function AgergarFilas(id_fila, guardado, mensaje, numArete, genero, peso, repeso, merma, pesopagar, costoxkilo, id_corral, total, id_estadoGanado) {
        //1 columna, imagen y aviso
        var html_imagen = '';
        if (guardado)
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido"  src="/Content/img/tabla/ok.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        else
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido" src="/Content/img/tabla/cancel.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        //2 columna, arete
        var html_arete = '<input id="arete_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="text" maxlength="15" value="' + numArete + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el número de arete.">';
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
        //4 columna, estado
        var html_estatusGanado = '<select id="estatusganado_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Por favor, seleccion el estado actual del ganado." >';
        var opciones_estatusGanado = '';

        for (var item in listaEstadoGanado) {
            if (listaEstadoGanado[item].id_estatusGanado == id_estadoGanado) {
                opciones_estatusGanado += '<option value="' + listaEstadoGanado[item].id_estatusGanado + '" selected>' + listaEstadoGanado[item].descripcion + '</option>';
            }
            else {
                opciones_estatusGanado += '<option value="' + listaEstadoGanado[item].id_estatusGanado + '">' + listaEstadoGanado[item].descripcion + '</option>';
            }
        }
        html_estatusGanado += opciones_estatusGanado;
        html_estatusGanado += '</select> ';

        //5 columna, peso
        var html_peso = '<input id="peso_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + peso + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el peso inicial del ganado.">';
        //6 columna, repeso
        var html_repeso = '<input id="repeso_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + repeso + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el repeso del ganado.">';
        //7 columna, merma
        var html_merma = '<input id="merma_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + merma + '" data-toggle="tooltip" data-placement="top" title="Merma generada del ganado." readonly="readonly">';
        //8 columna, pesoPagar
        var html_pesoPagar = '<input id="pesopagar_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + pesopagar + '" data-toggle="tooltip" data-placement="top" title="Peso a pagar." readonly="readonly">';
        //9 columna, costoXkilo
        var html_costoxkilo = '<input id="costoxkilo_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="number" value="' + costoxkilo + '" data-toggle="tooltip" data-placement="top" title="Costo por kilo." readonly="readonly">';
        //10 columna, corral
        //var html_corral = '<input id="corral_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="text" value="' + corral + '" data-toggle="tooltip" data-placement="top" title="Corral asignado." readonly="readonly">';

        var html_corral = '<select id="corral_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Corral asignado." >';
        var opciones_corrales = '';

        for (var item in listaCorrales) {
            if (listaCorrales[item].IDCorral == id_corral) {
                opciones_corrales += '<option value="' + listaCorrales[item].IDCorral + '" selected>' + listaCorrales[item].Descripcion + '</option>';
            }
            else {
                opciones_corrales += '<option value="' + listaCorrales[item].IDCorral + '">' + listaCorrales[item].Descripcion + '</option>';
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
            html_estatusGanado,
            html_peso,
            html_repeso,
            html_merma,
            html_pesoPagar,
            html_costoxkilo,
            html_corral,
            html_total,
            ' <div class="visible-md visible-lg hidden-sm hidden-xs">' +
            '<a id="a_' + id_fila + '" data-hrefa="/Admin/Compra/DEL_Ganado/" title="Eliminar" data-id="' + id_fila + '" class="btn btn-danger tooltips btn-sm deleteGanado cslElegido" data-toggle="tooltip" data-placement="top" data-original-title="Eliminar"><i class="fa fa-trash-o"></i></a>' +
            '</div>' +
            '<div class="visible-xs visible-sm hidden-md hidden-lg">' +
            '<div class="btn-group">' +
            '<a class="btn btn-danger dropdown-toggle btn-sm" data-toggle="dropdown" href="#"' +
            '<i class="fa fa-cog"></i> <span class="caret"></span>' +
            '</a>' +
            '<ul role="menu" class="dropdown-menu pull-right dropdown-dark">' +
            '<li>' +
             '<a id="aMin_' + id_fila + '" data-hrefa="/Admin/Compra/DEL_Ganado/" data-id="' + id_fila + '" class="deleteGanado cslElegido" role="menuitem" tabindex="-1" data-id="">' +
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
                    AgergarFilas(numeroFila, false, "Sin registrar",  0, "macho", 0, 0, 0, 0, 0, 0, 0, 1);
                    numeroFila++;
                }
            }
            //después de inserter muestra las nuevas filas
            tblGanado.order([0, 'desc']).draw(false);
        });
        $('#btnSaveRowGanado').on('click', function () {
            var flag_error = false, flag_ok = false, flag = true;
            var conteo = 1, NUM_ELEMENTOS_FILA = 13, mensaje_error_general;
            
            for (var i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA) {
                //(id_fila, guardado, mensaje, numArete, genero, peso, repeso, merma, pesopagar, costoxkilo, corral, total)
                var nId = nNodes[i + 2].dataset.id;
                var nArete = nNodes[i + 2].value;
                var nPeso = nNodes[i + 4].value;
                var nRepeso = nNodes[i + 5].value;

                console.log(nArete);

                /*INICIA VALIDACION*/
                if (nArete.length == 0 || nArete == '' || nArete == null || nArete == 0) {
                    nNodes[i + 2].classList.remove('okCSLGanado');
                    nNodes[i + 2].classList.add('errorCSLGanado');
                    flag = false;
                }
                else {
                    nNodes[i + 2].classList.remove('errorCSLGanado');
                    nNodes[i + 2].classList.add('okCSLGanado');

                }
                if (nPeso.length == 0 || nPeso == '' || nPeso == null || nPeso == '0') {
                    nNodes[i + 4].classList.add('errorCSLGanado');
                    nNodes[i + 4].classList.remove('okCSLGanado');
                    flag = false;
                }
                else {
                    nNodes[i + 4].classList.add('okCSLGanado');
                    nNodes[i + 4].classList.remove('errorCSLGanado');
                }
                /*TERMINA VALIDACION*/

                /*SI TODO ESTA BIEN ENVIAMOS*/
                if (flag) {
                    nNodes[i].src = "/Content/img/tabla/loading.gif";
                    nNodes[i + 1].innerText = "Guardando";
                    $.ajax({
                        url: '/Admin/Compra/AC_Ganado/',
                        type: "POST",
                        data: { IDFlete: id_flete, IDProducto: nId, numArete: nArete, genero: nGenero, peso: nPeso, posicionNode: i },
                        success: function (response) {
                            var obj = JSON.parse(response.Mensaje);
                            var indice = parseInt(obj.posicionNode);

                            if (response.Success) {
                                flag_ok = true;
                                //imagen
                                nNodes[indice].src = "/Content/img/tabla/ok.png";
                                nNodes[indice].id = "img_" + obj.IDProducto;
                                //label
                                nNodes[indice + 1].id = "lbl_" + obj.IDProducto;
                                nNodes[indice + 1].innerText = "Registrado";
                                //numArete 
                                nNodes[indice + 2].id = "arete_" + obj.IDProducto;
                                nNodes[indice + 2].dataset.id = obj.IDProducto;
                                //genero
                                nNodes[indice + 3].id = "genero_" + obj.IDProducto;
                                //peso
                                nNodes[indice + 4].id = "peso_" + obj.IDProducto;
                                //a (btn eliminar)
                                nNodes[indice + 5].id = "a_" + obj.IDProducto;
                                nNodes[indice + 5].dataset.id = obj.IDProducto;
                                //amin (btn eliminar min)
                                nNodes[indice + 6].id = "aMin_" + obj.IDProducto;
                                nNodes[indice + 6].dataset.id = obj.IDProducto;
                            }
                            else {
                                nNodes[indice].src = "/Content/img/tabla/cancel.png";
                                nNodes[indice + 1].innerText = obj.Mensaje;
                            }
                        }
                    });

                }

            }

        });
        $('#tblGanadoExterno tbody').on('click', '.deleteGanado', function (e) {
            var tr = $(this).parents('tr');
            var url = $(this).attr('data-hrefa');
            var id = $(this).attr('data-id');
            var box = $("#mb-remove-ganado");
            box.addClass("open");
            box.find(".mb-control-yes").on("click", function () {
                box.removeClass("open");
                if (id.length == 36) {
                    //eliminamos del servidor
                    var IDCompra = document.getElementById("IDCompra").value;
                    $.ajax({
                        url: url,
                        data: { IDProducto: id, IDCompra: IDCompra },
                        type: 'POST',
                        dataType: 'json',
                        success: function (result) {
                            if (result.Success) {
                                box.find(".mb-control-yes").prop('onclick', null).off('click');
                                Mensaje(result.Mensaje, "1");
                                //eliminamos del datatable
                                tblGanado.row(tr).remove().draw(false);
                            }
                            else
                                Mensaje(result.Mensaje, "2");
                        },
                        error: function (result) {
                            Mensaje(result.Mensaje, "2");
                        }
                    });

                } else {
                    //solo eliminamos la fila de datatables
                    tblGanado.row(tr).remove().draw(false);
                }
            });
        });


        $('#tblGanado tbody').on('change', '.inputCSL', function (e) {
            var $td = $(this).parent();
            $td.find('input').attr('value', this.value);

            //obtenemos el valor de toda la fila
            var $tr = $(this).closest("tr");
            var fila = $tr.find('.cslElegido');
            calculosGanado(fila);
            var idx = tblGanado.row($tr).index();
            tblGanado.cell($td).invalidate('dom').draw();
            guardarNodos.push(idx);
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
            calculosGanado(fila);
            var idx = tblGanado.row($tr).index();
            tblGanado.cell($td).invalidate('dom').draw();
            guardarNodos.push(idx);
        });
    }
    //Nuevas funciones para el calculo del peso
    function calculosGanado(fila) {
        var mermaObtenida, pesoPagar, precioXkilo, total;
        var genero = fila[3].value;
        var peso = fila[4].value;
        var repeso = fila[5].value;
        var pesoFinal = peso;
        //No hubo repeso
        if (repeso == 0){
            repeso = peso;
        }
        //merma
        mermaObtenida = MermaGenerada(peso, repeso);
        //peso_a_pagar
        pesoPagar = PesoSugerido(pesoFinal, mermaObtenida);
        //precio por kilo
        precioXkilo = PrecioSugerido(pesoPagar, genero);
        //total de la compra
        total = TotalPagar(pesoPagar, precioXkilo);
        fila[6].value = mermaObtenida;
        fila[7].value = pesoPagar;
        fila[8].value = precioXkilo;
        fila[10].value = total;

    }
    function MermaGenerada(pesoInicial, pesoFinal) {
        var mermaGenerada = (((pesoFinal * 100) / pesoInicial) - 100) * (-1);
        mermaGenerada = Math.abs(mermaGenerada.toFixed(2));
        return mermaGenerada;
    }
    function PesoSugerido(peso, MermaObtenida) {
        if (MermaObtenida > tolerancia) {

            var pesoPagar = peso - (peso * (tolerancia / 100));
            return pesoPagar;
        }
        else {
            return peso;
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
    
    return {
        init: function (lista, toleranciaP, listaEstadoG, listacorral) {
            listaPrecioPesoProveedor = lista;
            tolerancia = toleranciaP;
            listaEstadoGanado = listaEstadoG;
            listaCorrales = listacorral;
            LoadTableGanado();
            RunEventoGanado();
        }
    };
}();