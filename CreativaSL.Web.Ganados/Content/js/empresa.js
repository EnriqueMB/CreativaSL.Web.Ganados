
$("#btnAjax").click(function (e) {
    console.log("hola1");
    //e.preventDefault();
    //var form = $(this);
    //var formData = new FormData();
    var id = 1;
    $.ajax({
        type: "POST",
        url: "SaveEmpresa",
        data: '{name: "' + $("#txtName").val() + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            console.log(response);
            console.log(response.Error);
            console.log(response.Mensaje);
            if (response.Error) {
                alert(response.Mensaje);
                Mensaje(response.Mensaje, "2");
            }

        },
        failure: function (response) {
            //alert(response.responseText);
            //alert("failure");
        },
        error: function (response) {
            //console.log(response);
            //alert(response);
            //alert(response.responseText);
        }
    });



    //$.ajax({
    //    type: 'POST',
    //    url: 'SaveEmpresa',
    //    data: {"id": id},
    //    dataType: 'json',
    //    success: function (data) {
    //        alert("Hola");

    //    }
    //});
    
});
