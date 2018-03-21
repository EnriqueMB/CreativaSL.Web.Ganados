

//$('.datatable').delegate("a.deleteRow", "click", function (event) {
//    event.preventDefault();
//    var $button = $(this);
//    var row = $button.data('sku');
//    var url = $button.data('hrefa');
//    var box = $("#mb-remove-row");
//    console.log(row);
//    box.addClass("open");
//    $btnYes = box.find("a.mb-control-yes");
//    console.log($btnYes);
//    $btnYes.click(function (e) {
//        e.preventDefault();
//        console.log("click");
//        box.removeClass("open");
//        $.ajax({
//            url: url,
//            type: 'POST',
//            dataType: 'json',
//            async: 'false',
//            success: function (result) {
//                $('#Error').html('<h3>Eliminado<h3>');
//                $('#Error').css("display", "block");
//                $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
//                $('#Error').css("display", "block");
//                console.log('01');
//                $("#" + row).hide("slow", function () {
//                    $("#" + row).closest('tr').remove();
//                    //location.reload(true);
//                });
//            },
//            error: function () {
//                $('#Error').html('<h3>Ocurrio un error al eliminar este registro<h3>');
//                $('#Error').css("display", "block");
//                $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
//                $('#Error').css("display", "block");
//            }
//        });
//    });
//});

    



    function delete_row(row){
        var url = $("#delete-" + row).attr('data-hrefa');
        var $box = $("#mb-remove-row");
        $box.addClass("open");
        box.find(".mb-control-yes").on("click", function () {
        //$box.delegate(".mb-control-yes", "click", function (event){
            console.log("click");
            $box.removeClass("open");
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                async: 'false',
                success: function (result) {
                    $('#Error').html('<h3>Eliminado<h3>');
                    $('#Error').css("display", "block");
                    $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
                    $('#Error').css("display", "block");
                    console.log('01');
                    $("#" + row).hide("slow", function () {
                        $("#" + row).remove();
                        //location.reload(true);
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