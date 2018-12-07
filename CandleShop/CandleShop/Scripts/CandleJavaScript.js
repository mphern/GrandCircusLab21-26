function ValidAdminPass() {
    var pass = document.getElementById("adminPass").value;
    if (pass != "AdminPass")
    {
        alert("Admin Password is incorrect.")
        event.preventDefault();
    }
    ShowButtons();
}

function ShowButtons() {
    var x = document.getElementById("adminPass").value;
    if (x == "AdminPass") {
        $('#editProducts').show();
        $('#editUsers').show();
    }
    else {
        $('#editProducts').hide();
        $('#editUsers').hide();
    }
};