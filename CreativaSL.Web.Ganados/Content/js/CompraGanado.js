var CompraGanado = function () {
    "use strict"
    var tblGanado;
    var numeroFila = 1;
    var IDCompra = $("#IDCompra").val();

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
                { "type": "html-input", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
                { "width": "5%", "targets": [0] }
            ]
        });
        //para que realice la busqueda por el valor de cada elemento
        $.fn.dataTableExt.ofnSearch['html-input'] = function (value) {
            return $(value).val();
        };
        $.ajax({
            url: '/Admin/Compra/TableJsonGanadoCompra/',
            type: "POST",
            data: { IDFlete: IDFlete },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    AgergarFilasProductoGanadoExterno(true, data[i].numArete, data[i].genero, data[i].pesoAproximado, "Registrado", data[i].id_producto);
                }
                RunEventoProductoGanadoExterno();
            }
        });
    }

    function AgergarFilasProductoGanadoExterno(propio, numArete, genero, peso, mensaje, id_fila) {
        if (propio)
            var html_imagen = '<img id="img_' + id_fila + '" class="cslElegido"  src="/Content/img/tabla/ok.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        else
            var html_imagen = '<img id="img_' + id_fila + '" class="cslElegido" src="/Content/img/tabla/cancel.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        var html_macho = '<option value="MACHO">Macho</option>';
        var html_hembra = '<option value="HEMBRA">Hembra</option>';

        if (genero == "Macho" || genero == "MACHO") {

            html_macho = '<option value="Macho" selected>Macho</option>';
        }
        else if (genero == "Hembra" || genero == "HEMBRA") {
            html_hembra = '<option value="Hembra" selected>Hembra</option>';
        }

        var html_arete = '<input id="arete_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido" type="text" maxlength="15" value="' + numArete + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el número de arete.">';
        var html_genero = '<select id="genero_' + id_fila + '"class="selectpicker form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Por favor, seleccione el género del animal." >' +
            html_macho +
            html_hembra +
            '</select> ';
        var html_peso = '<input id="peso_' + id_fila + '"class="form-control cslElegido inputCSL" type="number" min="1" value="' + peso + '"  data-toggle="tooltip" data-placement="top" title="Por favor, escriba el peso del animal, debe ser mayor a 0.">';

        tblModalGanadoExterno.row.add([
            html_imagen,
            html_arete,
            html_genero,
            html_peso,
            ' <div class="visible-md visible-lg hidden-sm hidden-xs">' +
            '<a id="a_' + id_fila + '" data-hrefa="/Admin/Flete/DEL_ProductoGanadoExterno/" title="Eliminar" data-id="' + id_fila + '" class="btn btn-danger tooltips btn-sm deleteProductoGanadoExterno cslElegido" data-toggle="tooltip" data-placement="top" data-original-title="Eliminar"><i class="fa fa-trash-o"></i></a>' +
            '</div>' +
            '<div class="visible-xs visible-sm hidden-md hidden-lg">' +
            '<div class="btn-group">' +
            '<a class="btn btn-danger dropdown-toggle btn-sm" data-toggle="dropdown" href="#"' +
            '<i class="fa fa-cog"></i> <span class="caret"></span>' +
            '</a>' +
            '<ul role="menu" class="dropdown-menu pull-right dropdown-dark">' +
            '<li>' +
             '<a id="aMin_' + id_fila + '" data-hrefa="/Admin/Flete/DEL_ProductoGanadoExterno/" data-id="' + id_fila + '" class="deleteProductoGanadoExterno cslElegido" role="menuitem" tabindex="-1" data-id="">' +
            '<i class="fa fa-trash-o"></i> Eliminar' +
            '</a>' +
            '</li>' +
            '</ul>' +
            '</div>' +
            '</div>'
        ]).draw(false);
    }





    var RunEventoProductoGanadoExterno = function () {
        $('#btnAddRowGanadoExterno').on('click', function () {
            var numero = document.getElementById("inputGanadoExterno").value;
            if (!/^([0-9])*$/.test(numero)) {
                Mensaje("Por favor, ingrese un número.", 2);
            }
            else if (numero.length == 0) {
                Mensaje("Por favor, ingrese un número.", 2);
            }
            else {
                for (var i = 0; i < numero; i++) {
                    AgergarFilasProductoGanadoExterno(false, 0, "macho", 0, "Sin registrar", numeroFila);
                    numeroFila++;
                }
            }
            //después de inserter muestra las nuevas filas
            tblModalGanadoExterno.order([0, 'desc']).draw(false);
        });
        $('#tblGanadoExterno tbody').on('click', '.deleteProductoGanadoExterno', function (e) {
            //var data = tblModalGanadoExterno.row($(this).parents('tr')).data();
            var tr = $(this).parents('tr');
            var url = $(this).attr('data-hrefa');
            var id = $(this).attr('data-id');
            var box = $("#mb-remove-ganadoExterno");
            box.addClass("open");
            box.find(".mb-control-yes").on("click", function () {
                box.removeClass("open");
                if (id.length == 36) {
                    //eliminamos del servidor
                    var id_flete = document.getElementById("id_flete").value;
                    $.ajax({
                        url: url,
                        data: { IDProducto: id, IDFlete: id_flete },
                        type: 'POST',
                        dataType: 'json',
                        success: function (result) {
                            if (result.Success) {
                                box.find(".mb-control-yes").prop('onclick', null).off('click');
                                Mensaje(result.Mensaje, "1");
                                //eliminamos del datatable
                                tblModalGanadoExterno.row(tr).remove().draw(false);
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
                    tblModalGanadoExterno.row(tr).remove().draw(false);
                }
            });
        });
        $('#btnSaveRowGanadoExterno').on('click', function () {
            var flag_error = false, flag_ok = false, flag = true;
            var nNodes = tblModalGanadoExterno.rows().nodes().to$().find('.cslElegido');
            var id_flete, conteo = 1, NUM_ELEMENTOS_FILA = 7, mensaje_error_general;
            id_flete = document.getElementById("id_flete").value;

            //console.log(nNodes);
            for (var i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA) {
                //1 = img, 2 = label, 3 = numArete, 4 = genero, 5 = peso, 6 = eliminar, 7 = eliminar responsive
                var nId = nNodes[i + 2].dataset.id;
                var nArete = nNodes[i + 2].value;
                var nGenero = nNodes[i + 3].value;
                var nPeso = nNodes[i + 4].value;

                /*INICIA VALIDACION*/
                if (nArete.length == 0 || nArete == '' || nArete == null) {
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
                        url: '/Admin/Flete/AC_ProductoGanadoExterno/',
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
        $('#tblGanadoExterno tbody').on('change', '.inputCSL', function (e) {
            var $td = $(this).parent();
            $td.find('input').attr('value', this.value);
            tblModalGanadoExterno.cell($td).invalidate('dom').draw();
        });
        $("#tblGanadoExterno tbody").on('change', '.selectCSL', function (e) {
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
            tblModalGanadoExterno.cell($td).invalidate('dom').draw();
        });
    }
   
    var LoadTableProductoGanadoNOPropio = function () {
        var IDFlete = $("#id_flete").val();
        $.fn.dataTableExt.ofnSearch['html-input'] = function (value) {
            return $(value).val();
        };
        tblModalGanadoExterno = $('#tblGanadoExterno').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            "orderFixed": {
                "pre": [3, 'asc'],
                "post": [0, 'asc']
            },
            //"order": ([[4, 'asc'], [3, 'asc'], [2, 'asc'], [1, 'asc'], [0, 'asc']]),
            //"ordering": false,
            columnDefs: [
                { "type": "html-input", "targets": [1, 2, 3] },
                { "width": "5%", "targets": [0] }
                //{ "orderable": false, "targets": [0,1,2,3,4]}
            ]
        });
        $.ajax({
            url: '/Admin/Flete/TableJsonProductoGanadoNOPropioXIDFlete/',
            type: "POST",
            data: { IDFlete: IDFlete },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    AgergarFilasProductoGanadoExterno(true, data[i].numArete, data[i].genero, data[i].pesoAproximado, "Registrado", data[i].id_producto);
                }
                RunEventoProductoGanadoExterno();
            }
        });
    }
 
   
 
    
    return {
        init: function () {
            LoadTableGanado();
        }
    };
}();