
function Mensaje(messageC, typemessageA) {
    
    if (typemessageA && messageC) {
        if (typemessageA == '1') {
            $('#Success').html('<h3>' + messageC + '<h3>');
            $('#Success').css("display", "block");
            $('#Success').delay(400).slideDown(400).delay(2000).slideUp(400);
            $('#Success').css("display", "block");
        }
        else if (typemessageA == '2') {
            $('#Error').html('<h3>' + messageC + '<h3>');
            $('#Error').css("display", "block");
            $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
            $('#Error').css("display", "block");
        }
    }
    }
    