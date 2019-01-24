function delete_row(Id, url) {
    var box = $("#mb-remove-row");
    box.addClass("open");
    box.find(".mb-control-yes").on("click", function () {
        box.removeClass("open");

        $.ajax({
            url: url,
            data: { id: Id },
            type: 'POST',
            dataType: 'json',
            success: function (result) {
                window.location.href = '/Admin/CatTipoProveedor/Index';
            },
            error: function (result) {
                window.location.href = '/Admin/CatTipoProveedor/Index';
            }
        });
    });

}
