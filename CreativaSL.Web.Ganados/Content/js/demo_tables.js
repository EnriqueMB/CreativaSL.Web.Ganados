
    function delete_row(row){
        var url = $("#delete-" + row).attr('data-hrefa');
        var box = $("#mb-remove-row");
        box.addClass("open");
        box.find(".mb-control-yes").on("click", function () {
            box.removeClass("open");
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                success: function (result) { 
                    $("#" + row).hide("slow", function () {
                        $("#" + row).remove();
                        location.reload(true);
                    });
                },
                error: function () {
                    $('#Error').html('<h3>Ocurrio un error al eliminar este registro<h3>');
                    $('#Error').css("display", "block");
                    $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
                    $('#Error').css("display", "block");
                }
            });
        });
        
    }
    