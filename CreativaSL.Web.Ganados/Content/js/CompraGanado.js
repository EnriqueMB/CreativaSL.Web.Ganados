/// <reference path="plugins/jquery.maskmoney.min.js" />
/// <reference path="plugins/jquery.maskmoney.min.js" />
/// <reference path="plugins/jquery.maskmoney.min.js" />
/// <reference path="plugins/jquery.maskmoney.min.js" />
/// <reference path="plugins/jquery.maskmoney.min.js" />
/// <reference path="plugins/jquery.maskmoney.min.js" />
var CompraGanado = function () {
    "use strict";
    var CANTDECIMALES = 2;
    var CANTDECIMALESENTEROS = 0;
    var tblGanado;
    var numeroFila = 1;
    var IDCompra = $("#IDCompra").val();
    var SALUDABLE = 1;
    var nNodes, listaPrecioPesoProveedor, tolerancia, listaCorrales, listaFierros;
    var guardarIDs = new Array();
    var longitud_permitida_arete = 10;
    var IMAGEN = 0, MENSAJE = 1, ARETE = 2, GENERO = 3, PESO = 4, REPESO = 5, MERMA = 6, PESOPAGAR = 7, COSTOPORKILO = 8, CORRAL = 9, FIERRO1 = 10, FIERRO2 = 11, FIERRO3 = 12, TOTAL = 13, BTN_ELIMINAR = 14, BTN_ELIMINAR_MIN = 15;
    var mermaFavor = $("#MermaFavor").val();

    var LoadTableGanado = function () {
        tblGanado = $('#tblGanado')
            .DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "searching": false,
            "ordering": false,
            "autoWidth": false,
            columnDefs: [
                { "type": "html-input", "targets": [1, 2, 3, 4, 5, 6, 7, 8, 9, 10] },
                { "width": 132, "targets": 0 },
                { "width": 150, "targets": 1 },
                { "width": 115, "targets": 2 },
                { "width": 105, "targets": 3 },
                { "width": 105, "targets": 4 },
                { "width": 105, "targets": 5 },
                { "width": 105, "targets": 6 },
                { "width": 105, "targets": 7 },
                { "width": 180, "targets": 8 },
                { "width": 115, "targets": 12 },
                { "width": 65, "targets": 13 }
                ]
            , "drawCallback": function (settings) {
                    console.log("drw");
                $(".kg").maskMoney(
                    {
                        allowZero: true,
                        precision: CANTDECIMALESENTEROS,
                        suffix: ' kg'
                    }
                );
                $(".merma").maskMoney(
                    {
                        allowZero: true,
                        precision: CANTDECIMALES,
                        suffix: ' %'
                    }
                );
                $(".money").maskMoney(
                    {
                        allowZero: true,
                        precision: CANTDECIMALES,
                        prefix: '$ '
                    }
                );
                $(".kg").maskMoney('mask');
                $(".merma").maskMoney('mask');
                $(".money").maskMoney('mask');
            }
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
                        data[i].id_ganado, 
                        data[i].guardado, 
                        data[i].Estatus, 
                        data[i].numArete, 
                        data[i].genero,
                        Number.parseFloat(data[i].pesoInicial), 
                        Number.parseFloat(data[i].pesoFinal), 
                        Number.parseFloat(data[i].merma).toFixed(CANTDECIMALES), 
                        Number.parseFloat(data[i].pesoPagado), 
                        Number.parseFloat(data[i].precioKilo).toFixed(CANTDECIMALES),
                        data[i].id_corral,
                        Number.parseFloat(data[i].subtotal).toFixed(CANTDECIMALES),
                        data[i].id_detalleDocumentoPorPagar,
                        data[i].id_fierro1, data[i].id_fierro2, data[i].id_fierro3,
                        data[i].nombreFierro1, data[i].nombreFierro2, data[i].nombreFierro3
                    );
                }
                $(".kg").maskMoney('mask');
                $(".merma").maskMoney('mask');
                $(".money").maskMoney('mask');
               
            }
        });
    }
    
    function AgergarFilas(
        id_fila,    guardado,   mensaje,    numArete,   genero,
        peso, repeso, merma, pesopagar, costoxkilo,
        id_corral, total, iddetalledocumento, id_fierro1, id_fierro2, id_fierro3, nombreFierro1, nombreFierro2, nombreFierro3) {
        
        if (nombreFierro1 === '' || nombreFierro1 === null) {
            nombreFierro1 = '- Seleccione -';
        }
        if (nombreFierro2 === '' || nombreFierro2 === null) {
            nombreFierro2 = '- Seleccione -';
        }
        if (nombreFierro3 === '' || nombreFierro3 === null) {
            nombreFierro3 = '- Seleccione -';
        }
           
        //columna, imagen y aviso
        var html_imagen = '';
        guardado = String(guardado);

        if (guardado.localeCompare("1") == 0)
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido"  src="/Content/img/tabla/ok.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        else
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido" src="/Content/img/tabla/cancel.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        //columna, arete
        var html_arete = '<input id="arete_' + id_fila + '" data-id="' + id_fila + '"  data-iddetalledocumento = "' + iddetalledocumento +'" class="form-control inputCSL cslElegido cslArete" type="text" maxlength="15" value="' + numArete + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el número de arete.">';
        //columna, genero
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

        //columna, peso
        var html_peso = '<input id="peso_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido kg" type="text" value="' + peso + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el peso inicial del ganado (KG.).">';
        //columna, repeso
        var html_repeso = '<input id="repeso_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido kg" type="text" value="' + repeso + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el repeso del ganado (KG.).">';
        //columna, merma
        var html_merma = '<input id="merma_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido merma" type="text" value="' + merma + '" data-toggle="tooltip" data-placement="top" title="Merma generada del ganado. (%)" readonly="readonly">';
        //columna, pesoPagar
        var html_pesoPagar = '<input id="pesopagar_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslTotal cslElegido kg" type="text" value="' + pesopagar + '" data-toggle="tooltip" data-placement="top" title="Peso a pagar (KG.)." readonly="readonly">';
        //columna, costoXkilo
        var html_costoxkilo = '<input id="costoxkilo_' + id_fila + '" data-id="' + id_fila + '" class="form-control cslTotal cslElegido money" type="text" value="' + costoxkilo + '" data-toggle="tooltip" data-placement="top" title="Costo por kilo ($)." readonly="readonly">';
        //columna, corral
        var html_corral = '<select id="corral_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Corral asignado." >';
        var opciones_corrales = '';
        id_corral = parseInt(id_corral);

        for (var item in listaCorrales) {
            var id_corral_server = parseInt(listaCorrales[item].Id_corral);
            
            if (id_corral_server == id_corral) {

                opciones_corrales += '<option value="' + listaCorrales[item].Id_corral + '" data-id="' + listaCorrales[item].Id_corral + '" selected>' + listaCorrales[item].Descripcion + '</option>';
            }
            else {
                opciones_corrales += '<option value="' + listaCorrales[item].Id_corral + '" data-id="' + listaCorrales[item].Id_corral +'">' + listaCorrales[item].Descripcion + '</option>';
            }
        }
        html_corral += opciones_corrales;
        html_corral += '</select> ';

        //columna, fierro1
        var html_fierro1 = '<input id="fierro1_' + id_fila + '" readonly type="text" class="fierro1 cslElegido inputCSL" value="' + nombreFierro1 + '" name="fierro1" data-id = "' + id_fierro1 + '"/>';

        for (var itemFierro in listaFierros) {
            var id_fierro_server = listaFierros[itemFierro].IDFierro;
            
            $(".capaModal.modalFierro1 .modalBody .fierroList").append(`
            <div ref="fierro1" class="fierroItem">
                <div class="fierroImagen">
                    <figure>
                        <img src="/Imagenes/Fierro/`+ listaFierros[itemFierro].NombreArchivo + `" alt="` + listaFierros[itemFierro].NombreArchivo +`" />
                    </figure>
                </div>
                <div class="fierroDescripcion">
                    <input type="text" value="`+ listaFierros[itemFierro].NombreFierro + `" readonly data-id="` + listaFierros[itemFierro].IDFierro+`"/>
                </div>
            </div>
            `);
        }

        //columna, fierro2
        var html_fierro2 = '<input id="fierro2_' + id_fila + '" readonly type="text" class="fierro2 cslElegido inputCSL" value="' + nombreFierro2 + '" name="fierro2" data-id = "' + id_fierro2 + '" />';
        var opciones_fierro2 = '';

        //columna, fierro3
        var html_fierro3 = '<input id="fierro3_' + id_fila + '" readonly type="text" class="fierro3 cslElegido inputCSL" value="' + nombreFierro3 + '" name="fierro3" data-id = "' + id_fierro3 + '" />';
        var opciones_fierro3 = '';

        //columna, total
        var html_total = '<input id="total_' + id_fila + ' "class="form-control cslElegido inputCSL money" type="text" min="1" value="' + total + '"  data-toggle="tooltip" data-placement="top" title="Total de la compra ($)." readonly="readonly">';

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
            html_fierro1,
            html_fierro2,
            html_fierro3,
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

        //cualquier plugin que agrege mas elementos a la fila se debe de poner aqui, ademas hay que checar las constantes por la
        //validacion y recorrido al momento de enviar
        $('select.selectpicker').selectpicker({
            caretIcon: 'fa fa-sort-down'
        });

        nNodes = tblGanado.rows().nodes().to$().find('.cslElegido');
    }

    // Eventos click de inputs de fierro en tabla
    
    //Abrir modales
    $(document).on("click", "#tblGanado .fierro1", function () {
        var getIdFila = $(this).attr('id');
        $(".blackCap, .capaModal.modalFierro1 .modalFierroContent").addClass('active');
        $(".capaModal.modalFierro1").attr('idfila', getIdFila);

        var $tr = $(this).closest("tr");
        var fila = $tr.find('.cslElegido');
        var idx = fila[ARETE].dataset.id;
        guardarIDs.push(idx);
    });
    $(document).on("click", "#tblGanado .fierro2", function () {
        var getIdFila = $(this).attr('id');
        $(".blackCap, .capaModal.modalFierro1 .modalFierroContent").addClass('active');
        $(".capaModal.modalFierro1").attr('idfila', getIdFila);

        var $tr = $(this).closest("tr");
        var fila = $tr.find('.cslElegido');
        var idx = fila[ARETE].dataset.id;
        guardarIDs.push(idx);
    });
    $(document).on("click", "#tblGanado .fierro3", function () {
        var getIdFila = $(this).attr('id');
        $(".blackCap, .capaModal.modalFierro1 .modalFierroContent").addClass('active');
        $(".capaModal.modalFierro1").attr('idfila', getIdFila);

        var $tr = $(this).closest("tr");
        var fila = $tr.find('.cslElegido');
        var idx = fila[ARETE].dataset.id;
        guardarIDs.push(idx);
    });

    $(".blackCap,.closeModalButton").click(function () {
        $(".blackCap, .modalFierroContent").removeClass('active');
    });
    //End abrir modales

    // Asignación de valores a inputs
    $(document).on("click",".fierroItem",function () {
        var getIdFila = $(this).parent().parent().parent().parent().attr('idfila');
        var getDataID = $(this).find('input').attr('data-id');
        var inputValue = $(this).find('input').attr('value');

        $("#tblGanado input[id=" + getIdFila + "]").attr('value', inputValue);
        $("#tblGanado input[id=" + getIdFila + "]").attr('data-id', getDataID);
        $(".blackCap, .modalFierroContent").removeClass('active');

       
    });

    $(document).on("click", ".searchFierro a[class*='btn-danger']", function () {
        var getIdFila = $(this).parent().parent().parent().parent().attr('idfila');
        var inputValue = "Sin Asignar";
        $("#tblGanado input[id=" + getIdFila + "]").attr('value', inputValue);
        $(".blackCap, .modalFierroContent").removeClass('active');
    });
    // End asignación de valores

    // Función de búsqueda
    $(document).bind("change paste keyup", $(".modalFierroContent.active .searchFierro input").val(),function () {
        //alert("search ha cambiado");
        var actualValue = $(".modalFierroContent.active .searchFierro input").val();
        // $(".fierroItem").find("input:not(input[value*='" + actualValue + "'])").css("display","none");
        $(".fierroList .fierroItem:has(input:not(input[value*='" + actualValue + "']))").css("display", "none");
        $(".fierroList .fierroItem:has(input[value*='" + actualValue + "'])").css("display", "block");
        if (actualValue == "") {
            $(".fierroList .fierroItem").css("display", "block");
        }
    });
    // End función de búsqueda
    

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
                                tblGanado.row(tr).remove().draw(false);
                                try
                                {
                                    var obj = JSON.parse(result.Mensaje);
                                    ActualizarGenerales(obj.CantidadMachos, obj.CantidadHembras, obj.CantidadTotal, obj.MermaMachos, obj.MermaHembras, obj.MermaTotal, obj.KilosMachos, obj.KilosHembras, obj.KilosTotal, obj.MontoTotalGanado)
                                    $(".kg").maskMoney('mask');
                                    $(".merma").maskMoney('mask');
                                    $(".money").maskMoney('mask');
                                }
                                catch (e){
                                    console.log("Error: " + e);
                                }
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
        $('#btnVerListaFierros').on('click', function () {
            $("body").css("cursor", "progress");

            $.ajax({
                url: '/Admin/Compra/ModalListaFierros/',
                type: "POST",
                data: { IDCompra: IDCompra},
                success: function (data) {
                    $("body").css("cursor", "default");
                    $('#ContenidoModalFierro').html(data);
                    $('#ModalFierro').modal({ backdrop: 'static', keyboard: false });

                    $('#selectImgFierro').on('change', function (e) {
                        var Id_fierro = $(this).val();

                        $.ajax({
                            url: '/Admin/Compra/GetFierroXId/',
                            type: "POST",
                            dataType: 'json',
                            data: { Id_fierro: Id_fierro },
                            error: function () {
                                Mensaje("Ocurrió un error al cargar el combo", "1");
                                $("#imagenFierro").attr('src', "data:image/jpg;base64,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");
                            },
                            success: function (result) {
                                $("#imagenFierro").attr('src', result);
                            }
                        });
                    });

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
                    AgergarFilas(numeroFila, false, "Sin registrar", 0, "macho", 0, 0, 0, 0, 0, 0, 0, numeroFila, '', '', '', '', '', '');
                    numeroFila++;
                }
                $('select.selectpicker').selectpicker({
                    caretIcon: 'fa fa-sort-down'
                });
                $(".kg").maskMoney('mask');
                $(".merma").maskMoney('mask');
                $(".money").maskMoney('mask');
            }
            //después de inserter muestra las nuevas filas
            tblGanado.order([0, 'desc']).draw(false);
        });
        $('#btnSaveRowGanado').on('click', function () {
            console.log("Guardar Ganado Click");
            var flag = true;
            var NUM_ELEMENTOS_FILA = 16;

            guardarIDs = eliminarDuplicadosArray(guardarIDs);

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
                        console.log(nNodes[i]);
                        var valuePeso = Number.parseFloat(GetKilosSinSimbolo(nNodes[i + PESO].value));
                        var valueRePeso = Number.parseFloat(GetKilosSinSimbolo(nNodes[i + REPESO].value));
                        var valuePesoPagar = Number.parseFloat(GetKilosSinSimbolo(nNodes[i + PESOPAGAR].value));
                        console.log("Entra 0");
                        var valueCostoPorKilo = Number.parseFloat(GetMoneySinSimbolo(nNodes[i + COSTOPORKILO].value));
                        console.log("Entra 1");
                        if (nNodes[i + ARETE].value.length < longitud_permitida_arete || nNodes[i + ARETE].value === '' || nNodes[i + ARETE].value == null) {
                            nNodes[i + ARETE].classList.remove('okCSLGanado');
                            nNodes[i + ARETE].classList.add('errorCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + ARETE].classList.remove('errorCSLGanado');
                            nNodes[i + ARETE].classList.add('okCSLGanado');
                        }
                        //peso
                        //if (nNodes[i + PESO].value.length == 0 || nNodes[i + PESO].value == '' || nNodes[i + PESO].value == null || nNodes[i + PESO].value <= 0 || isNaN(nNodes[i + PESO].value)) {
                        if (valuePeso.length === 0 || valuePeso === '' || valuePeso === null || valuePeso <= 0 || isNaN(valuePeso)) {
                            nNodes[i + PESO].classList.add('errorCSLGanado');
                            nNodes[i + PESO].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + PESO].classList.add('okCSLGanado');
                            nNodes[i + PESO].classList.remove('errorCSLGanado');
                        }
                        //repeso
                        //if (nNodes[i + REPESO].value.length == 0 || nNodes[i + REPESO].value == '' || nNodes[i + REPESO].value == null || nNodes[i + REPESO].value < 0 || isNaN(nNodes[i + REPESO].value)) {
                        if (valueRePeso.length === 0 || valueRePeso === '' || valueRePeso === null || valueRePeso < 0 || isNaN(valueRePeso)) {
                            nNodes[i + REPESO].classList.add('errorCSLGanado');
                            nNodes[i + REPESO].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + REPESO].classList.add('okCSLGanado');
                            nNodes[i + REPESO].classList.remove('errorCSLGanado');
                        }
                        //pesoAPagar
                        //if (nNodes[i + PESOPAGAR].value.length == 0 || nNodes[i + PESOPAGAR].value == '' || nNodes[i + PESOPAGAR].value == null || nNodes[i + PESOPAGAR].value <= 0 || isNaN(nNodes[i + PESOPAGAR].value)) {
                        if (valuePesoPagar.length === 0 || valuePesoPagar === '' || valuePesoPagar === null || valuePesoPagar <= 0 || isNaN(valuePesoPagar)) {
                            nNodes[i + PESOPAGAR].classList.add('errorCSLGanado');
                            nNodes[i + PESOPAGAR].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + PESOPAGAR].classList.add('okCSLGanado');
                            nNodes[i + PESOPAGAR].classList.remove('errorCSLGanado');
                        }
                        //costoXkilo
                        //if (nNodes[i + COSTOPORKILO].value.length == 0 || nNodes[i + COSTOPORKILO].value == '' || nNodes[i + COSTOPORKILO].value == null || nNodes[i + COSTOPORKILO].value <= 0 || isNaN(nNodes[i + COSTOPORKILO].value)) {
                        if (valueCostoPorKilo.length === 0 || valueCostoPorKilo === '' || valueCostoPorKilo === null || valueCostoPorKilo <= 0 || isNaN(valueCostoPorKilo)) {
                            nNodes[i + COSTOPORKILO].classList.add('errorCSLGanado');
                            nNodes[i + COSTOPORKILO].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + COSTOPORKILO].classList.add('okCSLGanado');
                            nNodes[i + COSTOPORKILO].classList.remove('errorCSLGanado');
                        }
                      

                        //corral
                        if (nNodes[i + CORRAL].value.length == 0 || nNodes[i + CORRAL].value == '' || nNodes[i + CORRAL].value == null || nNodes[i + CORRAL].value <= 0) {
                            nNodes[i + CORRAL].classList.add('errorCSLGanado');
                            nNodes[i + CORRAL].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + CORRAL].classList.add('okCSLGanado');
                            nNodes[i + CORRAL].classList.remove('errorCSLGanado');
                        }

                        if (flag) {
                            nNodes[i].src = "/Content/img/tabla/loading.gif";
                            nNodes[i + MENSAJE].innerText = "Guardando";
                            var id_ganado = nNodes[i + ARETE].dataset.id;
                            var id_detalleDocumento = nNodes[i + ARETE].dataset.iddetalledocumento;
                            var numArete = nNodes[i + ARETE].value;
                            var id_genero = nNodes[i + GENERO].value;
                            var id_corral = nNodes[i + CORRAL].selectedOptions[0].dataset.id;
                            //var id_fierro1 = nNodes[i + FIERRO1].selectedOptions[0].dataset.id;
                            //var id_fierro2 = nNodes[i + FIERRO2].selectedOptions[0].dataset.id;
                            //var id_fierro3 = nNodes[i + FIERRO3].selectedOptions[0].dataset.id;

                            var id_fierro1 = nNodes[i + FIERRO1].dataset.id;
                            var id_fierro2 = nNodes[i + FIERRO2].dataset.id;
                            var id_fierro3 = nNodes[i + FIERRO3].dataset.id;

                            var peso = Number.parseFloat(GetKilosSinSimbolo(nNodes[i + PESO].value));
                            var repeso = Number.parseFloat(GetKilosSinSimbolo(nNodes[i + REPESO].value));
                            var peso_pagar = Number.parseFloat(GetKilosSinSimbolo(nNodes[i + PESOPAGAR].value));
                            var costo_kilo = Number.parseFloat(GetMoneySinSimbolo(nNodes[i + COSTOPORKILO].value));
                            var merma = Number.parseFloat(GetKilosSinSimbolo(nNodes[i + MERMA].value));

                            $.ajax({
                                url: '/Admin/Compra/AC_Ganado/',
                                type: "POST",
                                data: {
                                    IDCompra: IDCompra, IDGanado: id_ganado, numArete: numArete, id_genero: id_genero,
                                    peso: peso, repeso: repeso, merma: merma, peso_pagar: peso_pagar,
                                    costo_kilo: costo_kilo, id_corral: id_corral, Id_detalleDocumentoPorCobrar: id_detalleDocumento,
                                    indiceActual: i, id_fierro1: id_fierro1, id_fierro2: id_fierro2, id_fierro3: id_fierro3
                                },
                                success: function (response) {
                                    var obj = JSON.parse(response.Mensaje);
                                    var indice = parseInt(obj.indiceActual);

                                    if (response.Success) {
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

                                        ActualizarGenerales
                                        (
                                            obj.CantidadMachos,
                                            obj.CantidadHembras,
                                            obj.CantidadTotal,
                                            obj.MermaMachos,
                                            obj.MermaHembras,
                                            obj.MermaTotal,
                                            obj.KilosMachos,
                                            obj.KilosHembras,
                                            obj.KilosTotal,
                                            obj.MontoTotalGanado
                                        );

                                        $(".kg").maskMoney('mask');
                                        $(".merma").maskMoney('mask');
                                        $(".money").maskMoney('mask');

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

            var idx = fila[ARETE].dataset.id;
            tblGanado.cell($td).invalidate('dom').draw(false);
            guardarIDs.push(idx);

            //solo se le asigna el calculo al select de genero
            var id = this.id;
            var arete_ganado = "arete_";
            var buscar = id.indexOf(arete_ganado);

            if (buscar != -1) {
                var subtring = obtenerSubstring($(this).val());
                $(this).val(subtring);
            }
        });
        $('#tblGanado tbody').on('change', '.cslTotal', function (e) {
            var $td = $(this).parent();
            $td.find('input').attr('value', this.value);

            //obtenemos el valor de toda la fila
            var $tr = $(this).closest("tr");
            var fila = $tr.find('.cslElegido');

            var pesoPagar = Number.parseFloat(GetMoneySinSimbolo(fila[PESOPAGAR].value));
            var costoPorKilo = Number.parseFloat(GetMoneySinSimbolo(fila[COSTOPORKILO].value));

            fila[TOTAL].value = TotalPagar(pesoPagar, costoPorKilo);

            $(".money").maskMoney('mask');

            var idx = fila[ARETE].dataset.id;
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
            var idx = fila[ARETE].dataset.id;
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
    function obtenerSubstring(valor) {
        var nuevo_value = valor;
        var longitud_input = valor.length;

        if (longitud_input > longitud_permitida_arete) {
            var diferencia = longitud_input - longitud_permitida_arete;
            nuevo_value = valor.substring(diferencia, (longitud_permitida_arete + diferencia));
        }

        return nuevo_value;
    }
    function ActualizarGenerales(CantidadMachos, CantidadHembras, CantidadTotal, MermaMachos, MermaHembras,
        MermaTotal, KilosMachos, KilosHembras, KilosTotal, MontoTotalGanado) {

        var cantidadMachos = Number.parseFloat(CantidadMachos).toFixed(CANTDECIMALESENTEROS);
        var cantidadHembras = Number.parseFloat(CantidadHembras).toFixed(CANTDECIMALESENTEROS);
        var cantidadTotal = Number.parseFloat(CantidadTotal).toFixed(CANTDECIMALESENTEROS);
        var mermaMachos = Number.parseFloat(MermaMachos).toFixed(CANTDECIMALES);
        var mermaHembras = Number.parseFloat(MermaHembras).toFixed(CANTDECIMALES);
        var mermaTotal = Number.parseFloat(MermaTotal).toFixed(CANTDECIMALES);
        var kilosMachos = Number.parseFloat(KilosMachos).toFixed(CANTDECIMALESENTEROS);
        var kilosHembras = Number.parseFloat(KilosHembras).toFixed(CANTDECIMALESENTEROS);
        var kilosTotal = Number.parseFloat(KilosTotal).toFixed(CANTDECIMALESENTEROS);
        var montoTotalGanado = Number.parseFloat(MontoTotalGanado).toFixed(CANTDECIMALES);

        //actualizamos los generales
        $("#GanadosCompradoMachos").val(cantidadMachos);
        $("#GanadosCompradoHembras").val(cantidadHembras);
        $("#GanadosCompradoTotal").val(cantidadTotal);
        $("#MermaMachos").val(mermaMachos);
        $("#MermaHembras").val(mermaHembras);
        $("#MermaTotal").val(mermaTotal);
        $("#KilosMachos").val(kilosMachos);
        $("#KilosHembras").val(kilosHembras);
        $("#KilosTotal").val(kilosTotal);
        $("#MontoTotalGanado").val(montoTotalGanado);
    }

    //Nuevas funciones para el calculo del peso
    function calculosGanado(fila) {

        var mermaObtenida, pesoPagar, precioXkilo, total, pesoFinal;
        var genero = fila[GENERO].value;
        var peso = Number.parseFloat(GetKilosSinSimbolo(fila[PESO].value));
        var repeso = Number.parseFloat(GetKilosSinSimbolo(fila[REPESO].value));

        //NO hay repeso
        if (repeso <= 0) {
            repeso = peso;
        }
        //merma
        if (repeso >= peso) {
            mermaObtenida = 0;
        }
        else{
            mermaObtenida = MermaGenerada(peso, repeso);
        }
        pesoPagar = PesoSugerido(peso, repeso, mermaObtenida);
        precioXkilo = PrecioSugerido(pesoPagar, genero);
        total = TotalPagar(pesoPagar, precioXkilo);
        fila[MERMA].value = mermaObtenida;
        fila[PESOPAGAR].value = pesoPagar;
        fila[COSTOPORKILO].value = precioXkilo;
        fila[TOTAL].value = total;

        $(".merma").maskMoney('mask');
        $(".kg").maskMoney('mask');
        $(".money").maskMoney('mask');

    }
    function MermaGenerada(pesoInicial, pesoFinal) {
        var mermaGenerada = (((pesoFinal * 100) / pesoInicial) - 100) * (-1);
        mermaGenerada = Math.abs(mermaGenerada.toFixed(CANTDECIMALES));

        if (isNaN(mermaGenerada))
            mermaGenerada = 0;

        return mermaGenerada.toFixed(CANTDECIMALES);
    }

    function PesoSugerido(peso, repeso, MermaObtenida) {
        console.log(MermaObtenida);
        console.log(tolerancia);
        console.log(peso);
        if (tolerancia == 0) {
            return Math.trunc(peso);
        }
        else if (MermaObtenida > tolerancia && tolerancia != 0) {
            var pesoPagar = repeso + (repeso * (mermaFavor / 100));

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
                    return listaPrecioPesoProveedor[item].Precio.toFixed(CANTDECIMALES);
                }
            }
        }
        return 0;
    }
    function TotalPagar(peso, precioxkilo){
        var total = peso * precioxkilo;
        return total.toFixed(CANTDECIMALES);
    }

    function GetKilosSinSimbolo(value) {
        var newValue = value.split(" ", 1);
        newValue = newValue.toString().replace(/,/g, "");

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }

    function GetMoneySinSimbolo(value) {
        var newValue = value.split(" ", 2);
        newValue = newValue[1];
        newValue = newValue.toString().replace(/,/g, "");

        if (Number.isNaN(newValue)) {
            return 0;
        }
        else {
            return newValue;
        }
    }

    return {
        init: function (lista, toleranciaP, listacorral, listafierro) {
            listaPrecioPesoProveedor = lista;
            tolerancia = toleranciaP;
            listaCorrales = listacorral;
            listaFierros = listafierro;
            LoadTableGanado();
            RunEventoGanado();
            
        }
    };
}();
