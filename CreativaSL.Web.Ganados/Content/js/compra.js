function InitModales() {
    document.getElementById("btnGanado").addEventListener('click', ModalGanado,false);
    document.getElementById("RowGanado").addEventListener('click', ModalGanado, false);
}

function ModalGanado() {
    var id = this.dataset.id;
   
    $.ajax({
        url: 'ModalGanado',
        data: { idGanado: id },
        success: function (data) {
            $('#ContenidoModalGanado').html(data);
            $('#ModalGanado').modal({ backdrop: 'static', keyboard: false });
        }
    })
}

function LoadTableGanadoXCompraGanado(id) {
    console.log(id);
    $.ajax({
        type: "POST",
        url: "TableJsonGanado",
        data: { IDCompra: id },
        success: function (data) {
            console.log(data);
        }
    })
}