$(document).ready(function () {
    $("#frm_tab1").submit(function () {
        var form = $(this);
        console.log("ajax");

        $.ajax({
            dataType: 'JSON',
            type: 'POST',
            url: form.attr('action'),
            data: form.serialize(),
            success: function (r) {
                if (r.response) {
                    window.location.href = r.ref;
                }
                else {
                    alert(r.message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus + ' ' + errorThrown);
            }
        });
        return false;
    })


})